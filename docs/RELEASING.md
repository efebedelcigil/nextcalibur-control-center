# Releasing, and signing

## How a release happens

1. Commit everything, including `docs/releases/X.Y.Z.md` and the version in
   `src/Nextcalibur.App/Nextcalibur.App.csproj` and `build.ps1`.
2. `git tag vX.Y.Z && git push origin main vX.Y.Z`.
3. The `Release` workflow builds on a GitHub runner: tests, publish,
   Velopack package (with a delta against the previous release, fetched
   from GitHub), the Inno wizard, provenance attestation, and the release
   itself with the notes file. Nothing is built on the development machine
   for a release any more; `build.ps1` is for trying things locally.

Every published file can be verified against the workflow that made it:

    gh attestation verify Nextcalibur-Setup-X.Y.Z.exe --repo efebedelcigil/nextcalibur-control-center

## Signing: what it is and what it buys

Windows decides how to treat a downloaded executable from its **Authenticode
signature**: a certificate chained to a root Windows trusts, binding the
file to a named publisher. Without one, SmartScreen shows "Windows protected
your PC" with an unknown publisher, UAC shows a yellow prompt with
"Publisher: Unknown", and antivirus heuristics are less forgiving. With one:

- UAC and SmartScreen show the publisher's name from the certificate.
- SmartScreen **reputation** accrues to the certificate; once enough people
  have run signed files without incident, the warning stops. An EV
  certificate starts with reputation; an OV one earns it over weeks.
- The application's own dependency and update checks already verify
  signatures; a signed Nextcalibur closes the loop.

The publisher name comes from the certificate's subject, not from the
project - `Company`/`Product` in the csproj only fill the file's properties
dialog.

## The options

| | Cost | Identity check | SmartScreen | Notes |
|---|---|---|---|---|
| **Azure Trusted Signing** (Microsoft) | ~$10/month | individual or organisation, verified by Microsoft | reputation from the start, per Microsoft | Needs an Azure account; individuals can be validated. Short-lived certificates; signing via `signtool` with the Trusted Signing dlib or the `azure/trusted-signing-action`. The cheapest credible route for a solo open-source project. |
| OV code-signing certificate | ~$200-400/year | organisation or individual (documents) | earns reputation over time | Since 2023 keys must live on a hardware token or an HSM, so signing in CI means a cloud HSM or a signing service. |
| EV code-signing certificate | ~$300-600/year | organisation (registered business) | immediate | Hardware token; not for individuals. |
| SignPath.io Foundation | free for open source | project review | as OV | Signing done by their service from a build; approval process. |

## Wiring it into the workflow

The `Release` workflow already has two signing steps, skipped while the
secrets are absent. For a PFX-style certificate (OV/EV via a service that
exports one, or a test certificate):

- `SIGNING_CERT_PFX_BASE64` - the PFX, base64: `[Convert]::ToBase64String([IO.File]::ReadAllBytes('cert.pfx'))`
- `SIGNING_CERT_PASSWORD`

Set them under Settings → Secrets and variables → Actions. For Azure Trusted
Signing, replace those two steps with `azure/trusted-signing-action` and
its secrets (tenant, client, subscription, endpoint, account, profile); the
files to sign are `publish\Nextcalibur.exe` before packaging and
`installer\output\Nextcalibur-Setup-X.Y.Z.exe` after.

Velopack's `Update.exe` and the stub are Velopack's own signed binaries;
`vpk pack --signParams` can re-sign them with the same certificate if wanted.

## What to do today, in order

1. Enable branch protection and the security settings listed in the
   repository's Settings (see below); most were set from the command line.
2. Pick a signing route. Azure Trusted Signing is the recommendation: create
   an Azure account, a Trusted Signing account and an identity validation
   (individual), a certificate profile; then add the action to the workflow.
3. Until then, releases stay unsigned and SmartScreen warns; the README says
   so.

## Repository settings that matter

Set from the command line where the API allows it; the rest by hand:

- Branch protection on `main`: no force-push, no deletion, CI must pass,
  linear history. (API)
- Secret scanning and push protection: on. (API, already on)
- Dependabot security updates: on. (API)
- Actions: only actions from GitHub and verified creators; default
  `GITHUB_TOKEN` permissions read-only. (Settings → Actions → General)
- Private vulnerability reporting: on. (Settings → Security)
- Two-factor authentication on the account, and a passkey. (Account
  settings)
- Signed commits are optional; provenance attestations cover the releases.
