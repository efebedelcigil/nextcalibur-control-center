using Microsoft.Win32;

namespace Nextcalibur.Core.Power;

/// <summary>The three system modes offered on the System page.</summary>
public enum SystemMode
{
    Office,
    Gaming,
    Performance,
}

/// <summary>
/// Switches the machine between the three system modes.
///
/// A mode is a pairing of a Windows power plan and a power-mode overlay. Both
/// halves matter: the overlay overrides the plan, so setting only the plan is
/// what makes the vendor software's own modes appear to do nothing.
///
/// Offering a Performance mode that selects the Best-performance overlay is only
/// safe because <see cref="PowerOverlayService"/> caps that overlay's minimum
/// processor state. Without that guard this would reintroduce the very fault
/// this application exists to fix.
/// </summary>
public sealed class SystemModeService
{
    private const string SchemesKey = @"SYSTEM\CurrentControlSet\Control\Power\User\PowerSchemes";

    /// <summary>Plans the vendor software installs, preferred when present.</summary>
    private static readonly Dictionary<SystemMode, string[]> PreferredPlanNames = new()
    {
        [SystemMode.Office] = ["Office", "Balanced"],
        [SystemMode.Gaming] = ["Gaming", "Balanced"],
        [SystemMode.Performance] = ["High performance", "Balanced"],
    };

    private static readonly Dictionary<SystemMode, Guid> Overlays = new()
    {
        [SystemMode.Office] = PowerOverlays.None,
        [SystemMode.Gaming] = PowerOverlays.HighPerformance,
        [SystemMode.Performance] = PowerOverlays.MaxPerformance,
    };

    /// <summary>Every power plan on the machine, as friendly name to GUID.</summary>
    public static IReadOnlyDictionary<string, Guid> EnumeratePlans()
    {
        var plans = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);

        using var root = Registry.LocalMachine.OpenSubKey(SchemesKey);
        if (root is null) return plans;

        foreach (var name in root.GetSubKeyNames())
        {
            if (!Guid.TryParse(name, out var guid)) continue;

            using var scheme = root.OpenSubKey(name);
            if (scheme?.GetValue("FriendlyName") is not string friendly) continue;

            // Built-in plans store an indirect string ("@powrprof.dll,-15,Balanced");
            // the readable part is what follows the last comma.
            if (friendly.StartsWith('@'))
            {
                var comma = friendly.LastIndexOf(',');
                if (comma < 0 || comma == friendly.Length - 1) continue;
                friendly = friendly[(comma + 1)..].Trim();
            }

            plans[friendly] = guid;
        }

        return plans;
    }

    private static Guid? ResolvePlan(SystemMode mode)
    {
        var plans = EnumeratePlans();
        foreach (var candidate in PreferredPlanNames[mode])
            if (plans.TryGetValue(candidate, out var guid))
                return guid;

        return null;
    }

    /// <summary>
    /// Works out which mode the machine is currently in, or null when the
    /// current plan and overlay do not match any of them.
    /// </summary>
    public SystemMode? DetectCurrent()
    {
        var activePlan = GetActivePlan();
        var activeOverlay = PowerOverlayService.GetActiveOverlay();

        foreach (var mode in Enum.GetValues<SystemMode>())
        {
            if (Overlays[mode] != activeOverlay) continue;
            if (ResolvePlan(mode) is { } plan && plan == activePlan) return mode;
        }

        return null;
    }

    /// <summary>Applies a mode and returns a sentence describing what changed.</summary>
    public string Apply(SystemMode mode)
    {
        var plan = ResolvePlan(mode)
            ?? throw new InvalidOperationException(
                $"No power plan on this machine matches {mode}.");

        SetActivePlan(plan);
        PowerOverlayService.SetActiveOverlay(Overlays[mode]);

        return mode switch
        {
            SystemMode.Office => "Quiet and cool. The processor drops to low speeds when idle.",
            SystemMode.Gaming => "Balanced for games. The processor stays responsive under load.",
            _ => "Maximum speed. Expect more heat and louder fans.",
        };
    }

    private static Guid GetActivePlan()
    {
        using var key = Registry.LocalMachine.OpenSubKey(SchemesKey);
        return key?.GetValue("ActivePowerScheme") is string s && Guid.TryParse(s, out var g)
            ? g
            : Guid.Empty;
    }

    private static void SetActivePlan(Guid plan)
    {
        // powercfg is the supported way in; writing ActivePowerScheme in the
        // registry sets the value but does not make Windows apply it.
        var process = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = "powercfg.exe",
            Arguments = $"/setactive {plan:D}",
            UseShellExecute = false,
            CreateNoWindow = true,
        }) ?? throw new InvalidOperationException("Could not start powercfg.");

        process.WaitForExit(5000);
        if (process.ExitCode != 0)
            throw new InvalidOperationException($"powercfg refused the change (code {process.ExitCode}).");
    }
}
