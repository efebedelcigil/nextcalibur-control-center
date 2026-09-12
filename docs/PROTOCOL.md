# Hardware Protocol

Interface documentation for Tongfang **JS970**-class laptops (sold as Casper
Excalibur G870 among others), determined by observing firmware behaviour on the
machine and by inspecting publicly readable metadata of the software shipped
with it, for interoperability purposes.

Verified on: Casper Excalibur G870, i7-12650H, RTX 4050 Laptop, BIOS QQ141,
Windows 11 26xx.

---

## 1. Transport — ACPI-WMI mailbox

```
Namespace : root\wmi
Class     : RW_GMWMI
Instance  : ACPI\PNP0C14\0x1_0
```

| Property | Type | Meaning |
|---|---|---|
| `Active` | `bool` | interface available |
| `BufferBytes` | `uint8[32]` | request/response mailbox |
| `InstanceName` | `string` | ACPI device path |

`BufferBytes` is **not** a live sensor feed. It is a shared mailbox: a command is
written into it, firmware fills in the response, and the buffer retains the last
response until someone writes again. Reading it without writing returns whatever
the last command left behind — possibly minutes old, possibly all zeros.

Events arrive separately on class `GMC_WMIEvent` (`EventDetail: uint8[]`), used
for hotkeys and mode changes.

**The mailbox is administrators-only by default.** A kernel-WMI data block
is governed by a security descriptor, and Windows' default admits
administrators alone. The whole chain was watched from scratch, in a
disposable Windows that had never had either program installed:

| | Descriptor on the block | Who may use it |
|---|---|---|
| clean Windows | **none** | administrators, by Windows' default |
| vendor Control Center installed | `O:BAG:BAD:(A;;0x121fff;;;AU)` | **every authenticated user** |
| vendor Control Center removed | `O:BAG:BAD:(A;;0x121fff;;;BA)` | administrators again |

Exactly one entry appears under the security key when the vendor software is
installed - 539 values become 540 - and uninstalling does not remove it. It
rewrites it to administrators only and leaves it there. That last value is
byte-for-byte what the development laptop was found holding after its own
uninstall, which is what made every reading stop.

So: the interface is the machine's and the wide-open permission is the
vendor's doing. Nextcalibur runs elevated, as the vendor's software does, and
needs no descriptor of its own; an elevated version finds a grant an earlier,
unelevated version made to the account and takes it back. The descriptor
lives at `HKLM\SYSTEM\CurrentControlSet\Control\WMI\Security`, under the
block's GUID written **without braces and in lower case**; granting an
account `0x12001f` there lets it read and write unelevated, which
`tools/Grant-MailboxAccess.ps1` can do and undo by hand for a machine where
that is wanted - and which opens the embedded controller to everything
running as that account, which is why the application no longer does it.

The class definition is a separate matter and comes from the firmware, not from
anybody's installer: installing the vendor software in that same sandbox did
**not** create `RW_GMWMI`, and removing it from the laptop did not destroy it.
That is what lets an application tell "this machine has no such interface" from
"this account may not use it" - a class with no instances means the second.

## 2. Command structure

The 32-byte buffer maps to a packed structure:

```c
// StructLayout(Sequential, Pack = 8) — 32 bytes total
struct SmiCommand {
    uint16_t a0;    // offset  0 — command family
    uint16_t a1;    // offset  2 — subsystem
    uint32_t a2;    // offset  4 — arg / result 0
    uint32_t a3;    // offset  8 — arg / result 1
    uint32_t a4;    // offset 12 — arg / result 2
    uint32_t a5;    // offset 16 — arg / result 3
    uint32_t a6;    // offset 20 — arg / result 4
    uint32_t rev0;  // offset 24 — reserved
    uint32_t rev1;  // offset 28 — reserved
};
```

All fields little-endian.

### Command families (`a0`)

| Value | Meaning |
|---|---|
| `0xFA00` | READ |
| `0xFB00` | WRITE |

