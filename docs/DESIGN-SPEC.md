# Laptop Control Center — Visual Design Specification

> **Document Type:** Visual Analysis & Design System Specification  
> **Source Resolution:** Native 1024 × 556 px (Aspect Ratio ~1.84:1 / 128:69.5)  
> **Target Platform:** WPF (XAML) Desktop Application  
> **Scope:** Analysis only (No code)

---

## 1. Window and Frame

| Property | Specification |
| :--- | :--- |
| **Window Dimensions** | `1024 px` width × `556 px` height (client area) |
| **Aspect Ratio** | `128:69.5` (~`1.84:1`, widescreen custom desktop utility) |
| **Window Frame Type** | Fully custom frameless/borderless chrome (no standard Windows DWM title bar, no Aero/Mica frame) |
| **Window Corner Radius** | `0 px` (square, sharp 90° corners at all four vertices: `(0,0)`, `(1023,0)`, `(0,555)`, `(1023,555)`) |
| **Window Outer Border / Glow** | None. Outer boundary is a flush `1 px` cut edge without drop shadows or external OS glow |
| **Title Bar Height** | `56 px` (spans `y = 0` to `y = 55`) |
| **Title Bar Separation** | `1 px` horizontal separator line at `y = 55` (`#1C1E26`), separating chrome from content (`y = 56+`) |

### Title Bar Construction (Left to Right)

```
+-------------------------------------------------------------------------------------------------------+
| [Logo: 41×30] [10px] [Title: 202×12] ---------------- [Circuit Artwork: ~600×56] ---- [Min] [7px] [X] |
+-------------------------------------------------------------------------------------------------------+
```

1. **Brand Logo Mark**
   - **Position**: `x = 29` to `69` (`w = 41 px`), `y = 12` to `41` (`h = 30 px`).
   - **Center Anchor**: `(49, 26.5)`.
   - **Visual Style**: High-contrast pure white (`#FFFFFF`) silhouette emblem (downward-pointing shield crest with central chevron cutout).
2. **Title Wordmark**
   - **Position**: `x = 68` to `269` (`w = 202 px`), `y = 20` to `31` (`h = 12 px`).
   - **Visual Style**: Bold geometric all-caps sans-serif, letter-spaced `+2.5 px`, pure white (`#FFFFFF`).
3. **Background Surface & Decorative Banner**
   - **Left side** (`x = 0` to `~420`): Flat dark slate grey (`#272A34`).
   - **Center-to-right** (`x = 420` to `1023`): Digital circuit trace graphic in glowing amber/copper (`#BC530E`, `#E25B0C`, `#753B1A`) over dark copper base (`#3D1E14`).
4. **Caption Buttons (Minimize & Close)**
   - **Hit Areas**:
     - Minimize: `x = 928` to `964` (`w = 36 px`), `y = 10` to `42` (`h = 32 px`). Center anchor: `(946, 27)`.
     - Close: `x = 971` to `1007` (`w = 36 px`), `y = 10` to `42` (`h = 32 px`). Center anchor: `(989, 27)`.
     - Button center-to-center spacing: `43 px` (gap between hit areas: `7 px`).
   - **Glyphs**:
     - Minimize: Horizontal line `16 px` wide, `2 px` height, centered at `y = 27`. Light silver-grey (`#CDD2D8`).
     - Close: Symmetric `✕` cross, `14 px` wide × `14 px` high, stroke weight `2 px`. Light silver-grey (`#CDD2D8`).
   - **Resting State**: Both buttons sit directly on top of the copper circuit artwork with no visible background plate or border.
   - **Hover / Pressed**: Circular soft highlight (`rgba(255, 255, 255, 0.1)` on minimize, `rgba(230, 40, 20, 0.7)` on close).

---

## 2. Colour Palette

### Surface & Structural Colors

| Role | Hex | RGB | Usage |
| :--- | :--- | :--- | :--- |
| **Window Background (Center)** | `#000000` / `#050507` | `0, 0, 0` | Deep pure black central canvas |
| **Navigation Rail Background** | `#0C0D11` | `12, 13, 17` | Left rail background |
| **Right Column Background** | `#0C0D11` / `#101114` | `12, 13, 17` | Right stats/controls panel background |
| **Title Bar Base Background** | `#272A34` | `39, 42, 52` | Dark slate base of the title bar |
| **Panel Background (Raised)** | `#1A1A1A` | `26, 26, 26` | Bottom fan panel & lighting profile dock |
| **Inactive Tab / Control Fill** | `#27292D` | `39, 41, 45` | Unselected top mode tabs, effect buttons |
| **Panel Header Fill** | `#2E1C15` to `#351E17` | `46, 28, 21` | Gradient fill inside chamfered panel headers |

