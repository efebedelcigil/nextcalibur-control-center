using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Nextcalibur.Core.Configuration;

namespace Nextcalibur.Core.Power;

/// <summary>A game found installed: its name, which launcher it came from, and the program names it runs as.</summary>
public sealed record FoundGame(string Name, string Source, IReadOnlyList<string> Processes, string Folder);

/// <summary>
/// "Scan for games", the way NVIDIA's app does it (the owner's ask of 14
/// October 2026): the games installed through the usual launchers, read from
/// each launcher's own records - Steam's library manifests, Epic's install
/// manifests, GOG's and Ubisoft's registry keys, the EA, Riot and Xbox
/// folders - and, for each, the program it runs as, found among its files.
/// Nothing is started, nothing is written; a scan reads files and the
/// registry and takes a second or two. What it finds becomes program rules
/// (Gaming while the game runs), beside the ones added by hand.
/// </summary>
public static partial class GameLibrary
{
    // Steam's own tools, runtimes and redistributables are not games.
    private static readonly HashSet<string> SteamNotGames = ["228980", "250820", "1070560", "1391110", "1628350", "1493710", "2180100", "1826330", "431960"];

    // Where a game's files hold a tool bigger than the game itself, the program it runs as.
    private static readonly Dictionary<string, string> SteamKnown = new() { ["730"] = "cs2", ["570"] = "dota2", ["440"] = "tf_win64" };

    [GeneratedRegex(@"(unins|setup|install|redist|vcredist|vc_redist|dxsetup|directx|dotnet|prereq|crash|report|helper|launcher|updater|patcher|easyanticheat|anticheat|battleye|beservice|eac|cefprocess|webhelper|cleanup|activation|register|config|settings|benchmark|server|editor|sdk|python|java|nodejs|^unity ?crash|quicksfv|touchup|7z|winrar|ffmpeg|vconsole|resourcecompiler|hammer|wallpaper)", RegexOptions.IgnoreCase)]
    private static partial Regex NotTheGame();

    [GeneratedRegex(@"(Soundtrack|Redistributable|Dedicated Server|SDK|Proton|Steamworks|Steam Linux Runtime|SteamVR|Demo Tools|Wallpaper Engine)", RegexOptions.IgnoreCase)]
    private static partial Regex NotAGameName();

    /// <summary>Every launcher, every game found; never throws.</summary>
    public static IReadOnlyList<FoundGame> Scan()
    {
        var found = new List<FoundGame>();
        foreach (var (source, reader) in new (string, Func<IEnumerable<(string Name, string Folder, string? Exe)>>)[]
                 {
                     ("Steam", Steam), ("Epic", Epic), ("GOG", Gog), ("Ubisoft", Ubisoft),
                     ("EA", Ea), ("Riot", Riot), ("Xbox", Xbox),
                 })
        {
            try
            {
                foreach (var (name, folder, exe) in reader())
                {
                    if (NotAGameName().IsMatch(name)) continue;
                    var processes = exe is not null && File.Exists(exe)
                        ? new[] { Path.GetFileNameWithoutExtension(exe) }.Concat(ProgramsIn(folder, name)).ToList()
                        : ProgramsIn(folder, name).ToList();
                    var names = processes.Select(ProgramRules.Normalise).OfType<string>()
                        .Where(p => !NotTheGame().IsMatch(p))
                        .Distinct(StringComparer.OrdinalIgnoreCase).Take(2).ToList();
                    if (names.Count > 0) found.Add(new FoundGame(name.Trim(), source, names, folder));
                }
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException or System.Security.SecurityException or ArgumentException)
            {
                Log.Warn("games", $"{source}: {ex.Message}");
            }
        }
        return found
            .GroupBy(g => g.Processes[0], StringComparer.OrdinalIgnoreCase).Select(g => g.First())
            .OrderBy(g => g.Name, StringComparer.CurrentCultureIgnoreCase).ToList();
    }

    // ------------------------------------------------------------------ launchers

    private static IEnumerable<(string, string, string?)> Steam()
    {
        var steam = RegistryString(Registry.LocalMachine, @"SOFTWARE\WOW6432Node\Valve\Steam", "InstallPath")
                    ?? RegistryString(Registry.CurrentUser, @"Software\Valve\Steam", "SteamPath");
        if (steam is null) yield break;
        var libraries = new List<string> { steam };
        var vdf = Path.Combine(steam, "steamapps", "libraryfolders.vdf");
        if (File.Exists(vdf)) libraries.AddRange(SteamLibraryPaths(File.ReadAllText(vdf)));

        foreach (var library in libraries.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var apps = Path.Combine(library, "steamapps");
            if (!Directory.Exists(apps)) continue;
            foreach (var manifest in Directory.EnumerateFiles(apps, "appmanifest_*.acf"))
            {
                var fields = SteamManifest(File.ReadAllText(manifest));
                if (!fields.TryGetValue("installdir", out var dir) || !fields.TryGetValue("name", out var name)) continue;
                if (fields.TryGetValue("appid", out var id) && SteamNotGames.Contains(id)) continue;
                var folder = Path.Combine(apps, "common", dir);
                if (!Directory.Exists(folder)) continue;
                string? exe = null;
                if (id is not null && SteamKnown.TryGetValue(id, out var known))
                    exe = Directory.EnumerateFiles(folder, known + ".exe", new EnumerationOptions { RecurseSubdirectories = true, MaxRecursionDepth = 5, IgnoreInaccessible = true }).FirstOrDefault();
                yield return (name, folder, exe);
            }
        }
    }

