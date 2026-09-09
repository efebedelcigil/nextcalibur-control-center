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
| Keyboard illustration | outlined keycaps in live zone colours; 0.033% CPU |
| Zone selection | dimmed live colour plus a lift; verified on screen |
| Keyboard hover | stationary, with continuous hit regions; no flicker |
| One copy at a time | done — a second launch wakes the first |
| Backing off while in the tray | **verified on hardware** — 19× cheaper; see below |
| Handle leak | **fixed** — WMI from the interface thread; see below |
| Graphics mode switching | **detection only** — see below |
| Fan control | **deliberately out of scope** — see below |

## Next

Taken from an audit of the repository rather than from memory, roughly in the
order that would matter to somebody who installed this.

1. **Publish 0.4.0.** It is built and waiting in `releases/`. Nothing since
   0.3.0 has reached anybody: the threading work, the tray reclamation, the
   whole lighting page and the two items below exist only for people who build
   it themselves.
2. **Graphics mode** — determine what the vendor software actually does when each
   of its three buttons is pressed, by watching device state while a person
   clicks them. Until then the page reports and does not switch.

### Done from the audit

**Updates now arrive.** `UpdateService` checks GitHub a minute after startup and
every six hours after that, downloads what it finds, and says once — through the
tray, which is where this application lives when it has something to say and no
window on screen. The release is written on the way out, when the user closes
the application themselves, because restarting an application under somebody
using it is worse than waiting. A copy running from a build output or a portable
unzip has nothing to replace, and switches itself off rather than failing.

This does not rescue anybody already on 0.3.0 — that version has no updater and
cannot be told about this one. The chain starts at 0.4.0.

**There are tests** — `tests/Nextcalibur.Core.Tests`, 31 of them, run with
`dotnet test`. They cover what can be covered without the machine: the 32-byte
firmware block, asserted against the byte offsets rather than only against a
round trip, since a round trip agrees with itself when both halves are wrong the
same way; lighting state, including that black is a colour and not an absent
one, and that each profile keeps its own; storage arithmetic; processor-name
tidying; and the coexistence policy, where the invariant that matters is
*politeness only ever slows us down* — inverting it would turn a collision into
a stall for both applications.

They were checked by breaking something on purpose: a byte offset moved by four
in `SmiCommand.ToBytes` failed exactly one test and no others. A test suite
nobody has seen fail is not evidence.

Two findings came out of writing them. `StorageUse.Describe` took a `unit`
parameter nothing ever passed, and it only relabelled the string — `Describe("TB")`
would have printed gigabyte figures with "TB" after them. It is gone.
`SystemInfo.Tidy` is now `internal` rather than private, with
`InternalsVisibleTo`, which is the one accommodation the production code makes
for the tests.

Lighting **persistence** is deliberately not covered: `LedState.Save` writes to
the real settings directory, and a test that clobbers somebody's keyboard
colours to prove it can save them costs more than it finds.

**The three settings have an interface**, in the tray menu rather than the
window: the close button's behaviour, the overheat threshold (never, 80, 85, 90,
95 °C) and the sampling interval (1, 2, 5, 10 seconds). That is where Windows
users look for a tray application's preferences, and a fifth page would have
broken the deliberate resemblance to the software this replaces. The menu holds
the same settings object the window does, so a change applies at once rather
than being read back from disk.

## What the README claimed that was not true

Found in the same audit, and fixed. Worth recording because a public README is
the only thing most people will read:

- It called the installer **self-contained, no .NET runtime required**. It is
  framework-dependent; the installer acquires the runtime, which is a different
  promise and needs an internet connection the first time.
- Its build command pointed at `src/Nextcalibur.sln`. The solution is at the
  repository root, so the one instruction a contributor would follow first
  failed.
- Its status table listed lighting, power and sensors as *protocol known* and
  fan reading as *under investigation*, months after all of them shipped. It
  described a project that does nothing yet.

The status table there now says what works, and says plainly that graphics mode
reports without switching and that fan control is deliberately absent.

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
| clean SVG, one path per zone, no shader | 0.135% | 180 MB |
| firmware read moved off the interface thread | 0.042% | 189 MB |
| outlined keycaps, no backing slab | 0.033% | 189 MB |
| continuous hit regions, hover stationary | **0.029%** | 185 MB |

A round reported the last change at 0.075% from a **thirty-second** sample and
called it a cost. Ten minutes on the same build measured 0.029% — the lowest
figure recorded. The short window was measuring its own noise.

