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

The hardware protocol is in [PROTOCOL.md](PROTOCOL.md). The visual language is
in [DESIGN-SPEC.md](DESIGN-SPEC.md). The current work order for the design agent
is in [BRIEF.md](BRIEF.md); the naming contract it must satisfy is in
[CONTRACT.md](CONTRACT.md).

## Status

| Area | State |
|---|---|
| Firmware mailbox transport | done, verified on hardware |
| Temperature and fan readings | done |
| Keyboard lighting — colour, effects, brightness | done, protocol fully mapped |
| Power-mode overlay diagnosis and repair | done |
| System modes (Office / Gaming / Performance) | done |
| Tray, autostart, overheat warning | done |
| Coexistence with the vendor software | done |
| Installer (Velopack, ~6 MB) | done |
| Graphics mode switching | **detection only** — see below |
| RAM and disk gauges | drawn, not yet fed real data |
| Light theme | in progress |
| Fan control | **deliberately out of scope** — see below |

## Next

1. **Theme** — three-way dark / light / follow-system setting. The design agent
   is building the palettes; `OnThemeChanged` and persistence are pending here.
2. **Feed the RAM and disk gauges** real values.
3. **Graphics mode** — determine what the vendor software actually does when
   each of its three buttons is pressed, by watching device state while a person
   clicks them. Until then the page reports and does not switch.
4. **Plain language sweep** through the strings set from code. Several still
   name mechanisms rather than what the user sees.
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

### Brightness is applied by scaling colour, not by the hardware field

The firmware brightness field has three steps, too coarse to be a useful
control. Nextcalibur pins it to its top step and scales the RGB values instead —
the same technique the vendor software uses. Measured on hardware: all eleven
levels from 0% to 100% in steps of ten are distinguishable, and the progression
reads as even, so no gamma correction is applied.

### The power-overlay guard is what makes a Performance mode safe

Windows applies a power-mode overlay on top of the active plan, and the overlay
wins. When it is stuck on Best performance it carries a minimum processor state
of 100%, pinning the CPU at full speed at idle — the fault this project exists to
fix. Offering a Performance mode that selects that same overlay is only safe
because the repair caps its minimum processor state. Remove the guard and the
mode reintroduces the fault.

### Nothing animates continuously

No spinning fan gauges, no animated illustrations of lighting effects. This
application exists partly because the vendor's pins the processor at full speed;
it will not spend cycles on decoration. State transitions may fade, briefly.

### It shares the mailbox rather than competing for it

Both applications drive the same firmware mailbox and it holds one command at a
time. Neither can lock the other out, so when the vendor software is running
Nextcalibur polls further apart and gives up sooner. Retrying hard is exactly
what turns a collision into a stall for both.

### It never asks for administrator

The registry keys the overlay repair writes are writable by standard users, and
the firmware interface needs no driver. The vendor software installs a kernel
driver and so requires elevation; this does not, and should not start.

### Measured, not asserted

Claims about the machine belong in [PROTOCOL.md](PROTOCOL.md) with the
observation that produced them. Where something is inferred rather than
observed, it says so. The distinction is the point: a confident guess in this
domain costs more than an admitted gap.

## Boundaries between the two agents

The design agent owns presentation: styles, control templates, vector geometry,
layout markup, brand assets. It does not touch `src/Nextcalibur.Core/`,
`MainWindow.xaml.cs`, `App.xaml.cs`, `TrayPresence.cs`, `Controls/ColourWheel.cs`,
`src/Nextcalibur.Cli/`, `Nextcalibur.App.csproj` or `app.manifest`.

When markup needs a handler that does not exist yet, the correct move is to wire
it, let the build fail, and report — not to edit the code-behind. That has
happened twice and worked both times.

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