### Subsystems (`a1`)

| Value | Subsystem |
|---|---|
| `0x0100` | LED |
| `0x0200` | Thermal / fan |

### Call sequence

```
1. zero the whole 32-byte buffer
2. fill a0, a1, and any arguments
3. write the buffer to RW_GMWMI.BufferBytes
4. read RW_GMWMI.BufferBytes back — the response is in a2..a6
```

## 3. Thermal / fan — `a1 = 0x0200`

### Read (`a0 = 0xFA00`)

Response fields:

| Field | Meaning | Unit |
|---|---|---|
| `a2` | CPU temperature | °C |
| `a3` | GPU temperature | °C |
| `a4` | CPU fan speed | RPM |
| `a5` | GPU fan speed | RPM |
| `a6` | (observed 0) | — |

**Confirmed by live capture.** Passive polling of the mailbox while the stock
software was running produced, over 80 seconds:

```
a2=65 a3=58 a4=5133 a5=4418
a2=68 a3=59 a4=5133 a5=4391
a2=68 a3=60 a4=5133 a5=4400
a2=78 a3=60 a4=5109 a5=4409
a2=78 a3=61 a4=5085 a5=4400
a2=85 a3=62 a4=5109 a5=4391
a2=87 a3=64 a4=5097 a5=4373
```

Cross-checks:
- `a3` tracked NVML `temperature.gpu` within 1–2 °C throughout.
- `a4`/`a5` matched the values the stock UI displayed for CPU FAN / GPU FAN.

The stock software polls this once every **~6.1 seconds**.

One capture caught a partially-written buffer (`a2` set, `a3..a5` still zero),
confirming the clear-then-fill sequence is observable and that readers must
tolerate torn reads. **Treat an all-zero or partially-zero response as invalid
and retry.**

### Write (`a0 = 0xFB00`) — NOT YET DOCUMENTED

Fan control writes have not been observed and must not be guessed. See
§6 Safety.

## 4. LED — `a1 = 0x0100`

Fully mapped by observation on a Casper Excalibur G870. Every finding below was
verified by writing a value and looking at the keyboard.

### Write (`a0 = 0xFB00`)

```
a0 = 0xFB00
a1 = 0x0100
a2 = device
a3 = 0xEBRRGGBB
```

A single write carries three separate things, and they have different scopes:

| Part | Meaning | Scope |
|---|---|---|
| `RRGGBB` | colour | **the addressed device only** |
| `E` (high nibble of the top byte) | effect | **global** — all zones |
| `B` (low nibble of the top byte) | brightness | **global** — all zones |

That asymmetry is the thing to know when implementing: setting one zone's colour
also re-applies whatever effect and brightness that write carries to the whole
keyboard. To recolour a single zone without disturbing anything else, send the
current global effect and brightness along with the new colour.

### Devices (`a2`)

| Value | Target |
|---|---|
| 0 | broadcast to everything |
| 3 | Zone C — right of the keyboard |
| 4 | Zone B — middle |
| 5 | Zone A — left |
| 6 | broadcast to all keyboard zones |

Zone indices run **backwards** relative to the vendor UI's A/B/C labels. Indices
1 and 2 exist in the vendor's code but were never seen in live traffic; this
model has no lighting outside the three keyboard zones, so they are presumed to
address hardware other models have.

The vendor software writes zones one at a time, 25–40 ms apart, never using the
broadcast selectors — but both broadcasts do work.

### Brightness (`B`)

| Value | Result |
|---|---|
| 0 | off |
| 1 | 50% |
| 2 | 100% |
| 3+ | off (out of range) |

Three levels, matching the three stops on the vendor UI's brightness slider.
Levels 1 and 2 are visibly distinct on the hardware.