The 0.135% row is the mean of three twenty-five-second runs: 0.168%, 0.082%,
0.156%. **The spread between those runs is wider than most of the improvements
being measured**, which is the argument for the last row: 0.042% is a single
*ten-minute* sample, and a window that long costs nothing but patience and does
not need averaging. Prefer one long measurement to three short ones.

Memory has gone the other way: 145 MB at the start, 189 MB now. It does settle —
it climbed to 189 MB over six minutes and then stayed there for the remaining
four — but it is worth a look before 1.0.

### Solved: WMI on the interface thread leaks a kernel handle per call

**`System.Management` requires a multi-threaded apartment.** Called from the
single-threaded interface thread — which is every thread a WPF window runs code
on — each call is marshalled across to an MTA thread, and each marshalling
leaves a kernel event behind that lives until the garbage collector finalises
it. The firmware mailbox goes through `System.Management`, and it was being read
from the timer on that thread.

Measured at **2.4 handles a second** for the sensor timer alone, 3.5 with the
slow timer as well. Moving the read to a thread-pool thread — pool threads are
already MTA, so no marshalling happens — stops it completely: ten minutes with
both timers running now moves between 668 and 709 handles and ends lower than it
started.

The same change took idle CPU from 0.135% to 0.042%, because a firmware round
trip is no longer taken on the thread that draws the window.

Two consequences worth keeping in mind:

- `EcMailbox` is now reached from more than one thread, so it locks. The lock is
  held across a whole command *and* its response, and `Hold()` extends it over a
  sequence — writing the three lighting zones is one operation as far as the
  hardware is concerned, and a sample landing in the middle makes it drop them.
  Stopping the sampling timer used to be enough for that; it no longer is, since
  a sample already in flight is on another thread.
- `Sample` is `async void` off a timer, so it catches broadly. An exception
  escaping it would take the process down rather than surface anywhere useful.

#### How it was found, which matters more than the fault

Three earlier rounds reasoned about which call looked suspicious. Each removed
real waste and left the growth exactly where it was. What actually worked:

1. **Count handles by object type, not in total.** `NtQuerySystemInformation`
   with `SystemExtendedHandleInformation` gives every handle a process holds and
   its type index; duplicating one handle per type and asking `NtQueryObject`
   names them. This needs no driver, no elevation and no Sysinternals. The
   answer came back as "100% of the growth is `Event`", which eliminated
   rendering, files, registry keys and sockets in a single measurement.
2. **Sample on a schedule, not before and after.** The count moves in a
   sawtooth: it climbs, then a garbage collection reclaims several hundred at
   once. A single before/after pair lands inside a climb or across a collapse
   and reports anything you like — one such pair had earlier produced "3.5 per
   second, no plateau", and another produced a *decrease*. Both were the same
   process behaving the same way.
3. **Bisect with a switch, not with an opinion.** Two environment variables that
   skipped each timer turned six rebuilds into one. Both off: flat. Fast timer
   only: the full fault. That located it in one afternoon after three rounds of
   argument had not.
4. **Isolate outside the application.** A small harness calling PDH and NVML
   directly, and the existing command-line tool for the mailbox, each held flat
   for seven minutes. That is what made the apartment the only remaining
   difference — the command line runs MTA, the window does not.

The earlier note that this "blocks the release" and would reach hundreds of
thousands of handles in a day was wrong on the second point: the count was
bounded by garbage collection all along. It was still a real fault, and the
first point stood.

### Nothing that moves on hover may move hit-testing

The keyboard's hover state flickered with the pointer held completely still —
two captures a second apart, no input between them, showed the zone bright in
one and dim in the other. It was a loop the hover made for itself: hovering
applied a scale and a translate to the zone, a render transform carries
hit-testing with it, the moved geometry dropped the pointer into the gap
between two keycaps, and the gaps had no hit-testable fill. Hover lost,
transform reverted, hover returned.

Hover now changes opacity only. The lift is kept for the **selected** state,
where a click rather than the pointer decides it and no feedback is possible.
Each zone also carries a transparent region behind its keycaps covering the gaps,
so crossing between two keys no longer drops the highlight.

The general rule, because this will come up again with any growing button or
sliding card: **whatever moves on hover must not be what is being hovered.**
Keep hover changes to colour and opacity, and attach movement to a click.

### One copy at a time

Launching the application while it was already running started a second copy:
two processes on the same firmware mailbox, two notification-area icons. The
mailbox holds one command at a time, so writes from one could land inside the
other's sequence — the fault `EcMailbox.Hold()` exists to prevent within a
process.

