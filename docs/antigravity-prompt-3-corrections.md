# Antigravity — Prompt 3: corrections

The visual layer is in and the contract holds — 43 named elements, 13 handlers,
protected files untouched, clean build. Four things need fixing. Everything
below is still presentation only; the same boundaries apply.

---

## 1. Add the system mode tabs — the biggest gap

The reference's System page has three mode tabs across the top: Office, Gaming,
High Performance. The previous brief told you to omit them because there was no
API behind them. That was right at the time; the API is being added now, so
build the tabs.

Place them exactly where the analysis measured — `y = 61` to `111`, three tabs
of about `184 × 50 px`, icon left and label right, with the active tab's amber
gradient, `2 px` top highlight and downward bloom.

**New contract — these names and this handler must be exact:**

| `x:Name` | Type | `Tag` | Handler |
|---|---|---|---|
| `ModeOffice` | `RadioButton` | `Office` | `Checked="OnSystemModeChanged"` |
| `ModeGaming` | `RadioButton` | `Gaming` | `Checked="OnSystemModeChanged"` |
| `ModePerformance` | `RadioButton` | `Performance` | `Checked="OnSystemModeChanged"` |

All three in `GroupName="Mode"`. None checked in the markup — the code sets the
current one at startup, and a checked default in XAML would fire a hardware
write before the app knows what the machine is set to.

Reuse your existing icon language: a monitor with document lines for Office, a
gamepad for Gaming, a tachometer at redline for Performance.

Do **not** add the pentagon radar chart. It visualises trade-offs we do not
measure, and drawing an invented shape next to real sensor values would imply we
know things we do not.

## 2. The navigation rail reads wrong

The middle rail item is being mistaken for a GPU switch. Two causes:

- Its icon is a lightning bolt, which reads as performance or graphics.
- Its label, "Power", is ambiguous next to a page about Windows power modes.

Fix both:

- **Relabel** the item `Power Mode`, and the page heading likewise.
- **Redraw** `IconNavPower` so it cannot be read as a GPU: a battery or a power
  plug with a small gauge arc is unambiguous. Drop the lightning bolt entirely —
  it belongs to performance, which is what the mode tabs now own.

A fourth item, **Display**, for graphics-mode switching is planned but has no
implementation. Do not add it, and do not leave a visible gap or placeholder —
just make sure the rail's layout tolerates a fourth item later without a
redesign.

## 3. Rewrite the visible text for people, not developers

Static text in the XAML is yours; text set from code is being fixed separately.

The principle: **name what the user sees or decides, never the mechanism.** No
internal identifiers, no Windows API vocabulary, no words like firmware, mailbox,
overlay, interface, or index. If a sentence would only make sense to someone who
has read our source, rewrite it.

| Current | Replace with |
|---|---|
| `ACTIVE POWER OVERLAY` | `CURRENT MODE` |
| `Power Management` | `Power Mode` |
| `System Sensors` | `System` |
| `Reading sensors...` | `Starting up...` |
| `Checking...` | `Checking...` (fine) |
| `This effect drives its own colours in firmware, so the wheel does not apply.` | `This effect picks its own colours.` |

Panel headers `CPU & GPU` and `Memory & Disk`, and the labels `CPU FAN`,
`GPU FAN`, `CPU TEMPERATURE`, `GPU TEMPERATURE`, `Brightness`, `LED on/off`,
`Select all` are all fine — leave them.

While you are there: sentence case for sentences, uppercase only for the small
panel labels that are already uppercase. No trailing full stops on labels.

## 4. Draw a real laptop keyboard in the lighting preview

The current preview is three coloured rectangles with a four-by-five block grid
over each. Replace it with a recognisable laptop, seen from above, with an actual
key layout — the reference's illustration is the target.

Requirements:

- A laptop body: base, palm rest, touchpad, and the key field. Vector shapes, all
  yours, drawn from the reference's *composition* rather than traced from it.
- A real key layout: function row, number row, three letter rows, a bottom
  modifier row, and the arrow cluster. Keys individually shaped, with the wide
  ones actually wide — space, shift, enter, backspace.
- **Each key is lit by its zone's colour.** Split the key field into three
  vertical zones and let every key in a zone take that zone's light.

### How the colour reaches the keys

Keep `PreviewA`, `PreviewB` and `PreviewC` as named `Border` elements — the code
sets `Background` on them and that contract does not change. Put each zone's keys
inside its `Border` and bind their fill to it:

```xml
Fill="{Binding Background, ElementName=PreviewA}"
```

The colour the code supplies is already scaled for brightness, and goes flat
black when the lighting is switched off, so a key that simply shows that brush
will dim and extinguish correctly with no extra work on your side.

Keep the accent outline on the selected zone — it is how the user knows which
zone the wheel is editing. Draw it around the zone, not around every key.

### What not to simulate

Colour and brightness only. **No effect animation** — no breathing, no blinking,
no travelling wave, no colour cycling. Two reasons: an animated illustration of
an animation is noise, and this application does not run continuous animations
at all.

Suggested construction: keycaps as rounded rectangles with a dark face and a thin
lit border, sitting on a near-black deck, with the zone colour showing through as
the glow. That reads as backlighting far better than filling the whole key.

---

## Unchanged boundaries

Presentation only. `src/Nextcalibur.Core/`, `MainWindow.xaml.cs`, `App.xaml.cs`,
`TrayPresence.cs`, `Controls/ColourWheel.cs` and `src/Nextcalibur.Cli/` stay
untouched — `git diff --stat` must show it. The one exception is the new mode-tab
names in §1, which the code-behind is being written to expect.

`Nextcalibur.App.csproj` keeps its `NoWarn` for `WFAC010`. That suppression is
correct and now documented: the manifest's `dpiAware` block is the only route to
per-monitor DPI awareness in WPF, and removing it was measured to drop the
process to system-aware. Do not remove either the suppression or the manifest
block.

## Also: the logo does not match itself

`docs/logo.png` is a different mark from `Assets/app.ico`. The `.ico` is correct
— a symmetric blade with a crossguard, centred, legible at 16 px. The PNG is an
asymmetric shape pushed into the right half of the canvas with the left half
empty, and it is solid `#4C8DFF` rather than white.

Regenerate `docs/logo.png` from the same geometry `tools/build_icon.py` uses for
the icon, centred on the 512 × 512 canvas with even margins. Leave the `.ico`
alone.
