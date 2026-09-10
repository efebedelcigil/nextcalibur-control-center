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
        if (overlay == None) return "Balanced";
        if (overlay == BetterBattery) return "Best power efficiency";
        if (overlay == HighPerformance) return "Better performance";
        if (overlay == MaxPerformance) return "Best performance";
        return "Unrecognised";
    }

    /// <summary>The four power modes Windows offers, in order from coolest to fastest.</summary>
    public static IReadOnlyList<PowerModeOption> All { get; } =
    [
        new(BetterBattery, "Best power efficiency",
            "Longest battery life and the quietest fans. The processor is held back, so " +
            "heavy work takes longer."),

        new(None, "Balanced",
            "Windows decides. Speeds up when you need it and settles down when you don't. " +
            "The right choice for most of the time."),

        new(HighPerformance, "Better performance",
            "Leans towards speed. Slightly warmer and noisier than Balanced, with quicker " +
            "responses under load."),

        new(MaxPerformance, "Best performance",
            "Everything the machine has. Expect noticeably more heat, louder fans and " +
            "shorter battery life."),
    ];
}

/// <summary>One of the power modes offered on the Power page.</summary>
/// <param name="Overlay">The overlay this option selects.</param>
/// <param name="Name">Its name, as Windows calls it.</param>
/// <param name="Description">What choosing it means, in plain terms.</param>
public readonly record struct PowerModeOption(Guid Overlay, string Name, string Description);

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
/// What a repair actually managed to do.
/// </summary>
/// <param name="Done">Changes that were made, in the order they were made.</param>
/// <param name="Blocked">
/// Why the rest could not be done, or null when nothing was blocked.
///
/// Separate from <paramref name="Done"/> because the repair has two layers and
/// the second needs privileges the first does not. Reporting a half-finished
/// repair as a failure tells somebody nothing changed when something did.
/// </param>
public readonly record struct RepairOutcome(IReadOnlyList<string> Done, string? Blocked)
{
    /// <summary>Nothing was attempted, or nothing needed attempting.</summary>
    public bool Empty => Done.Count == 0 && Blocked is null;
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
    public RepairOutcome Repair()
    {
        var actions = new List<string>();
        var before = Diagnose();
        string? blocked = null;

        if (before.OverlayIsStuck)
        {
            SetActiveOverlay(PowerOverlays.None);
            actions.Add("Cleared the stuck Best-performance overlay (power mode is now Balanced).");
        }

        if (before.GuardMissing)
        {
            try
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
            catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException)
            {
                // This layer writes under HKLM and needs elevation; the first
                // layer does not. Without it the machine is repaired now but
                // not protected against the overlay being re-activated later,
                // and that difference is what the caller has to be told.
                blocked =
                    "The lasting part of the fix needs administrator rights, so it was not applied. " +
                    "Windows can pin the processor again if something re-activates the " +
                    "Best-performance power mode. Running Nextcalibur as administrator once " +
                    "is enough to make it permanent.";
            }
        }

        return new RepairOutcome(actions, blocked);
    }

    /// <summary>
    /// Whether selecting the Performance mode is safe right now.
    ///
    /// That mode activates the same overlay this class exists to defuse. With
    /// the guard in place the overlay is harmless; without it, choosing
    /// Performance recreates the exact fault the project was started to fix -
    /// the processor pinned at full speed while the machine sits idle.
    /// </summary>
    public static bool PerformanceModeIsSafe(OverlayDiagnosis diagnosis) => !diagnosis.GuardMissing;

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
