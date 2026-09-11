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
— `Nextcalibur-win-Setup.exe`, about 65 MB.

It carries the **.NET 8 desktop runtime** inside, so nothing is downloaded
during install. An earlier release tried to fetch the runtime instead and
stalled silently on a clean machine; sixty megabytes was the price of an
installer that finishes.

The installer is unsigned, so SmartScreen will warn: choose *More info* → *Run
anyway*. On first run the app repairs the power-overlay fault described above and
tells you what it changed.

Nextcalibur installs **no kernel driver**, and after setup it runs as an
ordinary user. It does need administrator twice, once each, and says so when it
asks:

- **To read the sensors and hear the keyboard's backlight key.** The firmware
  data block every reading comes from, and the event class Fn+Space reports
  on, are administrators-only until access is granted — two registry values,
  written once, in one prompt. On the same prompt it registers the scheduled
  task that lets the graphics card be switched off and on later without
  another one.
- **To make the power-overlay repair permanent.** Clearing a stuck overlay works
  unelevated; writing the guard that stops it coming back does not.

Neither is asked for again. Uninstalling removes all of it, and asks first about
the two things you might want to keep.

## What it does

| | |
|---|---|
| Power-mode overlay diagnosis and repair | working |
| Office / Gaming / Performance system modes | working |
| Windows power modes as choice cards | working |
| Temperatures, fan speeds, clock speeds | working |
| Memory and disk gauges, device names | working, read at runtime |
| Keyboard lighting — three zones, colour, six effects, brightness | working |
| Notification-area icon, start with Windows, overheat warning | working |
| Dark / light / follow-system theme | working |
| Coexistence with the vendor software | working |
| Graphics mode | **reports only — it does not switch** |
| Fan control | **deliberately not implemented** |

Idle cost on the development machine: **0.029%** CPU with the window open,
**0.002%** in the notification area.

Graphics-mode switching and fan control are both left out on purpose, and
[docs/ROADMAP.md](docs/ROADMAP.md) explains why. Not for want of looking: the
graphics modes were traced on hardware through every transition, and the answer
is that switching the display path needs an undocumented call into the vendor's
kernel driver, which this application does not ship and will not install. The
third mode, UMA, needs no driver — it is an ordinary device disable — and may yet
be worth doing. Fan control is a different kind of
no: the vendor's curves are documented here, but a curve is only safe relative
to how clean the cooling is, and that is not something a program can check.

[docs/PROTOCOL.md](docs/PROTOCOL.md) documents the hardware interface, with the
evidence behind each claim, and [docs/CLEAN-INSTALL.md](docs/CLEAN-INSTALL.md)
audits what the application needs on a machine that has never had the vendor
software on it - which is the point of replacing it rather than sitting beside it.

There is also a command line, `nextcalibur`, built alongside: `sensors`,
`watch`, `clocks`, `info`, `overlay`, and `led` for colour, effect and
brightness.

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

To produce the installer as well, you also need the Velopack CLI
(`dotnet tool install -g vpk`), then:

```
.\build.ps1
```

## Safety

This software talks to your laptop's embedded controller. Read
[docs/PROTOCOL.md](docs/PROTOCOL.md) before contributing. Rules of the project:

- Never send an EC command whose meaning is not documented.
- Read commands (`0xFA00`) before write commands (`0xFB00`).
- Fan control must always have a firmware-auto fallback.

## Legal

Licensed under the [MIT License](LICENSE).

The hardware interface described in `docs/PROTOCOL.md` was determined by
observing the behaviour of the machine's own firmware and by inspecting publicly
readable metadata of software shipped on the device, for the sole purpose of
interoperability. Interface facts are not copyrightable expression. No vendor
source code, binaries, or assets are reproduced or redistributed by this project.

`nvml.dll` is loaded from the installed NVIDIA driver at runtime and is not
redistributed.
