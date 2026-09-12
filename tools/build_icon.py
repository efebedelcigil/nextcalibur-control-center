"""Build the Windows application icon from the brand mark.

The mark is supplied as a single high-resolution PNG. Windows asks for it at
sizes from 16 px upwards, and the small ones need their own treatment: a
straight downscale of a detailed mark turns to mush, so the small frames are
sharpened and given a little extra contrast rather than simply resampled.

Usage:
    python tools/build_icon.py

Reads : src/Nextcalibur.App/Assets/logo.png
Writes: src/Nextcalibur.App/Assets/app.ico
"""

from __future__ import annotations

import pathlib
import sys

from PIL import Image, ImageEnhance, ImageFilter

ROOT = pathlib.Path(__file__).resolve().parent.parent
SOURCE = ROOT / "src" / "Nextcalibur.App" / "Assets" / "logo.png"
ICON = ROOT / "src" / "Nextcalibur.App" / "Assets" / "app.ico"

# Windows picks from these: 16 in the tray and title bar, 32 on the taskbar,
# 256 in large icon views and the installer.
SIZES = [16, 20, 24, 32, 48, 64, 128, 256]

# Below this a detailed mark loses its interior and reads as a blob, so those
# frames get sharpened to hold what edges remain.
SMALL = 32


def render(source: Image.Image, size: int) -> Image.Image:
    frame = source.resize((size, size), Image.LANCZOS)

    if size <= SMALL:
        # Recover edges that downsampling averaged away, then lift contrast so
        # the silhouette still separates from a dark taskbar.
        frame = frame.filter(ImageFilter.UnsharpMask(radius=1, percent=140, threshold=2))
        frame = ImageEnhance.Contrast(frame).enhance(1.15)

    return frame


def main() -> int:
    if not SOURCE.exists():
        print(f"missing source mark: {SOURCE}", file=sys.stderr)
        return 1

    source = Image.open(SOURCE).convert("RGBA")
    if source.width != source.height:
        print(f"warning: source is {source.width}x{source.height}, not square", file=sys.stderr)

    frames = [render(source, size) for size in SIZES]
    frames[-1].save(ICON, format="ICO", sizes=[(s, s) for s in SIZES], append_images=frames[:-1])
    print(f"{ICON.relative_to(ROOT)}  <- {', '.join(str(s) for s in SIZES)}")


    return 0


if __name__ == "__main__":
    raise SystemExit(main())