### Borders & Separators

| Role | Hex | RGB | Usage |
| :--- | :--- | :--- | :--- |
| **Title Bar Divider** | `#1C1E26` | `28, 30, 38` | `1 px` line separating title bar from body |
| **Column Dividers** | `#1A1D24` | `26, 29, 36` | Vertical hairline dividers (`x = 130`, `x = 720`) |
| **Panel Accent Border** | `#74361C` / `#8C4C14` | `116, 54, 28` | Copper/orange bottom underline & panel border |
| **Subtle Control Border** | `#30343C` | `48, 52, 60` | Utility button borders, inactive tab outlines |
| **Radar Chart Grid Lines** | `#22242B` / `#363840` | `34, 36, 43` | Concentric pentagon rings and radial axis spokes |

### Brand & Functional Accent Colors

| Role | Hex | RGB | Usage / Semantic Meaning |
| :--- | :--- | :--- | :--- |
| **Primary Brand Accent (Core)** | `#FF7700` | `255, 119, 0` | Selected states, primary indicator dots |
| **Vibrant Orange (Text / Icon)** | `#EF5F08` / `#DE6217` | `239, 95, 8` | Active navigation label, active radio button fill |
| **Amber Orange (Highlight Glow)** | `#FFA000` / `#FF9E00` | `255, 160, 0` | Radar chart polygon stroke, RAM gauge fill arc |
| **Deep Copper Accent** | `#8D6A42` / `#6B3D08` | `107, 61, 8` | Active tab body gradient, header badge fill |
| **Data Gauge: Yellow-Amber** | `#FF9E00` to `#FF7700` | `255, 158, 0` | RAM utilization donut arc (`46.8%`) |
| **Data Gauge: Electric Blue** | `#0088FF` to `#0055FF` | `0, 136, 255` | SSD utilization donut arc (`52.5%`) |
| **Data Bar: Cyan-to-Blue** | `#00A6FF` to `#3E5BFF` | `0, 166, 255` | CPU / GPU segmented progress chevrons |
| **Thermal / Heatpipe Red** | `#E83818` | `232, 56, 24` | Laptop exhaust lightbar / rear cooling vent |
| **Status Dot (Indicator)** | `#FF7700` | `255, 119, 0` | Circular bullet preceding metric labels (`CPU`, `GPU`, `RAM`, `SSD`, `CPU FAN`) |

### Typography Colors

| Role | Hex | RGB | Usage |
| :--- | :--- | :--- | :--- |
| **Primary Text (Maximum)** | `#FFFFFF` | `255, 255, 255` | Title wordmark, active tab label, RPM values |
| **High-Emphasis Data Value** | `#E2E2E2` | `226, 226, 226` | Large percentages (`46.8%`, `52.5%`, `100%`) |
| **Primary Metric / Label** | `#D4D4D4` / `#DCDCDC` | `212, 212, 212` | Metric labels, frequencies (`3,79 GHz`) |
| **Panel Header Text** | `#D4CFCE` | `212, 207, 206` | Text inside chamfered panel headers |
| **Secondary Subtitle / Unit** | `#A3A4A7` | `163, 164, 167` | Total capacity strings (`15,1/31,7GB`), inactive profile labels |
| **Inactive / Muted Label** | `#707070` / `#777777` | `112, 112, 112` | Unselected navigation rail labels |

---

## 3. Typography

The type system utilizes a **geometric neo-grotesque sans-serif** family (consistent with *Eurostile*, *Microgramma*, or *Bank Gothic* for titles/values, paired with *Segoe UI* or *DIN* for data readouts).

### Typography Scale & Hierarchy

| Role | Font Size | Visual Weight | Casing | Letter Spacing | Color | Alignment | Numeral Style |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Title Wordmark** | `13 px` | Extra Bold / Heavy | ALL CAPS | `+2.5 px` (wide) | `#FFFFFF` | Left | N/A |
| **Navigation Item Label** | `12 px` | Medium (Active: SemiBold) | Title Case | Normal | `#EF5F08` (active) / `#777777` | Center | N/A |
| **Top Mode Tab Label** | `13 px` | SemiBold | Sentence Case | Normal | `#FFFFFF` (active) / `#A4A5A9` | Left/Center | N/A |
| **Panel Header Title** | `12 px` | Bold | Title Case | `+0.5 px` | `#D4CFCE` | Left | N/A |
| **Metric Name Label** | `12 px` | SemiBold | ALL CAPS | Normal | `#D9D9D9` | Left | N/A |
| **Frequency Value** | `12 px` | Medium | As-is | Normal | `#DADADA` | Right | Tabular (comma decimal) |
| **Small Usage Percentage** | `13 px` | Bold | As-is | Normal | `#E2E2E2` | Right | Tabular |
| **Large Value (Gauges)** | `24 px` – `26 px` | Heavy / Black | As-is | `-0.5 px` (tight) | `#E2E2E2` / `#FFFFFF` | Right | Tabular, squarish glyphs |
| **Secondary Capacity Sub** | `10 px` | Regular | As-is | Normal | `#A3A4A7` | Right | Tabular |
| **Fan Speed Value** | `12 px` | Bold | ALL CAPS | Normal | `#FFFFFF` | Center | Tabular |
| **Fan Speed Sub-label** | `11 px` | SemiBold | ALL CAPS | Normal | `#D4D4D4` | Center | N/A |
| **Radar Chart Axis Label** | `11 px` | Medium | Title Case | Normal | `#DCDCDC` | Radial outwards | N/A |
| **Toggle & Profile Labels** | `11 px` | Medium | Title Case | Normal | `#FFFFFF` (active) / `#A8A8AC` | Center | N/A |