A named mutex now decides who runs. The copy that loses signals a named event
and exits; the copy that owns it brings its window forward, which is what
someone launching an application that is already in the tray means. Both names
are per-session rather than global, so two people signed in at once each get
their own.

### Verified: the tray backoff, and what going quiet cost

Hidden, the slow timer moves to thirty seconds and skips everything that only
exists to keep the window truthful. Measured from a cold start, sent to the tray
and left alone:

| | On screen | In the tray |
|---|---|---|
| CPU | 6.4 ms/s (0.040%) | **0.34 ms/s (0.002%)** |
| Working set | ~177 MB | 13–34 MB |

Nineteen times cheaper. The earlier attempt to measure this failed because
`ShowWindow(SW_MINIMIZE)` does nothing to a frameless window and said so
nowhere, so it measured a window that was never minimised. `CloseMainWindow()`
works, because `WM_CLOSE` goes through the window's own `OnClosing` handler and
hides to the tray — through the application's code rather than around it. Any
future measurement of this should record the window state alongside every
sample, so a bad run is visible rather than silent.

#### And what it broke

Going quiet caused an accumulation. Each firmware read leaves a few objects
whose handles are released only when a finaliser runs — about five and a half
per read. On screen this never shows: drawing allocates enough to keep
collections coming, and the finalisers run with them. Hidden, the application
allocates almost nothing, so no collection happens and nothing runs them. The
count climbed at **0.18 a second — some fifteen thousand a day** — and the
working set crept from 13 to 34 MB in twelve minutes. Making the application
cheaper is what made it accumulate.

Every tenth hidden tick, so once every five minutes, it now forces a collection:
queue the finalisers, wait for them — that is what actually closes the handles —
then reclaim, then hand back the pages. Over sixteen minutes in the tray the
count sawtooths and stays level, with no rise across cycles:

| Reclamation at | Peak | Trough |
|---|---|---|
| 4:40 | 748 | 670 |
| 9:40 | 720 | 666 |
| 14:41 | 723 | 666 |

Calling `GC.Collect` is normally the wrong instinct — the runtime schedules
collections better than a guess. The exception is an application that has gone
idle, where the heuristics have nothing left to work from. That is this case
exactly, and it is the same reasoning that already justified `EmptyWorkingSet`
on the same transition.

### Lighting writes now take the same route

Lighting writes had the same fault as the sensor reads and were left behind at
first: a colour-wheel drag pushed the handle count to 1183 before a collection
took it back. They now go to a thread-pool thread as well. Measured with an
automated drag — thirty-four wheel positions and two sweeps of the brightness
slider — the count went from 673 to 675, where the same interaction used to add
several hundred.

The write path is deliberately **not** asynchronous. `RunLighting` puts the work
on the pool and then blocks the interface thread waiting for it, which changes
the apartment and nothing else:

- Every caller here assumes the write has finished when it returns —
  `OnColourPicked` refreshes the preview on the next line, `OnProfileChecked`
  reloads four panels. Making it asynchronous means auditing all of that.
- Blocking is what throttles a drag. Mouse moves coalesce against a busy
  interface thread; freed from it, a drag would queue writes faster than the
  hardware takes them and the keyboard would trail the pointer. Fixing that
  needs coalescing, and coalescing has to know which writes supersede each other
  — a colour replaces a colour, but a brightness change must not swallow an
  effect change.

The hold is taken **inside** the lambda, on the thread that writes. `Monitor` is
thread-affine: taking it on the interface thread and writing on the pool one
would block the pool thread against a lock the interface thread owns while the
interface thread waits for the pool thread.

#### Found while measuring

`CpuClockReader.BaseMhz` caught only `ManagementException`, but WMI raises a
bare `COMException` in some conditions — seen on this machine. On the interface
thread that is an unhandled exception in a timer callback, which ends the
process. It now catches the COM failures too, and asks only once: on a machine
where the query fails it used to re-ask on every reading, which would have put a
WMI call on a two-second timer and leaked handles far faster than the fault
above.

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

Anything that touches `System.Management` belongs off the interface thread. It
needs an MTA thread, and calling it from the STA one the window runs on costs a
kernel handle per call — see the section above. `Task.Run` is enough; pool
threads are already MTA. This applies to any WMI added later, not only the
mailbox.

Drawing controls must expose their colours as dependency properties bound with
`DynamicResource`. A `Brush` cached in a field at construction survives a theme
change and stays the wrong colour — the fan gauge hub was exactly this bug.
