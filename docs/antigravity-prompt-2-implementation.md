# Antigravity — Prompt 2 of 2: implement the visual layer

Copy everything below the line into Antigravity. It already has
`DESIGN_SPECIFICATION.md` from the first pass; attach that plus the reference
screenshots and the repository.

---

## Task

Implement the visual layer of a WPF application so that it reads as the same
design family as the reference in `DESIGN_SPECIFICATION.md`.

**You are writing presentation only — think of it as the CSS.** Styles, control
templates, vector geometry, layout markup. Someone else owns the behaviour.

### You write

- `src/Nextcalibur.App/Themes/Palette.xaml` — brushes
- `src/Nextcalibur.App/Themes/Typography.xaml` — text styles
- `src/Nextcalibur.App/Themes/Controls.xaml` — control templates and styles
- `src/Nextcalibur.App/Themes/Icons.xaml` — `Geometry` resources, one per icon
- `src/Nextcalibur.App/MainWindow.xaml` — the layout
- `src/Nextcalibur.App/Controls/*.cs` — **only** for drawing primitives that XAML
  cannot express: the circular fan gauge, the donut gauge, the segmented bar.
  These take numbers in and draw. They contain no application logic.

### You must not touch

- `src/Nextcalibur.Core/` — hardware, power and settings. Finished and tested
  against real hardware.
- `src/Nextcalibur.App/MainWindow.xaml.cs` — the code-behind. It is written and
  working. Your XAML must fit it, not the other way round.
- `src/Nextcalibur.App/App.xaml.cs`, `TrayPresence.cs`, `Controls/ColourWheel.cs`
- `src/Nextcalibur.Cli/`

If your layout seems to need a change in the code-behind, stop and say which
change and why. Do not make it.

## The contract that matters most

The code-behind finds controls by name and subscribes to handlers by name. Every
name below must exist, with the stated type, or the build breaks.

### Named elements

| `x:Name` | Type | Notes |
|---|---|---|
| `TitleBar` | `Grid` | drag surface |
| `NavSystem`, `NavPower`, `NavLighting` | `RadioButton` | `GroupName="Nav"` |
| `PageSystem`, `PagePower`, `PageLighting` | `Grid` | shown one at a time |
| `SubtitleText` | `TextBlock` | live status line |
| `Banner` | `Border` | starts `Collapsed` |
| `BannerTitle`, `BannerBody` | `TextBlock` | inside `Banner` |
| `CpuTemp`, `GpuTemp`, `CpuFan`, `GpuFan` | `TextBlock` | code sets `.Text` and `.Foreground` |
| `OverlayState`, `OverlayDetail` | `TextBlock` | power page |
| `FixButton` | `Button` | starts `Collapsed` |
| `TabZoneA`, `TabZoneB`, `TabZoneC` | `RadioButton` | `GroupName="Zone"`, `Tag` = `Left`/`Middle`/`Right` |
| `PreviewA`, `PreviewB`, `PreviewC` | `Border` | code sets `.Background` and `.BorderBrush` |
| `ProfileRow` | `Panel` | dimmed as a unit |
| `ProfOffice`, `ProfGaming`, `ProfPerformance`, `ProfUser` | `RadioButton` | `GroupName="Profile"`, `Tag` = `Office`/`Gaming`/`Performance`/`UserDefine` |
| `LedPower`, `SelectAll` | `ToggleButton` | pill switches |
| `ReloadButton` | `Button` | |
| `EffectPanel` | `Panel` | dimmed as a unit |
| `FxStatic`, `FxBreathing`, `FxBlink`, `FxHeartbeat`, `FxCycle`, `FxWave` | `RadioButton` | `GroupName="Fx"` |
| `Wheel` | `ColourWheel` | existing control, do not replace |
| `WheelHint` | `TextBlock` | starts `Collapsed` |
| `BrightnessSlider` | `Slider` | `Minimum=0 Maximum=100 TickFrequency=10 IsSnapToTickEnabled=True` |
| `BrightnessValue` | `TextBlock` | |

### Wired handlers

Attach exactly these, spelled exactly:

