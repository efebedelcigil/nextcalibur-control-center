# Questions people ask

Written from the questions this project actually raised while it was being
built and tried, not from a template. Two halves: using it, and how it works.
Where a longer answer exists it is linked.

## Using it

**Will it work on my laptop?**
It was built against one machine, listed in the README under *Hardware*: a
Casper Excalibur laptop on the Tongfang JS970 platform. Any laptop whose
firmware exposes the same interface and answers it the same way is treated as
supported. The check is on the answers, not on the model name, so a related
model may well work and a different one will get a banner saying so. On an
unsupported machine nothing is clickable and nothing is changed - it offers
to open *Installed apps* so you can remove it.

**Does it need the vendor's Control Center?**
No. That is the point. It needs nothing the vendor installed except the
NVIDIA driver, which any machine with the card has. If the vendor's software
is still installed the application says so and recommends removing it, once;
if you keep it, both run, sharing the firmware interface.

**Why does it ask for administrator rights?**
Because everything it does needs them: reading the firmware, switching the
graphics card, and reading the processor's power draw through PawnIO, whose
device is administrators-only by design. The vendor's software runs the same
way. You are asked once, on the first run; after that a scheduled task starts
it with those rights and no prompt - from the Start menu, a pin, or at
sign-in.

Starting elevated without a prompt is safe only if the executable cannot be
swapped by something running as your account, which is why the wizard
installs under Program Files and why a copy in the profile is made read-only
to the account. Everything the window opens for you - the browser, the log
folder, Settings - is opened as you, not as administrator; only WinUtil is
deliberately elevated.

**What does it change on my machine?**
Three kinds of thing, all of them in the open:
- *Windows power settings*: a power plan per mode (the vendor's plans are
  reused when present), the active plan and power mode when you choose a
  mode, and a one-time repair of a fault the vendor's software leaves behind
  (a Windows power mode that holds the processor at full speed while the
  laptop is idle). The repair is explained on screen when it happens.
- *Firmware*: the thermal profile that goes with each mode - the same write
  the vendor makes - and, only when you ask for it and confirm, the graphics
  mode.
- *Its own footprint*: settings and a log under `%AppData%\Nextcalibur`, and
  two scheduled tasks (start elevated, start with Windows). The files
  themselves live under Program Files, or - for a copy installed into the
  profile by the plain installer or an earlier version - in a folder the
  application has made read-only to the account, so that the executable the
  task starts as administrator cannot be swapped by anything running as you.

**What does switching the graphics mode do to my PIN?**
Switching between Hybrid and Discrete changes what the TPM measures at
startup. After the restart Windows Hello's PIN has to be set up again, and
if BitLocker is on, it asks for its recovery key. The application says this
before it does anything and asks you to confirm. Have the recovery key to
hand before switching on a BitLocker machine.

**I chose a graphics mode and pressed "Restart now". Windows said some apps
are preventing the restart. If I cancel, what state am I in?**
Exactly the one you were in. The firmware is written only when Windows
reports that the session is really ending, so a cancelled restart writes
nothing; the Display page still shows the change as pending, and it happens
at the next restart - or not at all, if you exit the application first, which
it warns about.

**The mode changed by itself when I unplugged the charger.**
That is *Office mode on battery* in the tray menu, on by default: on battery
the machine drops to Office, and comes back to your mode when the charger is
plugged in. Turn it off in the tray menu and the mode stays where you put it.

**The CPU watts read `--`.**
The processor's power draw needs a driver to read (see below). The number
appears when PawnIO is installed and the processor is Intel; otherwise `--`
and nothing else changes. The application offers PawnIO when it is missing;
the installer's *Custom* type has it as a component.

**The GPU line says "asleep".**
In Hybrid mode the NVIDIA card sleeps when nothing is using it, and asking it
for its clock would wake it. Nextcalibur checks whether it is awake from
Windows' own records instead, and reads the clock and watts only when it is.
"Asleep" is the true state, and the cheapest one.

**How do updates work?**
The application checks GitHub's releases quietly (a minute after start,
then every six hours) and, when a newer one exists, tells you once through a
notification with an *Update now* button and lights the button in the
bottom-left corner. Nothing is downloaded until you say so; then a progress
bar, and the application restarts into the new version. Declined, it stays
quiet until the next start. *Check for updates automatically* in the tray
menu turns the checking off; *Check for updates now* asks on demand.

**What is PawnIO, and why would I install it?**
An open-source, Microsoft-signed kernel driver that runs small signed
modules; LibreHardwareMonitor and FanControl use it. Nextcalibur uses one
module, to read the processor's energy counter. It is the one thing shown in
the window that cannot be read without a driver, and PawnIO is the one
driver the project trusts enough to depend on rather than ship its own.
It is downloaded from PawnIO's own releases, its Authenticode signature and
chain verified, and installed quietly - and kept current the same way the
application is. Without it: `--`.

**Where is the log, and what is in it?**
`%AppData%\Nextcalibur\logs\nextcalibur-YYYYMMDD.log` - one file a day,
seven kept, events only: starts, the support verdict, mode changes, charger
transitions, graphics switches, updates, dependency installs, the vendor
coming and going, exits, and anything that went wrong. Never sensor
readings. *Open the log folder* is in the tray menu and the *Open log* button
is in the corner of the window. Attach the day's file when reporting a
problem.

