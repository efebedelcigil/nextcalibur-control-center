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
