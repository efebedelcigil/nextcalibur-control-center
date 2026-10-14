using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class GameLibraryTests
{
    [Fact]
    public void Steam_library_folders_are_read()
    {
        const string vdf = """
            "libraryfolders"
            {
                "0"
                {
                    "path"		"C:\\Program Files (x86)\\Steam"
                    "apps" { "228980" "1" }
                }
                "1"
                {
                    "path"		"D:\\SteamLibrary"
                }
            }
            """;
        Assert.Equal([@"C:\Program Files (x86)\Steam", @"D:\SteamLibrary"], GameLibrary.SteamLibraryPaths(vdf));
    }

    [Fact]
    public void Steam_manifest_fields_are_read()
    {
        const string acf = """
            "AppState"
            {
                "appid"		"730"
                "name"		"Counter-Strike 2"
                "installdir"		"Counter-Strike Global Offensive"
                "UserConfig" { "name" "something else" }
            }
            """;
        var f = GameLibrary.SteamManifest(acf);
        Assert.Equal("730", f["appid"]);
        Assert.Equal("Counter-Strike 2", f["name"]);
        Assert.Equal("Counter-Strike Global Offensive", f["installdir"]);
    }

    [Fact]
    public void Riot_install_path_is_read() =>
        Assert.Equal(@"C:\Riot Games\VALORANT\live",
            GameLibrary.RiotInstallPath("product_install_full_path: \"C:/Riot Games/VALORANT/live\"\nproduct_install_root: \"C:/Riot Games\""));

    [Fact]
    public void Unreal_shipping_build_is_the_game_not_its_stub()
    {
        var picked = GameLibrary.PickPrograms([
            (@"C:\G\VALORANT.exe", 2_000_000),
            (@"C:\G\ShooterGame\Binaries\Win64\VALORANT-Win64-Shipping.exe", 90_000_000),
            (@"C:\G\Engine\Binaries\Win64\CrashReportClient.exe", 30_000_000),
        ], "VALORANT", "live");
        Assert.Equal("VALORANT-Win64-Shipping", picked[0]);
        Assert.DoesNotContain("CrashReportClient", picked);
    }

    [Fact]
    public void Installers_launchers_and_anticheat_are_never_the_game()
    {
        var picked = GameLibrary.PickPrograms([
            (@"C:\G\unins000.exe", 3_000_000),
            (@"C:\G\EasyAntiCheat_EOS_Setup.exe", 9_000_000),
            (@"C:\G\GameLauncher.exe", 50_000_000),
            (@"C:\G\bin\win64\cs2.exe", 1_200_000),
        ], "Counter-Strike 2", "Counter-Strike Global Offensive");
        Assert.Equal(["cs2"], picked);
    }

    [Fact]
    public void Named_like_the_game_wins_over_a_bigger_tool()
    {
        var picked = GameLibrary.PickPrograms([
            (@"C:\G\Tool.exe", 80_000_000),
            (@"C:\G\Hades.exe", 20_000_000),
        ], "Hades", "Hades");
        Assert.Equal(["Hades"], picked);
    }

    [Fact]
    public void Tiny_stubs_are_ignored() =>
        Assert.Empty(GameLibrary.PickPrograms([(@"C:\G\stub.exe", 10_000)], "X", "X"));
}
