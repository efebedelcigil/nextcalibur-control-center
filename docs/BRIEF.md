# Current brief

Read [ROADMAP.md](ROADMAP.md) for where the project stands and
[CONTRACT.md](CONTRACT.md) for the names your markup must provide.

Three faults, found by looking at the running application. All three are in
`MainWindow.xaml`. Ordered by how wrong they look.

---

## 1. Unselected zones must show their own colour, dimmed — not go dark

Right now the two zones you are not editing render as flat grey, which reads as
"those zones are off". They are not off. The page should show the keyboard as it
actually is: **every zone painted its own current colour**, with the unselected
ones simply muted.

The colour is already there and you do not have to ask for it. The code-behind
sets `PreviewA`, `PreviewB` and `PreviewC` — one `Border` per zone — and puts
each zone's live colour in `.Background` on every lighting change. Drive each
zone path's fill from its own `PreviewX.Background`.

Selection is then carried by **emphasis, not by colour**: the selected zone at
full strength, the others at perhaps 35–45% opacity. Judge it by eye — an
unselected red zone must still read as red, not as grey and not as pink.

Keep the existing lift. Selection was always meant to be "those keys come
forward", never "those keys light up and the rest switch off".

## 2. The selected zone is a grey slab, not lifted keys

The selection currently paints a filled rectangle behind the zone. It covers the
gaps between keys and a band of empty deck below the bottom row, so it reads as
a highlighter stroke across a photograph.

This is the thing the user rejected two rounds ago, in their words: not selecting
with a frame, but bringing that section's keys forward with a slight zoom.
Whatever backing rectangle produces that slab has to go; the emphasis belongs on
the key shapes themselves.

## 3. Keys are cut in half at the zone boundaries

The boundary between the left and middle zones runs straight down through the
middle of key rows, slicing keycaps. A previous round reported this as fixed and
described the boundaries as following the natural spacing between keys; on
screen they do not. Every key belongs to exactly one zone, whole.

While you are in there: the light bar above the keyboard has a stray notch or
arrowhead in the middle of it, and the bottom-left of the selected block runs
past the last row into empty deck.

---

## Also: the title-bar logo looks poor, and it is not the icon file's fault

`Assets/app.ico` is well-formed — eight frames, PNG-encoded, 32bpp, at 16, 20,
24, 32, 48, 64, 128 and 256 px. Nothing is missing.

Two things are going wrong instead:

- The title bar draws it at **30×30**, which is not one of those sizes, so WPF
  resamples a frame to a fractional size. Ask for **32** and it maps one frame to
  one pixel grid with no resampling. That alone will sharpen it.
- Even at 32 the mark is a reduction of a 512 px original: a thin sword over a
  bevelled shield outline with a metallic gradient. That detail cannot survive at
  32 px whatever the resampler does. A mark that has to work small needs a
  simplified form — heavier strokes, no gradient, less inside the shield.

The most durable fix is to draw the title-bar mark as vector geometry rather than
an image: crisp at any size, no resampling, and no bitmap at all. The `.ico` stays
as it is for the taskbar, Alt-Tab and the installer, where Windows picks a real
frame and it looks correct.

---

## What changed underneath since your last round

Sensor readings no longer run on the interface thread. They were the
application's handle leak: `System.Management` needs an MTA thread, and calling
it from the STA one a window runs on cost a kernel event per call — 2.4 handles
a second. The read now happens on the thread pool.

Nothing in the markup has to change for that, but two things follow from it:

- **The lighting page needs a pass by hand.** A lighting write and a sensor
  sample can now genuinely overlap where before they could not. The firmware
  mailbox is locked for whole sequences to prevent it, but that wants trying:
  the colour wheel, each effect, the brightness slider, select-all, and the
  power toggle, with the System page's readings updating throughout.
- **Idle cost has dropped to 0.042%** from 0.135%, measured over ten minutes.
  That is the new number to beat, and it moves the goalposts: an addition that
  would once have hidden inside the noise is now visible.

## The state of the illustration

Three frozen `StreamGeometry` resources, one path per zone, no shader effects,
about 2 KB of markup between them. `MainWindow.xaml` is 98 KB, which is roughly
right — see *On the markup size* in the roadmap for why the earlier 40 KB target
was mistaken.

## The logo

`Assets/app.ico` is the one to use, at any size. It carries hand-sharpened
frames from 16 px to 256 px, so it stays crisp small — better than scaling
`Assets/logo.png`, which is a single 512 px square kept as the brand master and
is deliberately not compiled into the assembly.

## Measuring, if you change anything

Close the running application first — it locks its own build output. Build
Release, then take **one ten-minute sample** rather than three short ones:

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

Twenty-five-second samples vary by more than most of the changes being measured;
three of them disagreeing is not three results. Report the number you got, in
either direction — a change that turned out not to help is a useful finding, and
a claim that it helped when it did not costs the next round.

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