Three steps is too coarse to be a useful control, so **Nextcalibur does not use
this field for brightness**. It pins it to 2 and dims by scaling the RGB values
instead — the same technique the vendor software uses, visible in the captured
value `0x10101010`, a dark grey rather than a reduced brightness field.

Scaling was measured across the full range in ten-point steps. All eleven levels
from 0% to 100% are distinguishable by eye, and the progression reads as even,
so no gamma correction is applied. Mixing both mechanisms was rejected: the
hardware field would silently cap what the scaling could reach.

### Effects (`E`)

| Value | Result |
|---|---|
| 0 | off — overrides brightness entirely |
| 1 | static |
| 2 | blink, square wave, roughly 1 Hz |
| 3 | breathing |
| 4 | heartbeat — breathing with a double pulse at peak |
| 5 | not recognised, falls back to static |
| 6 | colour cycle |
| 7 | wave — colour travels across the keyboard |
| 8 | not recognised, falls back to static |

Effects 2, 3, 4, 6 and 7 run in firmware and keep animating with no further
writes. The vendor UI exposes only four of these (Static, Breathing, Colorful
cycle, Ambilight); blink and heartbeat are reachable through the interface but
have no button in the stock software. What its UI labels "Ambilight" is effect
7, a firmware wave — it does not sample the screen.

### Read (`a0 = 0xFA00`)

Not usable on this model. Firmware echoes `a0`, `a1` and `a2` correctly but
returns zero in `a3`–`a5` for every device index, so there is no way to read
back the current colour, effect or brightness. Software must track the state it
last wrote.

### A sweep of the registers nobody uses (read only, 11 September 2026)

Every register `0x0200`-`0x0207` and `0x0300`-`0x0302`, read once, Hybrid,
on the charger, idle:

```
0200  a2=62 a3=57 a4=4400 a5=3809 a6=2      thermal block; a6 = keyboard backlight step
0201  header 010D, a2=49                    a temperature that ignores CPU load (49 at 97 °C CPU)
0202  header 0114, a3=1 a4=1 a6=1           flags, meaning unknown
0203  a2=1 a4=1                             display mode (documented above)
0204  zeros
0205  a2=1
0206  zeros                                 the vendor reads this at startup
0207  zeros
0300  a2=1                                  the vendor writes 1 here at startup
0301  zeros
0302  zeros
```

Then the same sweep under conditions: 30 s of full CPU load (only `0x0200`
moved - `0x0201` stayed at 49, so it is not the CPU or GPU), on battery
(nothing but temperatures moved - none of the flags is the power source),
and with Fn+Space pressed: **`0x0200 a6` went 2, 0, 1** - it is the keyboard
backlight step, the value the key cycles. So the level *can* be read after
all, and `ThermalSample.BacklightLevel` now carries it on every sample.

**`0x0300` is the mode's other half.** With the vendor's software installed
again and each mode chosen in it, the register followed: Performance `0`,
Gaming `1`, Office `2` - and the fans stepped up on `0` (5390/5183 rpm
against ~5100/4400 a minute earlier at the same temperature). It is the
embedded controller's fan and thermal profile; the vendor's "1 at startup" was
it announcing the saved mode (Gaming). Written by Nextcalibur since, read back
`2`, `1`, `0` in turn, fans stepping up on `0` the same way. So a system mode
here is now the plan, the overlay and this register, as it is there.

Nothing else moved anything: lighting changes (colour, profile, off), the
Display Mode button answered No, the charger, Fn+Space, CPU load. `0x0206`
answered zero throughout; the vendor reads it once at startup and never
writes it, which reads like a capability query on a machine without the
capability. `0x0202`, `0x0205` and the temperature at `0x0201` (49 through
everything, including 97 °C on the CPU) stayed put and remain unnamed.

`0x0206` answers zero in every state and is written by nobody; a register
whose meaning is not known is not one to set on somebody else's machine.

## 5. Other interfaces (no mailbox involved)

### GPU sensors

