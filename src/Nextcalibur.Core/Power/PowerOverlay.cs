using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Nextcalibur.Core.Power;

/// <summary>Windows 11 power-mode overlays.</summary>
public static class PowerOverlays
{
    /// <summary>"Balanced" — no overlay applied over the active plan.</summary>
    public static readonly Guid None = Guid.Empty;

    /// <summary>"Best power efficiency".</summary>
    public static readonly Guid BetterBattery = new("961cc777-2547-4f9d-8174-7d86181b8a7a");

    /// <summary>"Better performance".</summary>
    public static readonly Guid HighPerformance = new("3af9b8d9-7c97-431d-ad78-34a8bfea439f");

    /// <summary>
    /// "Best performance". On affected machines this overlay carries
    /// PROCTHROTTLEMIN = 100%, which pins the CPU at maximum frequency even at
    /// idle and overrides whatever the active power plan says.
    /// </summary>
    public static readonly Guid MaxPerformance = new("ded574b5-45a0-4f42-8737-46345c09c238");

    public static string Describe(Guid overlay)
    {
        if (overlay == None) return "Balanced (no overlay)";
        if (overlay == BetterBattery) return "Best power efficiency";
        if (overlay == HighPerformance) return "Better performance";
        if (overlay == MaxPerformance) return "Best performance";
        return $"Unknown ({overlay})";
    }
}

/// <summary>Result of inspecting the machine's power-overlay configuration.</summary>
/// <param name="ActiveOverlay">The overlay currently in effect.</param>
/// <param name="MinProcessorStateOverride">
/// Our override of the Max Performance overlay's minimum processor state, or
/// null when no override is present.
/// </param>
/// <param name="ProvisionedMinProcessorState">
/// The OEM-provisioned minimum processor state for that overlay.
/// </param>
public readonly record struct OverlayDiagnosis(
    Guid ActiveOverlay,
    int? MinProcessorStateOverride,
    int? ProvisionedMinProcessorState)
{
    /// <summary>The CPU-pinning overlay is currently active.</summary>
    public bool OverlayIsStuck => ActiveOverlay == PowerOverlays.MaxPerformance;

    /// <summary>
    /// Nothing stops the overlay from pinning the CPU if something re-activates
    /// it later.
    /// </summary>
    public bool GuardMissing =>
        MinProcessorStateOverride is null or >= 100;

    public bool NeedsRepair => OverlayIsStuck || GuardMissing;
}

/// <summary>
/// Detects and repairs the stuck power-overlay fault.
///
/// Windows applies a power-mode <em>overlay</em> on top of the active power
/// plan, and the overlay wins. Software that only calls the legacy plan API
/// cannot see or change it — which is why picking a quieter mode in the vendor
/// Control Center has no effect while the overlay is stuck on Best performance.
///
/// The repair has two layers, because clearing the overlay alone is not durable:
/// any application may re-activate it at any time without notice.
/// </summary>
public sealed class PowerOverlayService
{
    private const string SchemesKey =
        @"SYSTEM\CurrentControlSet\Control\Power\User\PowerSchemes";

    // SUB_PROCESSOR \ PROCTHROTTLEMIN
    private const string MinProcessorStateKey =
        @"SYSTEM\CurrentControlSet\Control\Power\PowerSettings\" +
        @"54533251-82be-4824-96c1-47b60b740d00\893dee8e-2bef-41e0-89c6-b55d0929964c\" +
        @"DefaultPowerSchemeValues\ded574b5-45a0-4f42-8737-46345c09c238";

    private const string ProvisionedValue = "ProvAcSettingIndex";
    private const string OverrideValue = "OverrideACSettingIndex";

    /// <summary>Minimum processor state we write, as a percentage.</summary>
    public const int SafeMinProcessorState = 5;

    [DllImport("powrprof.dll", EntryPoint = "PowerGetEffectiveOverlayScheme")]
    private static extern uint PowerGetEffectiveOverlayScheme(out Guid overlay);

    [DllImport("powrprof.dll", EntryPoint = "PowerSetActiveOverlayScheme")]
    private static extern uint PowerSetActiveOverlayScheme(Guid overlay);

    /// <summary>The overlay currently in effect.</summary>
    public static Guid GetActiveOverlay()
    {
        // powercfg /getactiveoverlayscheme was removed on recent Windows 11
        // builds, so go through powrprof directly.
        if (PowerGetEffectiveOverlayScheme(out var overlay) == 0)
            return overlay;

        // Fall back to the registry if the export is unavailable.
        using var key = Registry.LocalMachine.OpenSubKey(SchemesKey);
        var raw = key?.GetValue("ActiveOverlayAcPowerScheme") as string;
        return Guid.TryParse(raw, out var parsed) ? parsed : Guid.Empty;
    }

    /// <summary>Switches the machine to the given power mode.</summary>
    public static void SetActiveOverlay(Guid overlay)
    {
        var rc = PowerSetActiveOverlayScheme(overlay);
        if (rc != 0)
            throw new InvalidOperationException($"PowerSetActiveOverlayScheme failed with code {rc}.");
    }

    public OverlayDiagnosis Diagnose()
    {
        using var key = Registry.LocalMachine.OpenSubKey(MinProcessorStateKey);
        return new OverlayDiagnosis(
            ActiveOverlay: GetActiveOverlay(),
            MinProcessorStateOverride: key?.GetValue(OverrideValue) as int?,
            ProvisionedMinProcessorState: key?.GetValue(ProvisionedValue) as int?);
    }

    /// <summary>
    /// Applies both repair layers and reports what changed.
    ///
    /// Layer 1 clears the active overlay so the power plan takes effect now.
    /// Layer 2 overrides the overlay's own minimum processor state, so that even
    /// if something re-activates it later the CPU can still idle down. Without
    /// layer 2 the fault simply returns.
    /// </summary>
    public IReadOnlyList<string> Repair()
    {
        var actions = new List<string>();
        var before = Diagnose();

        if (before.OverlayIsStuck)
        {
            SetActiveOverlay(PowerOverlays.None);
            actions.Add("Cleared the stuck Best-performance overlay (power mode is now Balanced).");
        }

        if (before.GuardMissing)
        {
            // The provisioned value stays untouched; the override takes
            // precedence over it, so this is reversible by deleting one value.
            using var key = Registry.LocalMachine.CreateSubKey(MinProcessorStateKey, writable: true)
                ?? throw new InvalidOperationException(
                    "Could not open the overlay setting key for writing.");
            key.SetValue(OverrideValue, SafeMinProcessorState, RegistryValueKind.DWord);
            actions.Add(
                $"Set the Best-performance overlay's minimum processor state to {SafeMinProcessorState}% " +
                "so it can no longer pin the CPU if re-activated.");
        }

        return actions;
    }

    /// <summary>
    /// Undoes <see cref="Repair"/>'s second layer by removing our override. The
    /// OEM-provisioned value takes effect again.
    /// </summary>
    public void RemoveGuard()
    {
        using var key = Registry.LocalMachine.OpenSubKey(MinProcessorStateKey, writable: true);
        if (key?.GetValue(OverrideValue) is not null)
            key.DeleteValue(OverrideValue, throwOnMissingValue: false);
    }
}
