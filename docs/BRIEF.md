# Current brief

Read [ROADMAP.md](ROADMAP.md) for where the project stands and
[CONTRACT.md](CONTRACT.md) for the names your markup must provide.

Five jobs.

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

## 5. The dialogues, in the application's own look

Every box the application raises now goes through one class,
`src/Nextcalibur.App/Dialogs.cs`, with four calls: `Tell`, `Warn`, `Ask`,
`Confirm`. Today they are still the system message box - silent now, and owned
by the main window so it is modal - but the owner wants them to look like the
application rather than like Windows.

That is one implementation to replace. The contract is the four static methods
and their return values; nothing else in the code-behind knows what a box looks
like. Requirements that must survive the swap:

- **Modal to the main window.** Nothing else in the window responds while a box
  is up. An overlay inside the window that dims and blocks the rest is the
  natural shape and reads as part of the application.
- **Silent.** No system sounds.
- **A safe default.** `Ask` has a `defaultNo` parameter; Escape and closing the
  box must return that default, because an unanswered question must not do the
  thing.
- **Works before the window exists.** Two boxes are raised with no owner - the
  first-run repair and the uninstall question. Fall back to something that
  stands alone.

Not in scope: Windows' own elevation prompt. It is the operating system's,
looks the way it looks, and appears once - at the first-run permission - and
never again.

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

## 6. Two small things on the System page (11 September)

- `SystemModeNote` (under the three mode tabs, collapsed by default) now says
  what Windows is on when none of Office/Gaming/Performance is active - e.g.
  "No mode is active. Windows is on the Balanced plan, Better performance.
  Pick one above." It is a plain `StatusTextStyle` TextBlock wrapped in a
  vertical StackPanel with the tabs; place it where it reads best.
- "Start with Windows" is on from install (first run), as the vendor's is. It
  lives in the tray menu only. If a settings area ever exists, it belongs
  there too; it is `StartupRegistration.IsEnabled` / `Set(bool, exe)`.

## 7. The light-theme icon is a plain circle (11 September)

The theme switch's light-mode glyph is a bare disc. Give it the sun's rays -
short strokes around the rim, same stroke weight as the other icons - so it
reads as "light" next to the moon rather than as a dot. Markup only.

## 8. Two overheat thresholds, as sliders (11 September, evening)

The warning now has a threshold per chip: `AppSettings.CpuWarningTemperatureC`
(default 90) and `GpuWarningTemperatureC` (default 85), both within
`AppSettings.MinWarningTemperatureC`..`MaxWarningTemperatureC` (60..105).
The tray keeps only the on/off switch; the numbers belong on the System page,
next to the `OverheatWarningToggle` you placed, where the live temperatures
are on screen.

Markup to add, names exact - the code-behind binds to them and shows the
value beside each:

- `CpuWarnSlider` — `Slider`, `Minimum="60" Maximum="105"`, integer steps
  (`IsSnapToTickEnabled="True" TickFrequency="1"`), same look as
  `BrightnessSlider`.
- `CpuWarnValue` — `TextBlock` showing e.g. `90 °C`.
- `GpuWarnSlider`, `GpuWarnValue` — the same for the GPU.

Both rows should read as disabled when the toggle is off (the code-behind
sets `IsEnabled`); a short label per row: "CPU" and "GPU". No numbers in the
markup itself — the code-behind fills them from settings.

## 9. Type the threshold, and a reset (12 September)

The person wants to click the number beside each slider and type one, and a
"reset to default" beside the pair. Rules are the code-behind's (positive
integer, within `AppSettings.MinWarningTemperatureC`..`MaxWarningTemperatureC`,
anything else refused and the old value put back); the markup is yours:

- `CpuWarnValue` and `GpuWarnValue` become `TextBox`es - same look as the
  `Value` text they replace (no visible border until focused is fine),
  right-aligned, wide enough for `105 °C`. The code-behind shows `90 °C`
  in them, strips the unit when the person edits, and validates on Enter
  and on focus loss. Keep the names.
- `OverheatResetButton` - a small text button, "Reset", next to the pair.
  The code-behind puts both thresholds back to their defaults on click.

Nothing else changes; the sliders and the toggle stay as they are.

## 10. The title-bar mark is blurry (12 September)

