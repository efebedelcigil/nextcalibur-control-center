"""
Nextcalibur Icon & Logo Generator
Generates:
  1. src/Nextcalibur.App/Assets/app.ico (multi-resolution .ico: 16, 20, 24, 32, 48, 64, 256)
  2. docs/logo.png (512x512 transparent PNG)
"""

import os
import struct
import io
from PIL import Image, ImageDraw

def get_polygon_points(size, simplified=False):
    s = size / 100.0
    if simplified:
        # Optimized for 16, 20, 24px: thicker edges, bold silhouette
        return [
            (50*s, 6*s),
            (70*s, 42*s),
            (58*s, 54*s),
            (88*s, 64*s),
            (76*s, 78*s),
            (57*s, 68*s),
            (56*s, 88*s),
            (50*s, 94*s),
            (44*s, 88*s),
            (43*s, 68*s),
            (24*s, 78*s),
            (12*s, 64*s),
            (42*s, 54*s),
            (30*s, 42*s),
        ]
    else:
        # Full precision geometry for >= 32px
        return [
            (50*s, 8*s),
            (66*s, 42*s),
            (57*s, 56*s),
            (86*s, 64*s),
            (76*s, 76*s),
            (57*s, 68*s),
            (56*s, 86*s),
            (50*s, 94*s),
            (44*s, 86*s),
            (43*s, 68*s),
            (24*s, 76*s),
            (14*s, 64*s),
            (43*s, 56*s),
            (34*s, 42*s),
        ]

def render_icon_variant(size):
    scale_factor = 4 if size <= 64 else 1
    render_size = size * scale_factor
    im = Image.new('RGBA', (render_size, render_size), (0, 0, 0, 0))
    draw = ImageDraw.Draw(im)
    
    simplified = (size <= 24)
    pts = get_polygon_points(render_size, simplified=simplified)
    draw.polygon(pts, fill=(255, 255, 255, 255))
    
    if scale_factor > 1:
        im = im.resize((size, size), Image.Resampling.LANCZOS)
    return im

def write_ico(images_with_sizes, output_path):
    # Pack into standard Windows ICO format using PNG compression for all frames
    png_data_list = []
    for im in images_with_sizes:
        buf = io.BytesIO()
        im.save(buf, format='PNG')
        png_data_list.append(buf.getvalue())
    
    count = len(images_with_sizes)
    # Header: 6 bytes
    # Entries: 16 bytes each
    header_size = 6 + 16 * count
    offset = header_size
    
    entries = []
    for i, im in enumerate(images_with_sizes):
        w = 0 if im.width == 256 else im.width
        h = 0 if im.height == 256 else im.height
        png_bytes = png_data_list[i]
        size_bytes = len(png_bytes)
        entry = struct.pack('<BBBBHHII', w, h, 0, 0, 1, 32, size_bytes, offset)
        entries.append(entry)
        offset += size_bytes
        
    with open(output_path, 'wb') as f:
        # Header: reserved(0), type(1=icon), count
        f.write(struct.pack('<HHH', 0, 1, count))
        for entry in entries:
            f.write(entry)
        for png_bytes in png_data_list:
            f.write(png_bytes)

def render_logo_png(size=512):
    # Render at 4x supersampling for ultra-crisp antialiased edges
    scale_factor = 4
    r_size = size * scale_factor
    im = Image.new('RGBA', (r_size, r_size), (0, 0, 0, 0))
    draw = ImageDraw.Draw(im)
    
    # Base polygon points normalized to 0-100
    base_pts = [
        (50.0, 8.0),
        (66.0, 42.0),
        (57.0, 56.0),
        (86.0, 64.0),
        (76.0, 76.0),
        (57.0, 68.0),
        (56.0, 86.0),
        (50.0, 94.0),
        (44.0, 86.0),
        (43.0, 68.0),
        (24.0, 76.0),
        (14.0, 64.0),
        (43.0, 56.0),
        (34.0, 42.0),
    ]
    
    # Bounding box is x in [14, 86] (width 72), y in [8, 94] (height 86)
    # Center is at (50, 51)
    # Scale to fit 512 canvas with clean margins (~46px margin on top/bottom)
    target_height = (size - 92) * scale_factor
    scale = target_height / 86.0
    cx = (r_size) / 2.0
    cy = (r_size) / 2.0
    
    pts = [(cx + (x - 50.0) * scale, cy + (y - 51.0) * scale) for (x, y) in base_pts]
    draw.polygon(pts, fill=(255, 255, 255, 255))
    
    im = im.resize((size, size), Image.Resampling.LANCZOS)
    return im

def main():
    root_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
    assets_dir = os.path.join(root_dir, 'src', 'Nextcalibur.App', 'Assets')
    docs_dir = os.path.join(root_dir, 'docs')
    os.makedirs(assets_dir, exist_ok=True)
    os.makedirs(docs_dir, exist_ok=True)
    
    logo_path = os.path.join(docs_dir, 'logo.png')
    logo_im = render_logo_png(512)
    logo_im.save(logo_path, format='PNG')
    print(f"Generated {logo_path} (512x512, centered white symmetric mark)")

if __name__ == '__main__':
    main()