The stock software reads GPU temperature, clocks, utilisation, memory and fan
via **NVML** (`nvml.dll`, shipped with the NVIDIA driver):

```
nvmlInit, nvmlDeviceGetCount, nvmlDeviceGetHandleByIndex,
nvmlDeviceGetName, nvmlDeviceGetTemperature, nvmlDeviceGetFanSpeed,
nvmlDeviceGetMemoryInfo, nvmlDeviceGetClockInfo,
nvmlDeviceGetUtilizationRates, nvmlShutdown
```

NVML is publicly documented by NVIDIA. Nextcalibur uses it directly.

### GPU mode ("Display Mode")

The stock UI offers Discrete / MS Hybrid / UMA. Internally:

```
GPUMMode: Hybrid = 1, Discrete = 2, UMA = 3
HSR:      OFF = 1, ON = 2
```

However the implementation does **not** switch a hardware MUX. It locates the
GPU through `Win32_PnPEntity` by PCI hardware ID and **disables the device**
through the SetupDi API (`SP_PROPCHANGE_PARAMS`). Hardware IDs come from a
bundled INI list of NVIDIA PCI IDs.

No code path was found that sends a `GPUMMode` value to firmware. **The real
display-path setting lives in BIOS.** Software can only disable the discrete GPU.

#### Corrected: the software *can* switch, through its kernel driver

An earlier version of this section said the display path was a BIOS setting that
no software could change, and that the restart prompt proved nothing. **Both
were wrong**, and the machine disproved them: pressing MS Hybrid and accepting
the restart moved the panel from the discrete card to the integrated one.

| | Before the change | After the reboot |
|---|---|---|
| Intel UHD | no display | **1920x1080, driving the panel** |
| RTX 4050 | 1920x1080, driving the panel | **no display** |

The mistake was searching the wrong layer. `SetFirmwareEnvironmentVariable` and
friends appear nowhere in the managed code, which is true and was measured — but
irrelevant, because the managed code does not talk to firmware. `DeviceIoControl`
lives in `ControlCenterC64.dll`, the native library, which reaches
`ControlCenter64.sys`, the kernel driver the vendor installs. A kernel driver
needs no such API: it can address the embedded controller or ACPI directly, and
nothing it does surfaces as a string in the managed assembly.

**A negative result only covers where you looked.**

**And the correction was wrong too.** The paragraph above says the switch goes
through `ControlCenter64.sys`. On 11 September 2026 that driver turned out never
to load on this machine: Code Integrity rejects it at every boot (event 3004,
"file hash could not be found"), Memory Integrity is enforcing, and the signing
certificate expired in July 2026. The switch watched on 10 September happened
with the driver in exactly that state. Whatever performs it, it is not the
driver - and with no driver there is no port I/O and no private IOCTL, which
leaves the ACPI-WMI mailbox this document is about. Unresolved; the next step is
to watch the mailbox during a click.

The lesson is the same one as above, one level up: `DeviceIoControl` in a
library is evidence that the library *can* talk to a driver, not that the thing
you watched happen went that way.

#### What the buttons do

| Button | Effect |
|---|---|
| MS Hybrid | switches the display path to the integrated GPU, through the driver; persists across reboot |
| Discrete | switches it back to the discrete GPU |
| UMA | disables the discrete GPU as a device, through SetupDi — refused directly from Discrete: *"Please switch to Hybrid mode first"* |

That refusal is the vendor enforcing the same safety rule this project derived
independently: in Discrete the panel is driven by the discrete card, so
disabling it takes the screen with it.

The SetupDi path is real and was decoded — `MyGPUFunc::SwitchDevice` reads
`VGA_HWID` and `VGA_Framework`, disables the first, sleeps a second, disables
the second, via `SP_PROPCHANGE_PARAMS`, `SetupDiSetClassInstallParams` and
`SetupDiChangeState`. `VGA.ini` names them: eighteen `PCI\VEN_10DE&…&SUBSYS_…152D`
adapters and `ACPI\VEN_NVDA&DEV_0820`, the Optimus ACPI companion. On the
development machine that second device does not exist, so only the adapter is
touched. But that path is UMA, not the Discrete/Hybrid switch.