### Numeral Characteristics
- **Decimal separators**: European format (comma: `,`, e.g. `46,8%`, `3,79 GHz`, `15,1/31,7GB`).
- **Alignment**: Numerals are strictly **tabular** (fixed-pitch digits) preventing layout jitter during live metric polling.
- **Glyph Style**: Large numerical percentages (`46.8%`, `52.5%`, `100%`) feature geometric, blocky, slightly squarish curves with wide apertures.

---

## 4. Layout and Spacing

```
+-----------------------------------------------------------------------------------------------+
|  Title Bar (56px) - Logo & Wordmark                           [Circuit]       [-]  [X]        |
+-------------+-------------------------------------------------------------+-------------------+
|  Nav Rail   |  Central Content Column (590px)                             |  Right Column     |
|  (130px)    |                                                             |  (304px)          |
|             |  System: Mode Tabs (50px)                                   |  CPU/GPU Stats    |
|             |          Radar Chart (305px)                                |  Panel (234px)    |
|             |  Display: 3 Mode Options (360px)                            |                   |
|             |  Lighting: Zone Tabs (25px) + Deck Graphic (340px)          |  Memory & Disk    |
|             |  ---------------------------------------------------------  |  Panel (245px)    |
|             |  Bottom Dock: Fan Gauges / Profile Buttons (125px)          |  [or Lighting]    |
+-------------+-------------------------------------------------------------+-------------------+
```

### Column Architecture

| Region | Horizontal Span | Width | Height | Outer Margins & Padding |
| :--- | :--- | :--- | :--- | :--- |
| **Window Frame** | `x = 0` to `1024` | `1024 px` | `556 px` | `0 px` |
| **Title Bar** | `x = 0` to `1024`, `y = 0` to `55` | `1024 px` | `55 px` | Left pad: `29 px`, Right pad: `17 px` |
| **Navigation Rail** | `x = 0` to `130`, `y = 56` to `556` | `130 px` | `500 px` | Internal pad: `10 px` horiz, `15 px` vert |
| **Central Column** | `x = 130` to `720`, `y = 56` to `556` | `590 px` | `500 px` | Margin: Left `10 px`, Right `10 px`, Bottom `10 px` |
| **Right Column** | `x = 720` to `1024`, `y = 56` to `556` | `304 px` | `500 px` | Margin: Left `12 px`, Right `16 px`, Bottom `12 px` |

### Vertical Rhythm & Recurring Units
- **Base Grid Unit**: `4 px` (recurring intervals: `4 px`, `8 px`, `12 px`, `16 px`, `24 px`, `32 px`, `48 px`).
- **Panel Gaps**: `10 px` to `12 px` uniform margin between adjacent panels.
- **Fixed vs Dynamic Across Pages**:
  - **Fixed Across All Pages**:
    - Window frame (`1024 × 556 px`) and entire Title Bar (`56 px`).
    - Left Navigation Rail (`130 px` width, 3 items).
    - Hairline dividers at `x = 130` and `x = 720`.
  - **Fixed Across System & Display Pages**:
    - Right statistics column (CPU/GPU Stats panel + Memory & Disk panel).
    - Bottom Fan Speed panel (`x = 137` to `703`, `y = 425` to `550`, height `125 px`).
  - **Page-Specific Content**:
    - **System Page**: Mode tabs at top, Pentagon radar chart in center.
    - **Display Page**: 3 large horizontal GPU mode selection cards in center.
    - **Lighting Page**: Complete replacement of center and right columns:
      - Center: Top zone tabs + full keyboard deck illustration + bottom profile dock.
      - Right: Master toggle switches + reload button + 4 effect buttons + 200px color wheel + brightness slider.

---

