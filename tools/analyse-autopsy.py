"""Compare autopsy snapshots taken before, during and after an install.

    python tools/analyse-autopsy.py notes/vendor-autopsy

Sandbox-Autopsy.ps1 captures a machine three times - clean, with the software
installed, and after uninstalling it. This turns those three files into the
answers worth having:

  * what the installer put on the machine, and who signed it
  * what it changed that was already there
  * what survived the uninstall

The third is the one that matters most here. An uninstaller that leaves things
behind is telling you which parts of itself the machine now depends on, and any
program claiming to replace it inherits exactly those.
"""

import collections
import io
import json
import sys
from pathlib import Path

SEP = chr(92)  # backslash: written this way because the path it splits is Windows'


def load(directory: Path, label: str) -> dict:
    path = directory / f"autopsy-{label}.json"
    with io.open(path, encoding="utf-8-sig") as handle:
        return json.load(handle)


def vendorish(text: str) -> bool:
    lowered = text.lower()
    return any(mark in lowered for mark in
               ("casper", "excalibur", "controlcenter", "control center", "quanta"))


def section(title: str) -> None:
    print()
    print("=" * 78)
    print(title)
    print("=" * 78)


def describe_file(path: str, record: dict) -> str:
    bits = [f"{record.get('size', 0):>10,} bytes"]
    if record.get("version"):
        bits.append(f"v{record['version']}")
    if record.get("company"):
        bits.append(record["company"])
    if record.get("signature"):
        signer = record.get("signer", "").replace("CN=", "")
        bits.append(f"signed {record['signature']}"
                    + (f" by {signer}" if signer else ""))
    elif path.lower().endswith((".exe", ".dll", ".sys")):
        bits.append("UNSIGNED")
    return "  ".join(bits)


def main() -> int:
    directory = Path(sys.argv[1] if len(sys.argv) > 1 else "notes/vendor-autopsy")
    snaps = {label: load(directory, label) for label in ("clean", "installed", "removed")}

    for label, snap in snaps.items():
        print(f"{label:<10} files={len(snap['files']):>5}  "
              f"registry={len(snap['registry']):>6}  system={len(snap['system']):>4}")

    files_clean = snaps["clean"]["files"]
    files_installed = snaps["installed"]["files"]
    files_removed = snaps["removed"]["files"]

    added = sorted(set(files_installed) - set(files_clean))
    section(f"What the installer added: {len(added)} files")

    # Grouped by directory, because a hundred files in one folder is one fact
    # and a hundred folders with one file each is a different one.
    by_dir = collections.Counter(path.rsplit(SEP, 1)[0] for path in added)
    for folder, count in by_dir.most_common():
        marker = " <-- vendor" if vendorish(folder) else ""
        print(f"  {count:>4}  {folder}{marker}")

    section("Every file the installer placed in its own folder")
    for path in added:
        if vendorish(path):
            print(f"  {path.rsplit(SEP, 1)[-1]:<44} {describe_file(path, files_installed[path])}")

    outside = [p for p in added if not vendorish(p)]
    if outside:
        section(f"Files added outside the vendor's own folder: {len(outside)}")
        for path in outside[:60]:
            print(f"  {path}")
            print(f"        {describe_file(path, files_installed[path])}")

    changed = [p for p in set(files_installed) & set(files_clean)
               if files_installed[p].get("sha256") and files_clean[p].get("sha256")
               and files_installed[p]["sha256"] != files_clean[p]["sha256"]]
    if changed:
        section(f"Existing files the installer modified: {len(changed)}")
        for path in sorted(changed):
            print(f"  {path}")

    section("What survived the uninstall")
    left = sorted(set(files_removed) - set(files_clean))
    vendor_left = [p for p in left if vendorish(p)]
    other_left = [p for p in left if not vendorish(p)]

    print(f"  {len(left)} files that were not there before remain.")
    if vendor_left:
        print(f"\n  Vendor files left behind ({len(vendor_left)}):")
        for path in vendor_left:
            print(f"    {path}")
            print(f"          {describe_file(path, files_removed[path])}")
    else:
        print("\n  None of them are the vendor's - its own files are all gone.")

    if other_left:
        print(f"\n  Other new files ({len(other_left)}), most likely Windows' own churn"
              f" during the test:")
        by_dir = collections.Counter(p.rsplit(SEP, 1)[0] for p in other_left)
        for folder, count in by_dir.most_common(15):
            print(f"    {count:>4}  {folder}")

    for name in ("registry", "system"):
        clean, installed, removed = (snaps[l][name] for l in ("clean", "installed", "removed"))

        section(f"{name.capitalize()}: added by the installer")
        added_keys = sorted(set(installed) - set(clean))
        for key in added_keys:
            print(f"  + {key} = {installed[key]}")

        changed_keys = sorted(k for k in set(installed) & set(clean) if installed[k] != clean[k])
        if changed_keys:
            print(f"\n  Changed ({len(changed_keys)}):")
            for key in changed_keys:
                print(f"  ~ {key}")
                print(f"        {clean[key]}")
                print(f"     -> {installed[key]}")

        section(f"{name.capitalize()}: still there after the uninstall")
        stayed = sorted(set(removed) - set(clean))
        for key in stayed:
            print(f"  ! {key} = {removed[key]}")
        changed_after = sorted(k for k in set(removed) & set(clean) if removed[k] != clean[k])
        for key in changed_after:
            print(f"  ~ {key}")
            print(f"        was {clean[key]}")
            print(f"    now     {removed[key]}")
        if not stayed and not changed_after:
            print("  Nothing.")

    return 0


if __name__ == "__main__":
    raise SystemExit(main())
