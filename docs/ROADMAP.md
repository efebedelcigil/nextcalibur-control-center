# Roadmap and decision log

Where the project stands, what is next, and the decisions worth not relitigating.
Read this first.

---

## What this is

An open-source replacement for the Casper Excalibur Control Center on Tongfang
JS970-class laptops. Two layers:

```
src/Nextcalibur.Core/   hardware, power, settings   — behaviour
src/Nextcalibur.App/    WPF interface                — presentation
src/Nextcalibur.Cli/    command line
```

The hardware protocol is in [PROTOCOL.md](PROTOCOL.md). The visual language is in
[DESIGN-SPEC.md](DESIGN-SPEC.md). The current work order for the design agent is
in [BRIEF.md](BRIEF.md); the naming contract it must satisfy is in
[CONTRACT.md](CONTRACT.md).

## Status

| Area | State |
|---|---|
| Firmware mailbox transport | done, verified on hardware |
| Temperature and fan readings | done |
| Keyboard lighting — colour, effects, brightness | done, protocol fully mapped |
| Power-mode overlay diagnosis and repair | done |
| System modes (Office / Gaming / Performance) | done |
| Windows power modes as choice cards | done |
| Dark / light / follow-system theme | done |
| Tray, autostart, overheat warning | done |
| Coexistence with the vendor software | done |
| Installer (Velopack, ~6 MB) | done |
| Graphics mode switching | **detection only** — see below |
| RAM and disk gauges | markup ready and named; **still showing placeholders** |
| Device names shown in the interface | not yet read from the system |
| Fan control | **deliberately out of scope** — see below |

## Next

1. **Feed the RAM and disk gauges** real values. The markup now has
   `RamGauge` / `RamPercent` / `RamDetail` and the SSD equivalents, so this is
   unblocked. Until it is done those four numbers are invented, which is the one
   thing this project otherwise refuses to do.
2. **Read device names from the system.** The processor and graphics card should
   name themselves via WMI at runtime. No model string may ever be committed —
   the repository is public and a hardcoded name would be both wrong for other
   people's machines and a small privacy leak from the author's.
3. **Plain language sweep** through the strings set from code. Several still name
   mechanisms rather than what the user sees.
4. **Graphics mode** — determine what the vendor software actually does when each
   of its three buttons is pressed, by watching device state while a person
   clicks them. Until then the page reports and does not switch.
5. Release 0.4.0.

## Decisions worth keeping

### Fan control is out of scope

The vendor software exposes no fan-curve writes, so there is no traffic to
observe and implementing it would mean guessing at embedded-controller writes.
That is the one place in this interface where a wrong value has physical
consequences. Fan **monitoring** and the overheat warning are in; fan **control**
is not, and this is not an oversight to be corrected later without new evidence.

### Graphics mode reports, it does not switch

The vendor software does not touch a hardware MUX. It finds the discrete GPU by
PCI hardware ID and disables the device through the SetupDi API. Which of its
three buttons produces which device state has not been observed, and a wrong
guess can leave a machine with no working display path. The page therefore
explains the current setting and says plainly that switching is unavailable.

### No hardware model is ever written into the source

Device names come from the system at runtime. The repository is public: a
hardcoded model would be wrong on every other machine, and it would publish a
detail of the author's own.

### Nothing on screen is invented

If a number cannot be read, the interface does not show one. Placeholder values
are tracked as defects, not as decoration — see the RAM and disk gauges above.

### Brightness is applied by scaling colour, not by the hardware field

The firmware brightness field has three steps, too coarse to be a useful control.
Nextcalibur pins it to its top step and scales the RGB values instead — the same
technique the vendor software uses. Measured on hardware: all eleven levels from
0% to 100% in steps of ten are distinguishable, and the progression reads as
even, so no gamma correction is applied.

### The power-overlay guard is what makes a Performance mode safe

Windows applies a power-mode overlay on top of the active plan, and the overlay
wins. When it is stuck on Best performance it carries a minimum processor state
of 100%, pinning the CPU at full speed at idle — the fault this project exists to
fix. Offering a Performance mode that selects that same overlay is only safe
because the repair caps its minimum processor state. Remove the guard and the
mode reintroduces the fault.

### Theme follows Windows by polling, not by subscribing

Following the system theme uses a registry read on the timer the application
already runs. Subscribing to `SystemEvents` would add a package and a
message-pump dependency to notice a change a few seconds sooner. The user's own
click is instant either way.

### Nothing animates continuously

No spinning fan gauges, no animated illustrations of lighting effects. This
application exists partly because the vendor's pins the processor at full speed;
it will not spend cycles on decoration. State transitions may fade, briefly.

### It shares the mailbox rather than competing for it

Both applications drive the same firmware mailbox and it holds one command at a
time. Neither can lock the other out, so when the vendor software is running
Nextcalibur polls at seven seconds instead of two — clear of the vendor's own
six-second rhythm — and drops its retry budget from eight to three. Retrying hard
is precisely what turns a collision into a stall for both.

### It never asks for administrator

The registry keys the overlay repair writes are writable by standard users, and
the firmware interface needs no driver. The vendor software installs a kernel
driver and so requires elevation; this does not, and should not start.

### Measured, not asserted

Claims about the machine belong in [PROTOCOL.md](PROTOCOL.md) with the
observation that produced them. Where something is inferred rather than observed,
it says so. A confident guess in this domain costs more than an admitted gap.

## Boundaries between the two agents

The design agent owns presentation: styles, control templates, vector geometry,
layout markup, drawing controls, brand assets. It does not touch
`src/Nextcalibur.Core/`, `MainWindow.xaml.cs`, `App.xaml.cs`, `TrayPresence.cs`,
`Controls/ColourWheel.cs`, `src/Nextcalibur.Cli/`, `Nextcalibur.App.csproj`,
`app.manifest`, `Assets/` or `tools/`.

When markup needs a handler that does not exist yet, the correct move is to wire
it, let the build fail, and report — not to edit the code-behind. That has
happened three times and worked every time.

**Build in Release before reporting.** `dotnet build Nextcalibur.sln -c Release`.
Debug can succeed where Release does not, and the shipped configuration is the
one that matters. Close the running application first — it locks its own output
and produces build errors that look like code errors but are not.

## Notes for anyone reading the source

`UseWindowsForms` is on solely for the tray icon, which makes many type names
ambiguous with WPF. The project file aliases the WPF ones; add to that list
rather than fully qualifying at call sites.

`WFAC010` is suppressed on purpose. It is a WinForms analyser asking for
`ApplicationHighDpiMode` instead of the manifest's `dpiAware` block, but for WPF
the manifest is the only route to per-monitor DPI awareness — removing it was
measured to drop the process to system-aware. The reasoning is repeated in both
files.

A custom `Main` in `App.xaml.cs` lets the installer's hooks run before any UI
exists, which is why `App.xaml` is compiled as a `Page` rather than an
`ApplicationDefinition`.

Drawing controls must expose their colours as dependency properties bound with
`DynamicResource`. A `Brush` cached in a field at construction survives a theme
change and stays the wrong colour — the fan gauge hub was exactly this bug.
