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

## 2. Fix the rail, and add the Display page

### The rail reads wrong

The middle rail item is being mistaken for a GPU switch. Two causes:

- Its icon is a lightning bolt, which reads as performance or graphics.
- Its label, "Power", is ambiguous next to a page about Windows power modes.

Fix both:

- **Relabel** the item `Power Mode`, and the page heading likewise.
- **Redraw** `IconNavPower` so it cannot be read as a GPU: a battery or a power
  plug with a small gauge arc is unambiguous. Drop the lightning bolt entirely —
  it belongs to performance, which is what the mode tabs now own.

### Add the Display page

The rail gains a fourth item, matching the reference's Display Mode page. Build
the page as the reference shows it: three large option cards in a row, each an
illustration above a radio indicator and label.

**New contract:**

| `x:Name` | Type | `Tag` | Notes |
|---|---|---|---|
| `NavDisplay` | `RadioButton` | — | `GroupName="Nav"`, `Tag="Display"`, `Checked="OnNavChanged"` |
| `PageDisplay` | `Grid` | — | starts `Collapsed` |
| `ModeDiscrete` | `RadioButton` | `Discrete` | `GroupName="Gpu"`, `Checked="OnGpuModeChanged"` |
| `ModeHybrid` | `RadioButton` | `Hybrid` | same |
| `ModeUma` | `RadioButton` | `Uma` | same |
| `GpuModeDetail` | `TextBlock` | — | one line of explanation the code fills in |

Rail order: System, Power Mode, Display, Lighting. Four items now share the
rail's height rather than three.

As with the mode tabs: **none of the three checked in the markup.** The code
sets the current one after reading the machine's actual state.

Card illustrations, in your icon language, from the analysis's descriptions:
a multi-blade cooling turbine for Discrete, a turbine linked to a chip by curved
arrows for Hybrid, a chip with a lightning bolt for UMA. Selected shows a filled
accent circle with a check; unselected shows a hollow ring.

Leave room under the cards for `GpuModeDetail` — a full-width line of text. What
switching modes actually does needs care, and the app will say so there rather
than let the user assume.

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

## 4. Draw the actual laptop keyboard

The current preview is three coloured rectangles with a four-by-five block grid
over each. Replace it with the real thing.

**Draw the keyboard shown in the reference screenshots.** That illustration
depicts a Casper Excalibur G870 — the machine this software runs on — so match
its layout, proportions and key shapes closely: the key field's aspect, where the
rows sit, the size of the wide keys, the arrow cluster's inverted-T tucked into
the bottom right, the touchpad's position and proportion on the palm rest.

You are reproducing **the hardware**, not their picture of it. A keyboard layout
is a physical fact; use the screenshot to get that fact right. Do not copy their
rendering — their shading, gradients, materials, highlights, bezel styling or any
badge or lettering on the deck. Draw it in your own flat vector language,
consistent with the rest of this interface.

Requirements:

- Laptop body seen from above: deck, palm rest, touchpad, and the key field.
- A real key layout: function row, number row, three letter rows, a bottom
  modifier row, and the arrow cluster. Keys individually shaped, with the wide
  ones actually wide — space, shift, enter, backspace.
- **Each key is lit by its zone's colour.** Split the key field into three
  vertical zones and let every key in a zone take that zone's light.
- No legends on the keycaps. They add nothing at this size and would be the one
  place where copying their artwork is tempting.

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
untouched — `git diff --stat` must show it. The exceptions are the new names in
§1 and §2 — the mode tabs, the Display page and its handlers — which the
code-behind is being written to expect.

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
