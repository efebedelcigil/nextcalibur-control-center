# Notice

Nextcalibur Control Center is an independent project. It is **not** affiliated
with, endorsed by, or connected to Casper Bilgisayar Sistemleri A.S., Tongfang,
Uniwill, NVIDIA, Intel, or Microsoft.

Trademarks referenced ("Casper", "Excalibur", "Tongfang", "Uniwill", "NVIDIA",
"Windows") belong to their respective owners and are used only nominatively, to
identify hardware and software this project interoperates with.

## What this project does NOT contain

- No vendor executables, DLLs, kernel drivers (`.sys`), or installers
- No vendor power plan files (`.pow`), configuration files (`.ini`), icons,
  images, or fonts
- No decompiled or transcribed vendor source code
- No redistribution of `nvml.dll` (loaded from the installed NVIDIA driver)

## Interoperability

`docs/PROTOCOL.md` documents hardware interface facts: WMI class names, a
structure layout, and command identifiers. These describe how the machine's
firmware behaves. Such interface information is factual, not creative
expression, and documenting it for interoperability is the standard basis on
which open-source hardware drivers are written.

All code in this repository is original work.
