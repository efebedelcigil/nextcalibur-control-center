# Releasing, and signing

## How a release happens

1. Write `docs/releases/X.Y.Z.md`, set the version in
   `src/Nextcalibur.App/Nextcalibur.App.csproj` and `build.ps1`, commit.
2. `git tag vX.Y.Z && git push origin main vX.Y.Z`.
3. The `Release` workflow builds on a GitHub-hosted runner: tests, a
   self-contained publish, the Velopack package with a delta against the
   previous release (fetched from GitHub), the Inno Setup wizard, a
   build-provenance attestation, and the release itself with the notes
   file. Nothing is built on a development machine for a release;
   `build.ps1` is for trying the same steps locally.

The release page carries, in this order:

| File | |
|---|---|
| `Nextcalibur-X.Y.Z-1-Installer.exe` | the wizard - what people download |
| `Nextcalibur-X.Y.Z-2-Portable.zip` | no install; prompted at every start |
| `Nextcalibur-X.Y.Z-delta.nupkg`, `Nextcalibur-X.Y.Z-full.nupkg`, `RELEASES`, `releases.win.json` | the updater's files, names fixed by Velopack |

The two downloads are numbered so that GitHub's case-insensitive sort puts
them first; a line at the top of the notes says which to take. The plain
Velopack `Setup.exe` is not published: it installs into the profile, where
the wizard will not.

Every published file can be verified against the workflow that made it:

    gh attestation verify Nextcalibur-X.Y.Z-1-Installer.exe --repo efebedelcigil/nextcalibur-control-center

## Signing: what it is and what it buys

Windows decides how to treat a downloaded executable from its
**Authenticode signature**: a certificate chained to a root Windows trusts,
binding the file to a named publisher. Without one, SmartScreen shows
"Windows protected your PC" with an unknown publisher and UAC shows
"Publisher: Unknown". With one:

- UAC and SmartScreen show the publisher's name from the certificate.
- SmartScreen **reputation** accrues to the certificate and carries from
  release to release; unsigned, it accrues to each file's hash and starts
  from nothing at every release.
- The application's own dependency and update checks already verify
  Authenticode signatures; a signed Nextcalibur closes the loop.

The publisher name comes from the certificate's subject, not from the
project: `Company`/`Product` in the csproj only fill the file's properties
dialog.

## The routes

| | Cost | Notes |
|---|---|---|
| **SignPath Foundation** | free for open source | Applied for on 13 September 2026. Signing is done in their service from the CI build after their review; reputation builds on their certificate. The route this project is waiting on. |
| Azure Trusted Signing | ~$10/month | Microsoft's; individuals can be validated; reputation from the start. |
| OV certificate | ~$200-400/year | Hardware token or cloud HSM since 2023. |
| EV certificate | ~$300-600/year | Organisations only; immediate reputation. |
| winget | free | `winget install` shows no SmartScreen prompt; a manifest in `winget-pkgs`. Does not help a browser download. |
| Self-signed | free | Worthless: an untrusted root warns exactly as no signature does. |

## Wiring it into the workflow

The `Release` workflow has two signing steps, skipped while the secrets are
absent, for a PFX-style certificate:

- `SIGNING_CERT_PFX_BASE64` - the PFX, base64: `[Convert]::ToBase64String([IO.File]::ReadAllBytes('cert.pfx'))`
- `SIGNING_CERT_PASSWORD`

For SignPath, those two steps are replaced by
`signpath/github-action-submit-signing-request` with the organisation, project
and signing-policy ids from their portal, submitting the wizard, the portable
zip and `publish\Nextcalibur.exe`. For Azure Trusted Signing,
`azure/trusted-signing-action` with its tenant, client, subscription,
endpoint, account and profile.

Velopack's `Update.exe` and its stub are Velopack's own signed binaries;
`vpk pack --signParams` can re-sign them with the same certificate.

## Repository settings that matter

- Branch protection on `main`: no force-push, no deletion, CI must pass,
  linear history.
- Secret scanning and push protection on; Dependabot security updates on.
- Actions: only actions from GitHub and verified creators; default
  `GITHUB_TOKEN` permissions read-only.
- Private vulnerability reporting on.
- Two-factor authentication on the account.

## The history rewrite of 13 September 2026

The repository's history was rewritten with `git filter-repo` and
force-pushed, to remove files that should never have been committed
(machine-identifying data and inventories of the vendor's software, and
internal working documents). Commit hashes before that day are gone; the
tags were moved to the rewritten commits and the releases stayed attached
to them. The provenance attestations of 0.5.2 and 0.5.3 name source
commits that no longer exist; the file signatures still verify. A GitHub
Support request to purge the unreachable objects was filed the same day.