## 5. Component Anatomy

### 1. Navigation Rail Item
- **Overall Size**: `130 px` width × `148 px` height (3 items fill the `500 px` height evenly).
- **Separator**: Horizontal `1 px` divider at `y = 205` and `y = 352` (`#1A1D24`).
- **Internal Arrangement**: Centered column; icon centered at `y ≈ +45 px` relative to item top, label centered below at `y ≈ +100 px`.
- **States**:
  - *Default (Unselected)*: Icon outline in cool grey (`#96979A`), label in medium grey (`#777777`, `12 px`), no background glow.
  - *Selected (Active)*: Icon rendered in bright amber-orange (`#FF9E00`), accompanied by a soft radial orange glow (`#FF7700`, opacity `~25%`) behind the icon. Label text turns vibrant orange (`#EF5F08`). Background surface remains dark (`#0C0D11`).

### 2. Top Mode Tab (System Page)
- **Overall Size**: `w ≈ 184 px`, `h = 50 px`, top margin `y = 61` to `111`.
- **Shape & Border**: Rounded rectangle (`corner radius = 6 px`), `1 px` border (`#25272B` inactive, `#8C4C14` active).
- **Internal Layout**: Horizontal row; icon on the left (`24 × 24 px`), label on the right (`13 px`, SemiBold), centered horizontally with `10 px` gap.
- **States**:
  - *Default (Inactive)*: Dark charcoal gradient fill (`#27292D` to `#35373C`), light grey icon (`#A3A4A8`) and label (`#A4A5A9`).
  - *Selected (Active)*:
    - Background: Warm amber-orange radial/linear gradient (`#6B3D08` at top fading downward).
    - Top Accent: `2 px` bright orange/amber highlight strip along the top border (`#FFA000`).
    - Icon & Label: Pure white (`#FFFFFF`).
    - Glow: Ambient downward orange bloom radiating `~15 px` below the tab.

### 3. Zone Tab (Lighting Page)
- **Overall Size**: `w ≈ 80 px`, `h = 25 px` (Zone A, Zone B, Zone C horizontally arranged from `x = 157` to `453`).
- **Shape**: Flat rectangular tab with `1 px` border.
- **States**:
  - *Inactive*: Dark fill, border `#333333`, label `#909296`.
  - *Active (Zone A)*: Solid `2 px` orange underline (`#FF7700`) at bottom border, text pure white (`#FFFFFF`), subtle amber inner glow.

### 4. Pill Toggle Switch (LED on/off & Select all)
- **Overall Size**: Track width `44 px`, track height `20 px` (pill shape, `corner radius = 10 px`).
- **Text Label Position**: Label ("LED on/off", "Select all") placed below the switch (`y = 95` to `114`).
- **Construction & States**:
  - *Active (ON)*:
    - Track Fill: Deep orange (`#D5580C`).
    - Track Text: Miniature white label `"on"` (`9 px`, bold) positioned on the left half of the track (`x = 744` to `756`).
    - Thumb / Knob: Circular solid white (`#FFFFFF`) disc, diameter `16 px`, locked against the right edge (`x = 766` to `782`).
  - *Inactive (OFF)*:
    - Track Fill: Dark slate/charcoal (`#2B2D32`) with a subtle `1 px` border.
    - Track Text: Miniature white label `"off"` (`9 px`, bold) positioned on the right half of the track (`x = 758` to `774`).
    - Thumb / Knob: Circular solid white (`#FFFFFF`) disc, diameter `16 px`, locked against the left edge (`x = 736` to `752`).

### 5. Small Utility Button (Reload)
- **Overall Size**: `50 px` width × `36 px` height, centered at `x = 945`, `y = 75`.
- **Shape & Border**: Rounded rectangle (`corner radius = 4 px`), `1 px` border `#30343C`, dark fill `#1E2025`.
- **Icon**: Two curved circular reload/synchronize arrows forming a clockwise loop (`18 × 18 px`, stroke `1.5 px`, color `#A0A2A6`).
- **Label**: "Reload" placed directly below the button (`y = 100` to `114`, color `#D0D0D0`, `11 px`).

### 6. Effect Icon Button (Lighting Page)
- **Overall Size**: `w ≈ 55 px`, `h ≈ 50 px` (4 buttons arranged horizontally: Static, Breathing, Colorful cycle, Ambilight).
- **Arrangement**: Centered vertical stack (icon `~28 × 28 px` above, label `11 px` below).
- **States**:
  - *Inactive*: Outline icon in cool grey (`#B0B1B6`), label in muted grey (`#A3A4A7`).
  - *Active (Static)*: Multi-colored sunburst icon (magenta, orange, green, cyan ray tips around cyan center), label in crisp white (`#F6F6F7`), subtle container highlight.

