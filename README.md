# Nextcalibur Control Center

An independent, open-source control center for Tongfang-based laptops
(Casper Excalibur G870 and relatives).

> **Not affiliated with, endorsed by, or connected to Casper Bilgisayar Sistemleri A.Ş.,
> Tongfang, or Uniwill.** "Casper" and "Excalibur" are trademarks of their respective
> owners and are referenced here only to describe hardware compatibility.
> This project ships no vendor code, drivers, binaries, fonts, or artwork.

---

## Why

The stock Excalibur Control Center has a design gap that hurts every user on
Windows 11:

Windows 11 layers power configuration in two levels — the **power plan**
(Balanced, Office, Gaming…) and the **power mode overlay** on top of it.
The overlay wins.

The stock software only ever calls the legacy plan API:

```
PowerGetActiveScheme / PowerSetActiveScheme
```

It never calls `PowerSetActiveOverlayScheme`. So when the AC overlay is stuck on
**Max Performance** — which carries `PROCTHROTTLEMIN = 100%` — the CPU is pinned
at maximum frequency at idle, and *nothing you pick in the Control Center changes
it*. Its "Office mode" only swaps the plan underneath an overlay that overrides it.

Measured on a Casper Excalibur G870 (i7-12650H, base 2300 MHz):

| | Stuck overlay | After fix |
|---|---|---|
| Idle floor | 123% → **2829 MHz** | 54% → **1242 MHz** |
| Peak | 180% → 4148 MHz | normal boost |

Nextcalibur detects and repairs this at install time, and keeps it from coming back.

## Download

