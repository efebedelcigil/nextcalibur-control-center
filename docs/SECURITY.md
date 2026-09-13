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

Known and accepted: a process running as the account can hold the
single-instance mutex or signal the wake event, which stops a second copy
from starting or brings the window up - a nuisance, not an elevation. The
reparse-point checks close the door rather than lock it: a link created
between the check and the write would still be followed, which is the
limit of what a process writing into another's folder can do.