### 7. Profile Button (Lighting Page Dock)
- **Overall Size**: `w ≈ 110 px`, `h ≈ 70 px` (4 buttons in bottom bar: Office, Gaming, Performance, User Define).
- **Arrangement**: Large icon (`36 × 36 px`) centered above text label (`11 px`).
- **States**:
  - *Inactive*: Outline icons in cool grey (`#B0B1B5`), labels in `#A8A8AC`.
  - *Active (User Define)*: Dual gear icon features an illuminated orange center accent ring (`#EF5F08`) inside the primary gear; label text turns bright orange (`#E06E29`).

### 8. Large Radio Option Card (Display Page)
- **Overall Size**: `w ≈ 150 px`, `h ≈ 160 px` (3 options: Discrete mode, MS hybrid mode, UMA mode).
- **Arrangement**: Large graphic illustration (`~100 × 100 px`) at top, radio selector + label (`24 px` height) below.
- **Radio Indicator & States**:
  - *Selected (Discrete mode)*:
    - Graphic: Full-color illuminated fan turbine with glowing orange blades (`#FF7700`) and "GPU" text in center.
    - Radio Button: Solid orange circular badge (`diameter = 18 px`, `#FF6B00`) with a white checkmark (`✓`) glyph inside.
    - Label: Bold pure white (`#FFFFFF`).
  - *Unselected (MS hybrid mode, UMA mode)*:
    - Graphic: Monochromatic metallic grey icon (`#808288`), unlit.
    - Radio Button: Hollow circular ring (`diameter = 18 px`, stroke `2 px`, color `#CCCCCC` / `#888888`), transparent center.
    - Label: Light grey (`#D0D0D0`).

### 9. Panel Header (Chamfered Corner)
- **Overall Size**: `w ≈ 220 px`, `h = 24 px`.
- **Geometric Construction**:
  - Flat bottom edge, vertical left edge.
  - **Top-right corner has a 45° diagonal chamfer (cut corner)**: extends from `y = 60, x = 931` to `y = 84, x = 959` (chamfer delta: `~28 px` horiz × `24 px` vert).
- **Fill**: Gradient fill from deep copper (`#351E17`) to dark brown (`#2E1C15`).
- **Bottom Accent Line**: Continuous `2 px` horizontal accent rule in dark copper/orange (`#74361C` / `#542B19`) extending along the bottom of the header.
- **Typography**: Header text indented `12 px` from left, vertically centered (`12 px`, Bold, `#D4CFCE`).

### 10. Circular Fan Gauge
- **Overall Size**: Outer diameter `104 px`, centered in the bottom dock (`CPU FAN` at `(335, 480)`, `GPU FAN` at `(585, 480)`).
- **Concentric Layering** (outside in):
  1. *Turbine Rotor Ring* (radius `24 px` to `52 px`): 12 curved forward-swept impeller fan blades in high-visibility safety orange (`#FF6C00` / `#FF8800`) against a pitch-black background.
  2. *Inner Hub Disc* (radius `0` to `24 px`): Solid dark charcoal circular plate (`#232427`).
  3. *RPM Value Readout*: Centered white bold text (`#FFFFFF`, `12 px`, e.g. `"5085 RPM"`, `"4400 RPM"`).
- **Sub-label**: `5 px` orange indicator dot (`#FF7700`) followed by label text (`"CPU FAN"` / `"GPU FAN"`, `#D4D4D4`, `11 px`) placed `15 px` below the gauge.

### 11. Donut Gauge (RAM & SSD)
- **Overall Size**: Gauge diameter `64 px`; outer bracket envelope `80 px`.
- **Layering & Geometry**:
  - *Outer Brackets*: Two concentric thin curved metallic brackets framing the circle on the left and right (`#C0C0C0` / `#8D8D8D`).
  - *Background Track*: Dark circular ring (`thickness = 8 px`, radius `24 px` to `32 px`, color `#252525`).
  - *Progress Arc*: Solid colored arc (`thickness = 8 px`, radius `24 px` to `32 px`):
    - RAM: Yellow-amber arc (`#FF9E00` to `#FF7700`) spanning from 12 o'clock clockwise to `~46.8%` (`168°`).
    - SSD: Cyan-blue arc (`#0088FF` to `#0055FF`) spanning from 12 o'clock clockwise to `~52.5%` (`189°`).
  - *Center Hub*: Solid dark circle (`radius = 22 px`, color `#2F2F2F`).
- **Associated Typography** (Right of gauge):
  - Metric name preceded by `5 px` orange dot (`● RAM` / `● SSD`).
  - Huge bold percentage (`46.8%`, `52.5%`, `26 px` height, `#E2E2E2`).
  - Secondary capacity sub-text (`14,9/31,7GB`, `733,0/1396,0G`, `10 px`, `#A3A4A7`).

