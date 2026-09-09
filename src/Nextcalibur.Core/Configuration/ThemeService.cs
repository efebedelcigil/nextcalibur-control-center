using Microsoft.Win32;

namespace Nextcalibur.Core.Configuration;

/// <summary>What the user picked in the title bar.</summary>
public enum ThemePreference
{
    Dark,
    Light,

    /// <summary>Follow whatever Windows is set to, and keep following it.</summary>
    System,
}

/// <summary>Which palette should actually be loaded.</summary>
public enum Theme
{
    Dark,
    Light,
}

/// <summary>
/// Resolves the user's theme preference against the Windows setting.
///
/// Following Windows is done by polling rather than by subscribing to
/// SystemEvents: that would pull in another package and a message-pump
/// dependency, to notice a change a few seconds sooner than a check the
/// application is already making on its existing timer. The user's own click is
/// instant either way — this only governs how quickly we notice Windows
/// changing underneath us.
/// </summary>
public sealed class ThemeService
{
    private const string PersonalizeKey =
        @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

    private Theme _lastResolved;

    public ThemePreference Preference { get; set; } = ThemePreference.System;

    /// <summary>The palette that should be loaded right now.</summary>
    public Theme Resolved => Preference switch
    {
        ThemePreference.Dark => Theme.Dark,
        ThemePreference.Light => Theme.Light,
        _ => WindowsUsesLightTheme() ? Theme.Light : Theme.Dark,
    };

    /// <summary>
    /// True when the resolved theme differs from the last time this was asked.
    /// Call it from an existing timer; it does nothing while the preference is
    /// Dark or Light, since neither can change on its own.
    /// </summary>
    public bool PollForChange()
    {
        if (Preference != ThemePreference.System) return false;

        var now = Resolved;
        if (now == _lastResolved) return false;

        _lastResolved = now;
        return true;
    }

    /// <summary>Records the current resolution as the baseline for polling.</summary>
    public void MarkApplied() => _lastResolved = Resolved;

    /// <summary>Reads the Windows app theme. Defaults to dark if unreadable.</summary>
    public static bool WindowsUsesLightTheme()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(PersonalizeKey);
            return key?.GetValue("AppsUseLightTheme") is int v && v != 0;
        }
        catch
        {
            // An unreadable setting is not worth an exception; dark is the
            // application's own default.
            return false;
        }
    }
}
