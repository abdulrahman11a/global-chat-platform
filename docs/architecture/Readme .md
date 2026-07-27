<div align="center">

# 📋 Architecture Decision Records (ADR)
### Global Chat Platform

*A log of significant architectural decisions, their context, and consequences*

![ADR](https://img.shields.io/badge/Format-ADR-blue)
![Status](https://img.shields.io/badge/Status-Living%20Document-green)

</div>

---

## What is an ADR?

An Architecture Decision Record captures a single important decision made along with its context and consequences. This lets anyone joining the project understand **why** things are the way they are, not just what they are.

---

## 📑 Index

| # | Title | Status |
|---|---|---|
| [0001](./0001-single-chat-api-container-for-mvp.md) | Single Chat API container for the MVP | ✅ Accepted |
| [0002](./0002-rate-limiting-inside-chat-api.md) | Rate limiting implemented inside the Chat API | ✅ Accepted |
| [0003](./0003-observability-in-infra-repo.md) | Observability stack lives in the infra repo, not here | ✅ Accepted |
| [0004](./0004-in-app-notifications-only-for-mvp.md) | In-app notifications only for the MVP | ✅ Accepted |
| [0005](./0005-self-issued-jwt-auth-for-mvp.md) | Self-issued JWT authentication for the MVP | ✅ Accepted |
| [0006](./0006-cqrs-event-driven-as-future-architecture.md) | CQRS + Event-driven design deferred to Future Architecture | ✅ Accepted |

---

## 📝 Note

New decisions should be added as a new numbered file (`000X-title.md`) and linked here. Superseded decisions should be marked `Superseded by ADR-000X` rather than deleted, to preserve history.
