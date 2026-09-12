# Privacy

Nextcalibur collects nothing about you and sends nothing about you
anywhere. This page says exactly what it does with data, so the claim can
be checked against the source rather than taken on trust.

## What leaves the machine

Only these, and only for the purposes named:

| When | Where to | What is sent | Why |
|---|---|---|---|
| A minute after start, then every six hours (if *Check for updates automatically* is on), or when you ask | `api.github.com` and `github.com` | An ordinary HTTPS request for this project's release list; a download of the release package if you accept an update | To find and install new versions |
| At the same times | `api.github.com` and `github.com` | A request for PawnIO's release list; a download of its installer if you accept | To offer the driver the CPU power reading needs, and keep it current |
| When you press *Open WinUtil* and confirm | `christitus.com` | A PowerShell download of that script | Because you asked for it; it is not part of Nextcalibur |
| When you press *Driver downloads* or *Open issues* | the laptop maker's site, `github.com` | Your browser opens the page | Because you asked for it |

Every request carries what any HTTPS request carries - your IP address and
a user-agent string (`Nextcalibur`, or Velopack's for the updater). No
identifier of yours, no hardware serial, no settings, no readings, no log
is ever included. There is no account, no sign-in, no telemetry, no crash
reporting, no analytics, no advertising, and no third-party library that
phones home.

Turn *Check for updates automatically* off in the tray menu or on the
Settings page and the application makes **no network request at all**
unless you press something that says it will.

## What stays on the machine

- **Settings** - `%AppData%\Nextcalibur\settings.json`: your preferences
  (mode, thresholds, lighting, the switches on the Settings page).
- **Log** - `%AppData%\Nextcalibur\logs\`: one file a day, seven kept,
  events only - starts, mode changes, graphics switches, updates, faults.
  Never sensor readings, never anything typed. It is yours: *Open log* in
  the corner shows it, and it is only ever sent anywhere if you attach it to
  a report yourself.
- **Scheduled tasks** and, on a laptop the firmware interface exists on,
  the changes to Windows' power settings described in the README.

All of it is removed by the uninstall; it asks first only about the power
plans, the power repair and the dependencies, which you may want to keep.

## What it reads

Temperatures, fan speeds, clocks, power draw, memory and disk use, the
keyboard lighting state and the graphics mode - from the laptop's firmware
interface, Windows and the drivers, for display in the window. None of it
is stored beyond the moment, except the lighting state and the chosen mode,
which are settings.

## Children of the application

Programs Nextcalibur opens for you - the browser, Explorer, Windows
Settings - are started with your ordinary rights, not the administrator
rights the application runs with. WinUtil alone is started elevated, on
purpose, after asking.

## Changes

This page is kept with the source, and changes to it appear in the release
notes. Questions: open an issue on the repository.