#### Measured: the two buttons work by entirely different means

Watched on hardware on 10 September 2026, with the registry, the vendor's own
files, driver state, PnP state and the firmware variable store captured before
each click, after each click, and after each reboot.

**Discrete -> MS Hybrid leaves no trace in Windows at all.** All 1550 captured
values were identical before and after the click. After the reboot only four
differed, and none of them is a setting: two are NVIDIA driver counters, and
the other two are the outcome itself - which adapter reports a resolution.

The boot log settles where the change is applied:

```
21:15:28  boot  NVIDIA  drivesPanel=no   vendorProcs=0
21:15:28  boot  Intel   drivesPanel=YES  vendorProcs=0
```

Twenty-three seconds into the boot, before a single vendor process is running,
the panel is already on the integrated chip. Nothing in Windows applies this at
logon; the machine comes up that way. The setting lives in firmware.

**MS Hybrid -> UMA is not a firmware change at all.** It is the SetupDi device
disable, and the trace timestamps it:

```
21:25:09.266  NVIDIA  OK     CM_PROB_NONE
21:25:24.844  NVIDIA  Error  CM_PROB_DISABLED
```

`nvlddmkm` went from Running to Stopped as a consequence. No restart was
offered and none was needed. A dump of all 90 runtime-visible firmware
variables before and after differs in **zero** bytes, so nothing was written
there. This also means UMA is the one mode reproducible without the vendor's
driver - it is an ordinary device disable, though it still needs administrator.

The vendor's own UI does not survive it: four seconds after disabling the card,
`ControlCenter.exe` died with an access violation inside `nvml.dll`. It kept
polling the GPU it had just switched off.

```
Faulting application: ControlCenter.exe 3.0.0.17
Faulting module:      nvml.dll
Exception code:       0xc0000005
```

A caution for reading the firmware dump: the AMI `Setup` variable, which
normally holds every BIOS option, is not among the 90. Variables without
`EFI_VARIABLE_RUNTIME_ACCESS` are invisible to the operating system, so
"unchanged in the dump" means unchanged *where the OS can see* - the same trap
as the managed-assembly search above. For UMA it is conclusive, because the
device disable fully explains the result. For Discrete/Hybrid the dump can
only ever be indirect evidence.

Captured with `tools/Trace-ModeSwitch.ps1` and `tools/Dump-UefiVars.ps1`.

#### Where the Discrete/Hybrid setting shows up in firmware

Going back the other way - Hybrid to Discrete - and dumping the firmware
variables again turns up a fingerprint. One byte tracks the mode:

```
TpvSetup   Hybrid           00-03-00-00-40-E9-42-00-00-00-00
           after the click  00-03-00-00-40-E9-42-00-00-00-00     unchanged
           Discrete         00-02-00-00-40-E9-42-00-00-00-00
```

`{1C3483D5-1E7E-4450-9806-DEDE002C974B}\TpvSetup`, second byte, `0x03` in
Hybrid and `0x02` in Discrete. UMA leaves it at the Hybrid value, which is
right: UMA never touches firmware, it only switches the card off in Windows.

Alongside it, `ConOut`, `ConOutDev`, `ErrOut` and `ErrOutDev` grow from 30 to
36 bytes. Those are the EFI device paths for the firmware console, and six
bytes is exactly one PCI device-path node: the integrated GPU sits directly on
the root bus, the discrete one behind a bridge and so needs one node more. The
display path is visible even in where the firmware prints its own output.

**None of this changes when the button is pressed - only after the reboot.**
So the click is not what writes it. The driver either writes somewhere the
running OS cannot see, or leaves a request that firmware acts on at the next
boot; from outside, those two look the same. What can be said with the evidence
in hand is narrower and still useful: `TpvSetup` is a readable indicator of the
current mode, but it has not been shown to be the switch itself.

