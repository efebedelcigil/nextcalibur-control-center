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
- **Files in the profile** - settings, the lighting state, the log - are
  written by the elevated process into folders the account owns. A folder
  or file that is a reparse point (a junction, a symbolic link) is not
  written to. What is read from those files is validated: thresholds and
  the polling interval are clamped, an effect number that is not an
  effect is not sent to the firmware.
- **The firmware is written in three registers only** (LED, display mode,
  thermal profile), each documented in `docs/PROTOCOL.md`, each from a
  value the code chose - never a value read from a file or the network.
- **The vendor's software is never run, downloaded or modified.** WinUtil
  is the one thing deliberately run elevated from the network, at the
  person's explicit request, after a dialogue that says exactly that.

- **The runtime goes out with the release, so it must be current.** This
  application carries its own .NET: nothing on the machine patches those
  files, and a release built against an old runtime pack carries that
  runtime's known holes on every machine that installs it, for as long as
  it is installed. Every workflow takes the newest SDK
  (`check-latest`), and the release refuses to publish a build whose
  runtime is behind Microsoft's latest patch or whose channel is out of
  support - `tools/Check-BundledRuntime.ps1`, which reads the version out
  of the produced binary rather than trusting the build.
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
