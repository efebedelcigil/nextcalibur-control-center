# Nextcalibur on a machine that has never had the vendor software

This project is meant to *replace* the Casper Excalibur Control Center, not to
sit beside it. So the question that matters is: format the laptop, install
Windows, install Nextcalibur, install nothing of the vendor's - does everything
still work?

This is the audit behind the answer. Every row was checked on hardware rather
than reasoned about.

## What the application actually depends on

| What it needs | Where that comes from | Needs vendor software? |
|---|---|---|
| The firmware mailbox - every temperature, fan speed and keyboard colour | `root\wmi` class `RW_GMWMI` | **no** |
| Power plans and the power-mode overlay | `powrprof.dll` and Windows' own registry | **no** |
| Which chip drives the display | WMI `Win32_VideoController` | **no** |
| GPU clock, power draw | `nvml.dll`, from the NVIDIA driver | no - degrades |
| Theme, start-with-Windows, settings | `HKCU` and `%AppData%` | **no** |
| The .NET 8 desktop runtime | installed by Setup.exe if absent | no - see below |

## The mailbox is firmware, not software

Everything this application reads from the machine goes through one WMI class,
so this is the dependency that decides the question. It resolves like this:

```
RW_GMWMI  instance  ACPI\PNP0C14\0X1_0
                    provider = WMIProv          Windows' own ACPI-WMI provider
          device    ACPI\PNP0C14\0X1
                    service  = WmiAcpi          C:\Windows\system32\drivers\wmiacpi.sys
                    inf      = wmiacpi.inf
                    provider = Microsoft
```

`PNP0C14` is the standard ACPI-WMI mapper. The class is declared by the
firmware's own ACPI tables and surfaced by an in-box Microsoft driver. Nothing
in that chain is installed by anybody's application.

The strongest evidence is accidental. On the development machine the vendor's
kernel drivers are **not installed at all**:

```
ControlCenter64    not present
ControlCenterC64   not present
```

and the mailbox works anyway - eight unbroken minutes of readings with the
vendor software closed. Whatever those drivers are for, this is not it.

## The vendor's settings are never touched

Nextcalibur reads and writes no key under `CASPER EXCALIBUR`. The only mention
of the vendor anywhere in the source is two process names, used to notice when
its software is running so the two can share the mailbox politely.

That covers the fan curves in particular. `UserFan1..3`, `FanControlStatus` and
`FanControlSelect` live under the vendor application's own key, put there by
the vendor application. They are inert - `FanControlStatus` is 0, meaning
firmware runs the fans, and all three "profiles" hold identical values, which is
a template nobody filled in. Whether they survive a format is not a question
this project has to care about: it never looks at them, and it does not write
fan speeds. See ROADMAP.md for why that is a decision rather than a gap.

## What degrades, and how

**No NVIDIA driver yet.** `nvml.dll` will not be there. Every entry point is
guarded for `DllNotFoundException`, the readings return null, and the interface
leaves them blank rather than inventing a figure. Temperatures and fan speeds
are unaffected - those come from the mailbox, not from NVIDIA.

**No .NET 8 desktop runtime.** The application is published
framework-dependent, and the installer is built with
`--framework net8.0-x64-desktop`, which is Velopack's instruction to check for
that runtime and install it first. **This path has never been watched working**,
because every machine it has been installed on already had the runtime. It is
the one row in the table that rests on documentation rather than observation.

**Not running as administrator.** This is the interesting one, because the
project has committed never to ask for elevation.

The power-overlay repair has two layers. Clearing a stuck overlay works as an
ordinary user. Writing the guard that stops the overlay pinning the processor
again does not: it lives under `HKLM\SYSTEM\CurrentControlSet\Control\Power`,
and an unelevated write is refused. Verified by trying it.

So on a fresh machine the repair does half its job. Two things follow, and both
are now built in:

- The repair **reports what it managed**, rather than reporting failure. It used
  to throw away the successful half and say it could not repair anything.
- **Performance mode is refused while the guard is missing.** That mode
  activates the very overlay that pins the processor; offering it unguarded
  would have this application create the fault it exists to repair. It explains
  itself and points at the repair, which needs administrator once.

Running Nextcalibur as administrator a single time is enough to write the guard
permanently. It never asks.

## What is still unproven

| Claim | Status |
|---|---|
| Setup installs the .NET runtime when it is missing | documented, never observed |
| A machine formatted without vendor drivers exposes the same ACPI device | firmware-declared, so it must - but not tested on such a machine |
| The updater finds, downloads and applies a release | never watched end to end |

None of these is known to be broken. They are listed because "not known to be
broken" and "seen working" are different things, and this file is about the
difference.
