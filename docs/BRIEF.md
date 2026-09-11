# Current brief

Read [ROADMAP.md](ROADMAP.md) for where the project stands and
[CONTRACT.md](CONTRACT.md) for the names your markup must provide.

Four jobs.

---

## 1. An icon audit

Confirm an icon is present, correct and current everywhere one belongs, on a
**running installed copy** rather than in the markup:

window · taskbar button · Alt-Tab · Task Manager · notification area ·
notification balloons · the installer itself · the desktop shortcut · the
Start-menu shortcut · Add or Remove Programs · the title bar

Report anything missing, stale or blurred, with where you saw it. The title-bar
mark is vector; everything else comes from `Assets/app.ico`, which has frames at
16, 20, 24, 32, 48, 64, 128 and 256 px.

## 2. A switch for the overheat notification

The owner wants the temperature warning turned on and off from the window
itself, not only from the notification-area menu.

The behaviour already exists and is settled; what is missing is the control.
Two settings back it, and both are already there:

| Setting | Meaning |
|---|---|
| `OverheatWarningEnabled` | whether it warns at all |
| `CpuWarningTemperatureC` | how hot is too hot; default 90 |

Bind the switch to `OverheatWarningEnabled` and call `Save()` on the settings
object the window already holds. **Do not write a zero into
`CpuWarningTemperatureC` to mean "off"** — that used to be how it worked, and
the whole point of the change was to stop having two ways to say the same
thing. The threshold has to survive being switched off and on again.

The tray menu offers the same choice and writes the same two settings, so
whatever you add has to read its state from the settings rather than keep its
own, or the two will disagree the moment somebody uses the menu.

Where it goes is your call. It belongs near the temperature readings rather
than buried in a settings page, because it is about them.

---

## 3. The Display page now switches, and needs to show it

Until now the three mode cards were a report. As of 11 September they act:
Hybrid and Discrete write the firmware and take effect at the next restart; UMA
switches the card off at once, and Hybrid from UMA switches it back on. The
code-behind handles the dialogues, the elevation prompt, and a ten-second lock
on the cards after a switch. What the markup does not yet show:

| State | What the page should make visible |
|---|---|
| A firmware switch is pending a restart | the card that is *selected* is not the card the machine is *in*, and it will be after a restart. Today the selection snaps back to the current mode, which reads as "it didn't take". |
| The cards are locked after a switch | ten seconds of disabled cards with no explanation. A short line - "settling" - is enough. |
| UMA is not available from Discrete | the card is driving the panel; the rule is explained in a dialogue when clicked, but the card could say so before. |
| Discrete is not available from UMA | same, the other way. |

The code-behind exposes what it knows: `GpuModeService.Detect()` for the current
mode, `GpuModeService.ReadFirmwareMode(mailbox)` for the stored one, and the
two differ exactly when a restart is pending. Nothing here needs new behaviour;
it needs the existing behaviour to be legible.

## 4. The markup's placeholder numbers

`RamGauge`, `RamPercent`, `RamDetail`, `SsdGauge`, `SsdPercent`, `SsdDetail`,
`CpuFan` and `GpuFan` start with real-looking numbers - 46.8%, 733 of 1396 GB,
0 rpm. On a machine where readings never arrive those stayed on screen and read
as readings. The code-behind now blanks them before anything loads, but the
markup should not carry them at all: use `--` the way `CpuTemp` and `GpuTemp`
already do. Nothing on screen is invented, including at design time.

## What the last round settled, so it does not get undone

These are right. Do not disturb them while fixing the hover.

- **Zone colour is never grey.** Each zone's key outlines are bound to that
  zone's own live colour, published by the code-behind on `PreviewA`, `PreviewB`
  and `PreviewC`. Selection is opacity and elevation only: the selected zone at
  full strength with a small lift, the others at about 40%. Checked on screen —
  an unselected zone reads as its own colour, and the lift is visible against
  the neighbouring zone.
- **No backing slab.** `PreviewA/B/C` exist as collapsed borders purely to carry
  the colour to the bindings. Nothing paints a rectangle behind the keys.
- **Whole keycaps.** All three zones share one coordinate system, so no key is
  split at a boundary and the spacebar sits entirely in the middle zone.
- **The title-bar mark is vector**, not a bitmap, so it stays sharp at any size.
  `Assets/app.ico` is still what the taskbar, Alt-Tab and the installer use,
  where Windows picks a real frame.

## One behaviour changed in the code-behind

Choosing a zone tab now clears **Select all**. With Select all on, every zone is
drawn at full strength and lifted, so pressing Zone A, B or C left the keyboard
pixel for pixel identical — the tabs looked broken. That fix is in
`MainWindow.xaml.cs`; markup should not try to reproduce it.

## Measuring, if you change anything

Close the running application first — it locks its own build output. Build
Release, then take **one ten-minute sample**, not three short ones:

```powershell
$exe = 'src\Nextcalibur.App\bin\Release\net8.0-windows\win-x64\Nextcalibur.exe'
$p = Start-Process $exe -PassThru
Start-Sleep -Seconds 20
$t = $p.TotalProcessorTime
Start-Sleep -Seconds 600
$p.Refresh()
'CPU {0:N3}%  RAM {1} MB  handles {2}' -f `
    (($p.TotalProcessorTime - $t).TotalMilliseconds / 600000 * 100 / [Environment]::ProcessorCount),
    [math]::Round($p.WorkingSet64 / 1MB), $p.HandleCount
```

The figures to beat: **0.033% on the Lighting page**, 0.002% in the tray.
Twenty-five-second samples vary by more than most changes being measured, so
three of them disagreeing is not three results. Report the number you got in
either direction — a change that did not help is a useful finding.

## Look at the result before reporting it

Two rounds have now reported something as done that the running application
disproved: keys described as whole were sliced at the zone boundaries, and a
logo described as sharp was a resampled bitmap. Run it, open the page, and check
what is on screen against what you are about to claim.

## Boundaries, unchanged

Presentation is yours: styles, control templates, vector geometry, layout markup,
drawing controls, brand assets. Not `src/Nextcalibur.Core/`, `MainWindow.xaml.cs`,
`App.xaml.cs`, `TrayPresence.cs`, `Controls/ColourWheel.cs`, `src/Nextcalibur.Cli/`,
`Nextcalibur.App.csproj`, `app.manifest`, `Assets/` or `tools/`.

When markup needs a handler that does not exist yet, wire it, let the build fail,
and report. **Report and stop — do not revert either.** A protected file was once
edited to fix a real bug and then reverted when that was pointed out; the revert
also discarded an unrelated repair someone else had made to the same file, and
the broken version was committed before anyone noticed.
