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

**Reading the mailbox requires no elevation.** Writing it does require the
process to be able to write the WMI instance.

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

Consequence worth knowing: on a machine left in Discrete mode, the panel is
driven by the dGPU, the iGPU drives nothing, and the dGPU never reaches its low
power states — measured 2220 MHz / 17 W at 1–6% utilisation.

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

## 6. Safety rules

The mailbox reaches the embedded controller. Rules for this project:

1. **Never send an undocumented command.** A write with wrong `a1`/`a2` can land
   on an unknown EC register.
2. **Reads before writes.** `0xFA00` is understood; `0xFB00` is only understood
   for LED.
3. **Fan control must keep a firmware-auto fallback** and restore it on exit and
   on crash.
4. **Tolerate torn reads.** Reject all-zero and partially-zero responses.
5. **The mailbox is shared.** The stock software, if running, writes to the same
   buffer. Do not run both at once; detect and warn.
6. Do not load the vendor kernel driver. Nothing here needs it.

## 7. Not supported

Machines using the older Uniwill WMI GUIDs (`ABBC0F6D/6E/6F-8EA1-11D1-...`,
method `0x04`) or direct EC port I/O — as targeted by tuxedo-cc-wmi,
tongfang-control, and tongfang-fan-controller — use a **different generation** of
this interface. Their command formats do not apply here and vice versa.