`app.ico` in an `Image` lets WPF pick a small frame and stretch it. Two
clean downsamples of `logo.png` are in `Assets/` now, made with Lanczos and
included as resources: `logo-48.png` and `logo-96.png`. Use `logo-96.png`
as the `Source` at `Width="24" Height="24"` (four pixels of source per
device pixel up to 400 % scaling), `RenderOptions.BitmapScalingMode="HighQuality"`,
and `UseLayoutRounding="True"` on the image so it lands on whole pixels.
If it still looks soft, say so and the owner will hand-tune a 48 px master.

## 11. The rail wordmark is now an image, and the version goes bottom-left (12 September)

- The owner drew the wordmark: `Assets/logo-text.png` is the master
  (1474 x 676, keep it, do not reference it), `Assets/logo-text-300.png` is a
  Lanczos downsample at 300 px wide (3x for the 100 px rail) and is the
  resource to use. Replace the two `TextBlock`s ("NEXT" / "CALIBUR") in the
  rail with an `Image Source="/Assets/logo-text-300.png"`, width to the rail
  minus margins, `RenderOptions.BitmapScalingMode="HighQuality"`,
  `UseLayoutRounding="True"`. Height follows the aspect ratio (2.18:1).
- Bottom-left of the rail, under the navigation items: a `TextBlock
  x:Name="VersionText"`, small and muted (`StatusTextStyle`, 10-11 px),
  empty in the markup - the code-behind writes `v0.5.1`.

## 12. Title bar: a third button, and one double space (12 September)

- `MainWindow.xaml:396`, the header text: `"NEXTCALIBUR  CONTROL CENTER"` has
  two spaces after NEXTCALIBUR. Make it one.
- The close button now **exits** (the code-behind asks "Exit Nextcalibur?"
  first, in the window's own dialogue). Hiding to the tray is a button of its
  own: add a third caption button between minimise and close,
  `Click="OnMinimiseToTrayClick"`, `Style="{DynamicResource CaptionButton}"`,
  with a glyph that reads as "to the tray" (an arrow into a tray, or a small
  chevron-down-into-bar - your call), tooltip "Hide to tray". Keep the three
  the same size and spacing. The tray menu's "Close button keeps it running"
  item is gone; nothing else in the menu changes.

## 13. Dialogue buttons: the affirmative blue, the negative red (12 September)

In the in-window dialogue (`ModalDialogOverlay`): `DialogButtonPrimary` is
always the affirmative - Yes / OK - and gets the accent blue; 
`DialogButtonSecondary` is always the negative - No / Cancel - and gets red
(`Warn`, or a dedicated danger brush if the theme has one), both with
readable text on them in **both** themes. Two named styles, applied in the
markup; the code-behind only sets `Content` and visibility, so the colours
must not depend on the text. Hover/pressed states in the same family. Same
in light and dark.

## 14. What the graphics card is drawing, on the Display page (12 September)

A line the code-behind fills every five seconds while the page is open:
`TextBlock x:Name="GpuDrawText"`, one of

    Graphics card: 17.5 W, 57 °C      (awake - Discrete, or Hybrid with something using it)
    Graphics card: asleep, 56 °C      (Hybrid, card powered down; nothing is woken to say so)
    Graphics card: off                (UMA)

Place it on the Display page where the cost of the current mode is read -
above the three cards or in the status area under them, your call. It is one
TextBlock and the code sets the whole string. No CPU line: its package power
is not readable without a kernel driver (roadmap).

## 15. The right-hand panels on every page but Lighting; the thresholds move into CPU & GPU (12 September)

- The two panels on the right of the System page - "CPU & GPU" and
  "Memory & Disk" - should be on **every page except Lighting**: System,
  Power Mode and Display keep them in the same place, same size, so the
  eye always finds the readings there. Lighting keeps its full width. One
  set of named elements (`CpuClock`, `GpuClock`, `CpuTemp`... whatever they
  are today), moved out of `PageSystem` to a level above the page switch,
  hidden only while Lighting is selected - do not duplicate them, the
  code-behind writes each name once.
- "CPU & GPU" has room. Move the overheat switch and the two threshold
  sliders (`OverheatWarningToggle`, `CpuWarnSlider`/`CpuWarnValue`,
  `GpuWarnSlider`/`GpuWarnValue`, `OverheatResetButton`) from the
  Hardware Monitoring header into that panel, under the two bars - each
  slider on the row of its chip reads naturally. Names unchanged; the
  header keeps the title and the subtitle line.
- The GPU row's clock field now reads e.g. `2,22 GHz · 17.5 W` or `asleep`;
  give it the width.
