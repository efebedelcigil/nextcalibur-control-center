# Current brief — make the keyboard illustration cheap

Read [ROADMAP.md](ROADMAP.md) for where the project stands and
[CONTRACT.md](CONTRACT.md) for the names your markup must provide.

One job this round, and a measurement that has to come with it.

---

## The problem

The keyboard illustration on the Lighting page is the most expensive thing in
the application. Measured on the development machine, idle, window open:

| Build | CPU | Working set |
|---|---|---|
| before any keyboard illustration | **0.010%** | 145 MB |
| first SVG trace | 0.319% | 157 MB |
| after "simplification" | 0.449% | 177 MB |
| after fixing an unrelated backend cost | 0.211% | 165 MB |

The last row is the current state. It is still **twenty times** the cost of the
build without the illustration, and `MainWindow.xaml` has grown to 118 KB.

The previous round reported that the simplification had "reduced CPU load to
zero". It had not. That is the thing to avoid repeating — see *Measure, don't
assert* below.

## The job

A smaller, cleaner SVG is being placed at `docs/keyboard_layout.svg`, replacing
the pixel trace. Rebuild the keyboard from it.

- Three frozen `StreamGeometry` resources, one per zone, as now:
  `KeyboardGeometryLeft`, `KeyboardGeometryMiddle`, `KeyboardGeometryRight`.
- Keep `PreviewA`, `PreviewB`, `PreviewC` and the contract around them.
- Keep the behaviour added last round: no border frames, the selected zone
  lifting slightly with a shadow, hover preview, click-to-select, white-grey
  inactive keycaps against vibrant selected ones. Those are good and the user
  likes them.
- `po:Freeze="True"` on every geometry, as now.

Aim to get `MainWindow.xaml` back under about 40 KB. If the new SVG cannot get
you there without losing the look, say what the trade-off is rather than
choosing silently.

### Two things worth checking while you are in there

The lift and shadow on the selected zone are drawn with a `DropShadowEffect`.
Effects in WPF are rendered per frame and are not free. If the measurement below
shows the illustration is still expensive, try a static shadow — a soft
translucent shape behind the zone — before concluding the geometry is at fault.

Hover previewing the RGB at 45% opacity means a second full copy of the
geometry is being drawn for every zone. Two layers per zone across three zones
is six geometries. Consider whether the inactive and active looks can share one
path with a switched brush.

## Measure, don't assert

**Report numbers, not adjectives.** "Reduced CPU to zero" is not a result; a
measurement is. Do this before and after your change and put both in your
report:

```powershell
# Close the running app first, build Release, then:
$p = Start-Process 'src\Nextcalibur.App\bin\Release\net8.0-windows\win-x64\Nextcalibur.exe' -PassThru
Start-Sleep -Seconds 12                      # let startup settle
$t = $p.TotalProcessorTime
Start-Sleep -Seconds 25
$p.Refresh()
'CPU {0:N3}%  RAM {1} MB' -f `
    (($p.TotalProcessorTime - $t).TotalMilliseconds / 25000 * 100 / [Environment]::ProcessorCount),
    [math]::Round($p.WorkingSet64 / 1MB)
```

Leave the window open on the **Lighting page** while measuring — that is where
the illustration is drawn. Also report the size of `MainWindow.xaml`.

If your change does not improve the numbers, say so. A change that turns out
not to help is a useful finding; a claim that it helped when it did not costs
everyone the next round.

---

## Boundaries

Presentation only. `src/Nextcalibur.Core/`, `MainWindow.xaml.cs`,
`App.xaml.cs`, `TrayPresence.cs`, `Controls/ColourWheel.cs`,
`src/Nextcalibur.Cli/`, `Nextcalibur.App.csproj`, `app.manifest`, `Assets/` and
`tools/` all stay untouched.

**If a fix appears to need a protected file, report it and stop.** Do not edit
it, and do not revert it either — last round a revert of `MainWindow.xaml.cs`
also discarded an encoding repair that had been made to the same file, and the
broken version was then committed. Leave protected files exactly as you found
them.

Build Release before reporting, and close the running application first: it
locks its own output and produces errors that look like code errors.
