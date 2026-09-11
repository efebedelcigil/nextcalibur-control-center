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

**Closed 11 September 2026, later.** Three rules now, all tested by renaming
the vendor's plan away and back with the application running:

- The vendor's plan is used when it exists; ours (`Nextcalibur <mode>`, a copy
  of Balanced told apart by overlay) only when it does not, and theirs takes
  over again if it comes back.
- Windows' own hidden plans are never candidates. Its "High performance" has
  the vendor's name and power-mode overlays do not take on it - the set
  succeeds and reads back Balanced - which is what "Performance shows no mode"
  turned out to be.
- The plan can vanish while running - the vendor's uninstaller takes it - so
  every `Apply` makes the plan first if it is missing, and the slow timer
  re-applies the current mode when its plan has gone. Seen working: rename
  away, ten seconds, `Nextcalibur Performance` created and selected.

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

**And when it leaves, 11 September 2026.** Its uninstaller, run with this
application open, did two things to it: narrowed the firmware permission back
to administrators (every reading stops) and put Windows on Balanced (the mode
drops). The plans survived this time. So the slow timer watches the vendor's
install state; on installed-to-gone it asks once for the permission back with
a sentence saying why, reconnects the mailbox, and re-applies the current
mode - plan, overlay and firmware profile. The recommendation to remove is
also re-armed when the vendor is gone, so a later reinstall is a new question,
and it is put while running rather than only at start. Startup path tested
live after the uninstall; the running path is the same code, untested live.

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

### Settled: the vendor's kernel driver never loads here

Found 11 September 2026 and confirmed across a restart. Windows refuses the
driver at every boot:

```
03:40:58  System        7000  ControlCenter service failed to start
03:40:58  CodeIntegrity 3004  unable to verify the image integrity of
                              ControlCenter64.sys - file hash could not be
                              found on the system
```

Memory Integrity is enforcing on this machine, and the driver's signing
certificate expired on 5 July 2026. `sc start` by hand gives 577,
`ERROR_INVALID_IMAGE_HASH`. The service is registered, set to start
automatically, and has never run.

The vendor software works regardless. The charger behaviour below was measured
with the driver in this state, and the Display Mode switch watched on 10
September - Discrete to Hybrid and back, panel moving between chips - happened
on this same machine with the same policy in force. **So the switch does not go
through the driver.** PROTOCOL.md said it did, on the strength of
`DeviceIoControl` appearing in the native library; that inference is retracted
there.

What is left, then, is the question that matters: **how does it switch?** With
no driver there is no port I/O and no private IOCTL. The obvious remaining route
is the ACPI-WMI mailbox this project already reads and writes - a command in the
`0xFB00` family the protocol document has never seen. If that is it, Nextcalibur
can do it too, with nothing to ship and nothing to install.

**Watched the mailbox during a click (11 September, 03:45).** Two things came
out, neither of them the switch itself.

The vendor application's **startup** sends traffic this protocol had never seen:

```
0xFB00 / 0x0300 / a2=1      write, subsystem 3, value 1   (twice at start, again later)
0xFA00 / 0x0206             read, thermal block, register 6
```

Subsystem 3 is new - only `0x01` (lighting) and `0x02` (thermal) were known.
What "3 = 1" means is not established. A plausible reading is "control software
present", the kind of flag firmware uses to decide whether to hand a key over
to software or handle it itself; that would fit the Fn+Space finding above and
is worth testing rather than assuming.

The **click** on Discrete, answered with No, produced no distinctive write at all
- only a residue of `00 00 00 00 01 00 00 00 ... 01 00 00 00`, which looks like
the response to a read whose command fell between two polls. So the button
reads the mode and shows its dialogue. **The write that actually switches must
happen on Yes**, immediately before the restart, and that has not been captured
because capturing it costs a PIN reset.

**Captured, 05:56.** One PIN reset later, the last line before the restart:

```
0xFB00 / 0x0203 / a2=2      write, subsystem 2, register 3, value 2  ->  Discrete
```

And the way back, 06:15:

```
0xFB00 / 0x0203 / a2=1      ->  Hybrid
```