**What does uninstalling leave behind?**
Nothing of its own: settings, logs, both scheduled tasks and the folder
protection are removed without asking. It asks about two things you might
want to keep: the power plans it created, and the power-overlay repair -
undoing the repair puts the vendor's fault back, so the default answer is to
keep it. Removing the tasks needs one administrator prompt, which Windows
shows during the uninstall.

**Can I run it alongside the vendor's software?**
Yes, though it recommends against it. Both use the same firmware interface
and neither breaks the other; the vendor's uninstaller, however, narrows the
permission on that interface and pulls Windows back to the Balanced plan.
Nextcalibur notices the vendor going and puts both right.

**Does it control the fans?**
No, deliberately. The vendor's software offers no fan control either; what
it does is write a thermal profile with each mode, and Nextcalibur writes
the same. A fan curve is only safe relative to how clean the cooling is,
which a program cannot see. See the roadmap's *Fan control stays out*.

**Does it change the screen's refresh rate?**
No. NVIDIA's and Windows' own settings do that.

## How it works

**What is the firmware interface?**
An ACPI-WMI mailbox: a method the firmware exposes through WMI, taking a
command word and arguments and answering in place. Reads are the `0xFA00`
family, writes `0xFB00`; the sub-commands the application uses - thermal
readings, the display mode, the thermal profile, the keyboard lighting - are
each documented in [PROTOCOL.md](PROTOCOL.md) with the evidence for what they
do. Nothing is sent whose meaning is not documented there.

**How was that established without the vendor's source?**
By watching the machine: read-only sweeps of the registers while each
setting in the vendor's software was changed, captures of the mailbox
traffic around a switch, and firmware variables dumped before and after
restarts. The captures are in `tools/trace/`. Interface facts are not
copyrightable; no vendor code or asset is in this repository.

**Why no kernel driver of its own?**
Because a driver is the one thing here that could damage a machine, and a
driver of the project's own would need an EV certificate, attestation
signing and a maintainer for the life of the project. Everything the window
shows except one number comes from the firmware interface, which needs no
driver. That one number - processor power - is read through PawnIO, which
already exists, is signed by Microsoft, and is maintained by someone whose
job it is.

**Why does it run elevated instead of using a service?**
A service would be a second process with the rights, always running, with an
interface between it and the window to secure. Elevating the window itself
is what the vendor did, is one process, and costs one prompt. The scheduled
task that starts it afterwards runs at the highest available level and only
on demand or at sign-in.

**How is the graphics mode switched?**
Hybrid and Discrete are one write to the firmware mailbox followed by a
restart; the firmware reads the register at boot. UMA is different: it is
the NVIDIA adapter disabled as a device, which Windows does at once with no
restart. Discrete cannot go straight to UMA - the screen is on the card UMA
switches off - so the application refuses that transition and says why.

**Why is the restart cancellable, and how?**
The application asks Windows for an ordinary restart, not a forced one.
Windows asks every program whether it may end the session; one that objects
gives you the "apps are preventing restart" screen. The firmware is written
only on the message Windows sends once the session is really ending, so
*cancel* on that screen means nothing was written.

**How does it know the card is asleep without waking it?**
From the PnP manager's record of the device's most recent power state
(`DEVPKEY_Device_PowerData`), which is Windows' own bookkeeping and can be
read without touching the device. Measured: the record follows the card's
runtime sleep within seconds, and reading it leaves the card where it was.
NVML is asked only when the record says D0.

**What is the power-overlay fault it repairs?**
The vendor's software sets Windows' *Best performance* power mode, and a
value it leaves behind makes that mode hold the processor at its full clock
even when the laptop is idle - so changing modes appears to do nothing and
the machine runs hot at rest. The repair restores the minimum processor
state for that mode and keeps a guard on it; it is done once, explained on
screen, and can be undone at uninstall.

**How much does it cost to run?**
Measured from a copy the person started: about 0.1 % of one processor core
with the window open on the System page, about 0.05 % hidden in the tray,
around 180-250 MB of private memory. Nothing polls where a notification
exists; nothing wakes a device the mode is keeping asleep. The numbers and
how they were taken are in the roadmap under *What things cost*.

**How are releases built, and how can I check one?**
By a GitHub Actions workflow from a tag, on GitHub's own runners: tests,
publish, the Velopack package with a delta against the previous release, the
Inno Setup wizard, and a build-provenance attestation. Nothing is built on
the developer's machine for a release. Any asset on a release page can be
verified:

    gh attestation verify Nextcalibur-Setup-X.Y.Z.exe --repo efebedelcigil/nextcalibur-control-center

Releases are unsigned (a certificate costs money the project does not spend),
which is why SmartScreen warns; the attestation is the substitute.
[RELEASING.md](RELEASING.md) has the details and the signing options.

**How do I read the log?**
Each line is `date time level [thread] category: message`. Look for `crash`
and `ui` (unhandled exceptions), `support` (what the machine answered at
start), `gpu` (switches and the session-end write), `update` and
`dependency`. Readings are never logged, so a quiet day is a short file.

**Where do I report a problem, or a security one?**
Problems: an issue on GitHub with the day's log attached and the machine's
model and firmware version (the *Hardware* table in the README shows the
form). Security: privately, through the repository's *Report a
vulnerability* - see [SECURITY.md](../SECURITY.md).
