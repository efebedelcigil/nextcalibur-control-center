# Notice

Nextcalibur Control Center is an independent project. It is **not**
affiliated with, endorsed by, or connected to Casper Bilgisayar Sistemleri
A.Ş., Tongfang, Uniwill, NVIDIA, Intel or Microsoft. "Casper", "Excalibur",
"Tongfang", "Uniwill", "NVIDIA" and "Windows" are trademarks of their
owners and are used only nominatively, to identify hardware and software
this project interoperates with.

## What this repository does not contain

- No vendor executables, DLLs, kernel drivers, installers, power-plan files,
  configuration files, icons, images or fonts.
- No decompiled or transcribed vendor source code, and no inventory of what
  the vendor's software installs.
- No `nvml.dll`: it is loaded from the installed NVIDIA driver at runtime.
- No PawnIO driver: the application downloads it from PawnIO's own releases
  and verifies its signature before installing it.
- Nothing read off a machine: no dumps, captures, serial numbers or
  identifiers.

## Third-party components

| Component | Licence | Where |
|---|---|---|
| Velopack (installer and updater engine) | MIT | NuGet package reference; its `Update.exe` and stub are Velopack's signed binaries |
| PawnIO `IntelMSR` module | LGPL-2.1, licence alongside | `src/Nextcalibur.Core/Resources/PawnIO/`; handed to the PawnIO driver through its documented interface, not linked into this code |
| Inno Setup (the installer wizard) | Inno Setup licence | used at build time; not in the repository |
| .NET 8 desktop runtime | MIT | carried inside the published package |

## Interoperability

`docs/PROTOCOL.md` documents hardware interface facts: a WMI class name, a
structure layout, command identifiers and the values a firmware register
takes. These describe how the machine's own firmware behaves. Interface
facts are not creative expression, and documenting them for
interoperability is the basis on which open-source hardware support is
written.

All code in this repository is original work, licensed under the MIT
License.