Nextcalibur does not read it either: which adapter reports a resolution
answers the same question through ordinary WMI, and the mailbox register
below answers it from the firmware.

#### Found: the switch is one mailbox write

Captured 11 September 2026 at 05:56, with the mailbox polled at ~260 reads a
second while the vendor's Discrete button was pressed and the restart accepted.
The last thing written before the machine went down:

```
05:56:12.393   a0=0xFB00  a1=0x0203  a2=2
               00 FB 03 02 02 00 00 00 00 00 00 00 00 00 00 00 ...
```

Write family, subsystem `0x02`, register `0x03`, value `2`. The machine came
back in Discrete.

Captured again on the way back, 06:15:11, Hybrid button and Yes:

```
06:15:11.701   a0=0xFB00  a1=0x0203  a2=1
```

The machine came back in Hybrid. So the register takes:

| Value | Mode | Evidence |
|---|---|---|
| `1` | Hybrid | written 06:15:11, machine came up Hybrid |
| `2` | Discrete | written 05:56:12, machine came up Discrete |

**An inference in an earlier draft of this section was wrong.** It read the
Hybrid value off the firmware variable `TpvSetup`, which holds `0x03` in Hybrid,
and predicted the write would be `3`. The write is `1`. The variable and the
mailbox use different encodings, and the only way to know a value is to watch
it written - which is why the second reset was spent.

The read side confirms the encoding independently. Pressing the button leaves
this residue in the buffer - the command falls between polls, the response
does not:

```
in Hybrid,   click:   00 00 00 00 01 00 00 00 00 00 00 00 01 00 00 00 ...
in Discrete, click:   00 00 00 00 02 00 00 00 00 00 00 00 02 00 00 00 ...
```

`a2` and `a4` carry the current mode, same numbers. So the register is
readable as well as writable - by construction `0xFA00 / 0x0203`, though that
command itself was never caught in the buffer - and reads back what was
written.

