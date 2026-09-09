# Antigravity — Prompt 4: corrections

Two of the four problems from the last round were mine and are already fixed —
the Display page showed black because the code-behind never made it visible, and
the warning banner never cleared because it was only evaluated once at startup.
Both are done.

Two are yours.

---

## 1. Power Mode page — make it a real page

Right now the page states a condition and offers a Repair button. That leaves the
user with nothing to decide and no idea what the setting even is.

Rebuild it as **four cards in a row**, in the same style as the Display page's
GPU cards. These are the four power modes Windows offers. Each card carries an
icon, a name, and a sentence explaining what choosing it means. The one currently
in effect is highlighted the way a selected card is.

**New contract:**

| `x:Name` | Type | `Tag` |
|---|---|---|
| `PowerModeEfficiency` | `RadioButton` | `Efficiency` |
| `PowerModeBalanced` | `RadioButton` | `Balanced` |
| `PowerModeBetter` | `RadioButton` | `Better` |
| `PowerModeBest` | `RadioButton` | `Best` |

All four `GroupName="PowerMode"`, all `Checked="OnPowerModeChanged"`, **none
checked in the markup** — the code reads the machine and sets the current one.

Each card needs a description `TextBlock` the code fills in, named to match:
`PowerModeEfficiencyText`, `PowerModeBalancedText`, `PowerModeBetterText`,
`PowerModeBestText`. Leave them empty in the markup; do not write placeholder
copy, the text comes from the code.

Sizing: four cards across the content column, so narrower than the Display page's
three. Icon above name above description. The description is two or three lines
of small text — give it room and let it wrap.

Icon suggestions in your existing language: a leaf or battery for efficiency, a
balance scale for balanced, a rising bar or arrow for better, a full gauge at
redline for best.

**Keep** `OverlayState`, `OverlayDetail` and `FixButton`. They move below the
cards and become a status strip: what the current setting means for this machine,
and the Repair button when something is wrong. `FixButton` stays collapsed unless
the code shows it. Style that strip so it reads as a footnote to the cards, not
as the main event.

## 2. The keyboard needs building properly, not drawing by hand

The hand-placed rectangles do not read as a keyboard. The problem is the method,
not the effort — a laptop keyboard is a hundred-odd keys with irregular widths,
and positioning each one by hand in XAML will not converge.

**Build it as a drawing control instead:** `Controls/KeyboardPreview.cs`, in the
same spirit as your `FanGauge` and `DonutGauge`.

Drive it from a layout table. Each row is a list of key widths in units, where
1u is a standard key:

```
row 1  (function)  1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
row 2  (numbers)   1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, ...
row 3  (tab)       1.5, 1, 1, ... , 1.5
row 4  (caps)      1.75, 1, ... , 2.25 (enter)
row 5  (shift)     2.25, 1, ... , 2.75
row 6  (modifiers) 1.25, 1.25, 1.25, 6.25 (space), ...
```

Measure the real proportions from the reference screenshot and put them in that
table. Then the control lays out rows, computes each key's rectangle from its
width and the running x offset, and draws it. Getting a key wrong becomes a
one-number edit rather than a hunt through markup.

This machine also has a numeric keypad — include it, the reference shows it.

Around the key field, draw the laptop: deck, palm rest, touchpad. Simple flat
shapes.

### Zones and colour

Three properties, plus which zone is selected:

```csharp
public Brush ZoneLeftBrush   { get; set; }
public Brush ZoneMiddleBrush { get; set; }
public Brush ZoneRightBrush  { get; set; }
public int   SelectedZone    { get; set; }   // 0 left, 1 middle, 2 right, -1 none
```

Assign each key to a zone by its x position — left third, middle third, right
third. Draw the key with a dark face and its zone brush as a lit edge or inner
glow; that reads as backlighting far better than filling the whole key.

Outline the selected zone's key group in the accent colour.

**This replaces the `PreviewA` / `PreviewB` / `PreviewC` borders.** Name the
control `KeyboardPreview` in the markup; the code-behind is being changed to set
its brushes directly instead. Do not keep the old borders.

Still colour and brightness only — no effect animation.

---

## Unchanged

Presentation only. `src/Nextcalibur.Core/`, `MainWindow.xaml.cs`, `App.xaml.cs`,
`TrayPresence.cs`, `Controls/ColourWheel.cs`, `src/Nextcalibur.Cli/`,
`Assets/app.ico`, `Nextcalibur.App.csproj` and `app.manifest` all stay untouched.

Everything else from the last round was right and should stay: the mode tabs, the
rail with four items, the Display page, the text rewrites, the logo.

The build will fail on `OnPowerModeChanged` until the code-behind catches up.
That is expected — report it and stop, exactly as you did last time.
