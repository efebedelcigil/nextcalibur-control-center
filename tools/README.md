# Tools

Scripts that are still worth running, and the captures the documents cite.
Everything here reads the machine unless its header says otherwise; nothing
here is needed to build or run Nextcalibur.

## Still worth running

| Script | What for |
|---|---|
| `Grant-MailboxAccess.ps1` | Grants (or with `-Revoke` restores) the WMI security descriptor on the firmware mailbox by hand. The application does this itself; the script is for a machine where it cannot. Cited by `docs/PROTOCOL.md` and `docs/CLEAN-INSTALL.md`. |
| `Trace-ModeSwitch.ps1`, `Dump-UefiVars.ps1` | Capture the mailbox traffic and the firmware variables around a graphics-mode switch. How `docs/PROTOCOL.md`'s display-mode section was established. |
| `Snapshot-Graphics.ps1`, `Watch-Graphics.ps1`, `Log-Graphics.ps1` | Record which chip drives the panel: once, continuously, or across restarts (`-Install` registers a logon task). Reads only. |
| `Arm-GpuRecovery.ps1` | A safety net before experimenting with graphics modes: `-Arm` registers a task that re-enables the NVIDIA adapter at every startup so a bad switch cannot leave a black screen. Remove it with `-Disarm` when done. |
| `Test-Led.ps1` | Sends lighting commands to the mailbox directly, for checking the protocol without the application. |
| `Sandbox-Autopsy.ps1` + `analyse-autopsy.py` | The three-snapshot method for install/uninstall questions in Windows Sandbox: clean, installed, removed - files, registry, services, drivers, tasks, certificates, power plans - and a diff of the snapshots. |
| `Sandbox-Check.ps1` | The clean-machine test of a release inside Windows Sandbox, printed as one report. `sandbox-test.wsb` opens a sandbox that runs it; `sandbox-wizard.wsb` opens one that starts the installer wizard (`/skipnvidia=1`, there being no NVIDIA driver in a sandbox). The `.wsb` files carry this machine's paths; edit them for another. |
| `Test-Ui.ps1` | Drives the running, elevated window through UI Automation and writes a PASS/FAIL report (`nc-uitest.txt`): hide/show, mode → power cards, unelevated browser, settings file, auto-install gating, the close question and its log line. UIPI keeps an ordinary process off an elevated window, so register it once as a task with `RunLevel HighestAvailable` and `schtasks /run` it: `-Part 1` or `-Part 2`. How the 12 September bug-hunt fixes were confirmed on the machine. |
| `Count-HandleTypes.cs` | Counts a process's handles by kernel object type twice and prints the change - the way a "leak" is told from the garbage collector's sawtooth. Build as a console project; run elevated to read an elevated process. |
| `build_icon.py` | Builds `src/Nextcalibur.App/Assets/app.ico` from `logo.png`, with the small frames treated separately so they do not turn to mush. Needs Pillow. |

## Captures (`trace/`)

Raw evidence the documents refer to. Not scripts; do not run.

- `01-…07-*.json`, `10-vendor-installed.json`, `uefi-*.csv`, `wmi-sd-before.txt`:
  graphics state and firmware variables before and after each of the vendor
  software's Display Mode buttons, and after the restart each one asks for.
  The basis of the graphics-mode section of `docs/PROTOCOL.md`.
- `quiet-hybrid.txt`, `quiet-discrete.txt`: what the card draws at idle in
  each mode.

Captures from a single night whose conclusions were written up (the Fn-key
events, the charger transitions) were removed on 12 September 2026.
