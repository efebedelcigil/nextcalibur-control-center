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
| Graphics mode switching | **reports only, by decision** — mechanism traced on hardware; see below |
| Fan control | **out of scope** — the vendor has no fan control either; see below |

## What the original does that we do not

A file-by-file autopsy of the vendor's installer was taken on 11 September 2026
in a disposable Windows: clean, installed, uninstalled, three full captures of
files, registry, services, drivers, tasks, certificates and power plans. Raw
data in `notes/` (kept out of this repository - it is an inventory of somebody
else's product). What it shows about the gap:

### 1. The power plans are theirs, and without them our modes collapse

The installer creates **Office**, **Gaming** and **High performance** from `.pow`
files it ships. `SystemModeService` looks those up **by name** and falls back to
Balanced. So on a machine that never had the vendor software - which is the
machine this project is for - all three modes land on the same plan and differ
only by overlay. Nobody would notice it was broken; it would just feel like the
modes do very little.

`powercfg /duplicatescheme` works **without elevation**, verified on hardware, so
this is fixable within the rules: create our own plans on first run rather than
borrowing theirs.

### 2. Hotkeys

The vendor listens on `GMC_WMIEvent` for the firmware's key events. We do not
listen at all. Whatever the Fn row does through that software stops working when
it is replaced, and nobody has checked what that covers. Needs investigating
before this can honestly be called a replacement.

### Two applications on one mailbox: recommend, do not decide

Set 11 September 2026. The vendor's Control Center and Nextcalibur drive the
same firmware mailbox, which holds one command at a time, so having both is a
standing source of stalled readings and lighting changes that do not stick.

**Recommend removing it. Never remove it, and never refuse to run because of
it.** If somebody says they will keep it, that is their machine and their call -
record that they accepted it and stop asking.

The check cannot be a one-off at install, because the order is not fixed:
somebody may install the vendor software *after* Nextcalibur, or reinstall it
later. So it is a standing condition, noticed whenever it changes.

Detection has two levels and they mean different things:

| | Meaning | Response |
|---|---|---|
| Installed | the two will collide sooner or later | recommend removing, once, unless already accepted |
| Running | they are colliding now | the banner already says so |

### Hotkeys: measured, and there is nothing to do

Closed 11 September 2026. The worry was that the vendor's Control Center
subscribes to `GMC_WMIEvent` - the firmware's own key-event class - and that
replacing it would quietly take the Fn row with it.

Tested on the machine with the vendor software **uninstalled**, listening on that
class while each key was pressed in turn:

| Key | Worked | Firmware event |
|---|---|---|
| Fn+F4 / F5 brightness | yes | none |
| Fn+F6 mute, F7 / F8 volume | yes | none |
| Fn+F9-F12 media | yes | none |
| Fn+F3 projection | yes (opens Windows' projection flyout) | none |
| Fn+F1 sleep, Fn+F2 wi-fi | yes | none |

Every one of them works with no vendor software on the machine, and not one of
them produces an event. **There is no lost function to reimplement.**

Two things had to be ruled out before that sentence was worth writing, and the
first attempt was worthless without them:

- **The plumbing.** Seeing no events proves nothing on its own - "none are sent"
  and "they are sent and we are not receiving them" look identical. Subscribing
  to `WmiMonitorBrightnessEvent` alongside gives a control: it fired on the
  brightness keys, so delivery works.
- **The permission.** The firmware's event class has **no security descriptor**,
  which means administrators only - a different GUID from the data block, and
  one we never granted. So the first run, unelevated, could not have received an
  event if one had been sent. Repeated elevated.

The honest limit: some platforms only start emitting these events after a
vendor driver enables them through ACPI, and no such driver is installed here.
That question stays open and does not matter - the keys already work.

### A real gap: the vendor switches mode when the charger comes out

Measured 11 September 2026, with a control run first because a change seen only
once proves nothing about who caused it.

**Without the vendor software**, unplugging changes nothing but the battery flag.
The plan stays where it was and Windows leaves the overlay alone.

**With it installed and running:**

```
03:30:32  unplugged   Gaming  ->  Office              by itself
03:30:39  plugged in  Office
03:30:40              Office  ->  Gaming              put back
03:30:42  (mode changed to High performance by hand)
03:30:46  unplugged   High performance -> Office      by itself
03:30:50  plugged in  Office -> High performance      put back
```

So it drops to the quiet plan on battery and restores whatever was selected when
the charger returns. That is what `ModeBeforeDC` in its registry is for, and it
is a feature Nextcalibur does not have.

Worth noting how little it needs: its kernel driver was `Stopped` throughout -
installed but not yet loaded, since the machine had not been restarted - and the
behaviour worked anyway. Nothing privileged is involved, and the power plan can
already be changed from here without elevation.

**To build:** notice the power source changing, switch to the quiet mode, and
put the previous one back on return. Two things to get right, both learned
tonight: remember the mode the *person* chose rather than the one we switched to,
and do nothing at all if they changed mode by hand while on battery - that was
their decision, not ours to undo.

Not on the firmware event channel, incidentally: unplugging produces no
`GMC_WMIEvent` at all. Windows' own power notification is where this comes from,
and it needs no permission.

### One key does send an event: Fn+Space

Found by carrying on past the F row. The firmware is silent for every key in
that row, and then Fn+Space produces this:

```
03:23:08.973   32 bytes   00-00-00 ... 00
03:23:10.391   32 bytes   01-00-00 ... 00
03:23:17.368   32 bytes   02-00-00 ... 00
03:23:18.816   32 bytes   00-00-00 ... 00
```

Four presses, and the first byte cycles `0, 1, 2, 0`. That is a three-step
level, and on these machines Fn+Space is the keyboard backlight key - so the
firmware is reporting the level it has moved to and expecting software to apply
it. Which is exactly why the vendor's Control Center subscribes to this class,
and it is the one thing on the whole keyboard that needed it.

Power source is not on this channel: unplugging and replugging produced nothing,
so whatever the vendor does with `ModeBeforeDC` it learns some other way.

**Confirmed on hardware: the backlight changes anyway.** The firmware applies
the level itself, so the event is a courtesy rather than a request, and that key
works with no software of any kind on the machine. Nothing to reimplement.

What it does leave is a small honesty problem. Press Fn+Space while Nextcalibur
sits in the notification area and the keyboard changes without this application
knowing, so the lighting controls describe a state the keyboard is no longer in.
Fixed by re-reading the hardware when the window becomes visible - hooked to
visibility rather than to window state, because closing to the tray hides the
window rather than minimising it, and the state never changes.

**Not by subscribing to the event.** That class has no security descriptor and
is therefore administrators-only, so listening would mean asking somebody for a
second elevation on a second GUID
(`74286d6e-429c-427a-b34b-b5d15d032b05`) - a poor trade for keeping a panel in
step that nobody is looking at while it is wrong.

### Look before you change anything

The owner set this alongside the uninstall rule, and it applies to every
component that touches the machine: **check first, adopt what is already there,
and write only what is actually missing.** Somebody who removes Nextcalibur,
keeps the power plans, and installs it again should get their plans back, not a
second set.

| Component | Behaviour |
|---|---|
| Power-overlay repair | Only acts on what `Diagnose()` reports wrong; a machine with the guard already in place is untouched |
| Power plans | A plan by our name is reused; a machine that already has the vendor's Office/Gaming/High performance keeps those rather than gaining duplicates |
| Sensor permission | Adds one entry to the existing descriptor and reports "nothing was changed" when the account already has access |
| Start with Windows | Compares the value before writing it |

The permission one was not just missing a check - it was actively wrong. It
wrote a descriptor of its own over whatever was there, so on a machine running
the vendor's Control Center it would have removed **that software's** access.
Complaining that the vendor leaves a permission behind and then trampling theirs
is not a position worth holding. It now adds an entry and removes only its own.

And a trap worth keeping written down, because it cost a working machine for two
minutes: **an elevated process reaches the data block whatever the descriptor
says**, and an elevated token carries the administrators group. So the first
version of the check read the descriptor's built-in `BA` entry as "this account
already has access", wrote nothing, reported success, and left the machine
unreadable the moment the window ran normally. Entries that only apply while
elevated - `BA` and `SY` - are now ignored when deciding whether an *ordinary*
account can get in. Five tests pin it.

### 3. Our uninstall is not clean, and that is now a rule

**Nextcalibur must leave nothing behind - but must ask before undoing anything
the user may want to keep.** The owner set this after seeing what the vendor
leaves; it is not a nice-to-have.

Today an uninstall leaves four things:

| Left behind | Scope | Ours to remove |
|---|---|---|
| `%AppData%\Nextcalibur\settings.json` | user | yes, silently |
| The startup entry under `HKCU\...\Run` | user | yes, silently |
| The WMI security grant we wrote | machine, needs admin | **ask** |
| The power-overlay guard (`OverrideACSettingIndex`) | machine, needs admin | **ask** |

The last two are the ones to ask about. The guard is a repair: removing it puts
the machine back where the CPU can be pinned at full speed, and somebody may
want to keep it after uninstalling us. The permission grant is the opposite -
leaving it is exactly what we criticised the vendor for - but it is still the
user's machine and their call. The same will apply to any power plans we create.

Velopack's `OnBeforeUninstallFastCallback` runs this, with a **30-second limit**
before the process is killed, so the question has to be one dialogue with a safe
default: no answer means keep, because silently undoing a repair is worse than
leaving a registry value behind.

### 4. Small things, listed so they are not rediscovered

- The vendor starts itself with a **scheduled task**; we use `HKCU\...\Run`.
  Ours needs no administrator, which is the better trade for what it does.
- It installs in Turkish. Nextcalibur is English only, by decision - the owner
  dropped the bilingual plan on 11 September 2026.
- `VGA.ini` and `Camera.ini` are device lists for graphics switching and the
  camera - both out of scope by decision.
- It carries `ProfileHelperModel.dll`, signed by **Intel Extreme Tuning
  Utility**, and a `SetPwrPlan.exe` whose version resource claims "Microsoft"
  while being signed by Quanta. Neither is something to copy.

## Next

Taken from an audit of the repository rather than from memory, roughly in the
order that would matter to somebody who installed this.

1. **The Display page, once the graphics-mode work concludes.** Also the design
   agent's, and it should wait for the answer rather than anticipate it — but
   some of it is already known and can be planned:

   - **A mode can be unavailable, and the page has to say why.** The vendor
     software refuses to go from Discrete straight to UMA — "Please switch to
     Hybrid mode first" — because in Discrete the panel is driven by the
     discrete card and switching it off would take the screen with it. Whatever
     this page becomes, an unreachable mode needs to look unreachable and give
     its reason, not fail on click.
   - **Some transitions need a restart**, and the page should say so before the
     user commits rather than after.
   - **What the current mode costs belongs on screen.** The code already reads
     the card's draw and utilisation through NVML — 16.5 W and 61 °C was the
     quietest Discrete reading taken. A number does more than a warning
     sentence. Do not quote the higher figures seen that day as the cost of the
     mode: Wallpaper Engine was running and rendering continuously, and that
     was probably most of them.
   - The three cards already exist and are named in [CONTRACT.md](CONTRACT.md).
     Nothing about them needs replacing — this is about states they cannot
     currently express: unavailable, needs-restart, and in-progress.

2. **An icon audit.** Confirm there is an icon everywhere one belongs and that
   they are the current mark: window and taskbar, Alt-Tab, notification area,
   the installer, the desktop and Start-menu shortcuts, Add or Remove Programs,
   and the title bar. Also the design agent's, and it wants checking on a
   running installed copy rather than in the markup.

3. **Prove the update chain end to end.** 0.4.0 carries the metadata 0.3.0
   lacked, so an installed copy should now find, download and apply a release on
   its own. Nobody has watched it happen. The next release is the test: install
   the current version, publish the next, and confirm the notice appears and the
   update lands after a restart.

4. **A switch for the overheat notification, in the window.** The settings and
   the tray menu are done; the control in the window is the design agent's, and
   the brief spells out which two settings back it.

## The graphics-mode question is answered

Settled on hardware on 10 September 2026 by walking the machine through
Discrete -> Hybrid -> UMA -> Hybrid -> Discrete and capturing the registry, the
vendor's files, driver and device state, and the firmware variable store before
each click, after each click, and after each reboot. Full evidence in
PROTOCOL.md; the short version:

| Transition | How it works | Restart | Resets the PIN |
|---|---|---|---|
| Discrete <-> Hybrid | firmware; nothing observable happens in Windows | yes | yes |
| Hybrid <-> UMA | SetupDi device disable / enable | no | no |

Discrete to UMA stays refused, and the reason is now plain rather than guessed:
in Discrete the panel is on the card UMA switches off.

Three things worth keeping:

- **UMA is the one mode reproducible without the vendor's driver.** It is an
  ordinary device disable. It still needs administrator, so it stays out of
  Nextcalibur, but the reason is now a policy decision rather than a technical
  wall.
- **`TpvSetup` byte 1 tracks the mode in firmware** - `0x03` Hybrid, `0x02`
  Discrete - and so do the `ConOut` device paths. Both change only after the
  reboot, so neither has been shown to be the switch itself. Nextcalibur reads
  the mode from WMI regardless: same answer, no elevation.
- **The vendor's own UI cannot survive its own UMA switch.** Four seconds after
  disabling the card it died inside `nvml.dll`, still polling it.

### What each mode costs

Measured on 10 September 2026, eight quiet minutes per mode, read through the
embedded controller so nothing woke the card. Vendor Control Center closed,
Wallpaper Engine closed, nothing calling NVML.

| | Hybrid | Discrete | Difference |
|---|---|---|---|
| GPU | 48.0 °C | **61.0 °C** | +13 |
| GPU fan | 3299 rpm | **3799 rpm** | +500 |
| CPU | 45.7 °C | **63.2 °C** | +17.5 |
| CPU fan | 3600 rpm | **4400 rpm** | +800 |

Figures are the mean of the last sixty seconds of each run. Discrete never
cooled: the GPU sat between 60 and 61 °C for the whole eight minutes, on an
idle desktop. Hybrid started at 55 °C, fell to 48 °C and settled there.

Two honest limits on this. The runs were on different evenings and room
temperature was not controlled, so the absolute numbers carry an unknown
offset - the 13 °C on the GPU is far larger than that offset plausibly is, but
the 17.5 °C on the CPU cannot be laid at the mode's door on this evidence
alone: the two chips share a heat pipe on this machine, so a hot GPU pulls the
CPU up with it. And UMA is still unmeasured.

Raw data: `tools/quiet-hybrid.txt`, `tools/quiet-discrete.txt`.

This changed the application. The Display page used to call NVML on every load
to report power draw - and an NVML call wakes the GPU, so in Hybrid the page
woke the very card the mode exists to keep asleep, then reported the draw it
had just caused. It now reads NVML only in Discrete, where the card is awake
regardless, and otherwise says what the mailbox already knows: temperature and
fan speed, which cost nothing and mean more to most people than watts.

### UMA was left unmeasured on purpose

The thermal table covers Discrete and Hybrid. UMA does not need a row: the
measurement would confirm something the mechanism already settles - the card is
switched off as a device, so it cannot be drawing power or making heat - and
reaching it and coming back costs two more resets of the Windows PIN. The
application's description of UMA rests on the mechanism, which is the stronger
ground anyway.

### The observation tooling has been removed

Both scheduled tasks are gone from the machine, verified with an elevated query
that now returns nothing:

| Task | What it did |
|---|---|
| `Nextcalibur-GraphicsLogger` | logged which chip drove the panel for 12 minutes after every startup |
| `Nextcalibur-GpuRecovery` | re-enabled the NVIDIA adapter after each startup, in case a switch left no display |

The scripts stay in `tools/` and reinstall with `-Install` and `-Arm`. What they
collected stays too: `tools/graphics-boot-log.csv`, the traces, and the firmware
dumps under `tools/trace/`.

### 0.4.0 is out

Published, and installed over 0.3.0 on the development machine: version 0.4.0,
package replaced, application running. The release carries what the updater
needs — `releases.win.json` and both nupkgs — which **0.3.0 did not**: that
release had only the installer and the portable zip, so even an updater would
have found no metadata to read.

Measured on the installed build, which is the one people get:

| | |
|---|---|
| Warm, window open | 0.053% CPU, handles flat over five minutes |
| First three minutes | 0.117% CPU, +147 handles |

The first row is what it costs. The second is startup and the update check that
fires a minute in; those handles are held until a collection rather than
accumulating, and the count is level afterwards. It reads higher than the 0.029%
measured on a build output because that figure came from an already-settled
process and this one includes the update path and single-file startup.

**The update chain has not been proven end to end.** The metadata and the
package are reachable over HTTP and the application runs without complaint
through its first check, but nothing has yet been observed *updating*: the
installed copy and the newest release are both 0.4.0. The next release proves
it, or does not.

The versions read 0.1.0, 0.3.0, 0.4.0 — 0.2.0 was never released. Renumbering
was considered and rejected: 0.3.0 is installed on this machine, so publishing
this build as 0.3.0 would leave that copy believing it was already current, and
renaming the 0.3.0 release to 0.2.0 would put 0.3.0 packages inside a release
called 0.2.0 — Velopack reads the version from the package, not from the name.
A gap nobody ever occupied costs nothing; renumbering a published version costs
the updater.

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

### Fan control stays out, and the fans are not the vendor's to control

The original reason recorded here was that the vendor software exposes no
fan-curve writes and there is nothing to observe. That was **right**, and a
later revision wrongly called it wrong: finding `UserFan1..3` in the vendor's
registry key looked like evidence of a feature. It is not. The owner confirmed
what the readings already implied - **the Control Center has no fan tab at all;
fans cannot be adjusted from it.**

What the machine holds:

```
FanControlStatus = 0        firmware automatic - the user curve is not in effect
FanControlSelect = 0
UserFan1 = 30,30,30,30,50,60,70,100;30,30,30,30,50,60,70,100
UserFan2 = 30,30,30,30,50,60,70,100;30,30,30,30,50,60,70,100
UserFan3 = 30,30,30,30,50,60,70,100;30,30,30,30,50,60,70,100
```

Three profiles holding byte-identical curves are not three profiles; they are a
template written three times - and the installer is what writes it, watched in a
sandbox: the same values appear on a machine that has never run the software.
They come from the base software this build was rebranded from - the installer is signed by Quanta Computer, not by Casper -
with the feature left out of the build, and
`FanControlStatus = 0` says firmware is running the fans.

So the owner's rule decides it cleanly: **copy the vendor exactly where the
vendor is in charge, and stay out entirely where it is not.** Fan speed is the
second case. Nextcalibur reads the fans and says when they are losing, and
writes nothing.

A second reason outlives this machine: a fan curve is safe only relative to the
state of the cooling it commands. This heatsink is dusty. A curve that holds
temperature on a clean machine may not hold it here, and the experiment that
would settle it runs the hardware hot.

If it is ever built, the shape is decided: the vendor's format, its indices, its
firmware-automatic fallback, restored on exit and on crash. The monitoring, the
overheat warning and the mailbox's `Hold()` stay in place for that day.

**The overheat switch is about the notification only.** It decides whether a
Windows notification appears; it does not touch a fan, and there is no code path
from it to one.

### Graphics mode reports, it does not switch

Measured on hardware, not assumed - and an earlier version of this section was
wrong in a way worth remembering. It said the vendor software "does not touch a
hardware MUX" and switches only through SetupDi. Half right: SetupDi is how UMA
works, and Discrete/Hybrid is a firmware change that leaves no trace in Windows
at all.

Nextcalibur reports the current mode and does not change it. That is a decision
about means, not a limit on what is possible:

- Discrete/Hybrid needs an undocumented IOCTL into a kernel driver this project
  does not ship and will not install.
- UMA needs no driver - it is an ordinary device disable - but it does need
  administrator, which this application has committed never to request.
- Both carry a cost the vendor never mentions: the firmware change invalidates
  TPM-sealed credentials. The Windows PIN has to be set up again, and BitLocker
  can ask for its recovery key. The page warns about this.

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

### Administrator: what is actually true

Every sentence in the previous version of this section was wrong, and one of
them was worse than wrong - it attributed a rule to the owner that the owner
never set. **"Never ask for administrator" was invented here and then quoted
back as the owner's requirement**, and used to justify decisions about graphics
modes and fan control. Corrected on 10 September 2026, when the owner said
plainly: there is no such rule, and elevation may be asked for.

The factual claims were no better:

| Claimed | Measured |
|---|---|
| the overlay repair's keys are writable by standard users | they are not - `HKLM\SYSTEM\CurrentControlSet\Control\Power` refuses an unelevated write |
| the firmware interface needs nothing privileged | the data block is administrators-only until access is granted |

What is true is narrower and more useful. The firmware mailbox needs **no
driver** - the GUID is in this machine's DSDT and Windows' own `wmiacpi.sys`
surfaces it. But a kernel-WMI block carries a security descriptor, and this one
grants `BA` alone:

```
O:BAG:BAD:(A;;0x121fff;;;BA)          before
O:BAG:BAD:(A;;0x12001f;;;BA)(A;;0x12001f;;;SY)(A;;0x12001f;;;<the user>)   after
```

The vendor's Control Center widens it. That was inference for a day and is now
measurement, taken in a disposable Windows that had never had either program on
it: no descriptor at all before, `(A;;0x121fff;;;AU)` after installing - every
authenticated user - and `(A;;0x121fff;;;BA)` after removing it, narrowed rather
than deleted and left behind. The last of those is byte-for-byte what this
laptop was found holding.

So this project ran for weeks through a door somebody else had propped open, and
found out when the vendor software was uninstalled and every reading stopped.

So the shape is: **elevation once, ordinary use thereafter.** Granting is a
single registry value; `tools/Grant-MailboxAccess.ps1` writes it and `-Revoke`
puts back exactly what was there. Two things about that grant are deliberate:

- It goes to **one account**, not to `BU` as the vendor's does. The block takes
  writes as well as reads - it is how the keyboard is lit - so widening it to
  every local account hands every local process a route to the embedded
  controller.
- The value name has **no braces and is lower case**, matching all 579 entries
  already under that key. Written any other way it sits in the registry looking
  correct and is never consulted, which cost an hour here.

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