So the register is `1` Hybrid, `2` Discrete - both watched being written, both
confirmed by the machine coming up in that mode, and both matching what the
button's own read returns. (An earlier draft inferred `3` for Hybrid from the
firmware variable `TpvSetup`; that was wrong, and is why the second PIN reset
was spent rather than saved.) The switch is **one write, through the mailbox
this project already uses, with a permission it already holds**. No driver, no
IOCTL, nothing to ship.

**Nextcalibur can switch graphics modes, and will.** Decided by the owner on
11 September 2026, all three modes. The costs do not go away because the
command turned out to be simple - the change invalidates TPM-sealed
credentials, the Windows PIN has to be set up again, and BitLocker can demand
its recovery key - so the difference from the vendor is honesty about them:
said plainly before the switch, with a confirmation, rather than a bare
"restart?" that mentions none of it.

What gets built:

| Mode | How | Needs | Restart |
|---|---|---|---|
| Hybrid | mailbox write `FB00/0203` = `1` | the sensor permission it already has | yes |
| Discrete | mailbox write `FB00/0203` = `2` | same | yes |
| UMA | SetupDi disable of the discrete adapter | administrator, once per switch | no |
| leaving UMA | SetupDi enable | administrator | no |

Rules carried over from watching the vendor: UMA is only offered from Hybrid,
because in Discrete the panel is on the card being switched off. Only the two
observed values are ever written to the register. And the register is read
back before any restart is suggested, so a write that did not land is reported
rather than followed by a pointless reboot.

**Built and tested, 11 September 2026, 06:52.** UMA and back through
Nextcalibur's own code, verified with timestamps: `CM_PROB_DISABLED` and
`nvlddmkm` Stopped six seconds after the request, OK and Running six seconds
after the return. The firmware register reads back correctly (1, in Hybrid) with
the reply shaped exactly as captured.

Two defects the test found, both fixed, both worth remembering:

- **A disabled card read as Hybrid.** A switched-off adapter is still listed by
  Windows, so Detect saw "present, not driving" and called it Hybrid. "Switch to
  Hybrid" from UMA was then a no-op, and the card stayed off. Code 22,
  `CM_PROB_DISABLED`, now means UMA.
- **The process died inside nvml.dll on the way back** - the exact crash the
  vendor's Control Center has four seconds after its UMA button, which this
  project had noted with some satisfaction the day before. NVML's device handle
  names a device that has just been recreated; using it is an access violation
  .NET cannot catch. The handle is dropped before and after every switch, and
  the slow timer drops it when anything else switches the card.

**One prompt, not one per switch.** The vendor never asks for administrator
when switching the card; its daemon starts at logon from a scheduled task with
the highest privileges, so it is elevated all day. Nextcalibur will not run
elevated, but the same mechanism fits a narrower job: two scheduled tasks -
card off, card on - registered once, with no trigger, highest privileges,
started on demand. The one elevation the application already asks for, to open
the sensors, registers them; after that the card switches without a prompt.
Both tasks are part of the footprint and go at uninstall.

Still owed: a Discrete/Hybrid round trip through our own code rather than the
vendor's. The write and the read-back are verified; the boot that follows is
not. Two PIN resets, when the owner chooses.

Known and not chased: `Watch-Graphics.ps1` missed both device switches while
they were happening, though direct queries in the same seconds saw them. Its
change detection or its CIM session is stale somewhere.

The earlier check that "no vendor driver is involved" asked about
`ControlCenter64` and `ControlCenterC64`, which are file names; the service is
`ControlCenter`. That check proved nothing. This one does.

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

**Built, 11 September 2026.** `BatteryModePolicy` holds the rules,
`SystemEvents.PowerModeChanged` is the notice (no poll, no permission), the
tray's "Office mode on battery" turns it off. Tested Gaming and Performance
both ways. Two things found on the way: Windows keeps one overlay for AC and
one for battery, so the instant after unplugging, detection sees no mode at
all - the application uses the mode it last knew, not a fresh read. And
Windows comes up on Balanced after a restart whatever was active, so the
chosen mode is saved (`LastSystemMode`) and put back at start: as it was on
the charger, Office on battery with theirs restored when the charger returns.

