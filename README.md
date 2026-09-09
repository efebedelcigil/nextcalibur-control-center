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
— `Nextcalibur-win-Setup.exe`, about 6 MB.

It needs the **.NET 8 desktop runtime**, and installs it for you if the machine
does not already have it — which means the first install wants an internet
connection. The alternative was a seventy-megabyte download for everyone.

The installer is unsigned, so SmartScreen will warn: choose *More info* → *Run
anyway*. On first run the app repairs the power-overlay fault described above and
tells you what it changed.

It never asks for administrator. The registry keys the repair writes are
writable by a standard user, and the firmware interface needs no driver — unlike
the vendor software, which installs a kernel driver and requires elevation.

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
[docs/ROADMAP.md](docs/ROADMAP.md) explains why — briefly, a wrong guess at
either can leave a machine with no display or no cooling, and neither has been
observed well enough to guess. [docs/PROTOCOL.md](docs/PROTOCOL.md) documents
the hardware interface, with the evidence behind each claim.

There is also a command line, `nextcalibur`, built alongside: `sensors`,
`watch`, `clocks`, `info`, `overlay`, and `led` for colour, effect and
brightness.

## Hardware

Developed against:

- Casper Excalibur G870 — Tongfang **JS970** barebone
- Intel Core i7-12650H, NVIDIA RTX 4050 Laptop
- ACPI-WMI interface `RW_GMWMI` on `ACPI\PNP0C14`

Other Tongfang/Uniwill machines exposing `RW_GMWMI` may work. Machines using the
older Uniwill `ABBC0F6x` WMI GUIDs or direct EC port I/O are **not** supported —
see [docs/PROTOCOL.md](docs/PROTOCOL.md) for why.

## Building

Requires the .NET 8 SDK.

```
dotnet build Nextcalibur.sln -c Release
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
