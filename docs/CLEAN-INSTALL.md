# Nextcalibur on a machine that has never had the vendor software

This project replaces the Casper Excalibur Control Center rather than
sitting beside it, so the question that matters is: format the laptop,
install Windows, install Nextcalibur and nothing of the vendor's - does
everything work? This is the audit behind the answer. Every row was checked
on hardware or in a clean Windows Sandbox, not reasoned about.

## What the application depends on

| What it needs | Where it comes from | Needs the vendor's software? |
|---|---|---|
| The firmware mailbox - every temperature, fan speed, thermal profile, keyboard colour and the graphics mode | the `root\wmi` class `RW_GMWMI`, declared by the machine's own ACPI tables and served by the in-box `wmiacpi.sys` | no - it needs administrator rights, which the application runs with |
| Power plans and the power-mode overlay | `powrprof.dll` and Windows' registry | no |
| Which chip drives the display, whether the card is asleep or disabled | WMI's video-controller class and the PnP manager (`cfgmgr32`) | no |
| The card's clock and power draw | `nvml.dll`, from the NVIDIA driver | no - degrades to `--` |
| The processor's package power | the PawnIO driver, offered and installed on a yes | no - degrades to `--` |
| Settings, the log, the theme | `%AppData%` | no |
| The .NET 8 desktop runtime | carried inside the package | no |

Nothing the vendor installs is loaded, read or written. The only mention of
the vendor in the source is two process names, used to notice its software
running so that the two do not fight over the mailbox.

## The mailbox is firmware; reaching it is a matter of rights

The data block's GUID is declared in the DSDT (a `_WDG` block) and surfaced
by Microsoft's ACPI-WMI mapper:

```
RW_GMWMI  instance  ACPI\PNP0C14\0x1_0
          device    ACPI\PNP0C14\0X1
                    service  = WmiAcpi   C:\Windows\system32\drivers\wmiacpi.sys
                    provider = Microsoft
```

A kernel-WMI data block is governed by a security descriptor, and the
default is administrators only. Watched from scratch in a sandbox that had
never had either program:

| | Descriptor on the data block |
|---|---|
| clean Windows | none - administrators only |
| after installing the vendor's Control Center | `(A;;0x121fff;;;AU)` - every authenticated user, reads and writes |
| after uninstalling it | `(A;;0x121fff;;;BA)` - administrators only, and left behind |

So the vendor's installer opens the embedded controller to every local
account, and its uninstaller narrows the descriptor rather than removing it -
which is what stopped every reading on a laptop the vendor's software had
been removed from.

**Nextcalibur runs elevated**, as the vendor's software does, and needs no
descriptor of its own. Earlier versions, which ran as an ordinary user,
granted the account access by name; an elevated version finds that grant and
takes it back at start, because a permission that lets anything running as
the account send firmware commands is one the machine is better without.
`tools/Grant-MailboxAccess.ps1` can still write and revoke such a grant by
hand, for a machine where that is wanted.

A class with no instances is therefore a rights problem, not the wrong
laptop; the application tells the two apart and says which.

## What the vendor's uninstaller does to a running Nextcalibur

Measured 11 September 2026: it narrows the mailbox descriptor (which no
longer matters, elevated) and puts Windows back on the Balanced plan, which
drops the mode. Sometimes it removes its three power plans, sometimes not.
The application watches for the vendor's software going, once a minute, and
re-applies the chosen mode; when the vendor's plans are gone it makes plans
of its own, named after itself, and uses them until the vendor's return.

## What degrades, and how

**No NVIDIA driver.** `nvml.dll` is absent: the card's clock and draw read
`--`, temperatures and fans are unaffected (they come from the mailbox). The
driver is watched at start and once a minute: when it is removed the Display
page locks and the banner says so; when it returns, everything comes back.

**No PawnIO.** The processor's power reads `--`; the application offers the
driver, from its own releases, signature checked.

**No .NET runtime.** Nothing to do: the package carries its own. (The
framework-dependent package of the first releases relied on the installer
fetching the runtime, which was watched failing silently in a clean sandbox
on 11 September 2026 - a prompt, a bar that never moved, a Finish button
that worked - and was replaced by a self-contained one the same day.)

**An unsupported laptop.** One without the interface gets a banner, nothing
to click, and no change to the machine at all; one whose firmware answers
oddly gets readings only. The check is on the answers, never the model name.

## The vendor's settings are never touched

Nothing under `CASPER EXCALIBUR` is read or written. The fan-curve values the
vendor's application keeps there (`UserFan1..3`, `FanControlStatus`,
`FanControlSelect`) are inert - `FanControlStatus` is 0, firmware runs the
fans, and the three "profiles" are identical templates. This project has no
fan control, deliberately: the vendor has none either, and a curve is only
safe for a known state of the cooling.

## Verified

| Claim | How |
|---|---|
| The interface needs nothing the vendor installs | readings on a laptop with the vendor's software, driver and registry key all removed |
| The vendor's installer is what opened the data block to every user | the descriptor watched before, during and after in a clean sandbox |
| A self-contained package installs and runs with no .NET on the machine | clean sandbox, 11 September 2026 |
| The wizard installs under Program Files and the uninstall leaves nothing | clean sandbox, 12 September 2026 |
| The updater finds, downloads and applies a release | 0.5.2 to 0.5.3 on the machine, 12 September 2026 |
