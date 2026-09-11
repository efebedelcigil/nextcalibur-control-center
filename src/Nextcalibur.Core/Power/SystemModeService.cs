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
    /// <param name="includeBuiltIn">
    /// Whether Windows' own hidden plans count. They do not for choosing:
    /// Windows' "High performance" shares its name with the vendor's, and
    /// power-mode overlays do not take effect on it - measured 11 September
    /// 2026, the set succeeds and reads back Balanced. Balanced itself is
    /// always included; it is the one built-in plan this code selects.
    /// </param>
    public static IReadOnlyDictionary<string, Guid> EnumeratePlans(bool includeBuiltIn = false)
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
            var builtIn = friendly.StartsWith('@');
            if (builtIn)
            {
                var comma = friendly.LastIndexOf(',');
                if (comma < 0 || comma == friendly.Length - 1) continue;
                friendly = friendly[(comma + 1)..].Trim();
            }

            if (builtIn && friendly != "Balanced" && (!includeBuiltIn || plans.ContainsKey(friendly))) continue;
            plans[friendly] = guid;
        }

        return plans;
    }

    /// <summary>The name Nextcalibur gives a plan it creates itself.</summary>
    private static string OwnPlanName(SystemMode mode) => $"Nextcalibur {mode}";

    /// <summary>
    /// Whether this mode has a plan of its own - ours or the vendor's. The
    /// Balanced fallback does not count: it is what every mode collapses to
    /// when the real plans are gone, and the point of asking is to notice that.
    /// </summary>
    public static bool HasPlanFor(SystemMode mode)
    {
        var plans = EnumeratePlans();
        return plans.ContainsKey(OwnPlanName(mode))
            || PreferredPlanNames[mode].Any(n => n != "Balanced" && plans.ContainsKey(n));
    }

    private static Guid? ResolvePlan(SystemMode mode)
    {
        var plans = EnumeratePlans();

        // The vendor's first: theirs are tuned per mode, ours are copies of
        // Balanced told apart by overlay. Ours exist for when theirs are gone,
        // and step aside again if theirs come back.
        foreach (var candidate in PreferredPlanNames[mode])
        {
            if (candidate == "Balanced") break;
            if (plans.TryGetValue(candidate, out var vendor)) return vendor;
        }

        if (plans.TryGetValue(OwnPlanName(mode), out var own)) return own;

        return plans.TryGetValue("Balanced", out var balanced) ? balanced : null;
    }

    /// <summary>
    /// Makes sure each mode has a plan of its own to select.
    ///
    /// The Office, Gaming and High performance plans this used to rely on are
    /// not Windows'. They are created by the vendor's installer from `.pow`
    /// files it ships - watched happening in a clean Windows on 11 September
    /// 2026. On a machine that never had that software, all three names are
    /// missing, every mode falls back to Balanced, and the modes quietly become
    /// three labels for one plan. Nobody would see an error; the modes would
    /// just seem to do very little.
    ///
    /// So we make our own rather than borrow theirs. `powercfg /duplicatescheme`
    /// needs no administrator - verified on hardware - so this costs nothing at
    /// first run, and the plans are named after this application so anybody
    /// looking at their power settings can see where they came from and remove
    /// them.
    /// </summary>
    /// <returns>The names of any plans that had to be created.</returns>
    public static IReadOnlyList<string> EnsurePlansExist()
    {
        var created = new List<string>();
        var plans = EnumeratePlans();

        foreach (var mode in Enum.GetValues<SystemMode>())
        {
            if (plans.ContainsKey(OwnPlanName(mode))) continue;

            // A vendor plan by the old name is left alone and used as it is:
            // on a machine that has both, duplicating it would give somebody two
            // plans that do the same thing.
            if (PreferredPlanNames[mode].Any(n => n != "Balanced" && plans.ContainsKey(n))) continue;

            var source = ResolvePlan(mode);
            if (source is not { } from) continue;

            var copy = DuplicatePlan(from, OwnPlanName(mode));
            if (copy is not null) created.Add(OwnPlanName(mode));
        }

        return created;
    }

    /// <summary>Deletes a power plan. No privileges needed.</summary>
    public static void DeletePlan(Guid plan) => RunPowercfg($"/delete {plan:D}");

    private static Guid? DuplicatePlan(Guid source, string name)
    {
        var output = RunPowercfg($"/duplicatescheme {source:D}");
        if (output is null) return null;

        var match = System.Text.RegularExpressions.Regex.Match(output, @"([0-9a-fA-F-]{36})");
        if (!match.Success || !Guid.TryParse(match.Value, out var created)) return null;

        RunPowercfg($"/changename {created:D} \"{name}\" \"Created by Nextcalibur\"");
        return created;
    }

    private static string? RunPowercfg(string arguments)
    {
        try
        {
            var process = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "powercfg.exe",
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            });
            if (process is null) return null;

            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(5000);
            return process.ExitCode == 0 ? output : null;
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            return null;
        }
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

    /// <summary>
    /// What Windows is actually running, in words, for when
    /// <see cref="DetectCurrent"/> finds none of the three modes. A machine
    /// straight from setup sits on Balanced with whatever overlay Windows
    /// chose, and a blank row of tabs says nothing about that.
    /// </summary>
    public static string DescribeCurrent()
    {
        var active = GetActivePlan();
        var plan = EnumeratePlans(includeBuiltIn: true).FirstOrDefault(p => p.Value == active).Key ?? "an unnamed plan";
        return $"{plan} plan, {PowerOverlays.Describe(PowerOverlayService.GetActiveOverlay())}";
    }

    /// <summary>
    /// Applies a mode and returns a sentence describing what changed.
    /// </summary>
    /// <exception cref="UnsafeModeException">
    /// Performance was asked for while the overlay guard is missing.
    /// </exception>
    public string Apply(SystemMode mode)
    {
        // Performance activates the Best-performance overlay, which carries a
        // minimum processor state of 100% unless the guard caps it. Offering it
        // without the guard would mean this application creating the fault it
        // exists to repair - on a machine where the repair has not run, or where
        // it ran without the rights to finish.
        if (mode == SystemMode.Performance &&
            !PowerOverlayService.PerformanceModeIsSafe(new PowerOverlayService().Diagnose()))
        {
            var gap = Environment.NewLine + Environment.NewLine;
            throw new UnsafeModeException(
                "Performance mode is not safe on this machine yet." + gap +
                "It switches Windows to the Best-performance power mode, which on this " +
                "machine pins the processor at full speed even when nothing is running - " +
                "the fault Nextcalibur exists to repair." + gap +
                "Run the repair from the Power Mode panel first. It needs administrator " +
                "rights once, and after that this mode is safe to use.");
        }

        // The plan can have gone since start: the vendor's uninstaller takes
        // its three with it, and a mode whose plan it borrowed would be left
        // with nothing to select. Make ours the moment that happens, rather
        // than at the next start.
        if (!HasPlanFor(mode)) EnsurePlansExist();
        var plan = ResolvePlan(mode)
            ?? throw new InvalidOperationException(
                $"No power plan on this machine matches {mode}, and one could not be created.");

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

/// <summary>
/// Raised when a mode would leave the machine in the state this project exists
/// to fix.
/// </summary>
public sealed class UnsafeModeException(string message) : InvalidOperationException(message);