    private static IEnumerable<(string, string, string?)> Epic()
    {
        var manifests = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Epic\EpicGamesLauncher\Data\Manifests");
        if (!Directory.Exists(manifests)) yield break;
        // Epic's own list of what is installed: a manifest left behind by an
        // uninstall is not a game on the machine. Without the list, the
        // manifests and their folders are all there is to go by.
        var installed = EpicInstalledFolders();
        foreach (var item in Directory.EnumerateFiles(manifests, "*.item"))
        {
            using var doc = JsonDocument.Parse(File.ReadAllText(item));
            var root = doc.RootElement;
            if (root.TryGetProperty("AppCategories", out var cats) && cats.ValueKind == JsonValueKind.Array
                && !cats.EnumerateArray().Any(c => c.GetString() is "games")) continue;
            var name = root.TryGetProperty("DisplayName", out var n) ? n.GetString() : null;
            var folder = root.TryGetProperty("InstallLocation", out var l) ? l.GetString() : null;
            var exe = root.TryGetProperty("LaunchExecutable", out var e) ? e.GetString() : null;
            if (name is null || folder is null || !Directory.Exists(folder)) continue;
            if (installed is not null && !installed.Contains(NormaliseFolder(folder))) continue;
            yield return (name, folder, exe is null ? null : Path.Combine(folder, exe));
        }
    }

    private static HashSet<string>? EpicInstalledFolders()
    {
        var list = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Epic\UnrealEngineLauncher\LauncherInstalled.dat");
        if (!File.Exists(list)) return null;
        try
        {
            using var doc = JsonDocument.Parse(File.ReadAllText(list));
            if (!doc.RootElement.TryGetProperty("InstallationList", out var items) || items.ValueKind != JsonValueKind.Array) return null;
            return items.EnumerateArray()
                .Select(i => i.TryGetProperty("InstallLocation", out var l) ? l.GetString() : null)
                .OfType<string>().Select(NormaliseFolder).ToHashSet(StringComparer.OrdinalIgnoreCase);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            return null;
        }
    }

    /// <summary>A folder as both of Epic's files can be compared by: backslashes, no trailing one.</summary>
    public static string NormaliseFolder(string folder) => folder.Replace('/', '\\').TrimEnd('\\');

    private static IEnumerable<(string, string, string?)> Gog()
    {
        using var games = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\GOG.com\Games");
        if (games is null) yield break;
        foreach (var id in games.GetSubKeyNames())
        {
            using var game = games.OpenSubKey(id);
            var name = game?.GetValue("gameName") as string;
            var folder = game?.GetValue("path") as string;
            var exe = game?.GetValue("exe") as string;
            if (name is not null && folder is not null && Directory.Exists(folder)) yield return (name, folder, exe);
        }
    }

    private static IEnumerable<(string, string, string?)> Ubisoft()
    {
        using var installs = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Ubisoft\Launcher\Installs");
        if (installs is null) yield break;
        foreach (var id in installs.GetSubKeyNames())
        {
            using var game = installs.OpenSubKey(id);
            if (game?.GetValue("InstallDir") is not string folder || !Directory.Exists(folder)) continue;
            yield return (new DirectoryInfo(folder).Name, folder, null);
        }
    }

    private static IEnumerable<(string, string, string?)> Ea()
    {
        foreach (var root in new[] { Environment.SpecialFolder.ProgramFiles, Environment.SpecialFolder.ProgramFilesX86 })
        {
            var ea = Path.Combine(Environment.GetFolderPath(root), "EA Games");
            if (!Directory.Exists(ea)) continue;
            foreach (var folder in Directory.EnumerateDirectories(ea)) yield return (new DirectoryInfo(folder).Name, folder, null);
        }
    }

    private static IEnumerable<(string, string, string?)> Riot()
    {
        var metadata = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Riot Games\Metadata");
        if (!Directory.Exists(metadata)) yield break;
        foreach (var settings in Directory.EnumerateFiles(metadata, "*.product_settings.yaml", SearchOption.AllDirectories))
        {
            var folder = RiotInstallPath(File.ReadAllText(settings));
            if (folder is null || !Directory.Exists(folder)) continue;
            var product = Path.GetFileName(settings).Split('.')[0];
            if (product.Equals("riot_client", StringComparison.OrdinalIgnoreCase)) continue;
            var name = product switch
            {
                "valorant" => "VALORANT",
                "league_of_legends" => "League of Legends",
                "bacon" => "Legends of Runeterra",
                "teamfighttactics" => "Teamfight Tactics",
                _ => product.Replace('_', ' '),
            };
            // League runs as "League of Legends.exe" under Game\; its client is not the game.
            var game = Path.Combine(folder, "Game", "League of Legends.exe");
            yield return (name, folder, File.Exists(game) ? game : null);
        }
    }