**Written by Nextcalibur, 11 September 2026, Hybrid to Discrete and back.**
Same bytes, machine came up Discrete, then Hybrid - so the switch needs
nothing from the vendor.
One thing the read side does *not* do: confirm a staged write. Read straight
after writing `2` from Hybrid, and again two seconds later, the register still
answers `1`; writing `0x0300 = 1` first (the vendor's startup traffic) changes
nothing. The read reports the mode the machine is running in, not the one it
will boot into, and `TpvSetup` stays at its old value until the reboot too.
The only verification is the next boot.

UMA is not a value here at all: it is a SetupDi device disable, as measured
earlier, and never touches firmware.

The button itself writes nothing. Twenty seconds before the write above, the
click left only the residue of a read - `00 00 00 00 01 00 00 00 ... 01` - so
the button reads the current mode and shows its dialogue, and the write happens
on Yes, immediately before the restart. Twice now a click answered with No has
left the mode unchanged, which is consistent.

**So the driver was never involved**, which is what the previous section
suspected once the driver turned out not to load. The whole switch is one
command in a family this document already describes. Nextcalibur does it:
the choice is made on the Display page, and the register is written on
`WM_ENDSESSION` - the message Windows sends only once a restart or shutdown
is really under way - so that a restart cancelled at Windows' "these apps
are preventing restart" screen leaves the firmware untouched. The restart
is asked for with `ExitWindowsEx(EWX_REBOOT)`, unforced.

#### The restart has a cost nobody mentions

Changing the mode **invalidated the Windows Hello PIN**. It had to be set up
again from scratch. That is the signature of a change to something measured at
boot: TPM-sealed credentials are bound to platform configuration registers, and
moving the display path changes what is measured.

Worth stating plainly for anyone who does this: on a machine with BitLocker
bound to the TPM, the same change can ask for a recovery key at the next boot.
**Have it to hand before switching.** The vendor software gives no such warning.

#### Out of scope: the refresh rate

`HSR` is a separate feature and not a graphics mode at all: the strings around
it are `HSR_OFF_120_OnClick`, `HSR_ON_240_OnClick` and icons named
`ic_hsr_120_mode_*` / `ic_hsr_240_mode_*`. It switches the panel between 120 Hz
and 240 Hz, and it has its own restart prompt. Out of scope by the user's
decision: NVIDIA's and Windows' own settings already do that.

### Power management

The stock software uses only the legacy power-plan API:

```
PowerGetActiveScheme, PowerSetActiveScheme,
PowerReadACValueIndex, PowerWriteACValueIndex, PowerEnumerate
```

It never calls `PowerSetActiveOverlayScheme`. This is the root cause of the
stuck-overlay bug described in the README. Relevant identifiers:

```
Max Performance overlay : ded574b5-45a0-4f42-8737-46345c09c238
SUB_PROCESSOR           : 54533251-82be-4824-96c1-47b60b740d00
PROCTHROTTLEMIN         : 893dee8e-2bef-41e0-89c6-b55d0929964c

Active overlay (registry):
  HKLM\SYSTEM\CurrentControlSet\Control\Power\User\PowerSchemes
    ActiveOverlayAcPowerScheme / ActiveOverlayDcPowerScheme

Overlay setting values:
  HKLM\SYSTEM\CurrentControlSet\Control\Power\PowerSettings
    \<subgroup>\<setting>\DefaultPowerSchemeValues\<overlay guid>
      ProvAcSettingIndex      (OEM-provisioned)
      OverrideACSettingIndex  (wins over the provisioned value)
```

Note `powercfg /getactiveoverlayscheme` and `/overlaylist` are unavailable on
recent Windows 11 builds; read the registry or call
`powrprof.dll!PowerGetEffectiveOverlayScheme`.

### Settings storage

The stock software persists to `HKLM\SOFTWARE\WOW6432Node\<vendor>\ControlCenter`:

```
FanControlStatus   0 = firmware automatic, otherwise user curve
FanControlSelect   selected curve index
UserFan1..3        8-point percentage curve, e.g. 30,30,30,30,50,60,70,100
UserFan1..3_SYS    system-fan variants
```

Nextcalibur uses its own settings location and does not read or write the
stock software's keys.

The fan entries are inert on this machine, and reading them as a feature would
be a mistake. `FanControlStatus` is 0 - firmware automatic - and all three
"profiles" hold byte-identical curves, which is a template nobody filled in
rather than three curves somebody chose. The Control Center has no fan tab: the
keys come from the base software this build was rebranded from, with the feature
left out. Firmware runs the fans.

## 6. Safety rules

The mailbox reaches the embedded controller. Rules for this project:

1. **Never send an undocumented command.** A write with wrong `a1`/`a2` can land
   on an unknown EC register.
2. **Reads before writes.** `0xFA00` is understood; `0xFB00` is written to
   three registers only: LED (`0x0100`), the display mode (`0x0203`) and the
   thermal profile (`0x0300`), each documented here with the capture that
   established it.
3. **No fan control.** The vendor's software has none; what it writes with
   each mode is the thermal profile, and that is copied exactly.
4. **Tolerate torn reads.** Reject all-zero and partially-zero responses.
5. **The mailbox is shared.** The stock software, if running, writes to the same
   buffer. Do not run both at once; detect and warn.
6. Do not load the vendor kernel driver. Nothing here needs it.

## 7. Not supported

Machines using the older Uniwill WMI GUIDs (`ABBC0F6D/6E/6F-8EA1-11D1-...`,
method `0x04`) or direct EC port I/O — as targeted by tuxedo-cc-wmi,
tongfang-control, and tongfang-fan-controller — use a **different generation** of
this interface. Their command formats do not apply here and vice versa.
