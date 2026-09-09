# Antigravity — Prompt 1 of 2: visual analysis

Copy everything below the line into Antigravity, attached to the three reference
screenshots. This pass produces **a written design analysis and no code**. A
second prompt will ask for the implementation.

---

## Task

You are given screenshots of a Windows desktop application: a laptop control
centre. I am rebuilding this interface in **WPF (XAML)** for a different product,
and I want the result to read as the same family of design — someone who has
seen the original should recognise it immediately.

**This pass is analysis only. Do not write any code.** Produce a design
specification document precise enough that a second agent could rebuild the look
from your description alone, without seeing the screenshots.

Work only from what is visible in the images. Where you cannot tell something,
say so explicitly rather than guessing — a list of open questions at the end is
more useful to me than confident invention.

## What the screenshots show

Three pages of the same application, sharing one window frame:

1. **System page** — mode tabs across the top, a pentagon radar chart, fan
   speed gauges at the bottom, and a right-hand column of statistics.
2. **Display page** — three large mode options in a row, same bottom bar, same
   right column.
3. **Lighting page** — zone tabs, a keyboard illustration, profile buttons along
   the bottom, and a control panel down the right. Two variants of this page are
   included: one with lighting **on**, one with the master toggle **off**.

## What I need from you

Deliver a Markdown document with the sections below. Be concrete: give pixel
measurements and hex colours, not adjectives. Measure against the screenshot's
own pixel dimensions and say what those dimensions are.

### 1. Window and frame

- Overall dimensions and aspect ratio.
- Is the window frame custom or the operating system's? What does the title bar
  contain, left to right?
- Title bar height, background, and how it separates from the content below.
- The caption buttons: size, spacing, glyph weight, hit area, hover treatment,
  and whether close is styled differently from minimise.
- Corner radius of the window itself, and any border or outer glow.

### 2. Colour palette

A table of every distinct colour you can sample, each with: hex value, where it
is used, and what role it plays. Cover at minimum:

- window background, panel background, raised surface, rail background
- separator and border colours
- the accent colour, and any lighter or darker variants of it
- text at each level: primary, secondary, disabled
- any colour used only for data (gauge arcs, chart fills, status dots)

Note where colour carries meaning rather than decoration.

### 3. Typography

For each distinct text role — page heading, panel header, control label, large
value, small caption, button text — record size in pixels, weight, casing,
letter spacing, colour, and alignment. Note whether the numerals are
proportional or tabular, and whether any text is condensed or stretched.

### 4. Layout and spacing

- Width of the navigation rail; width of the right column.
- Outer margins of the content area, and the gap between panels.
- The vertical rhythm: what spacings recur, and what the base unit appears to be.
- How the three pages align with each other — what stays fixed across all
  three, and what changes.

### 5. Component anatomy

This is the most important section. For **each** recurring component, describe
its construction and every visual state you can observe (default, hover if
visible, selected, disabled):

- navigation rail item
- top mode tab (System page)
- zone tab (Lighting page)
- pill toggle — the on/off switch
- small utility button (Reload)
- effect icon button (Lighting page)
- profile button (Lighting page)
- large radio option (Display page)
- panel header — note its shape, which is not a plain rectangle
- circular fan gauge
- donut gauge
- segmented bar
- pentagon radar chart
- colour wheel and its selector
- slider and its tick marks

For each, give: overall size, shape and corner radius, border, fill, internal
padding, the arrangement of icon and label, and precisely what changes between
states. If a selected state changes more than one property at once, list all of
them.

### 6. Iconography

- Outline or filled? Stroke weight in pixels? Line caps and joins?
- Nominal icon canvas size, and how much of it the drawing occupies.
- Level of detail: geometric and minimal, or illustrative?
- Do icons share a consistent visual language, or do some differ?
- Describe each individual icon's subject well enough to redraw it from scratch.

### 7. Decorative treatment

- The graphic in the top-right of the header: what is it, how much space does it
  take, what is its opacity and colour?
- Any gradients, glows, inner shadows, or texture.
- How the design signals depth — borders, fills, shadows, or none.

### 8. State comparison — lighting on versus off

Two screenshots of the Lighting page differ only in the master toggle. Compare
them and list **exactly** what changes: which elements dim, to roughly what
opacity, whether anything disappears entirely, whether the keyboard illustration
changes, and whether any element stays fully lit.

### 9. Open questions

Everything you could not determine from static screenshots: hover and pressed
states, animations and transitions, what happens on interaction, any element
that is cut off or ambiguous.

## Constraints on your analysis

- **Describe the brand assets; do not reproduce them.** The logo, wordmark, and
  the header illustration will be replaced with my own. Tell me their size,
  position, and weight in the composition, so I can place my own equivalents —
  do not attempt to recreate the artwork itself.
- Report the palette faithfully, including the accent colour. I will decide
  later whether to keep that hue.
- Ignore all text content, product names and numbers. I care about the design
  system, not the words.

## Output

One Markdown document, sections in the order above, tables where tables help.
Measurements and hex values throughout. No code, no XAML, no CSS in this pass.
