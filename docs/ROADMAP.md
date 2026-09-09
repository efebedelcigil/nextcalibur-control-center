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
| RAM and disk gauges | done, fed from the system |
| Device names and clock speeds | done, read at runtime |
| Keyboard illustration | rebuilt from a clean vector; 0.135% CPU |
| Backing off while in the tray | written, **not yet verified** |
| Handle leak | **open, blocks release** — see below |
| Graphics mode switching | **detection only** — see below |
| Fan control | **deliberately out of scope** — see below |

## Next

1. **The handle leak.** Nothing ships until this is understood. It is the only
   thing standing between the application and a release, and it is the sort of
   fault that makes an application that runs unattended untrustworthy — which is
   the whole point of one that lives in the notification area.
2. **Verify the tray backoff.** The code is written: hidden, the slow timer moves
   to thirty seconds and skips everything that only exists to keep the window
   truthful. It has not been measured, because minimising the window
   programmatically does not work on a frameless window and the earlier attempt
   silently measured a window that was never minimised.
3. A small interface correction is pending with the design agent.
4. **Graphics mode** — determine what the vendor software actually does when each
   of its three buttons is pressed, by watching device state while a person
   clicks them. Until then the page reports and does not switch.
5. Release 0.4.0.

## What things cost

Measured on the development machine, idle, window open. Kept because this
project's whole argument is that it is cheap to run, and that claim needs
evidence rather than confidence.

| Build | CPU | Working set |
|---|---|---|
| no keyboard illustration | 0.010% | 145 MB |
| first SVG trace | 0.319% | 157 MB |
| after "simplification", with a costly clock reader | 0.449% | 177 MB |
| PDH clock reader, pixel-trace illustration | 0.211% | 165 MB |
| current: clean SVG, one path per zone, no shader | **0.135%** | 180 MB |

The last row is the mean of three runs: 0.168%, 0.082%, 0.156%. **The spread
between runs is wider than most of the improvements being measured**, so take a
single reading as an indication and not a result — three runs minimum, and treat
anything under a factor of two as noise.

Memory has gone the other way: 145 MB at the start, 180 MB now.

### Open: a handle leak

The process gains kernel handles steadily — measured at roughly 3.5 per second,
climbing from 949 to 1504 over three minutes with no plateau. Private memory is
stable at 122–125 MB throughout, and GDI and USER object counts are flat at 41
and 35, so this is neither a rendering leak nor a memory leak.

What is known:

- It is **not** in `Nextcalibur.Core`. The command-line tool doing the same
  sensor reads holds steady at 232 handles, and the clock readers at 264.
- Caching two repeated WMI calls — the interface check that ran every five
  seconds, and the vendor-process detection that ran on every sensor read —
  reduced the rate but did not stop it.
- It continues at the same rate whether or not the window is on screen.

At this rate a machine left running for a day would reach a few hundred thousand
handles. This blocks the release: an application that sits in the notification
area is one nobody looks at for days, and that is exactly the case it would
fail.

Ruled out so far, and worth not re-testing:

| Suspect | Result |
|---|---|
| Firmware mailbox reads | CLI doing only those holds at 232 handles |
| Clock readers (PDH, NVML) | CLI doing only those holds at 264 handles |
| Repeated `EcMailbox.IsSupported()` | cached; rate fell, growth continued |
| Repeated process enumeration | cached; rate fell, growth continued |
| Rendering or brushes | GDI 41 and USER 35, both flat throughout |
| Window being on screen | same rate hidden as visible |

What is left is the work the timers do inside the window: storage readings, the
theme registry poll, the tray tooltip update, and the WMI round trips the
mailbox makes through `System.Management`. The next step is to switch those off
one at a time and watch the rate, rather than reason about which of them looks
suspicious — two rounds of reasoning have now each removed real waste and left
the growth untouched.

For comparison, the fault this application exists to fix pinned the processor at
4.1 GHz while idle.

### On the markup size

An earlier brief asked for `MainWindow.xaml` back under 40 KB, on the assumption
the keyboard was most of its bulk. That was wrong: git history shows the file
was already 87–91 KB before any keyboard illustration existed, and the three
keyboard geometries now occupy about 2 KB of it. The rest is four pages of
markup, templates and two palettes. The file is 98 KB and that is roughly what
it should be.

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

If a number cannot be read, the interface does not show one — the clock readings
go blank rather than showing a stale or invented figure. Placeholder values are
tracked as defects, not as decoration.

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

### Measure, don't assert

A round reported that a change had "reduced CPU load to zero". Measurement
showed it had risen. Claims about cost must come with the number that produced
them — in either direction. A change that turns out not to help is a useful
finding; a claim that it helped when it did not costs the next round.

The obvious way to read the processor's clock is a WMI class. That query was
measured at **275 ms**, which on a two-second timer is a seventh of a core held
permanently for one number. The same counter through PDH answers in well under a
millisecond. Neither cost was visible without measuring.

The same applies to claims about the hardware. Those belong in
[PROTOCOL.md](PROTOCOL.md) with the observation that produced them, and where
something is inferred rather than observed it says so. A confident guess in this
domain costs more than an admitted gap.

## Boundaries between the two agents

The design agent owns presentation: styles, control templates, vector geometry,
layout markup, drawing controls, brand assets. It does not touch
`src/Nextcalibur.Core/`, `MainWindow.xaml.cs`, `App.xaml.cs`, `TrayPresence.cs`,
`Controls/ColourWheel.cs`, `src/Nextcalibur.Cli/`, `Nextcalibur.App.csproj`,
`app.manifest`, `Assets/` or `tools/`.

When markup needs a handler that does not exist yet, the correct move is to wire
it, let the build fail, and report — not to edit the code-behind. That has
happened three times and worked every time.

**Report and stop; do not revert either.** A protected file was once edited to
fix a real bug, then reverted when that was pointed out. The revert also
discarded an encoding repair made to the same file by the other agent, and the
broken version was committed before anyone noticed. Leave protected files
exactly as found.

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
