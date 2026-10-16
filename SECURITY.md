# Security policy

InclusiveSTONE is a completed research and training project published as
a portfolio copy. It is not actively maintained, but security reports
are welcome.

## Reporting a vulnerability

Please **do not open a public issue** for security problems.

Report them privately through GitHub instead: go to the **Security**
tab of this repository and choose **Report a vulnerability**. See
[GitHub private vulnerability reporting](https://docs.github.com/en/code-security/security-advisories/guidance-on-reporting-and-writing-information-about-vulnerabilities/privately-reporting-a-security-vulnerability).

Please include:

- what you found and where (file, scene or build),
- how to reproduce it,
- the impact you expect.

Reports are handled on a best-effort basis.

## Scope and known issues

- The project uses **Unity 2020.3.20f1**, which is affected by
  **CVE-2025-59489** (untrusted search path in Unity runtime builds).
  Builds made with this version should be rebuilt with Unity 2020.3.49f1
  or later, or patched with the Unity Application Patcher. See the
  [Unity security advisory](https://unity.com/security/sept-2025-01).
- The application makes no network requests and stores only local
  preferences (language and score) in `PlayerPrefs`.
- Secrets scanning runs on every push and pull request
  (`.github/workflows/security.yml`).