```
TitleBar          MouseLeftButtonDown = OnTitleBarDrag
minimise Button   Click                = OnMinimiseClick
close Button      Click                = OnCloseClick
Nav*              Checked              = OnNavChanged
FixButton         Click                = OnRepairClick
TabZone*          Checked              = OnZoneTabChanged
Prof*             Checked              = OnProfileChecked
LedPower          Click                = OnLedPowerToggled
SelectAll         Click                = OnSelectAllToggled
ReloadButton      Click                = OnReloadClick
Fx*               Checked              = OnEffectChecked
Wheel             ColourPicked         = OnColourPicked
BrightnessSlider  ValueChanged         = OnBrightnessChanged
```

### Brush keys the code-behind looks up by name

`Ink`, `Muted`, `Accent`, `Good`, `Warn`, `Bad` must all exist as
`SolidColorBrush` resources reachable from the window. Keep the keys; change the
colours freely.

## What is different from the reference

The reference shows data this application does not have. **Do not draw
placeholder numbers for data we cannot read.** Omit those parts.

| Reference element | Our situation |
|---|---|
| CPU / GPU clock and utilisation | not read — omit |
| Pentagon radar chart | belongs to system modes, which do not exist yet — omit |
| Top mode tabs (Office / Gaming / High Performance) | same — omit |
| Display Mode page with three GPU cards | no API for it — omit, but leave rail room for a fourth item later |
| RAM and SSD donut gauges | **keep** — these will be fed real values |
| Fan gauges, CPU/GPU temperature | **keep** — real hardware readings |

Two pages have no reference equivalent. Design them in the same language:

- **Power** — a large statement of the current Windows power mode, explanatory
  body text, and a Repair button that is normally collapsed.
- The **System** page centre, without its radar chart, should carry the two fan
  gauges at a larger size plus the CPU and GPU temperatures. Fill the space
  properly; do not leave a hole where the chart was.

Right column on the System page, following the reference's structure:

- A chamfered panel header reading "CPU & GPU", containing a row per device:
  status dot, name, large temperature, and a segmented bar scaled to 100 °C.
- A second chamfered header reading "Memory & Disk" with the two donut gauges,
  RAM and SSD, matching the reference.

## Answers to your open questions

1. **Hover states.** Nav rail: surface lifts to `rgba(255,255,255,0.05)`, no
   accent bar. Caption buttons: minimise gets a neutral `#3A3E48` plate, close
   gets `#E81123` with a white glyph. Tabs, effect and profile buttons: a subtle
   surface lift only — never a colour change, so hover is not confused with
   selection.
2. **Fan gauge rotation: no.** Draw the turbine as a static graphic with live
   text. This application exists partly because the vendor's pins the CPU at
   full speed; it will not spend cycles animating a decoration. The same applies
   everywhere — no continuous animation. State transitions may use a short fade,
   150 ms at most.
3. **Window sizing: strictly fixed.** `Width="1024" Height="556"`,
   `ResizeMode="NoResize"`, `WindowStyle="None"`, `AllowsTransparency="True"`.
   Design to the pixel; no `ScrollViewer` used to escape fitting the content.
   Square corners, matching the reference.
4. **Colour wheel re-tints the keyboard: yes.** The code-behind already does it.
   Give `PreviewA/B/C` a plain `Background` the code can set.

## Brand and assets

Match the palette, the geometry, the spacing and the state behaviour. Do **not**
reproduce the vendor's own assets:

- **Logo**: draw an original mark for the title bar at the same size and position
  the analysis measured. Do not redraw their shield crest.
- **Wordmark**: the text is `NEXTCALIBUR  CONTROL CENTER`, in the analysed style.
- **Header graphic**: design your own abstract circuit-trace field as XAML
  geometry — angled traces, junction nodes, the same copper palette, the same
  footprint and gradient fade. Original artwork in the same spirit; not a trace
  of theirs.
- **Icons**: redraw every one from the subject descriptions in §6 of the
  analysis, as `Geometry` in `Icons.xaml`. Nothing extracted from their binaries.

We need six effect icons, not four. The analysis covers Static, Breathing,
Colorful cycle and Ambilight. Design two more in the same language:

- **Blink** — a square wave; the light is hard on and hard off.
- **Heartbeat** — an ECG trace with a double pulse.

Rename **Ambilight** to **Wave**: it is a firmware wave across the keyboard and
does not sample the screen. Keep its icon subject.

## Enable and disable rules — build these into the templates

Every interactive style needs an `IsEnabled=False` trigger. The analysis
measured the disabled state precisely; follow it:

