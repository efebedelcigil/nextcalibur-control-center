# Current brief

Read [ROADMAP.md](ROADMAP.md) for where the project stands and
[CONTRACT.md](CONTRACT.md) for the names your markup must provide.

One fault, and it is the hover state on the keyboard.

---

## Hovering a zone flickers, and the hover is what breaks it

Park the pointer on the spacebar and leave it there. The middle zone alternates
between hovered and not hovered, on its own, with the mouse held still. Two
captures a second apart with no input between them show the zone bright in one
and dim in the other.

It is a feedback loop, and both halves of it are in `MainWindow.xaml`:

1. Hovering a zone applies `ScaleTransform 1.012` and `TranslateTransform Y="-1"`
   to the whole `RadioButton`. A render transform moves hit-testing with it, and
   the scale is about the origin rather than the centre, so points far from the
   top-left move by several pixels, not one.
2. The moved geometry slides the pointer off a keycap and into the gap between
   two keys — and the gaps are not hit-testable. `Fill="#01000000"` covers the
   keycap rectangles only, so there is nothing to hover between them.

Hover is lost, the transform reverts, the pointer is back over the keycap, hover
returns. Round and round, at layout speed.

### What to change

**Hover must not move anything.** Take `RenderTransform` out of both hover
triggers in all three zone styles. Hover may change opacity, stroke thickness,
stroke colour — anything that does not alter where the shape is. Keep the lift
for the **selected** state: selection comes from a click, so it cannot feed back
into the pointer.

**Give each zone a continuous hit region.** Even standing still, dragging across
the deck should not drop the hover every time the pointer crosses between two
keys. Put a transparent filled shape behind each zone's keycaps covering that
zone's whole area, gaps included, and let that carry the hit-testing. The three
regions tile the deck and do not overlap, so no zone can steal another's hover.

Once the zones no longer overlap or move, the `Panel.ZIndex` juggling on hover
has nothing left to do and can go.

### How to check it

Park the pointer over a large key and watch for ten seconds without touching the
mouse: the zone must hold one state. Then move slowly across a zone, over keys
and the gaps between them: the highlight must stay on, not blink at every gap.
Do this before reporting it fixed.

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
