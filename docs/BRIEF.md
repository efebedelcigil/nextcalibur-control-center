# Current brief

Read [ROADMAP.md](ROADMAP.md) for where the project stands and
[CONTRACT.md](CONTRACT.md) for the names your markup must provide.

Two jobs. **The first one is a plan, not a change** — write the plan, and stop.

---

## 1. Turkish and English, switched from the title bar

The user wants a control beside the theme toggle that switches the whole
application between Turkish and English, taking effect at once, with no restart.
Everything the application says, not only its labels.

**Plan this before building it, and read this part carefully, because the
obvious plan is wrong.** Most of the visible words are in `MainWindow.xaml`,
but a good number are not, and those are in files you do not touch:

| Where | What |
|---|---|
| `MainWindow.xaml.cs` | the vendor-software banner, the unsupported-machine banner, "Readings have stalled…", the lighting failure dialogue |
| `TrayPresence.cs` | the tray tooltip and the whole settings menu — start with Windows, close behaviour, the overheat thresholds, the sampling intervals |
| `UpdateService.cs` / wiring | the "a new version is ready" notice |
| `App.xaml.cs` | the power-repair dialogue shown at first run |

Translating only the markup produces an application that is half in each
language, which is worse than either.

So the plan's first job is the shared decision: **where do the strings live, and
how does a window already on screen re-read them?** The palettes are the working
precedent — `Palette.Dark.xaml` and `Palette.Light.xaml` swap in
`ApplyTheme`, and everything bound with `DynamicResource` follows without a
restart. A `Strings.tr.xaml` / `Strings.en.xaml` pair swapped the same way would
let markup bind by key and let the code behind look them up by key too.

Propose the mechanism, name the resource keys the markup needs, and say which
strings you expect to come from the code side. Do not implement the code side —
report the list and it will be written for you.

Decide in the plan, not during it:

- What the control looks like next to the theme toggle. Two letters, a globe, a
  segmented pair — it has to sit in that title bar without crowding it.
- Whether the choice is remembered in `AppSettings` beside `Theme`, and whether
  a first run should follow the system language.
- What happens to text the machine supplies and that is not ours to translate:
  processor and graphics names, Windows' own power-mode names.
- The Turkish is longer than the English almost everywhere. Which labels break,
  and what gives — wrapping, a smaller size, a shorter wording.

## 2. An icon audit

Confirm an icon is present, correct and current everywhere one belongs, on a
**running installed copy** rather than in the markup:

window · taskbar button · Alt-Tab · Task Manager · notification area ·
notification balloons · the installer itself · the desktop shortcut · the
Start-menu shortcut · Add or Remove Programs · the title bar

Report anything missing, stale or blurred, with where you saw it. The title-bar
mark is vector; everything else comes from `Assets/app.ico`, which has frames at
16, 20, 24, 32, 48, 64, 128 and 256 px.

---

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
