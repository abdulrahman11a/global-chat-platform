# ADR 0002: Rate limiting implemented inside the Chat API

![Status](https://img.shields.io/badge/Status-Accepted-brightgreen)

## Context
The API needs protection against a single user sending excessive requests per second. This could be built as a separate service/container, or handled inline within the existing Chat API using the Redis instance already present in the MVP.

## Decision
Rate limiting is implemented as **Redis-based logic inside the Chat API container** — not as a separate container or external service.

## Consequences
- ✅ Simple to implement; reuses the Redis instance already in the MVP for caching.
- ✅ Avoids adding a new container just for this, in line with the MVP's "minimize moving parts" principle (see [ADR 0001](./0001-single-chat-api-container-for-mvp.md)).
- ⚠️ If the Chat API is ever split into multiple specialized services, rate-limiting logic will need to be extracted into a shared layer or gateway.
- ℹ️ This is reflected as a **solid** (not dashed/future) box inside the Chat API container in the Container Diagram.