- Lighting off: zone tabs, keyboard illustration, profile dock, Select all,
  Reload, all effect buttons, brightness slider and its readout all drop to
  **50% opacity**. The colour wheel goes further, to about **29%**.
- The `LED on/off` toggle itself, the title bar and the nav rail stay at
  **100%** and stay interactive.
- When the effect is Colour cycle or Wave, the wheel alone disables. `WheelHint`
  becomes visible beneath it.

The code-behind sets `IsEnabled` and `Visibility`; your templates must respond
to them visibly.

## Definition of done

- `dotnet build Nextcalibur.sln -c Release` — clean, zero warnings.
- Every name and handler in the contract present and spelled correctly.
- Window opens at exactly 1024×556 and cannot be resized.
- Every icon is `Geometry` in our own source; no image file from the vendor.
- No continuous animation anywhere.
- `Nextcalibur.Core`, `MainWindow.xaml.cs`, `App.xaml.cs`, `TrayPresence.cs` and
  `ColourWheel.cs` unchanged — verify with `git diff --stat`.

## A note on the project

`UseWindowsForms` is on for the tray icon, which makes many type names ambiguous
between WPF and WinForms. `Nextcalibur.App.csproj` already aliases the WPF ones.
If you hit `CS0104`, add another alias there rather than fully qualifying at
every call site.

---

## Additional deliverable: the logo

Design an original brand mark for this product and deliver it in three forms.
All three must be recognisably the same mark.

### Design constraints

- **Name**: Nextcalibur. A coined word — "next" joined to a legendary blade.
  A forward chevron, a blade profile, or an angular arrow are all natural
  directions.
- **Do not resemble the reference's mark.** Theirs is a downward shield crest
  with a chevron cutout. Avoid the shield-crest silhouette entirely; find a
  different form.
- **Single weight, single colour.** The title-bar mark is pure white on dark.
  The mark must work as one flat colour with no gradient, no inner detail that
  depends on a second tone.
- **It must read at 16 pixels.** This is the binding constraint, not the 256px
  version. If a detail disappears at 16px, remove it from the design rather than
  keeping it and hoping. Test this before you commit.
- Same industrial, geometric, hard-edged family as the rest of the interface.

### Form 1 — in-app mark

A `Geometry` resource in `Themes/Icons.xaml`, keyed `LogoMark`.

Nominal 41 × 30 px in the title bar, at the position the analysis measured
(`x = 29`, vertically centred). Drawn as a `Path` filled with the primary text
brush so the code can recolour it. Do not bake a colour into the geometry.

### Form 2 — application icon

Replace `src/Nextcalibur.App/Assets/app.ico`.

This one file is the executable icon, the taskbar icon, the notification-area
icon and the installer icon — everything is already wired to it, so replacing
the file is enough. Requirements:

- Sizes in a single `.ico`: **16, 20, 24, 32, 48, 64, 256**.
- 32-bit BGRA with a real alpha channel. No white or magenta matte.
- At 16, 20 and 24 the mark may be **simplified** — thicker strokes, dropped
  detail, tighter margins. A separate simplified geometry for the small sizes is
  the right answer if the full mark does not survive.
- Square canvas with the mark inset by roughly 8%, so it is not clipped by the
  rounded masks Windows applies in some surfaces.

**Do not commit a binary you cannot account for.** Write a small generator —
`tools/Build-Icon.ps1` or `tools/build_icon.py` — that renders the icon from the
same geometry and writes the `.ico`. Commit the script and the generated file
together. This keeps the artwork reproducible and demonstrably ours, which
matters because the repository is public.

### Form 3 — repository image

`docs/logo.png`, 512 × 512, transparent background, the mark in white with the
accent colour where the design calls for it. For the README and the GitHub
social preview.

### Where the mark is used

| Surface | Form | Notes |
|---|---|---|
| Title bar | `LogoMark` geometry | 41 × 30, white |
| Executable and taskbar | `Assets/app.ico` | via `<ApplicationIcon>` |
| Notification area | `Assets/app.ico` | 16px variant carries this |
| Installer | `Assets/app.ico` | passed to the packaging tool |
| README | `docs/logo.png` | |

Do not add the mark to the navigation rail or repeat it inside the content
area — once in the title bar is enough.
