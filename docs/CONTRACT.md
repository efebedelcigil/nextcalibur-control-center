# Interface contract

The code-behind finds controls by `x:Name` and subscribes to handlers by name.
Every name here must exist in `MainWindow.xaml`, spelled exactly, with the stated
type. A single misspelling breaks the build.

The markup fits the code, not the other way round. If a layout appears to need a
change in the code-behind, stop and report which change and why.

---

## Named elements

### Window and navigation

| `x:Name` | Type | Notes |
|---|---|---|
| `TitleBar` | `Grid` | drag surface |
| `NavSystem`, `NavPower`, `NavDisplay`, `NavLighting` | `RadioButton` | `GroupName="Nav"` |
| `PageSystem`, `PagePower`, `PageDisplay`, `PageLighting` | `Grid` | one visible at a time |

### System page

| `x:Name` | Type | Notes |
|---|---|---|
| `ModeOffice`, `ModeGaming`, `ModePerformance` | `RadioButton` | `GroupName="Mode"`, `Tag` = `Office` / `Gaming` / `Performance` |
| `SubtitleText` | `TextBlock` | live status line |
| `Banner` | `Border` | starts `Collapsed` |
| `BannerTitle`, `BannerBody` | `TextBlock` | inside `Banner` |
| `CpuTemp`, `GpuTemp`, `CpuFan`, `GpuFan` | `TextBlock` | code sets `.Text` and `.Foreground` |
| `CpuClock`, `GpuClock` | `TextBlock` | live clock speed, e.g. `2.14 GHz`; empty when unreadable |
| `CpuName`, `GpuName` | `TextBlock` | model read from the system at startup |

### Power page

| `x:Name` | Type | Notes |
|---|---|---|
| `PowerModeEfficiency`, `PowerModeBalanced`, `PowerModeBetter`, `PowerModeBest` | `RadioButton` | `GroupName="PowerMode"`, `Tag` = `Efficiency` / `Balanced` / `Better` / `Best` |
| `PowerModeEfficiencyText`, `PowerModeBalancedText`, `PowerModeBetterText`, `PowerModeBestText` | `TextBlock` | empty in markup; the code writes them |
| `OverlayState`, `OverlayDetail` | `TextBlock` | status strip below the cards |
| `FixButton` | `Button` | starts `Collapsed` |

### Display page

| `x:Name` | Type | Notes |
|---|---|---|
| `ModeDiscrete`, `ModeHybrid`, `ModeUma` | `RadioButton` | `GroupName="Gpu"`, `Tag` = `Discrete` / `Hybrid` / `Uma` |
| `GpuModeDetail` | `TextBlock` | full-width explanation |

### Lighting page

| `x:Name` | Type | Notes |
|---|---|---|
| `TabZoneA`, `TabZoneB`, `TabZoneC` | `RadioButton` | `GroupName="Zone"`, `Tag` = `Left` / `Middle` / `Right` |
| `PreviewA`, `PreviewB`, `PreviewC` | `Border` | zone regions of the keyboard illustration; the code sets `.Background` to that zone's **live colour** on every lighting change, and `.BorderBrush` to the accent when selected. Paint each zone from its own `Background` — an unselected zone is that colour dimmed, never grey. |
| `ProfileRow` | `Panel` | dimmed as a unit |
| `ProfOffice`, `ProfGaming`, `ProfPerformance`, `ProfUser` | `RadioButton` | `GroupName="Profile"`, `Tag` = `Office` / `Gaming` / `Performance` / `UserDefine` |
| `LedPower`, `SelectAll` | `ToggleButton` | pill switches |
| `ReloadButton` | `Button` | |
| `EffectPanel` | `Panel` | dimmed as a unit |
| `FxStatic`, `FxBreathing`, `FxBlink`, `FxHeartbeat`, `FxCycle`, `FxWave` | `RadioButton` | `GroupName="Fx"` |
| `Wheel` | `ColourWheel` | existing control, do not replace |
| `WheelHint` | `TextBlock` | starts `Collapsed` |
| `BrightnessSlider` | `Slider` | `Minimum=0 Maximum=100 TickFrequency=10 IsSnapToTickEnabled=True` |
| `BrightnessValue` | `TextBlock` | |

### Theme

| `x:Name` | Type | Notes |
|---|---|---|
| `ThemeDark`, `ThemeLight`, `ThemeSystem` | `RadioButton` | `GroupName="Theme"`, `Tag` = `Dark` / `Light` / `System` |

## Handlers

```
TitleBar             MouseLeftButtonDown = OnTitleBarDrag
minimise Button      Click               = OnMinimiseClick
close Button         Click               = OnCloseClick
Nav*                 Checked             = OnNavChanged
Mode*                Checked             = OnSystemModeChanged
PowerMode*           Checked             = OnPowerModeChanged
ModeDiscrete/Hybrid/Uma  Checked         = OnGpuModeChanged
Theme*               Checked             = OnThemeChanged
FixButton            Click               = OnRepairClick
TabZone*             Checked             = OnZoneTabChanged
Prof*                Checked             = OnProfileChecked
LedPower             Click               = OnLedPowerToggled
SelectAll            Click               = OnSelectAllToggled
ReloadButton         Click               = OnReloadClick
Fx*                  Checked             = OnEffectChecked
Wheel                ColourPicked        = OnColourPicked
BrightnessSlider     ValueChanged        = OnBrightnessChanged
```

## Brush keys

`Ink`, `Muted`, `Accent`, `Good`, `Warn`, `Bad` must exist as `SolidColorBrush`
resources reachable from the window, **in every palette**. The code looks them up
by name and a missing key throws.

## Two rules that fail silently

**Nothing selected in the markup.** No `IsChecked="True"` on any mode, profile,
theme or GPU option. The code reads the machine's actual state and sets the
current one. A default in the markup fires a hardware write before the
application knows what the machine is set to.

**Controls must not cache brushes.** Drawing controls that read a colour in C#
need a dependency property bound with `DynamicResource`. A `Brush` stored in a
field at construction will not follow a theme change — the fan gauge hub was
exactly this bug.

The same trap exists in code: assigning a `Brush` to `Foreground` sets a local
value that outranks the style and survives a palette swap. Use
`SetResourceReference` instead. This one caught the code-behind too.

### Memory and disk

| `x:Name` | Type | Notes |
|---|---|---|
| `RamGauge`, `SsdGauge` | `DonutGauge` | code sets `.Value` |
| `RamPercent`, `SsdPercent` | `TextBlock` | the large reading |
| `RamDetail`, `SsdDetail` | `TextBlock` | the capacity line |

No hardware model may appear in the markup, and no placeholder number. The
repository is public: a hardcoded name would be wrong on every other machine and
would publish a detail of the author's own. Leave every reading empty; the code
fills them.
