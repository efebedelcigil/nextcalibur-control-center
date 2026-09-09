# LED test plan

A structured way to determine the LED protocol on a JS970-class machine by
observation, one command at a time.

## Why this is needed

The write command's *shape* is known with certainty — it appears at three call
sites in the vendor software and is identical at each:

```
a0 = 0xFB00   (write)
a1 = 0x0100   (LED)
a2 = device selector
a3 = packed 32-bit value
```

LED **reads** were tried first and are useless here: firmware echoes `a0`, `a1`
and `a2` correctly but returns zero in `a3`–`a5` for every device index. There is
no current value to read back and preserve, so values have to be written
outright.

### What passive capture established

Watching the mailbox while the vendor software drove the lighting caught its
writes directly:

```
a0=0xFB00 a1=0x0100 a2=5 a3=0x11F2FFF7
a0=0xFB00 a1=0x0100 a2=4 a3=0x11F2FFF7
a0=0xFB00 a1=0x0100 a2=3 a3=0x11F2FFF7

a0=0xFB00 a1=0x0100 a2=5 a3=0x10101010
a0=0xFB00 a1=0x0100 a2=4 a3=0x10101010
a0=0xFB00 a1=0x0100 a2=3 a3=0x10101010
```

Confirmed by this:

- Lighting really does go through the mailbox on this model.
- **The keyboard zones are device indices 3, 4 and 5.** The software writes all
  three in a burst roughly 25–40 ms apart. Indices 0 and 6 appear in the vendor's
  code as broadcast selectors but were never used in practice.
- The packed value is `0xMMRRGGBB`. `F2FFF7` matches the RGB stored in the
  vendor's `HighPerformance` profile exactly.

Still **not** known, and not to be guessed:

- what the high byte means — only `0x10` and `0x11` were observed live, with
  `0x12`, `0x60` and `0x61` appearing in stored profiles
- which of 3, 4 and 5 is Zone A, B and C
- whether indices 1 and 2 address the light bar

## Safety

- These commands only ever address `a1 = 0x0100`. The thermal subsystem is never
  touched, so nothing here can affect fan behaviour.
- Every value in Phase 1 and 2 is taken from the vendor software's own saved
  profiles, so the firmware already accepts them.
- Lighting is recoverable: reopen the vendor Control Center and pick a profile,
  or reboot.

## Before starting

1. **Close the vendor Control Center.** It writes to the same mailbox and will
   race with these tests.
2. In the vendor software beforehand, set brightness to a visible level so the
   lights are actually on.
3. Record the result of each step before moving on.

Each command is a single line:

```powershell
.\tools\Test-Led.ps1 -Device <n> -Value 0x<MMRRGGBB>
```

---

## Phase 1 — does a write do anything at all?

The value `0x11FF0000` is lifted verbatim from the vendor's `PowerSaving`
profile, where it is one of five entries.

Start with the exact value captured from the vendor software, so the very first
write is one the firmware demonstrably just accepted.

| # | Command | Expected |
|---|---|---|
| 1.1 | `-Device 3 -Value 0x11F2FFF7` | one keyboard zone turns near-white |
| 1.2 | `-Device 4 -Value 0x11F2FFF7` | a second zone matches it |
| 1.3 | `-Device 5 -Value 0x11F2FFF7` | the third zone matches — whole keyboard white |
| 1.4 | `-Device 3 -Value 0x11FF0000` | that zone turns pure red |

1.1–1.3 replay captured traffic verbatim. 1.4 is the first genuinely new value:
same mode byte, different colour.

**If 1.1 does nothing**, stop — something about replaying the write differs from
how the vendor software issues it, and that needs investigating before going on.

## Phase 2 — device mapping

The vendor UI exposes three keyboard zones (A, B, C) plus a light bar. Saved
profiles carry five packed values, so there are believed to be five addressable
devices. Establish which index is which by lighting one at a time.

Set everything to a dim base first, then light a single device bright:

Light one device at a time in a distinct colour, leaving the others white from
Phase 1, so the mapping is unambiguous.

| # | Command | Record |
|---|---|---|
| 2.1 | `-Device 3 -Value 0x11FF0000` | which physical area turned red? |
| 2.2 | `-Device 4 -Value 0x1100FF00` | which area turned green? |
| 2.3 | `-Device 5 -Value 0x110000FF` | which area turned blue? |

That settles Zone A/B/C. Then probe the indices never seen in live traffic —
these are new territory, so note carefully whether anything at all responds:

| # | Command | Record |
|---|---|---|
| 2.4 | `-Device 1 -Value 0x11FF00FF` | anything? light bar? |
| 2.5 | `-Device 2 -Value 0x11FFFF00` | anything? |
| 2.6 | `-Device 6 -Value 0x1100FFFF` | all three zones at once? |
| 2.7 | `-Device 0 -Value 0x11FFFFFF` | everything including light bar? |

## Phase 3 — the mode byte

Fix the colour at red and vary only the high byte. Values seen in vendor
profiles are `0x10`, `0x11`, `0x12`, `0x60`, `0x61`.

| # | Command | Record |
|---|---|---|
| 3.1 | `-Device 3 -Value 0x10FF0000` | brightness? effect? |
| 3.2 | `-Device 3 -Value 0x11FF0000` | compare with 3.1 |
| 3.3 | `-Device 3 -Value 0x12FF0000` | compare |
| 3.4 | `-Device 3 -Value 0x13FF0000` | does it go further? |
| 3.5 | `-Device 3 -Value 0x60FF0000` | different effect? |
| 3.6 | `-Device 3 -Value 0x61FF0000` | compare with 3.5 |

Hypothesis to confirm or reject: the **low nibble** is a brightness step
(`0x10` → `0x11` → `0x12` getting brighter) and the **high nibble** selects an
effect family (`1` = static, `6` = something animated). The vendor's `Game`
profile uses `6`, all others use `1`, which is what suggests this.

## Phase 4 — off and effects

| # | Command | Record |
|---|---|---|
| 4.1 | `-Device 3 -Value 0x10101010` | the other captured value — dim? |
| 4.2 | `-Device 3 -Value 0x00000000` | do the lights go off? |
| 4.3 | `-Device 3 -Value 0x11000000` | off, or black-but-on? |
| 4.4 | `-Device 3 -Value 0x20FF0000` | unknown high nibble — any effect? |

The vendor UI offers Static, Breathing, Colorful cycle and Ambilight. Only two
high nibbles appear in saved profiles, so the remaining effects are likely
driven by the software rather than firmware — Ambilight in particular almost
certainly samples the screen and streams colours, which would explain why it is
not a stored value.

## Recording results

For each step record: command, what physically changed, and whether the
response buffer differed from all zeros. Findings go into `docs/PROTOCOL.md` §4,
with confirmed facts kept separate from hypotheses.
