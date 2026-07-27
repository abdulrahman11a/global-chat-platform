<div align="center">

# 📦 Container Diagram
### Global Chat Platform — Future Architecture

*Shows the internal containers of the Global Chat Platform and how they communicate*

![Software System](https://img.shields.io/badge/Level-C4%20Container-blue)
![Status](https://img.shields.io/badge/Status-Future%20Architecture-orange)

<img width="1130" height="953" alt="Container Diagram" src="https://github.com/user-attachments/assets/4f79bd9d-748d-43af-8ea5-a332c08f35fd" />

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
| **User** | Person | Uses the platform to communicate and collaborate. |
| **CloudFront CDN** | Container: CDN | Serves React static assets (HTML, JS, CSS, images, etc.). |
| **Web Client (React)** | Container: React | Single Page Application running in the browser. Provides UI for users. |
| **API Gateway (NGINX)** | Container: NGINX | Handles routing, load balancing, rate limiting, and HTTPS termination. |
| **Backend Application** | Container: ASP.NET Core Monolith | REST API, SignalR Hub, CQRS, Authentication, and Business Logic. |
| **Write Database** | Container: Database (PostgreSQL - Primary) | Stores the source of truth. All writes happen here. |
| **Read Database** | Container: Database (PostgreSQL - Read Replica) | Stores denormalized data optimized for queries. |
| **Message Broker (RabbitMQ)** | Container: Message Broker | In-memory message broker for event-driven communication. |
| **Projection / Event Processor** | Container: Worker | Consumes domain events, updates the read database, and indexes OpenSearch. |
| **Redis Cache** | Container: Cache | Caches hot queries, sessions, presence, typing indicators, unread counters. |
| **Search Engine (OpenSearch)** | Container: Search | Provides full-text search. Indexes messages, files, and users. |
| **Push Notification Service** | External System: Firebase / APNs | Delivers push notifications to user devices. |
| **Email Service** | External System: SMTP | Sends emails for verification, password reset, and notifications. |
| **OAuth Provider** | External System: Google / GitHub | Provides external identity verification for Google/GitHub login. |

---

## 🔗 Relationships

| From | To | Description | Protocol | Type |
|---|---|---|---|---|
| User | CloudFront CDN | (1) Loads static assets | `HTTPS` | 🔵 Synchronous |
| User | API Gateway | (2) Makes API calls | `HTTPS` | 🔵 Synchronous |
| User | Backend Application | (3) Establishes real-time connection | `WSS` | 🟣 Asynchronous |
| CloudFront CDN | Web Client | Serves | `HTTPS` | 🔵 Synchronous |
| Web Client | API Gateway | Makes API calls | `HTTPS` | 🔵 Synchronous |
| API Gateway | Backend Application | Forwards requests | `HTTP` | 🔵 Synchronous |
| Backend Application | Write Database | Writes | `SQL` | 🔴 Synchronous (Write path) |
| Write Database | Backend Application | Returns command result | `HTTP` | 🔴 Synchronous |
| Backend Application | Read Database | Reads | `SQL` | 🔵 Synchronous (Read path) |
| Read Database | Backend Application | Returns query result | `HTTP` | 🔵 Synchronous |
| Read Database | Redis Cache | Caches data | `Key/Value` | 🟢 Asynchronous |
| Write Database | Message Broker | Publishes events | `AMQP` | 🔴 Asynchronous |
| Message Broker | Projection / Event Processor | Delivers events | `AMQP` | 🔴 Asynchronous |
| Projection / Event Processor | Read Database | Updates | `SQL` | 🔵 Synchronous |
| Projection / Event Processor | Search Engine | Indexes data | `HTTP` | 🟣 Asynchronous |
| Backend Application | Search Engine | Searches | `HTTP` | 🟣 Synchronous |
| Backend Application | Push Notification Service | Sends push notifications | `HTTPS` | ⚫ Asynchronous |
| Backend Application | Email Service | Sends emails | `SMTP` | ⚫ Asynchronous |
| Backend Application | OAuth Provider | Authenticates users | `HTTPS` | ⚫ Synchronous |

---

## ✅ MVP Status

| Element | Status | Note |
|---|:---:|---|
| Web Client (React) | ✅ Confirmed | Core container |
| API Gateway | ✅ Confirmed | NGINX, includes rate limiting |
| Backend Application | ⚠️ Simplified in MVP | MVP uses a single Chat API container with Auth self-issued JWT; no CQRS split yet |
| Write / Read Database split | 🔜 Future | MVP uses a single PostgreSQL database, no CQRS/read-replica split |
| Message Broker (RabbitMQ) | 🔜 Future | Not present in MVP |
| Projection / Event Processor | 🔜 Future | Not present in MVP |
| Redis Cache | ✅ Confirmed | Used for caching & rate limiting in MVP |
| Search Engine (OpenSearch) | 🔜 Future | Not present in MVP |
| CloudFront CDN | 🔜 Future | Documented for future consideration |
| Push Notification Service | 🔜 Future | Notifications are in-app only for now |
| Email Service | 🔜 Future | Notifications are in-app only for now |
| OAuth Provider | 🔜 Future | MVP uses self-issued JWT only, no external OAuth login yet |

---

## 🗂 Legend

| Symbol | Meaning |
|---|---|
| 🟦 Blue | Container (General) |
| 🟥 Red | Container (Write Path) |
| 🔷 Blue (Read) | Container (Read Path) |
| 🟩 Green | Container (Cache) |
| 🟪 Purple | Container (Search) |
| ⬜ Gray | External System |
| ➡️ Solid arrow | Synchronous Communication |
| ➡️ Dashed arrow | Asynchronous / Event Communication |

---

## 📝 Notes

> This diagram represents the **Future architecture** for the Global Chat Platform — a full CQRS + Event-driven design intended for a later scale-up phase.
>
> At the current MVP stage, the system uses a **single Chat API container** (self-issued JWT auth, inline rate limiting, one PostgreSQL database, no message broker or search engine). This document is kept as an architectural reference for the team's future direction and is not part of the active MVP scope.
