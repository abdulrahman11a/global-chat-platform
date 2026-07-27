<div align="center">

# 🧱 Component Diagram
### Global Chat Platform — Future Architecture

*Shows the internal components of the Backend Application container and how they interact*

![Software System](https://img.shields.io/badge/Level-C4%20Component-blue)
![Status](https://img.shields.io/badge/Status-Future%20Architecture-orange)

<img width="1207" height="1303" alt="Component Diagram" src="https://github.com/user-attachments/assets/2a469539-5468-4301-9c1d-7e112c11a023" />

</div>

---

## 📑 Table of Contents
- [Elements](#-elements)
- [Relationships](#-relationships)
- [MVP Status](#-mvp-status)
- [Legend](#-legend)
- [Notes](#-notes)

---

## 🧩 Elements

| Element | Type | Description |
|---|---|---|
| **Web Client (React SPA)** | Container: React, TypeScript | Provides the web interface for users to chat and collaborate. |
| **API Controllers** | Component: ASP.NET Core | Exposes REST endpoints for commands and queries. |
| **SignalR Hub** | Component: ASP.NET Core | Handles real-time connections and messaging. |
| **CQRS Handlers** | Component: MediatR | Processes commands and queries. |
| **Authentication** | Component: ASP.NET Core Identity | Handles user registration, login, JWT issuance and validation. |
| **Authorization** | Component: Policy/Claims | Validates permissions and access to resources. |
| **Validation** | Component: FluentValidation | Validates incoming requests and business rules. |
| **Workspace Module** | Component: Domain Module | Manages workspaces, members and roles. |
| **Channel Module** | Component: Domain Module | Manages channels and channel membership. |
| **Chat Module** | Component: Domain Module | Handles messaging, reactions and threads. |
| **File Module** | Component: Domain Module | Handles file uploads, metadata and attachments. |
| **Notification Publisher** | Component | Publishes domain events for other services. |
| **PostgreSQL (Primary - Write)** | Container: PostgreSQL | Stores all application data (source of truth). |
| **PostgreSQL (Read Replica)** | Container: PostgreSQL | Optimized for read operations and queries. |
| **RabbitMQ** | Container: RabbitMQ | Handles events, retries and dead-letter queue. |
| **Redis** | Container: Redis | Caches sessions, presence, counters and rate limiting. |
| **Amazon S3** | Container: S3 | Stores files, images, videos and other attachments. |
| **OpenSearch** | Container: OpenSearch | Provides full-text search for messages and files. |

---

## 🔗 Relationships

| From | To | Description | Protocol | Type |
|---|---|---|---|---|
| Web Client (React SPA) | API Controllers / SignalR Hub | Makes API calls to | `JSON/HTTPS` | 🔵 Synchronous |
| API Controllers | SignalR Hub | Uses | — | 🔵 Synchronous |
| SignalR Hub | CQRS Handlers | Uses | — | 🔵 Synchronous |
| CQRS Handlers | Validation | Uses | — | 🔵 Synchronous |
| Authentication | Authorization | Uses | — | 🔵 Synchronous |
| Authorization | Validation | Uses | — | 🔵 Synchronous |
| Authorization | Workspace / Channel / Chat / File Modules | Uses | — | 🔵 Synchronous |
| Workspace Module | Channel Module | Uses | — | 🔵 Synchronous |
| Channel Module | Chat Module | Uses | — | 🔵 Synchronous |
| Chat Module | File Module | Uses | — | 🔵 Synchronous |
| Workspace / Channel / Chat / File Modules | Notification Publisher | Publishes to | — | 🟣 Asynchronous |
| Domain Modules | PostgreSQL (Primary) | Reads from / writes to | `JDBC` | 🔴 Synchronous |
| Domain Modules | PostgreSQL (Read Replica) | Reads from | `JDBC` | 🔵 Synchronous |
| Notification Publisher | RabbitMQ | Publishes / consumes events | `AMQP` | 🟣 Asynchronous |
| Domain Modules | Redis | Caches data | `RESP` | 🟢 Asynchronous |
| File Module | Amazon S3 | Uploads / downloads files | `HTTPS` | 🔵 Synchronous |
| RabbitMQ | OpenSearch | Indexes documents | `HTTP` | 🟣 Asynchronous |
| PostgreSQL (Primary) | PostgreSQL (Read Replica) | Replicates | — | 🔴 Asynchronous |

---

## ✅ MVP Status

| Element | Status | Note |
|---|:---:|---|
| Web Client (React SPA) | ✅ Confirmed | Core container |
| API Controllers | ✅ Confirmed | Core REST layer |
| SignalR Hub | ⚠️ Simplified in MVP | Real-time messaging exists in MVP, without a separate named component |
| CQRS Handlers | 🔜 Future | MVP uses direct service calls, no CQRS split yet |
| Authentication | ✅ Confirmed | MVP handles self-issued JWT issuance & validation |
| Authorization | ✅ Confirmed | Basic permission checks exist in MVP |
| Validation | ✅ Confirmed | Input validation exists in MVP |
| Workspace / Channel / Chat / File Modules | ✅ Confirmed | Core domain modules, present in MVP |
| Notification Publisher | ⚠️ Simplified in MVP | MVP notifications are in-app only, no event publishing to external services |
| PostgreSQL (Primary) | ✅ Confirmed | Single database in MVP (no read replica split) |
| PostgreSQL (Read Replica) | 🔜 Future | Not present in MVP |
| RabbitMQ | 🔜 Future | Not present in MVP |
| Redis | ✅ Confirmed | Used for caching & rate limiting in MVP |
| Amazon S3 | ✅ Confirmed | Used for file/media uploads in MVP |
| OpenSearch | 🔜 Future | Not present in MVP |

---

## 🗂 Legend

| Symbol | Meaning |
|---|---|
| 🟦 Blue box | Component / Container (internal) |
| 🔲 Dashed border | Backend Application boundary |
| 🔵 Solid arrow | Synchronous Communication |
| 🟣 Dashed arrow | Asynchronous / Event Communication |

---

## 📝 Notes

> This diagram zooms into the **Backend Application** container (from the Container Diagram) to show its internal components — following the standard C4 model levels (Context → Container → Component).
>
> At the current MVP stage, the Backend Application is a **single Chat API container** with self-issued JWT authentication, inline rate limiting, and one PostgreSQL database — without the CQRS handlers, message broker, or search indexing shown here. This document is kept as an architectural reference for the team's future scale-up direction and is not part of the active MVP scope.