**[Download the latest release](https://github.com/efebedelcigil/nextcalibur-control-center/releases/latest)**
— `Nextcalibur-Setup-<version>.exe`, about 65 MB: a wizard that installs
under Program Files by default and does the rest. (`Nextcalibur-win-Setup.exe`,
beside it, is the plain installer the wizard drives; it installs to
`%LOCALAPPDATA%\Nextcalibur` without asking, and the application then puts
that folder out of the account's reach at its first start - see below.)

It carries the **.NET 8 desktop runtime** inside, so nothing is downloaded
during install. An earlier release tried to fetch the runtime instead and
stalled silently on a clean machine; sixty megabytes was the price of an
installer that finishes.

The installer is unsigned, so SmartScreen will warn: choose *More info* → *Run
anyway*. The wizard is in English and Turkish, shows a notice you have to
accept, and asks whether to start with Windows. One copy per machine: a
second install is refused until the first is removed.

**It runs as administrator**, the way the vendor's software does. That is what
reading the firmware, switching the graphics card, and reading the
processor's power all need. You are asked once, on the first run; after that
a scheduled task starts it with those rights and no prompt - from the Start
menu, a pin, or at sign-in. On first run it also repairs the power-overlay
fault described above and tells you what it changed.

Because a scheduled task starts it elevated without asking, the files it
runs from must be somewhere only administrators can write - otherwise
anything running as your account could swap the executable and be run as
administrator at the next start. The wizard installs under Program Files for
that reason. A copy that lives in `%LOCALAPPDATA%` (the plain installer, or
an earlier version updated in place) is protected by the application itself:
at its first elevated start it takes ownership of that folder for
Administrators and leaves the account read-only on it; the uninstall hands
it back.

Nextcalibur installs **no kernel driver of its own**. One reading - the
processor's power draw - is only possible through a driver, and for that it
uses [PawnIO](https://pawnio.eu), a Microsoft-signed, open-source driver
that is the person's to install. Without it the number reads `--` and
nothing else changes. When PawnIO is missing or behind, the application
offers it: downloaded from its own releases, signature checked, installed
quietly, on a yes.

Updates work the same way: found quietly, offered once through a
notification with an *Update now* button, downloaded with a progress bar,
and the application restarts into the new version. Both can be turned off
in the tray menu.

Uninstalling removes everything it put on the machine - settings, logs,
the scheduled tasks, its permissions - and asks first about the two things
you might want to keep: the power plans and the power-overlay repair.

## What it does

| | |
|---|---|
| Power-mode overlay diagnosis and repair | working |
| Office / Gaming / Performance system modes | working |
| Windows power modes as choice cards | working |
| Temperatures, fan speeds, clock speeds | working |
| Memory and disk gauges, device names | working, read at runtime |
| Keyboard lighting — three zones, colour, six effects, brightness | working |
| Graphics mode — Hybrid, Discrete, UMA — switched from the firmware | working; a restart you can cancel |
| The card's clock and draw, without waking it when it sleeps | working |
| CPU package power, through PawnIO | working when PawnIO is installed |
| Office mode on battery, your mode back on the charger and after a restart | working |
| Overheat warning with a threshold per chip | working |
| Notification-area icon, start with Windows, self-update, dependency update | working |
| Dark / light / follow-system theme, its own dialogues | working |
| Coexistence with the vendor software, and surviving its removal | working |
| A log of its own | `%AppData%\Nextcalibur\logs` |
| Fan control | **deliberately not implemented** |

Fan control is left out on purpose, and [docs/ROADMAP.md](docs/ROADMAP.md)
explains why: the vendor offers none, and a curve is only safe relative to how
clean the cooling is, which a program cannot check. What the vendor does do
with the fans - a thermal profile written with each mode - is copied exactly.

The graphics switch was traced on hardware through every transition: Hybrid
and Discrete are one write to the firmware mailbox and a restart, UMA is the
card switched off as a device. Nothing is written until Windows says the
session is really ending, so cancelling the restart leaves the machine as it
was. Switching changes what the TPM measures at startup: the Windows PIN has
to be set up again, and BitLocker, if on, will ask for its recovery key. The
application says so before it does anything.

[docs/FAQ.md](docs/FAQ.md) answers the questions people ask - what it needs,
what it changes, the PIN and BitLocker warning, updates, PawnIO, uninstall -
and the technical ones behind them.
[docs/PROTOCOL.md](docs/PROTOCOL.md) documents the hardware interface, with the
evidence behind each claim, and [docs/CLEAN-INSTALL.md](docs/CLEAN-INSTALL.md)
audits what the application needs on a machine that has never had the vendor
software on it - which is the point of replacing it rather than sitting beside it.

There is also a command line, `nextcalibur`, built alongside: `sensors`,
`watch`, `clocks`, `info`, `overlay`, `gpu`, `access`, and `led` for colour,
effect and brightness.

## Hardware

Developed and tested on exactly one machine. Every claim in this repository
was measured there, and nowhere else:

| | |
|---|---|
| Laptop | Casper Excalibur G870 (vendor package `G870.12XX`) — Tongfang **JS970** barebone |
| CPU | 12th Gen Intel Core i7-12650H |
| GPU | NVIDIA GeForce RTX 4050 Laptop GPU + Intel UHD Graphics (hybrid) |
| Firmware | AMI BIOS `QQ141`, 27 June 2024; SMBIOS left unfilled by the vendor (`Type1MTM` / `Type2ProjectName`) |
| Windows | Windows 11 Pro, Insider Dev channel, build 10.0.29661 |
| Interface | ACPI-WMI `RW_GMWMI` on `ACPI\PNP0C14`, through the in-box `wmiacpi.sys` |

The model name is read at runtime, never written into the source; the table
above is documentation of where the testing happened, so you can judge how far
your machine is from it.

Other Tongfang/Uniwill machines exposing `RW_GMWMI` may work. Machines using the
older Uniwill `ABBC0F6x` WMI GUIDs or direct EC port I/O are **not** supported —
see [docs/PROTOCOL.md](docs/PROTOCOL.md) for why.

## Building

Requires the .NET 8 SDK.

```
dotnet build Nextcalibur.sln -c Release
dotnet test Nextcalibur.sln -c Release
```

To produce the installers as well, you also need the Velopack CLI
(`dotnet tool install -g vpk`) and, for the wizard, Inno Setup 6; then:

```
.\build.ps1
```

`releases\` gets the Velopack package and its plain Setup; `installer\output\`
gets the wizard. Without Inno Setup the plain Setup is still built.

## Safety

This software talks to your laptop's embedded controller. Read
[docs/PROTOCOL.md](docs/PROTOCOL.md) before contributing. Rules of the project:

- Never send an EC command whose meaning is not documented.
- Read commands (`0xFA00`) before write commands (`0xFB00`).
- Nothing the vendor's software does not do to the fans.
- A machine that does not answer like the reference machine gets nothing
  to click: the check is on its answers, not its model name.
- Nothing on screen is invented: no reading, no default number where a
  measurement failed. `--` is the honest value.

## Legal

Licensed under the [MIT License](LICENSE).

The hardware interface described in `docs/PROTOCOL.md` was determined by
observing the behaviour of the machine's own firmware and by inspecting publicly
readable metadata of software shipped on the device, for the sole purpose of
interoperability. Interface facts are not copyrightable expression. No vendor
source code, binaries, or assets are reproduced or redistributed by this project.

`nvml.dll` is loaded from the installed NVIDIA driver at runtime and is not
redistributed.

The PawnIO driver is not redistributed either; the application offers to
download it from its own releases. The one file of PawnIO's shipped here is
the `IntelMSR` module (`src/Nextcalibur.Core/Resources/PawnIO/`), which is
LGPL-2.1 and travels with its licence; it is loaded into the driver through
its documented device interface and nothing of it is linked into this code.

Inno Setup, used for the installer wizard, is free software under its own
licence and is not part of the repository.
