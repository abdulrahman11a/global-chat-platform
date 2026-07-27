# ADR 0005: Self-issued JWT authentication for the MVP

![Status](https://img.shields.io/badge/Status-Accepted-brightgreen)

## Context
User authentication could be delegated entirely to external OAuth providers (Google/GitHub), built entirely in-house (email/password with self-issued tokens), or both at once. The Future Architecture explores both working together — an internal Authentication component alongside an external OAuth Provider used for identity verification only.

## Decision
For the MVP, the **Auth module is self-contained**: it generates its own access/refresh tokens and stores password hashes itself. No external OAuth provider is integrated at this stage.

## Consequences
- ✅ No external dependency required to log in — simpler to stand up and test the MVP.
- ✅ Directly shapes the Users table design (must store password hashes, not just external identity references).
- ⚠️ Users cannot log in with Google/GitHub yet; this is deferred (see [ADR 0006](./0006-cqrs-event-driven-as-future-architecture.md) and the Future Architecture Container Diagram).
- ℹ️ If/when OAuth is added later, the Backend Application would receive the OAuth callback and exchange it for an internally issued JWT/session — the two are not mutually exclusive.
