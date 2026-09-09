# UI specification — Nextcalibur Control Center

A brief for rebuilding the interface. Written for a coding agent working with
reference screenshots of the vendor software.

---

## 1. What you are building

`Nextcalibur.App` is a WPF desktop app that replaces the Casper Excalibur
Control Center on Tongfang JS970-class laptops. The hardware layer is finished,
tested against real hardware, and **must not be changed**. Your job is the
presentation layer only.

```
src/Nextcalibur.Core/     hardware + power + settings   -- DO NOT MODIFY
src/Nextcalibur.App/      WPF application               -- your work
src/Nextcalibur.Cli/      command line                  -- leave alone
```

Build with `dotnet build Nextcalibur.sln -c Release`. Target is `net8.0-windows`,
WPF with `UseWindowsForms=true` (needed for the tray icon).

## 2. Hard constraints

**Window is fixed size and cannot be resized.**
`Height="610" Width="1120"`, `ResizeMode="NoResize"`. Every layout must be
designed for exactly this size. No `ScrollViewer` as a way to avoid fitting the
content. No `MinWidth`/`MinHeight`. Design to the pixel.

**Copyright.** Match the vendor's *layout, control types, states and interaction
behaviour* — those are functional and fair to reimplement. Do **not** copy:

- their logo, wordmark, or any brand graphic
- their icon artwork traced or extracted from their binaries
- the circuit-board header illustration
- any image, font, or asset file from their installation

Draw every icon yourself as WPF `Path` geometry. Fonts must be system fonts
(Segoe UI). The title bar reads "Nextcalibur Control Center" — never "Casper" or
"Excalibur". A visual family resemblance achieved with your own assets is the
goal; asset-level duplication is not.

**Do not invent hardware calls.** Use only the API in §6. If a control seems to
need something that API does not offer, leave the control out and say so — do
not write to the firmware mailbox directly. Wrong values there have real
consequences.

## 3. Visual language to match

From the reference screenshots:

- Near-black background, panels a step lighter, thin cool-grey separators.
- One saturated accent colour used for every active state: selected nav item,
  selected tab, filled toggle, active effect, gauge arcs. The vendor uses
  orange; **use our accent `#4C8DFF`** so the app is visibly its own product.
- Section headers sit in small angled "tab" shapes above their panel.
- Selected items are shown by accent-coloured icon *and* label together, not by
  a background fill alone.
- Values are large and light-weight; their labels are small, grey, uppercase.
- Rounded corners throughout, roughly 6–10px.
- Disabled controls drop to roughly 30% opacity and stop responding.

## 4. Screens

The rail has three items. A fourth, Display, is planned and not yet built —
leave room for it.

### 4.1 Navigation rail (left, ~112–148px, full height)

Vertical stack of items, each an outline icon above a label:

| Item | Label | Shows |
|---|---|---|
| 1 | System | sensors page |
| 2 | Power | power-overlay page |
| 3 | Lighting | keyboard lighting page |

One is always selected. Selected: accent icon and label, slightly lighter row
background. Hover: subtle background lift.

A product wordmark sits at the top of the rail. Keep it typographic — no logo
image.

### 4.2 System page

Match the reference's System mode page in structure:

- **Fan gauges**, bottom-left area: two circular gauges side by side, RPM
  numeral in the centre, a ring drawn as an arc. Labels "CPU FAN", "GPU FAN".
  Scale the arc against a 6000 RPM maximum.
- **Right column**, "CPU & GPU" panel: a row per device with a coloured dot, the
  device name, a large right-aligned temperature, and a segmented bar underneath
  showing that temperature against a 100 °C scale.
- The reference shows CPU/GPU clock and utilisation, and a Memory & Disk panel.
  **We do not read those yet** — omit them rather than showing placeholder
  numbers. Do not fabricate values.
- The radar chart on the reference belongs to system modes, which we have not
  implemented. Omit it.

Temperature colouring: below 80 °C normal ink, 80–89 °C warning, 90 °C and above
danger. Use the existing brush resources `Ink`, `Warn`, `Bad`.

Keep the existing warning banner (`Banner`, `BannerTitle`, `BannerBody`) — it
tells the user when the vendor software is running and competing for the
mailbox.

### 4.3 Power page

No equivalent in the reference; design it in the same visual language.