### 12. Segmented Progress Bar (CPU & GPU Usage)
- **Overall Size**: Width `160 px`, height `7 px` (`y = 148..155` for CPU, `y = 245..252` for GPU).
- **Segment Geometry**:
  - Composed of **16 discrete parallelogram / chevron segments** angled forward at `~70°`.
  - Each segment width: `6 px`, segment gap: `4 px`.
- **States**:
  - *Unlit Segments*: Dark grey-black silhouette slots (`#1A1B1D`).
  - *Lit Segments*: Glowing gradient from electric cyan-blue to violet (`#00A6FF` to `#3E5BFF`). E.g., `10%` lit = 2 segments; `15%` lit = 3 segments.

### 13. Pentagon Radar Chart
- **Overall Size**: Bounding diameter `210 px` (radius `105 px`), center anchor `(425, 250)`.
- **Axes & Vertices** (5-axis spider plot):
  - Top (90°): *Noise Reduction*
  - Top-Right (18°): *Energy Saving*
  - Bottom-Right (306°): *CPU Performance*
  - Bottom-Left (234°): *GPU Performance*
  - Top-Left (162°): *Cooling*
- **Grid Structure**:
  - 4 concentric pentagon guide rings (`#22242B`) with outer concentric guide circles.
  - White circular vertex reference dots (`diameter = 5 px`, `#FFFFFF`) at max radius.
- **Polygon Representation**:
  - Stroke: `2 px` solid vibrant orange (`#FFA000` / `#FF7700`).
  - Fill: Semi-transparent amber/copper radial gradient (`#8B553C` with `~45%` alpha).
  - Multi-mode coordinate values observed:
    - *Office Mode*: High Noise Reduction (`83%`), Moderate Energy Saving (`63%`), Moderate Cooling (`53%`), CPU (`100%`), GPU (`73%`).
    - *Gaming Mode*: Low Noise Reduction (`65%`), Zero Energy Saving (`0%`), High Cooling (`76%`), CPU (`100%`), High GPU (`97%`).
    - *High Performance Mode*: Minimal Noise Reduction (`46%`), Zero Energy Saving (`0%`), Maximum Cooling (`90%`), CPU (`100%`), Max GPU (`100%`).

### 14. Colour Wheel & Selector
- **Overall Size**: Disc diameter `200 px` (radius `100 px`), outer bezel ring diameter `238 px`, center anchor `(858, 329)`.
- **Color Distribution**:
  - Polar HSV color wheel: Center is pure desaturated white (`#FFFFFF`).
  - Hue sweeps 360° at full saturation along the outer perimeter (Red at 9 o'clock, Yellow at 10:30, Green at 1:30, Cyan at 3 o'clock, Blue at 5 o'clock, Magenta at 7 o'clock).
- **Bezel**: Segmented dark bracket bezel at radius `110 px` to `118 px` (`#191919`).
- **Selector Indicator**: Small circular targeting cursor positioned over the currently selected color coordinate.

### 15. Brightness Slider
- **Overall Size**: Track width `235 px`, height `4 px` (spanning `x = 745` to `980`, `y = 531`).
- **Track**: Low-profile horizontal line (`#2A2A2A`).
- **Thumb**:
  - Circular handle (`diameter = 16 px`, `y = 524` to `538`).
  - Color: Glowing bright orange core (`#FFC189` / `#FFA000`) with a luminous ambient halo.
- **Value Readout**: Prominent `"100%"` in large bold type (`20 px` height, `#FFFFFF`) positioned directly above the right end of the track.

---

## 6. Iconography

### Visual Style & Language
- **Type**: Clean geometric **stroke outlines** (line weight `1.5 px` to `2.0 px`), with rounded caps and joins on complex shapes.
- **Nominal Canvas Size**:
  - Navigation Rail: `48 × 48 px` canvas (drawings occupy `~40 × 40 px` to `44 × 44 px`).
  - Top Tabs: `24 × 24 px` canvas.
  - Lighting Effects & Utility: `28 × 28 px` canvas.
  - Display Mode Cards: `96 × 96 px` illustrative badges.
- **Cohesion**: Industrial gaming aesthetic with consistent stroke weights, corner radii (`2 px`), and dual-tone state behavior.

### Detailed Subject Breakdown

