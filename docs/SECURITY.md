# Security

Nextcalibur talks to a laptop's embedded controller, switches a graphics
card off and on, and runs as administrator. A fault here is not a cosmetic
one, so reports are taken seriously and answered.

## Reporting

Please do not open a public issue for a security problem. Use GitHub's
private reporting on this repository (Security → Report a vulnerability),
which reaches the maintainer alone. Expect an acknowledgement within a week.

## What counts

- Anything that lets an unprivileged process reach the firmware mailbox or
  the graphics switch through this application.
- Anything in the update or dependency flow that would let a download be
  substituted: both verify Authenticode signatures and, for releases,
  GitHub's build provenance.
- A write to firmware whose meaning is not documented in `docs/PROTOCOL.md`.

## Supported versions

The latest release only. The application updates itself.

## How releases are made

Releases are built by the `Release` workflow from a tag, on GitHub's own
runners, with build provenance attested. Every asset on a release page can be
verified with `gh attestation verify <file> --repo efebedelcigil/nextcalibur-control-center`.

## The threat model, and what the code does about it

The application runs as administrator, from a scheduled task, without a
prompt. The threat that matters is therefore anything already running as
the account - ordinary, unelevated - using the application to become
administrator. Reviewed on 13 September 2026, before 0.5.4; these are the
rules the code follows.