- Large statement of the current Windows power mode.
- Body text explaining the diagnosis.
- A "Repair" button, visible **only** when `OverlayDiagnosis.NeedsRepair` is true.
- Healthy state uses the `Good` brush; a problem uses `Warn`.

### 4.4 Lighting page — match the reference closely

Layout, left to right:

**Zone tabs**, top-left: `Zone A`, `Zone B`, `Zone C`. Mutually exclusive.
Selected gets accent border and lighter fill.

**Keyboard preview**, centre: draw a laptop keyboard as vector shapes — an
outer body, a key grid, and three colour regions left/middle/right. Each region
is filled with that zone's current colour. The selected zone is outlined in the
accent colour; with "Select all" on, all three are outlined.

Draw this yourself from primitives. Do not trace their illustration.

**Profile buttons**, bottom-centre: `Office`, `Gaming`, `Performance`,
`User Define`, each an icon above a label, mutually exclusive, accent when
selected.

**Control panel**, right column, top to bottom:

1. Row of three: `LED on/off` pill toggle, `Select all` pill toggle, `Reload`
   button. Pills show "on"/"off" text inside the track and slide a knob.
2. Effect buttons in a row — **six**, as labelled icons, never a dropdown:
   `Static`, `Breathing`, `Blink`, `Heartbeat`, `Colour cycle`, `Wave`.
   Mutually exclusive. Blink and Heartbeat have no equivalent in the vendor UI;
   include them, the hardware supports them.
3. HSV colour wheel — use the existing `Controls/ColourWheel`. Hue around the
   circumference, saturation centre-to-edge, a ring border and a white selector
   dot. Do not replace it; restyle its container if needed.
4. Brightness: the word "Brightness" on the left, a large percentage on the
   right, and below them a slider from 0 to 100 snapped to steps of 10.

## 5. Behaviour — this is the important part

### 5.1 Enable and disable rules

| Condition | Effect |
|---|---|
| `LED on/off` is **off** | every other control on the lighting page disables and dims: zone tabs, Select all, Reload, all effect buttons, colour wheel, brightness slider, profile buttons. The lights physically go out. |
| Effect is `Colour cycle` or `Wave` | the **colour wheel alone** disables. Everything else stays live. Show a one-line note saying the effect drives its own colours in firmware. |
| Any other effect | wheel enabled. |
| Firmware interface missing | disable the Lighting rail item entirely and show the banner. The Power page must still work. |

The vendor software greys the whole panel when LED is off — match that.

### 5.2 What each control does

| Control | On interaction |
|---|---|
| Zone tab | changes which zone the wheel edits. No hardware write. Update the preview outline and move the wheel's selector to that zone's colour. |
| `Select all` on | wheel edits all three zones at once (`LedZone.AllKeyboard`). Outline all three in the preview. No hardware write on toggle itself. |
| Colour wheel | `SetColour(target, r, g, b)` where target is `AllKeyboard` when Select all is on, else the selected zone. Then refresh the preview. |
| Effect button | `SetEffect(effect)`. Re-evaluate the wheel's enabled state **before** writing. |
| Brightness slider | `SetBrightness(percent)`. Update the percentage text on every change, including while dragging. |
| `LED on/off` | `SetEnabled(bool)`. Re-evaluate the whole panel's enabled state. |
| Profile button | `SetProfile(name)` then reload every lighting control from `State`. Edits always land in the active profile. |
| `Reload` | `Apply()` — re-sends the stored state to the hardware. Use when something else has disturbed the lighting. |
| `Repair` (Power page) | `Repair()`, then re-run `Diagnose()` and refresh. Catch `UnauthorizedAccessException` and tell the user to run elevated. |

### 5.3 Two things that will break if you get them wrong

**Suppress events while populating.** Assigning `IsChecked` or `Slider.Value`
raises the same events a click does. The existing code guards this with a
`_ledUiReady` flag set false during population and true afterwards. Keep that
pattern or an equivalent, or loading a profile will write to the hardware once
per control.

**Pause sensor sampling around lighting writes.** Sensors and lighting share one
firmware mailbox. A sensor read landing between zone writes makes the hardware
drop them. The existing `RunLighting(Action)` helper stops the timer, runs the
change, and restarts it. Route every lighting write through it.

### 5.4 Tray and lifecycle — keep working

- Closing the window hides to the notification area; it does not exit. Exit is
  in the tray menu.