| Icon Subject | Location | Description for Redrawing |
| :--- | :--- | :--- |
| **System Mode** | Nav Rail | Speedometer arc with 3 tick marks at the top-left, intersecting a gaming controller D-pad/buttons at the bottom, and a computer monitor outline at the right. |
| **Display Mode** | Nav Rail | Clamshell laptop open at 110°, screen divided into two panes: left pane shows a video camera icon, right pane shows a gamepad controller silhouette. |
| **LED Control** | Nav Rail | Circular dial with an internal needle and 8 radiating triangular/tapered sunburst light rays, overlaid with an angled laboratory pipette/eyedropper aiming at the center. |
| **Office Mode** | Top Tab / Dock | Miniature monitor/laptop silhouette with an embedded webcam glyph in the top bezel and 3 horizontal document lines on the screen. |
| **Gaming Mode** | Top Tab / Dock | Ergonomic console gamepad silhouette featuring a directional D-pad on the left, 4 action buttons on the right, and 3 central menu dots. |
| **Performance Mode**| Top Tab / Dock | Circular tachometer gauge with an active needle pinned to the redline / maximum mark (far right). |
| **User Define** | Lighting Dock | Two interlocking mechanical gears (one large gear at 7 o'clock with orange hub, one smaller gear at 2 o'clock). |
| **Reload** | Lighting Right | Two semicircular arrows arranged in a closed counter-clockwise circle with triangular arrowheads. |
| **Static Effect** | Lighting Right | Symmetrical circular hub with 8 radiating diamond/petal light rays in rainbow colors (cyan, lime, yellow, orange, magenta). |
| **Breathing Effect** | Lighting Right | Three horizontal flowing wind/breath stream lines with curved curled tails. |
| **Colorful Cycle** | Lighting Right | Two crossing infinity-style ribbon arrows looping past each other. |
| **Ambilight Effect**| Lighting Right | Four stacked sinusoidal / wave curves representing fluid light ripples. |
| **Discrete GPU** | Display Page | Stylized circular multi-blade cooling turbine with "GPU" stamped on the inner hub. |
| **MS Hybrid** | Display Page | GPU cooling turbine on the left connected to a CPU semiconductor chip on the right via two curved circular data flow arrows. |
| **UMA Mode** | Display Page | Square microprocessor package (die with outer connector pins) emblazoned with a central high-voltage lightning bolt. |

---

## 7. Decorative Treatment

### Top-Right Header Graphic
- **Subject**: Abstract digital printed circuit board (PCB) trace network, featuring copper/gold conductive traces, junction nodes/vias, and angled 45° trace bends.
- **Footprint**: Spans `x ≈ 420` to `1023` (`~600 px` width), occupying the full `56 px` height of the title bar.
- **Color & Opacity**: Vivid copper and neon amber (`#BC530E`, `#E25B0C`, `#753B1A`) fading into the dark slate base `#272A34` via a soft linear gradient mask (`x = 400` to `500`).

### Gradients, Glows, and Depth Signaling
- **Depth Paradigm**: **Flat layered surfaces with illuminated optical glows** (tactical HUD / cyberpunk hardware aesthetic). No skeuomorphic drop shadows; depth is achieved via:
  1. *Surface Luminance Stepping*: Deep black canvas (`#000000`) -> Dark slate panels (`#0C0D11`) -> Raised control containers (`#1A1A1A`, `#27292D`).
  2. *Accent Underlines & Borders*: `1 px` to `2 px` warm copper/orange rules (`#74361C`) anchoring panel headers.
  3. *Radial Optical Blooms*: Selected icons, active slider thumbs, and active tabs emit a soft Gaussian radial bloom (radius `10 px` to `20 px`, opacity `25%` to `40%`), simulating back-lit hardware LEDs.
  4. *Specular Sheen*: Top tabs feature a subtle vertical linear gradient with an intense `1 px` top highlight, creating the appearance of bevelled acrylic or brushed anodized metal.

---

## 8. State Comparison — Lighting ON versus Lighting OFF

An exact pixel-by-pixel mathematical comparison of `media_1788924131710.png` (Lighting ON) against `media_1788925003195.png` (Lighting OFF) demonstrates the precise mechanics of the disabled state:

```
+-----------------------------------------------------------------------------------------------+
|  Title Bar (Unchanged - 100% Opacity)                         [Circuit Artwork]  [-] [X]      |
+-------------+-------------------------------------------------------------+-------------------+
|  Nav Rail   |  DIMMED CONTENT CONTAINER (Opacity = 0.50)                  |  [OFF] [Select]   |
|  (Unchanged |                                                             |  (Master: 100%)   |
|   100%      |  - Zone Tabs: 50% opacity                                   |  ---------------- |
|   Opacity)  |  - Keyboard Illustration: 50% opacity                       |  - Effects: 50%   |
|             |    (Rear exhaust red lightbar dims by 50%)                  |  - Wheel: ~29%    |
|             |    (Key borders & WASD backlights dim by 50%)               |  - Slider: 50%    |
|             |  - Profile Buttons Dock: 50% opacity                        |                   |
+-------------+-------------------------------------------------------------+-------------------+
```