**Found while chasing "it drops to Balanced", 11 September, later.** It did
not drop; it never took. Two plans on this machine are called "High
performance" - the vendor's and Windows' own - and the enumeration let the
built-in one win the name. Power-mode overlays do not take effect on Windows'
High performance plan: the set succeeds and reads back Balanced (probed on all
four plans; the vendor's two and Balanced take it, the built-in does not). So
Performance mode selected the wrong plan, its overlay evaporated, and at the
next start detection saw "High performance plan, Balanced" - no mode. A
built-in plan no longer displaces a custom one of the same name.

**The original note:** notice the power source changing, switch to the quiet
mode, and put the previous one back on return. Two things to get right, both learned
tonight: remember the mode the *person* chose rather than the one we switched to,
and do nothing at all if they changed mode by hand while on battery - that was
their decision, not ours to undo.

Not on the firmware event channel, incidentally: unplugging produces no
`GMC_WMIEvent` at all. Windows' own power notification is where this comes from,
and it needs no permission.

### Fn+Space, and a rudeness of ours it exposes

The F row is silent, and then Fn+Space speaks. Four presses without the vendor
software, eight more with it installed, and the first byte cycles cleanly:

```
02  00  01  02  00  01  02  00
```

A three-state cycle, and on this keyboard it is the backlight: off, dim, full.
The behaviour is **identical with and without the vendor software**, so the
firmware sends this on its own and does not need anybody's driver to be asked
first. The backlight changes whether or not software is listening - confirmed by
watching the keyboard - so there is no lost function to reimplement. This is
simply why the vendor's Control Center subscribes to that class at all.

**But it collides with how this project does brightness.** `LedController` pins
the firmware's brightness field to its top step on every write and scales the
RGB values instead, because three hardware steps are too coarse to be a useful
control. Fn+Space moves that same field. So:

1. Somebody presses Fn+Space and dims or extinguishes the keyboard.
2. Nextcalibur writes anything at all - a colour, an effect, a zone - and the
   brightness snaps back to full.

Their key, undone by us, silently. That is the same discourtesy this project
objects to elsewhere, and it needs deciding rather than leaving:

- **Leave it.** The percentage in the window is the control, and the key is a
  hardware override that lasts until the next write. Simple, and wrong in the
  way described above.
- **Read the level and respect it.** Fold the firmware's three steps into the
  percentage when state is re-read, so the window shows what the key did and a
  later write preserves it. Faithful, and more work: level 0 means the keyboard
  is off whatever colour is set, which the interface has to say rather than
  imply.

The second is the right one. Not done yet, and on inspection not quick either:

- There is nothing to read. LED reads on `0x0100` echo the header and return
  zero for every payload field (that is why `LedController` tracks its own
  state to begin with). The firmware never says where Fn+Space left the level.
- The only source is the event itself: `GMC_WMIEvent`, byte 0 = 0/1/2, which
  is exactly `LedBrightness.Off/Half/Full`. That class is administrators-only
  with no descriptor of its own, so it needs a second grant on
  `74286d6e-429c-427a-b34b-b5d15d032b05` in the same elevated step that grants
  the mailbox, plus a `ManagementEventWatcher` on `root\WMI` for the life of
  the process and a `LedController.HardwareLevel` that `Send()` uses instead of
  `Full`. `SetBrightness` from the window is the one place that may still force
  `Full`, because there the person has said what they want.
- Narrower than it first looked: the application writes to the keyboard only
  on an explicit click (colour, effect, brightness, on/off, profile, reload).
  Nothing is re-sent at startup or on wake. So the undo happens only when
  somebody presses Fn+Space and then changes lighting in the window.

Deferred on 11 September 2026 in favour of the Discrete round; the design
above is what to build.

### Start with Windows, from install

Set 11 September 2026. The vendor registers a logon task at install; a control
centre that is not running cannot warn about heat. So the first run writes the
`Run` entry (`--tray`), and the tray menu's "Start with Windows" is where it is
turned off. The first-run hook sets it once; it never re-asserts it after that,
so the person's choice sticks. A place for the toggle inside the window is
Antigravity's (BRIEF §6).

**A wrong turn, taken back the same night.** The Run entry looked skipped
at sign-in, so it was replaced by a logon task; the task then failed with
"file not found" on a file that plainly existed. The real cause was the
tooling, not Windows: the assistant's shell runs inside the desktop app's
sandbox, which gives its process tree - and every application it launches -
a private view of `HKCU` and the user's profile folders. The Run entry, the
"installed" 0.5.0 under `%LOCALAPPDATA%`, the uninstall key reading 0.5.0
and the settings file with `LastSystemMode` all lived in that view; the
real session had none of them (uninstall key 0.3.0, no `Nextcalibur`
folder, an old settings file). Explorer never saw the entry, so it never
ran it. The logon-task commit is reverted; the Run entry stands, untested
for real until the person installs 0.5.0 themselves. Rule from this:
**anything that must reach the real session - installs, settings, HKCU -
is done by the person, not from the assistant's shell**, and a claim about
what the machine has is checked from a process the person started.

**Then tested for real, 22:37.** The person installed 0.5.0 from Setup
themselves, turned on Start with Windows from the tray, chose Gaming, and
signed out and in: Explorer at 22:37:50, Nextcalibur in the tray at 22:38:00
with no window, plan Gaming, overlay Better performance, firmware profile
Gaming. The Run entry works; the mode survives a sign-in.

### The System page said nothing when Windows was on none of the modes

Seen 11 September after a fresh boot: Balanced plan with the Better-performance
overlay is none of Office/Gaming/Performance, so no tab was checked and nothing
explained why. Now a line under the tabs names the plan and overlay. Done.

Not on this channel: unplugging the charger produces no event here at all, which
is covered above.

### Unsupported machines get nothing to click

Set by the owner on 11 September 2026, after the graphics switch went in. This
application now writes to firmware and switches devices off. On the laptop it
was built against that is measured and safe. On a laptop it was not built
against it is a way to break somebody's machine, and the repository is public.

So there is a check, and it gates everything:

| Verdict | Meaning | What the person can do |
|---|---|---|
| Supported | the firmware mailbox is there, answers a thermal read with plausible numbers, and the mode register reads 1 or 2 | everything |
| Read-only | the mailbox is there but its answers do not look like this protocol | see readings; nothing that writes |
| Unsupported | no mailbox class at all | **nothing** - every control locked, a banner saying so, and an offer to remove the application |

The check runs at first run and at every start, because a machine does not
become supported by having the application installed on it. A verdict short of
Supported disables the lighting page, the graphics switch, and the command-line
verbs that write. Unsupported disables the power page too: those features are
Windows-generic and would work, but the rule is simpler to trust when it has no
exceptions, and it can be loosened later if there is a reason.

What the check cannot do is identify the model. SMBIOS on this machine reads
`Type1MTM` and `Type2ProjectName` - the vendor never filled it in - so nothing
here keys on a name. The mailbox and its answers are the identity.

Nothing in the check writes. A machine is judged by what it says to reads it
was always going to receive.

**Sandbox, 0.5.0 candidate, 11 September 2026:** banner, every page locked,
the uninstall offer - all as specified. One thing slipped past: the
first-run hook runs before the window exists, so the sandbox was still
offered the power-overlay repair (with its UAC prompt) and given a Run entry.
Both now check for the mailbox first and do nothing without it. Re-run in a
fresh sandbox: banner and locks only, no repair, no prompt. Passed.

### Dialogues are the application's, not Windows'

Set by the owner on 11 September 2026. Twenty message boxes across the
application, all of them the system default: the system's look, the system's
"blink" sound, and some without an owner window, so the rest of the application
stayed clickable underneath. Three things, in order of what the owner asked for:

1. **No sound.** The beep comes from the icon parameter; a box with no system
   icon makes none.
2. **Nothing else clickable while one is up.** Every box is owned by the main
   window, which makes it modal to that window. The ones raised before the
   window exists - the first-run repair, the uninstall question - are modal by
   being the only thing on screen.
3. **The application's own look.** That is markup and style, so it goes to the
   design agent - but every box now goes through one place, `Dialogs`, so the
   swap is one implementation rather than twenty edits.

What cannot be restyled: Windows' own elevation prompt. It is the operating
system's and looks the way it looks. It also no longer appears for card
switches; the one time it does is the first-run permission, once.

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

- Control Center 3.0.0.34 has a **macro key** feature the earlier version did
  not. Out of scope by the owner's decision, 11 September 2026: not a thing
  this project adds.
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

00. **Tidy the codebase, set 12 September 2026.** Two jobs before 1.0.
    First, nothing in the repository that does not belong in a product:
    leftovers from abandoned directions, old test material, one-off
    scripts that measured something once and are now history (`tools/`
    has a dozen), captures that only ever mattered for a single night.
    Second, a place for everything: images in one folder (today the icon
    and the logo masters sit beside the resources; the docs folder held a
    logo until it was moved), documents in one, tools in one with a README
    saying which are still worth running. Done as one deliberate pass with
    the audit written down, not piecemeal - and not before 0.5.1 is out.

0. **A proper installer, set 11 September 2026.** The owner wants what Inno
   Setup gives: a wizard that asks where to install, shows what it is doing,
   and looks like a product rather than a progress bar. Velopack stays for
   what it is good at - the update chain, proven today - so the shape is
   *Inno as the face, Velopack as the engine*. The pieces are already there:
   Velopack's Setup takes `--installto <DIR>` and `--silent`, so an Inno
   script can ask for the directory, then run Setup silently into it, and
   its uninstall entry can hand over to Velopack's `Update.exe --uninstall`.
   To settle before building: whether a Velopack copy outside
   `%LOCALAPPDATA%` still updates itself without elevation (it will, if the
   chosen directory is writable by the user; a Program Files choice needs an
   answer), and that the two do not leave two Add/Remove entries. Inno Setup is free
   and its licence permits this; the script goes in `installer/`.


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

3. **Prove the update chain end to end.** Done, 11 September 2026, 0.4.0 to
   0.5.0: the installed copy checked a minute after start, downloaded the
   delta (58 MB - the one-time cost of going self-contained; from here deltas
   are small), rebuilt the full package from it, announced through the tray,
   and applied on exit. Reopened as 0.5.0, uninstall entry 0.5.0 - **all of
   it inside the assistant's sandboxed view of the profile** (see "Start
   with Windows" below), so the chain works but the person's real session
   still has no 0.5.0; they install it themselves. Two notes:
   the binary's informational version names the commit at *build* time, so
   build after the release commit next time, not before; and the missing
   Start-menu and desktop shortcuts were the sandboxed view again - the
   person's own install of 0.5.0 has both.

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

### 0.5.1

Notes in [releases/0.5.1.md](releases/0.5.1.md). The one that tests the
update chain in the person's real session: 0.5.0 was installed by hand,
this one has to arrive on its own.

Sandbox, 12 September: the first build showed a taskbar entry and no
window. The "not supported - remove?" question is raised from Loaded, and
as an in-window dialogue its nested message loop ran before Windows had put
the window on screen. In-window dialogues now wait for the first frame;
before it, Windows' box. Second build: question, then window, banner, every
page locked. Passed.

### 0.5.0 is out

Published 11 September 2026 and installed here through the update chain.
Notes in [releases/0.5.0.md](releases/0.5.0.md). Since then: a switch for the
automatic check (tray: "Check for updates automatically", plus "Check for
updates now"). Off means no request at all; on means one small request a
minute after start and one every six hours - not polling.

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

**One exception, 11 September 2026, and it is the vendor's own.** Register
`0x0300` is the firmware's fan and thermal profile, written by the vendor
with every mode: Performance 0, Gaming 1, Office 2. Found by sweeping the
registers while each mode was chosen there. So `ApplyMode` writes it too -
copying exactly what the vendor does, which is the rule - and the support
check reads it as one more sign of the right machine. No curve, no manual
speed: that is still nothing the vendor offers.

### Graphics mode: reported only until 11 September 2026, switched after

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