- **Only a copy under Program Files is started without a prompt.** The
  tasks run whatever file they point at as administrator, and only Program
  Files keeps that file out of the account's reach. A copy in the profile
  (an earlier version's install, the portable zip, a build output) is
  prompted at every start; a task an earlier version registered for such a
  copy is removed at the next start. Tightening the folder's own
  permissions is not enough - the folder above it is the account's, and a
  folder can be renamed and replaced from there.
- **The task is registered in memory**, through the Task Scheduler's
  interface, never through a file in the account's temporary folder that
  anything could rewrite between the write and the read.
- **A download that is then run is verified with `WinVerifyTrust`** - the
  hash, the signature, the chain, the revocation - and the signer's name
  is matched as a whole subject component. Reading the certificate out of
  the file is not verification. The file is written where only
  administrators can reach it (`%WINDIR%\Temp`), under a random name,
  created new, and held open for reading from the check to the end of the
  run. The installer wizard checks and runs PawnIO's installer from `{app}`
  for the same reason. Downloads are capped in size.
- **The uninstall's delayed script** lives in the same protected place:
  `cmd` reads a batch file line by line as it runs, and this one runs for
  up to two minutes.
- **Every Windows tool is run by its full path** under the system folder
  (`schtasks`, `pnputil`, `powercfg`, `cmd`, `powershell`, `explorer`).
  The process's current directory is set to its own folder at start, and
  native libraries are loaded from the application's folder and the
  system folder only (`SetDefaultDllDirectories`, `DllImportSearchPath.System32`):
  never from the current directory, never from `PATH`, both of which the
  account controls.
- **Settings, lighting and the log live where only administrators can
  write.** Up to 0.5.8 they were in `%AppData%`, the account's own folder:
  anything running as the account could rewrite them, and the elevated
  process turned what they said into firmware commands and a driver's
  start type. Since 0.5.9 they are in `%ProgramData%\Nextcalibur\<account SID>`,
  owned by Administrators and not inheriting - SYSTEM and Administrators
  may write, the account may only read. The folder's permissions are
  checked at every start and put back if they drifted; a folder whose name
  was taken before the first start is secured, and anything in it the
  application did not write is deleted rather than adopted. The old files
  are carried over once, through the same validation, and removed.
- **What the application saves, it can tell apart from what somebody else
  saved.** Every file in that folder is recorded - its SHA-256, and a
  last-good copy. On every load and every five minutes the file is compared
  with the record; a file changed, replaced or deleted from outside is not
  used, the last-good copy takes its place, and the person is told. What
  is read is validated as well: thresholds and the polling interval are
  clamped, only named lighting effects reach the firmware, and only the
  start types a network driver may have are written to NDU's key.
- **The application checks that it is what was released.** Each release
  carries `Nextcalibur.integrity.json` beside the executable - the SHA-256
  of every file, written by the release workflow after signing
  (`tools/Write-IntegrityManifest.ps1`). At start and every hour the
  installed copy compares itself with it, checks that nothing but
  SYSTEM, Administrators and TrustedInstaller may write to its folder
  (taking back any other write permission it finds), and checks that every
  .NET runtime library loaded into it is signed by Microsoft. If any of
  that fails, the application stops writing to the firmware until it is
  reinstalled, and says so. The honest limit: a check inside a program
  cannot guard that program against something able to replace it - the
  folder's permissions are what stop that, which is why they are checked
  and restored rather than trusted.
- **The dependencies are checked before they are used.** NVIDIA's
  `nvml.dll` and PawnIO's driver are signed the way Windows installs
  driver packages - through a signed catalog, not in the file - and are
  verified that way (`CryptCATAdmin*` and `WinVerifyTrust`, offline) before
  the library is loaded or the device opened. One that fails is not used.
  The .NET runtime and PawnIO are only ever installed from downloads
  verified as described above.
- **The firmware is written in three registers only** (LED, display mode,
  thermal profile), each documented in `docs/PROTOCOL.md`, each from a
  value the code chose - never a value read from a file or the network.
- **The vendor's software is never run, downloaded or modified.** WinUtil
  is the one thing deliberately run elevated from the network, at the
  person's explicit request, after a dialogue that says exactly that.

- **The runtime is the machine's, so the machine patches it.** Until 0.5.4
  this application carried its own .NET, which meant nothing on a machine
  ever patched it: 0.5.3 shipped 8.0.30 five days after 8.0.31 fixed five
  CVEs, and only a new release could have cured it. It is a dependency now
  - installed by the wizard from Microsoft when a machine has none,
  patched by Windows afterwards, and offered by the application itself
  through the same check as its own updates when Windows has not got there
  yet. The download is verified against the SHA-512 Microsoft publishes for
  it and against its Authenticode signature, and the link must be one of
  Microsoft's own hosts over HTTPS. `tools/Check-Runtime.ps1` stops a
  release that targets a channel out of support - after a channel's end of
  life nobody patches it at all - and still catches a stale runtime in any
  build that carries one.
- **Two names, and what happens when they are taken.** The single-instance
  mutex and the wake event live in the session's namespace, where anything
  running as the account can create them first and refuse everyone. A name
  that cannot be had is believed only when another copy of this
  application is really running; otherwise the start goes ahead without
  it. Neither is a place to keep a secret or a decision.
- **The helper switches are elevated-only.** `--clean-up` takes the
  installation apart and asks nobody; `--switch-card` drives a device.
  Both refuse to run unelevated, so neither is a button an unprivileged
  process can press. The elevated relaunch carries only `--tray` over, not
  the command line it was given.
- **Nothing writes a line of the log but the log.** Text from outside -
  an exception, a server's message, a path - has its control characters
  replaced and its length capped, so nothing can forge an entry with a
  timestamp and a level of its choosing.
- **An answer from the network is read as a stranger's.** The release
  answer must be JSON of the expected shape, read with a cap; the download
  link must be an HTTPS release asset of the repository that was asked
  about; the file is then verified as above before it is run.

## Attacks that were tried

Run against the application on the development machine on 13 September
2026, elevated, with an ordinary process playing the attacker.

| Attack | Result |
|---|---|
| Seven libraries planted in the working directory and first on `PATH` (`nvml`, `pdh`, `cfgmgr32`, `powrprof`, `wintrust`, `version`, `profapi`) | None loaded; every one came from System32 |
| The log folder replaced with a directory junction pointing elsewhere | Nothing written through it; the application ran on |
| The single-instance mutex and the wake event taken first, denying everyone | The application started, with its window; both refusals logged |
| `--clean-up` from an unprivileged process | Refused; nothing removed |
| A signed file edited by one byte, and a signature block copied onto another file | Both refused (they passed the check this project used until that day) |
| Release answers with links off GitHub, on plain HTTP, to another repository, and bodies that are not JSON | All refused, none threw |

Known and accepted: a process running as the account can hold the
single-instance mutex or signal the wake event, which stops a second copy
from starting quietly or brings the window up - a nuisance, not an
elevation, and no longer a way to stop the application from starting at
all. The reparse-point checks close the door rather than lock it: a link
created between the check and the write would still be followed, which is
the limit of what a process writing into another's folder can do. The
update package itself is verified by hash against a list fetched over TLS;
an Authenticode check on it waits on the signing certificate.