### 1. The Master Toggle Switch (`LED on/off`)
- **Stays at 100% full opacity** (not dimmed).
- **Knob Position**: Slides from the right edge (`x ≈ 774`) in the ON state to the **left edge** (`x ≈ 744`) in the OFF state. The knob remains pure white (`#FFFFFF`, luminance `255`).
- **Track Fill**:
  - ON: Vibrant orange fill (`#D5580C`).
  - OFF: Dark slate/charcoal fill (`#2B2D32`).
- **Internal Text**:
  - ON: Miniature white text `"on"` on the left half of the track.
  - OFF: Miniature white text **`"off"`** on the right half of the track.
- **Label Below Switch**: Remains `"LED on/off"` at full `100%` white luminance (`#FDFDFE`).

### 2. The Keyboard Illustration
- **Does not disappear**; it remains fully visible in place.
- **Luminance & Opacity**: Every pixel dims uniformly by **50%** (`mean luminance: 80.0 -> 39.7`, ratio = `0.496` ≈ `0.50`).
- **Lighting Effects**:
  - The rear red-orange exhaust lightbar does not extinguish entirely; its peak luminance drops by exactly 50% (`#D93F33` -> `#6C1F19`).
  - The keycap electroluminescent borders and WASD highlights drop by 50% (`#F8F9F8` -> `#7B7C7B`), rendering them as low-contrast silhouettes.

### 3. Right-Hand Control Column
- **Select all Toggle**: Dims to **50% opacity** (`Opacity="0.5"`). The orange track fill drops from `#D5580C` to `#6B2D19`; the knob drops from `#FFFFFF` to `#7F7F7F`; the label drops from `#FDFDFE` to `#7F7F7F`.
- **Reload Button**: Container and sync icon dim to **50% opacity** (`#ADAEB2` -> `#565759`).
- **Effect Buttons (Static, Breathing, Colorful cycle, Ambilight)**: All 4 buttons dim to **50% opacity** (Static peak `#F6FFFF` -> `#7D7F7F`).
- **Color Wheel**: Undergoes deep dimming to **~29% luminance** (`mean: 102.2 -> 30.6`, ratio = `0.287`), appearing as a heavily darkened, muted rainbow disc (compound effect of container `0.5` opacity and control-level dimming).
- **Brightness Slider & Readout**:
  - Label `"Brightness"` dims to 50% (`#FCFCFC` -> `#7D7D7D`).
  - Readout `"100%"` dims to 50% (`#E2E2E2` -> `#717171`).
  - Slider thumb glowing core dims to 50% (`#FFC189` -> `#7F6044`).

### 4. Zone Tabs & Profile Dock
- **Zone Tabs (A, B, C)**: Dims to **50% opacity**; Zone A's active orange underline drops from `#E7602F` to `#743017`.
- **Bottom Profile Dock**: Dims to **50% opacity**; the active "User Define" orange label drops from `#FC6E3C` to `#7C371D`.

### 5. Elements Remaining Completely Unaffected (100% Opacity)
- **Title Bar**: Logo, wordmark `"CONTROL CENTER"`, circuit board art, and caption buttons (`-`, `✕`) remain at `100%` opacity (ratio = `1.0`).
- **Left Navigation Rail**: Rail surface, dividers, and all 3 items (including the active `"LED Control"` item with its glowing orange icon and orange text) remain at `100%` opacity (ratio = `1.0`).
- **Master Toggle**: Remains active and interactive at `100%` opacity.

---

## 9. Open Questions & Implementation Considerations

1. **Interactive Hover & Pressed States**:
   - What is the hover feedback on the Navigation Rail items? (Recommended: `1 px` left accent bar or `rgba(255, 255, 255, 0.05)` surface highlight).
   - How do caption buttons behave on hover? (Recommended: Close button turns standard red `#E81123`; Minimize turns `#3A3E48`).
2. **Dynamic Polling & Transitions**:
   - Are the fan speed gauges continuously rotating with an angular velocity mapped to RPM, or are they static turbine graphics with live text updates?
   - When switching System modes (Office -> Gaming -> High Perf), does the pentagon polygon animate smoothly via spline interpolation between vertex sets?
3. **Window Sizing & Responsiveness**:
   - Is the application strictly fixed at `1024 × 556 px` (`ResizeMode="NoResize"`), or does the central area stretch responsively on high-DPI / larger viewports?
4. **Color Selection Data Binding**:
   - When selecting a color on the wheel, does the keyboard illustration dynamically re-tint its illumination to match the chosen RGB hue in real time?
