# Nextcalibur on a machine that has never had the vendor software

This project is meant to *replace* the Casper Excalibur Control Center, not to
sit beside it. So the question that matters is: format the laptop, install
Windows, install Nextcalibur, install nothing of the vendor's - does everything
still work?

This is the audit behind the answer. Every row was checked on hardware rather
than reasoned about.

> **12 September 2026:** the application now runs elevated (one prompt on
> the first run, a scheduled task after that), so the permission grant this
> document describes is no longer what makes the readings possible - being
> an administrator is. The mechanism is still documented below because the
> grant is real, older installs have it, and the uninstall takes it back.
> Two more things a clean machine gets offered: the PawnIO driver (for CPU
> power; optional) and, from the wizard, a choice about starting with
> Windows.

## What the application actually depends on

| What it needs | Where that comes from | Needs vendor software? |
|---|---|---|
| The firmware mailbox - every temperature, fan speed and keyboard colour | `root\wmi` class `RW_GMWMI`, declared by the machine's ACPI tables | no driver - but **access needs granting once**, see below |
| Power plans and the power-mode overlay | `powrprof.dll` and Windows' own registry | **no** |
| Which chip drives the display | WMI `Win32_VideoController` | **no** |
| GPU clock, power draw | `nvml.dll`, from the NVIDIA driver | no - degrades |
| Theme, start-with-Windows, settings | `HKCU` and `%AppData%` | **no** |
| The .NET 8 desktop runtime | installed by Setup.exe if absent | no - see below |

## The mailbox is firmware - but reaching it is not free

**This section previously claimed the mailbox needs nothing the vendor
installs. That was tested on 10 September 2026 by uninstalling the vendor
software, and it is wrong.** What follows is what the test actually showed.

The firmware half holds up. The data block's GUID is declared by the machine's
own ACPI tables - found in the DSDT at offset 508487, in a `_WDG` block - and
surfaced through the standard mapper:

```
RW_GMWMI  instance  ACPI\PNP0C14\0x1_0
          device    ACPI\PNP0C14\0X1
                    service  = WmiAcpi        C:\Windows\system32\drivers\wmiacpi.sys
                    inf      = wmiacpi.inf
                    provider = Microsoft
```

No vendor driver is involved - though the first evidence given for that here was
worthless. It said `ControlCenter64` and `ControlCenterC64` were not installed as
services, which is true and proves nothing: the service the vendor registers is
called **`ControlCenter`**, and it points at `ControlCenter64.sys`. Querying two
names that never existed and reading the empty answer as absence is the same
mistake as searching the wrong assembly for a firmware call.

What actually settles it: on a laptop with no vendor software, no vendor driver
and no vendor registry key, granting the account access to the data block
brought every reading back. Nothing was loaded to make that work.

**Access is the problem.** With the vendor software uninstalled:

| | Instance visible? | `nextcalibur sensors` |
|---|---|---|
| ordinary user | **no** | fails after 8 attempts |
| administrator | yes - `ACPI\PNP0C14\0x1_0` | - |

Before the uninstall the same query worked without elevation. So something the
vendor installed was granting ordinary users access to that data block, and
removing the software took it away. A WMI data block's access is governed by a
security descriptor, and the default for a kernel-WMI GUID is administrators
only - which fits exactly. **That last step is inference, not measurement:**
the security descriptor itself has not been read yet.

### Watched from scratch, in a machine that had never had either program

The account above was reconstructed after the fact, from a laptop where the
vendor software had been installed for a year. Running the same thing forwards
in a disposable Windows removes the guesswork:

| | Descriptor on the data block |
|---|---|
| clean Windows, nothing installed | **none** - so administrators only |
| after installing the vendor Control Center | `O:BAG:BAD:(A;;0x121fff;;;AU)` - every authenticated user |
| after uninstalling it | `O:BAG:BAD:(A;;0x121fff;;;BA)` - administrators only, and left behind |

One value appears under the security key on install (539 become 540) and
uninstalling narrows rather than removes it. The final line is byte-for-byte
what the laptop was found holding, which is what stopped every reading.

The uninstall is otherwise thorough: files, the `ControlCenter` driver service,
the scheduled task, the registry values and even the three power plans all go.
On the laptop it left an empty `CASPER EXCALIBUR` key and nothing else.

Worth noting what the install does **not** do: it does not create the `RW_GMWMI`
class. That was absent before and after in the sandbox, and survived the
uninstall on the laptop, which is how we know the class comes from the firmware
and why "a class with no instances" is a safe reading of "permission missing".

### Settled: elevation once, ordinary use thereafter

