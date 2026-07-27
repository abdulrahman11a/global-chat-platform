# ADR 0001: Single Chat API container for the MVP

![Status](https://img.shields.io/badge/Status-Accepted-brightgreen)

## Context
Early architecture exploration produced a full CQRS + Event-driven design (separate write/read databases, message broker, projection workers, search engine). This is a valid production-scale pattern, but implementing it from day one for an MVP risks significant overengineering — more moving parts than the current user base or feature set requires.

## Decision
The MVP will use **one Chat API container** that handles both commands (writes) and queries (reads) against a **single PostgreSQL database**. No CQRS split, no message broker, and no separate read replica at this stage.

## Consequences
- ✅ Faster to build, deploy, and reason about for the MVP.
- ✅ Fewer moving parts to operate and monitor.
- ⚠️ Read-heavy scaling will eventually require revisiting this (see [ADR 0006](./0006-cqrs-event-driven-as-future-architecture.md)).
- ⚠️ Schema changes affecting both read and write paths happen in one place — acceptable at MVP scale.