- Sampling stops while the window is hidden or minimised. A separate 10-second
  timer updates the tray tooltip and raises the overheat warning.
- `--tray` on the command line starts hidden.

## 6. API you may call

Everything below already exists and is tested. Signatures are exact.

```csharp
// Nextcalibur.Core.Hardware
static bool EcMailbox.IsSupported();
new EcMailbox();                       // throws EcMailboxUnavailableException
void EcMailbox.Dispose();

enum LedZone : uint { Everything = 0, Right = 3, Middle = 4, Left = 5, AllKeyboard = 6 }
//   Left = Zone A, Middle = Zone B, Right = Zone C. Indices run backwards; this
//   is correct and confirmed on hardware.

enum LedEffect : byte { Off = 0, Static = 1, Blink = 2, Breathing = 3,
                        Heartbeat = 4, ColourCycle = 6, Wave = 7 }

class LedController(EcMailbox mailbox, LedState? state = null)
{
    const int BrightnessStep = 10;
    LedState State { get; }
    void SetColour(LedZone zone, byte r, byte g, byte b);
    void SetEffect(LedEffect effect);
    void SetBrightness(int percent);     // rounded to nearest 10
    void SetEnabled(bool enabled);
    void SetProfile(string name);
    void TurnOff();
    void Apply();
}

class ThermalReader(EcMailbox mailbox)
{
    ThermalSample Read();                // throws on failure
    bool TryRead(out ThermalSample s);   // prefer this in UI code
}
readonly record struct ThermalSample(
    int CpuTemperatureC, int GpuTemperatureC,
    int CpuFanRpm, int GpuFanRpm, DateTimeOffset Timestamp);

// Nextcalibur.Core.Configuration
class LedState
{
    const string Office, Gaming, Performance, UserDefine;
    static readonly string[] ProfileNames;
    bool Enabled { get; set; }
    string ActiveProfile { get; set; }
    LedEffect Effect { get; set; }
    int BrightnessPercent { get; set; }
    (byte R, byte G, byte B) GetColour(LedZone zone);
    void SetColour(LedZone zone, byte r, byte g, byte b);
}

class AppSettings
{
    int PollIntervalMs;          // default 2000
    bool MinimiseToTray;
    bool StartMinimised;
    int CpuWarningTemperatureC;  // default 90
    static AppSettings Load();
    void Save();
}

static class StartupRegistration
{
    static bool IsEnabled { get; }
    static void Set(bool enabled, string executablePath);
}

// Nextcalibur.Core.Power
class PowerOverlayService
{
    OverlayDiagnosis Diagnose();
    IReadOnlyList<string> Repair();      // returns what it changed
    void RemoveGuard();
}
readonly record struct OverlayDiagnosis(
    Guid ActiveOverlay, int? MinProcessorStateOverride, int? ProvisionedMinProcessorState)
{
    bool OverlayIsStuck { get; }
    bool GuardMissing { get; }
    bool NeedsRepair { get; }
}
static string PowerOverlays.Describe(Guid overlay);

// Nextcalibur.App.Controls
class ColourWheel : Control
{
    Color SelectedColour { get; set; }
    event EventHandler<Color>? ColourPicked;   // user picks only, not code-set
}
```

There is **no API for fan control or GPU mode switching.** Both are deliberately
absent. Do not add controls for them.

## 7. Gotchas in this project

`UseWindowsForms` is on for the tray icon, which makes many type names ambiguous
between WPF and WinForms. The project file already aliases the WPF ones
(`Application`, `MessageBox`, `Color`, `Control`, `Image`, `Size`, `Point`,
`Brushes`, `RadioButton`, `MouseEventArgs`, …). If you hit `CS0104`, add another
alias in `Nextcalibur.App.csproj` rather than fully qualifying at every call
site.

A custom `Main` lives in `App.xaml.cs` so Velopack's install hooks run before any
UI exists. `App.xaml` is therefore compiled as a `Page`, not an
`ApplicationDefinition`. Leave that alone.

## 8. Done means

- Builds clean: `dotnet build Nextcalibur.sln -c Release`, zero warnings.
- Window opens at exactly 1120×610 and cannot be resized.
- Every rule in §5.1 verified by hand.
- Switching profiles does not write to hardware once per control.
- No file from the vendor installation is referenced, embedded, or copied.
- Every icon is `Path` geometry in our own source.
