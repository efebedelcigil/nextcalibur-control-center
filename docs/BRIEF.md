# Current brief — new brand mark, and a light theme

Read [ROADMAP.md](ROADMAP.md) for where the project stands and
[CONTRACT.md](CONTRACT.md) for the names your markup must provide. Both are
short.

Two jobs. The second is the larger one by a distance.

---

## 1. Put the new brand mark on screen

The mark is a downward silver triangle enclosing a cyan letter N with a sword
through it. It is already in the tree and the icon is already generated:

```
src/Nextcalibur.App/Assets/logo.png    512 px, transparent
src/Nextcalibur.App/Assets/app.ico     16, 20, 24, 32, 48, 64, 128, 256
docs/logo.png                          512 px, for the README
tools/build_icon.py                    regenerates the icon from logo.png
```

The executable, taskbar, tray and installer already point at `app.ico`, so those
are done. What remains is the interface:

- **Title bar**: show `Assets/logo.png` as an `Image`, not redrawn as geometry.
  The mark has metallic gradients and bevels that vector paths would not
  reproduce. Same slot as before — roughly 41 × 30, left of the wordmark.
- **Delete** `LogoMarkGeometry` from `Icons.xaml`. It described the old blade
  mark and would only rot.
- **Accent colour**: the mark's cyan is about `#1FC8E0`; the interface accent is
  `#4C8DFF`. Move the accent to the mark's cyan so the product agrees with
  itself. Adjust `Good` / `Warn` / `Bad` only if they now clash.

Do not regenerate `app.ico` — `tools/build_icon.py` already produces it, and its
small frames are sharpened deliberately because a detailed mark turns to mush
when downscaled.

---

## 2. Light theme, dark theme, and follow-the-system

A three-way setting in the title bar that applies **instantly**, on every page,
with no restart and no flicker.

### The control

Three small buttons in a segmented group, left of the caption buttons:
`ThemeDark`, `ThemeLight`, `ThemeSystem` — see [CONTRACT.md](CONTRACT.md) for the
exact names, group and handler.

Icons: a moon, a sun, and a half-filled circle or small monitor for System.
Compact — this sits in a 38 px title bar and must not crowd the wordmark.

### The real work: making the palette swappable

Everything else follows from this, and there is one mistake that will sink it.

Split `Palette.xaml` into two dictionaries with **identical keys**:

```
Themes/Palette.Dark.xaml
Themes/Palette.Light.xaml
```

Every key present in one must be present in the other. `App.xaml` merges the dark
one by default; the code-behind swaps that entry at runtime.

**Then convert every brush reference in the application from `StaticResource` to
`DynamicResource`.** A `StaticResource` is resolved once when the element is
created and never looks again — swap the dictionary underneath it and it keeps
the old brush. Every `Background`, `Foreground`, `BorderBrush`, `Fill`, `Stroke`,
and every reference inside a `ControlTemplate` and its triggers. Miss one and
that element stays dark on a light background. This sweep is most of the job; do
it thoroughly rather than quickly.

Your drawing controls — `FanGauge`, `DonutGauge`, `SegmentedBar`,
`KeyboardPreview` — read colours in C#. Give each a dependency property for the
colours it needs and bind it with `DynamicResource` from the markup, so a theme
change repaints them too. A control that caches a `Brush` in a field at
construction will not update.

### Designing the light theme

Do not simply invert. A light interface built by inverting a dark one looks grey
and dirty. Design it:

- Near-white page background, white panels, a soft grey for the rail.
- Borders become visible dividers rather than the faint lines that read on black.
- Text: near-black primary, mid-grey secondary.
- The accent keeps its hue but usually needs to go a shade darker to hold
  contrast against white.
- **The keyboard preview keeps a dark deck in both themes.** A keyboard is a dark
  object, and backlight colours only read against dark.
- Panel headers, gauge tracks and unlit segments all need light-theme values — a
  dark track on white looks like a mistake.

Check text contrast at 4.5:1 against its background in both themes.

### System mode

`ThemeSystem` follows Windows' own app theme and changes with it while running.
The code-behind handles detection and tells the application which palette to
load; you provide the two palettes and make sure nothing caches.

---

## Boundaries

Presentation only. `src/Nextcalibur.Core/`, `MainWindow.xaml.cs`, `App.xaml.cs`,
`TrayPresence.cs`, `Controls/ColourWheel.cs`, `src/Nextcalibur.Cli/`,
`Nextcalibur.App.csproj`, `app.manifest`, `Assets/app.ico` and
`tools/build_icon.py` all stay untouched.

The build will fail on `OnThemeChanged` until the code-behind catches up. That is
expected — wire it, report it, and stop.

## Definition of done

- Switching theme repaints **every** page with no restart and nothing left dark.
- Walk all four pages in both themes and confirm no element kept its old brush.
- `ThemeSystem` follows Windows and changes live when Windows does.
- The new mark appears in the title bar.
- Clean build, boundaries intact, `git diff --stat` in your report.