    private static IEnumerable<(string, string, string?)> Xbox()
    {
        foreach (var drive in DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed && d.IsReady))
        {
            var xbox = Path.Combine(drive.RootDirectory.FullName, "XboxGames");
            if (!Directory.Exists(xbox)) continue;
            foreach (var folder in Directory.EnumerateDirectories(xbox))
            {
                var content = Path.Combine(folder, "Content");
                yield return (new DirectoryInfo(folder).Name, Directory.Exists(content) ? content : folder, null);
            }
        }
    }

    // ------------------------------------------------------------------ parsing, tested

    [GeneratedRegex("\"path\"\\s+\"([^\"]+)\"", RegexOptions.IgnoreCase)]
    private static partial Regex VdfPath();

    [GeneratedRegex("^\\s*\"([^\"]+)\"\\s+\"([^\"]*)\"", RegexOptions.Multiline)]
    private static partial Regex VdfPair();

    [GeneratedRegex("product_install_full_path:\\s*\"?([^\"\\r\\n]+)\"?", RegexOptions.IgnoreCase)]
    private static partial Regex RiotPath();

    /// <summary>The library folders listed in Steam's libraryfolders.vdf.</summary>
    public static IReadOnlyList<string> SteamLibraryPaths(string vdf) =>
        VdfPath().Matches(vdf).Select(m => m.Groups[1].Value.Replace(@"\\", @"\")).ToList();

    /// <summary>The top-level "key" "value" pairs of an appmanifest_*.acf (the first of each key wins).</summary>
    public static IReadOnlyDictionary<string, string> SteamManifest(string acf)
    {
        var fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (Match m in VdfPair().Matches(acf)) fields.TryAdd(m.Groups[1].Value, m.Groups[2].Value.Replace(@"\\", @"\"));
        return fields;
    }

    /// <summary>The install folder in a Riot product_settings.yaml.</summary>
    public static string? RiotInstallPath(string yaml) =>
        RiotPath().Match(yaml) is { Success: true } m ? m.Groups[1].Value.Trim().Replace('/', '\\') : null;

    /// <summary>
    /// Which of a game's programs is the game: Unreal's "-Shipping" build
    /// first, then one named like the game or its folder, then the largest -
    /// never an installer, a crash reporter, an anti-cheat service or a
    /// launcher.
    /// </summary>
    public static IReadOnlyList<string> PickPrograms(IEnumerable<(string Path, long Size)> exes, string gameName, string folderName)
    {
        var candidates = exes
            .Select(e => (e.Path, e.Size, Name: System.IO.Path.GetFileNameWithoutExtension(e.Path)))
            .Where(e => !NotTheGame().IsMatch(e.Name) && e.Size >= 512 * 1024)
            .ToList();
        if (candidates.Count == 0) return [];

        static string Squash(string s) => new(s.Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray());
        var game = Squash(gameName);
        var folder = Squash(folderName);

        var picked = new List<string>();
        picked.AddRange(candidates.Where(c => c.Name.EndsWith("-Shipping", StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(c => c.Size).Select(c => c.Name).Take(1));
        picked.AddRange(candidates.Where(c => Squash(c.Name) is { Length: > 1 } n && (game.StartsWith(n) || n.StartsWith(game) || folder.StartsWith(n) || n.StartsWith(folder)))
            .OrderByDescending(c => c.Size).Select(c => c.Name).Take(1));
        if (picked.Count == 0) picked.Add(candidates.OrderByDescending(c => c.Size).First().Name);
        return picked.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
    }

    private static IEnumerable<string> ProgramsIn(string folder, string gameName)
    {
        var exes = new List<(string, long)>();
        try
        {
            var options = new EnumerationOptions { RecurseSubdirectories = true, MaxRecursionDepth = 5, IgnoreInaccessible = true, AttributesToSkip = FileAttributes.ReparsePoint };
            foreach (var path in Directory.EnumerateFiles(folder, "*.exe", options))
            {
                exes.Add((path, new FileInfo(path).Length));
                if (exes.Count >= 400) break;
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
        return PickPrograms(exes, gameName, new DirectoryInfo(folder).Name);
    }

    private static string? RegistryString(RegistryKey hive, string path, string name)
    {
        try
        {
            using var key = hive.OpenSubKey(path);
            return key?.GetValue(name) as string is { Length: > 0 } s && Directory.Exists(s) ? s.Replace('/', '\\') : null;
        }
        catch (Exception ex) when (ex is System.Security.SecurityException or UnauthorizedAccessException or IOException) { return null; }
    }
}