The block's security descriptor granted `BA` alone. Adding the current account
to it - one registry value - brings the readings back for an ordinary user, and
this was verified end to end with the vendor software uninstalled:

```
before   O:BAG:BAD:(A;;0x121fff;;;BA)
         ordinary user: no instance, sensors fail

after    O:BAG:BAD:(A;;0x12001f;;;BA)(A;;0x12001f;;;SY)(A;;0x12001f;;;<the user>)
         ordinary user: CPU 55 C fan 3829 rpm, GPU 51 C fan 3472 rpm
```

`tools/Grant-MailboxAccess.ps1` writes it, `-Revoke` restores exactly what was
there, and `-Show` reports what the current account can actually do.

Two details are deliberate. The grant goes to **one account** rather than to
`BU`, which is what the vendor's does: the block accepts writes as well as reads,
so opening it to every local account hands every local process a route to the
embedded controller. And the value name carries **no braces and is lower case**,
matching every entry already under that key - written any other way it is never
consulted, which is a failure that looks exactly like success.

There was no "never ask for administrator" rule to break, either. That was
written here as though the owner had set it; he had not.

### What the application does about it now

Not a footnote for the reader to act on - the application handles it.

At startup it asks the question the old code never asked: is this a machine
without the interface, or a machine this account may not use? The class
definition comes from the binary MOF the firmware itself carries, so it is
present wherever the interface is; the instances come through the security
descriptor. **A class with no instances is a permission problem, not the wrong
laptop.**

- **Missing permission** - one dialogue explaining what is needed and why, then
  Windows' own prompt. Granting relaunches the same executable with
  `--grant-sensor-access`, elevated, for one registry write. Shipping a script
  would mean depending on the execution policy of a machine we have just
  established we know nothing about.
- **Declined** - a banner saying permission is missing and that reopening will
  ask again. Not "this laptop isn't supported", which is what it used to say and
  reads as final.
- **Genuinely unsupported** - that message, correctly, and the power pages keep
  working.

Asked only while access is missing. Once granted it never comes back, which is
the difference between this and the vendor software prompting at every start.

From the command line, `nextcalibur access` reports, `--grant` and `--revoke`
change it. Run elevated it says so, because an elevated prompt reads the block
whatever the descriptor says.


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
fan speeds. That is a decision rather than a gap: the vendor has no fan
control either, and a curve is only safe for a known state of the cooling.

## What degrades, and how

**No NVIDIA driver yet.** `nvml.dll` will not be there. Every entry point is
guarded for `DllNotFoundException`, the readings return null, and the interface
leaves them blank rather than inventing a figure. Temperatures and fan speeds
are unaffected - those come from the mailbox, not from NVIDIA.

**No .NET 8 desktop runtime.** The package carries its own, and that is not a
preference. Publishing framework-dependent rested on the installer fetching the
runtime when a machine lacked one - documented behaviour, never watched. Watched
on 11 September 2026 in a clean Windows sandbox: the prompt appeared, the
progress bar never moved, five minutes passed, and the Finish button was
clickable throughout. The installer reported success, installed nothing, and
would have left somebody hunting for an application that was not there.

Not the network: the same runtime downloaded directly from that same sandbox at
4.13 MB/s, 56 MB in fourteen seconds.

So the step was removed rather than repaired. Setup went from 7 MB to 65 MB and
needs nothing from the internet. Updates are unaffected - a 0.5.0 to 0.5.1 delta
is 0.1 MB, because the runtime files do not change between versions - and that
number is measured, not assumed.

Verified end to end afterwards, in a sandbox with no .NET at all: no prompt, the
application installed to %LocalAppData%\Nextcalibur, the window opened, and the
event log recorded nothing. The banner said "This laptop isn't supported", which
in a virtual machine is the correct answer and the one the permission logic has
to get right.

**Not running as administrator.** This is the interesting one, because the
project asks for elevation only where it must, and says why.

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
permanently, and the repair says so when it cannot finish.

## What is still unproven

| Claim | Status |
|---|---|
| ~~Setup installs the .NET runtime when it is missing~~ | tested: it does not, so the package now carries its own |
| ~~The vendor's installer is what granted ordinary users access to the data block~~ | **proven** in a sandbox that had never had it: no descriptor before, `AU` after installing, `BA` after removing |
| The updater finds, downloads and applies a release | never watched end to end |

One of them turned out to be broken, which is the point of the list. "Documented"
and "seen working" are different things, and the runtime bootstrap sat here as
documented-but-unwatched until somebody insisted on a machine that had never had
any of this installed. It failed silently, and told the user it had succeeded.

The remaining rows are not known to be broken. That is not the same as working.
