# Nextcalibur Control Center

An independent, open-source replacement for the Casper Excalibur Control
Center on Tongfang-built laptops: temperatures, fans, keyboard lighting,
power modes and the graphics mode, from the machine's own firmware
interface. Windows 11, English and Turkish.

> **Not affiliated with, endorsed by, or connected to Casper Bilgisayar
> Sistemleri A.Ş., Tongfang or Uniwill.** "Casper" and "Excalibur" are
> trademarks of their owners and are used here only to say which hardware
> this is for. The project ships no vendor code, driver, binary, font or
> artwork.

## Download

**[Latest release](https://github.com/efebedelcigil/nextcalibur-control-center/releases/latest)**

| File | What it is |
|---|---|
| `Nextcalibur-<version>-1-Installer.exe` | The installer: a wizard, English or Turkish, that installs under Program Files and does the rest. **This is the one to download.** |
| `Nextcalibur-<version>-2-Portable.zip` | Runs without installing, from any folder - and asks for administrator rights at every start (see *Why it runs as administrator*). |
| the rest | Read by the application's own updater; not for you. |

About 65 MB: the .NET 8 desktop runtime is carried inside, so nothing is
downloaded during install. The installer is not code-signed, so SmartScreen
warns: *More info → Run anyway*. Every release is built by a public GitHub
Actions workflow from a tag, with a build-provenance attestation; any file on
the release page can be checked with
`gh attestation verify <file> --repo efebedelcigil/nextcalibur-control-center`.

The wizard shows a notice you have to accept, asks whether to start with
Windows, refuses to install without the NVIDIA driver, and installs one copy
per machine (a second run offers to repair the first). The application then
keeps itself current: a new release is found quietly and offered once, and
installs on a yes - or, if you switch it on, without asking.

## What it does

| | |
|---|---|
| Office / Gaming / Performance modes | the Windows power plan, the power mode and the firmware's thermal profile together, as the vendor does it |
| Windows power modes as cards, within the chosen mode | |
| Office on battery, your mode back on the charger, and after a restart | |
| Repair of the power-mode fault the vendor's software leaves | a Windows power mode that holds the CPU at full speed while idle - found, fixed once, guarded |
| Temperatures, fan speeds, clocks, CPU package power | CPU power through PawnIO, when installed |
| The graphics mode - Hybrid, Discrete, UMA | switched in the firmware; a restart you can cancel; the card's clock and draw read without waking it |
| Keyboard lighting | three zones, colour, six effects, brightness, four profiles; Fn+Space respected |
| Overheat warning | a threshold per chip, typeable |
| Memory and every fixed drive | |
| A guided tour | `?` in the title bar walks every page and every control, and ends at the tray |
| A Settings page | everything the tray menu has, kept in step with it; the language; WinUtil, the laptop maker's driver page, the issues page, the privacy policy |
| Tray icon, start with Windows, self-update, dependency update, a log | |
| Dark, light or Windows' theme; the application's own dialogues | |
| Fan control | **deliberately not implemented** - see the FAQ |

The guided tour is the manual: sixty-six steps, one control at a time.

## Why it runs as administrator

Reading the firmware, switching the graphics card and reading the
processor's power all need it; the vendor's software runs the same way. You
are asked once, at the first start; after that a scheduled task starts it
with those rights and no prompt.

A task that starts a program elevated without asking is only safe if the
program's files cannot be swapped by something running as your account. So
the installer puts them under Program Files, where only administrators can
write, and accepts nowhere else; a copy that lives in your profile from an
earlier version is made Administrators' by the application at its first
elevated start. The portable copy, which can live anywhere, is prompted at
every start instead. Everything the window opens for you - the browser, the
log folder, Windows Settings - is opened with your own rights, not the
application's.

Nextcalibur installs **no kernel driver of its own**. The one reading that
needs one, the processor's power draw, comes through
[PawnIO](https://pawnio.eu) - a Microsoft-signed, open-source driver that is
yours to install; the application offers it, verifies its signature, and
keeps it current. Without it that number reads `--` and nothing else changes.

## Privacy

Nothing is collected and nothing about you is sent anywhere. The only
network requests are to GitHub, for the application's own updates and
PawnIO's, and there are none at all with automatic checks off.
[docs/PRIVACY.md](docs/PRIVACY.md) lists every request the application ever
makes.

## Uninstalling

Removes everything of its own - files, shortcuts, settings, logs, the
scheduled tasks - without asking, and asks about what you may want to keep:
the power plans it created, the power-mode repair, and each dependency
(PawnIO) separately.

## Hardware

Developed and tested on one machine. Every claim in this repository was
measured there:

| | |
|---|---|
| Laptop | Casper Excalibur G870 (Tongfang **JS970** barebone) |
| CPU / GPU | Intel Core i7-12650H / NVIDIA GeForce RTX 4050 Laptop GPU + Intel UHD Graphics |
| Firmware | AMI BIOS `QQ141` |
| Windows | Windows 11 Pro, Insider Dev channel, build 10.0.29661 |
| Interface | ACPI-WMI `RW_GMWMI` on `ACPI\PNP0C14`, through the in-box `wmiacpi.sys` |

The model name is read at runtime and never written into the source. A
laptop is treated as supported by what its firmware answers, not by its
name: one that answers like this machine gets everything, one that answers
oddly gets readings only, and one without the interface gets nothing to
click - and nothing is changed on it. Other Tongfang/Uniwill machines that
expose `RW_GMWMI` may work; machines using the older Uniwill `ABBC0F6x` WMI
GUIDs or direct EC port I/O are not supported.

## Documents

- [docs/FAQ.md](docs/FAQ.md) - the questions people ask, and the technical ones behind them
- [docs/PRIVACY.md](docs/PRIVACY.md) - the privacy policy
- [docs/PROTOCOL.md](docs/PROTOCOL.md) - the firmware interface, with the evidence for each claim
- [docs/CLEAN-INSTALL.md](docs/CLEAN-INSTALL.md) - what the application needs on a machine that never had the vendor's software
- [docs/RELEASING.md](docs/RELEASING.md) - how releases are built, verified and (one day) signed
- [docs/SECURITY.md](docs/SECURITY.md) - how to report a vulnerability
- `docs/releases/` - the notes of every release

There is also a command line, `nextcalibur`, built alongside: `sensors`,
`watch`, `clocks`, `info`, `overlay`, `gpu`, `access`, and `led`. Run it
from an elevated prompt.

## Building

Requires the .NET 8 SDK.

```
dotnet build Nextcalibur.sln -c Release
dotnet test Nextcalibur.sln -c Release
```

To produce the installers as well you need the Velopack CLI
(`dotnet tool install -g vpk`) and, for the wizard, Inno Setup 6:

```
.\build.ps1
```

`releases\` gets the Velopack package and the portable zip; `installer\output\`
gets the wizard. Releases themselves are built by the `Release` workflow from
a tag, not on a development machine.

## Rules of the project

This software talks to the laptop's embedded controller. Read
[docs/PROTOCOL.md](docs/PROTOCOL.md) before contributing.

- Never send a firmware command whose meaning is not documented.
- Read commands (`0xFA00`) before write commands (`0xFB00`).
- Nothing the vendor's software does not do to the fans.
- A machine that does not answer like the reference machine gets nothing to
  click: the check is on its answers, not its model name.
- Nothing on screen is invented: no reading, no default number where a
  measurement failed. `--` is the honest value.
- Nothing polls where a notification exists; nothing wakes a device the mode
  is keeping asleep.
- Nothing read off a machine - inventories, dumps, serial numbers - goes into
  the repository.

## Legal

Licensed under the [MIT License](LICENSE). Third-party notices are in
[docs/NOTICE.md](docs/NOTICE.md).

The hardware interface described in `docs/PROTOCOL.md` was determined by
observing the behaviour of the machine's own firmware and by inspecting
publicly readable metadata of software shipped on the device, for the sole
purpose of interoperability. Interface facts are not copyrightable
expression. No vendor source code, binaries or assets are reproduced or
redistributed by this project.

`nvml.dll` is loaded from the installed NVIDIA driver at runtime and is not
redistributed. The PawnIO driver is not redistributed either; the
application downloads it from its own releases. The one file of PawnIO's
shipped here is the `IntelMSR` module (`src/Nextcalibur.Core/Resources/PawnIO/`),
LGPL-2.1, travelling with its licence; it is handed to the driver through
its documented interface and nothing of it is linked into this code. Inno
Setup, used for the wizard, is free software under its own licence and is
not part of the repository.
