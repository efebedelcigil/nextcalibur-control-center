# Antigravity — Prompt 5: new brand mark, and a light theme

Two jobs. The second is the larger one by a distance.

---

## 1. Adopt the supplied brand mark

Two files sit at the repository root, made outside this project:

```
logo.png        2048 × 2048, transparent
logo_icon.ico   9 frames: 16, 24, 32, 48, 64, 72, 96, 128, 256
```

A downward silver triangle enclosing a cyan letter N with a sword through it.
Replace every use of the old blade mark with these.

- Move them into the tree: `src/Nextcalibur.App/Assets/logo.png` and
  `src/Nextcalibur.App/Assets/app.ico` (replacing the current icon, which the
  executable, taskbar, tray and installer all already point at).
- Update `docs/logo.png` to the new mark as well.
- **Title bar**: show `logo.png` as an `Image`, not redrawn as geometry. The mark
  has metallic gradients and bevels that vector paths would not reproduce
  faithfully. Same slot as before — about 41 × 30, left of the wordmark.
- Delete `LogoMarkGeometry` from `Icons.xaml` and `tools/build_icon.py`. Both
  described the old mark and would only rot.

### One caveat, your call how to handle it

At 16 px the mark collapses. The N and the sword disappear; what survives is a
grey triangle with a cyan smudge. The supplied `.ico` does contain a 16 px frame,
so nothing is broken — but the tray and title bar will show mush.

If you can, add a **simplified small variant** for the 16, 24 and 32 px frames:
the triangle outline and a solid N, no sword, no gradients, no bevel. Keep the
supplied artwork for 48 px and up. If you would rather not touch the user's
`.ico`, leave it and say so — this is a nicety, not a defect.

### Accent colour

The mark's cyan is roughly `#1FC8E0`. The interface accent is currently
`#4C8DFF`. Move the accent to the mark's cyan so the product agrees with itself.
Adjust the `Good` / `Warn` / `Bad` brushes only if they now clash.

---

## 2. Light theme, dark theme, and follow-the-system

A three-way setting in the title bar that applies **instantly**, on every page,
with no restart and no flicker.

### The control

Three small buttons in a segmented group, in the title bar, left of the caption
buttons:

| `x:Name` | Type | `Tag` |
|---|---|---|
| `ThemeDark` | `RadioButton` | `Dark` |
| `ThemeLight` | `RadioButton` | `Light` |
| `ThemeSystem` | `RadioButton` | `System` |

All `GroupName="Theme"`, all `Checked="OnThemeChanged"`, **none checked in the
markup** — the code reads the saved preference and sets it.

Icons: a moon, a sun, and a half-filled circle or a small monitor for System.
Compact — this sits in a 38 px title bar and must not crowd the wordmark.

### The real work: making the palette swappable

Everything else follows from this, and there is one mistake that will sink it.

Split `Palette.xaml` into two dictionaries with **identical keys**:

```
Themes/Palette.Dark.xaml
Themes/Palette.Light.xaml
```

Every key present in one must be present in the other. `App.xaml` merges the
dark one by default; the code-behind swaps that entry at runtime.

**Then convert every brush reference in the entire application from
`StaticResource` to `DynamicResource`.** A `StaticResource` is resolved once when
the element is created and never looks again — swap the dictionary underneath it
and it keeps the old brush. Every `Background`, `Foreground`, `BorderBrush`,
`Fill`, `Stroke` and every reference inside a `ControlTemplate` and its triggers.
Miss one and that element stays dark on a light background. This sweep is most of
the job; do it thoroughly rather than quickly.

Your custom drawing controls — `FanGauge`, `DonutGauge`, `SegmentedBar`,
`KeyboardPreview` — read colours in C#. Give each a dependency property for the
colours it needs and bind it with `DynamicResource` from the markup, so a theme
change repaints them too. A control that caches a `Brush` in a field at
construction will not update; do not do that.

### Designing the light theme

Do not simply invert. A light interface built from an inverted dark one looks
grey and dirty. Design it:

- Near-white page background, white panels, a soft grey for the rail.
- Borders become visible dividers rather than the faint lines that read on black.
- Text: near-black primary, mid-grey secondary.
- The accent stays the same hue but usually needs to go a shade darker to hold
  contrast against white.
- The keyboard preview keeps a dark deck in both themes. A keyboard is a dark
  object, and the backlight colours only read against dark.
- Panel headers, gauge tracks and unlit segments all need light-theme values —
  a dark track on white looks like a mistake.

Check text contrast at 4.5:1 against its background in both themes.

### System mode

`ThemeSystem` follows Windows' own app theme and changes with it while running.
The code-behind handles the detection and tells the application which palette to
load; you provide the two palettes and make sure nothing caches.

---

## Contract additions

```
ThemeDark, ThemeLight, ThemeSystem   RadioButton, GroupName="Theme",
                                     Checked="OnThemeChanged"
```

Everything from previous rounds stays. `Ink`, `Muted`, `Accent`, `Good`, `Warn`,
`Bad` must exist in **both** palettes — the code-behind looks them up by name and
would throw on a missing key.

## Unchanged boundaries

Presentation only. `src/Nextcalibur.Core/`, `MainWindow.xaml.cs`, `App.xaml.cs`,
`TrayPresence.cs`, `Controls/ColourWheel.cs`, `src/Nextcalibur.Cli/`,
`Nextcalibur.App.csproj` and `app.manifest` stay untouched. `Assets/app.ico` is
the one exception this round — replacing it is the job.

The build will fail on `OnThemeChanged` until the code-behind catches up. Expected
— report it and stop.

## Definition of done

- Switching theme repaints **every** page with no restart and nothing left dark.
- Walk all four pages in both themes and confirm no element kept its old brush.
- `ThemeSystem` follows Windows and changes live when Windows does.
- The new mark appears in the title bar, the taskbar, the tray and the README.
- Clean build, boundaries intact, `git diff --stat` in your report.
