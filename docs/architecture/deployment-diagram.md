<div align="center">

# 🚀 Deployment Diagram
### Global Chat Platform — Future Architecture

*Shows how the Global Chat Platform is deployed, delivered, and observed in production*

![Software System](https://img.shields.io/badge/Level-C4%20Deployment-blue)
![Status](https://img.shields.io/badge/Status-Future%20Architecture-orange)

<img width="1536" height="1024" alt="Deployment Diagram" src="https://github.com/user-attachments/assets/5364f28d-e79e-4764-949d-e23416c64cf4" />

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

### Entry & Edge
| Element | Type | Description |
|---|---|---|
| **Users** | Person | End users accessing the platform. |
| **Route53 (DNS)** | Edge / Network | Resolves domain requests and routes to CDN or Load Balancer. |
| **CloudFront (CDN)** | Edge / Network | Serves static assets. |
| **Application Load Balancer** | Edge / Network | Routes HTTPS/WSS traffic into the Kubernetes cluster. |

### CI/CD Pipeline (GitHub Actions)
| Element | Type | Description |
|---|---|---|
| **Developer** | Person | Pushes code changes. |
| **GitHub Repository** | Source Code | Stores application source code. |
| **GitHub Actions (CI)** | Compute (Application) | Checkout, build, test, Docker build, security scan, push image. |
| **Container Registry** | Storage | Amazon ECR / Docker Hub — stores built images. |
| **Update Image Tag** | Compute (Application) | Updates the image tag in the GitOps repo. |
| **GitOps Repository** | Source Code | `abdulrahman11a_argocd-demo` — apps, base, overlays, terraform, monitoring. |
| **Argo CD** | Compute (Application) | Running in EKS. Watches GitOps repo and syncs desired state. |

### Amazon EKS Kubernetes Cluster (Production)
| Element | Type | Description |
|---|---|---|
| **NGINX Ingress Controller** | Compute (Application) | Routes external traffic inside the cluster. |
| **ASP.NET Core Monolith Pods** | Compute (Application) | Multiple replicas — REST API, SignalR Hub, CQRS, Auth, business logic. |
| **Projection Worker Pods** | Compute (Worker) | Multiple replicas — consumes RabbitMQ events, updates read DB, indexes OpenSearch. |

### Data & Messaging Layer
| Element | Type | Description |
|---|---|---|
| **PostgreSQL Primary (RDS)** | Data Store | Write database. |
| **PostgreSQL Read Replica (RDS)** | Data Store | Read database, replicated from primary. |
| **RabbitMQ (Amazon MQ)** | Messaging | Message broker. |
| **Redis (ElastiCache)** | Cache | Cache / sessions / presence / counters. |
| **Amazon S3 (File Storage)** | Storage | User files, attachments, media. |
| **OpenSearch (Amazon OpenSearch Service)** | Search | Full-text search — indexes messages, files, users. |

### Monitoring & Observability
| Element | Type | Description |
|---|---|---|
| **OpenTelemetry Collector** | Monitoring / Observability | Receives, processes, and exports telemetry (traces, metrics, logs) via OTLP. |
| **Prometheus** | Monitoring / Observability | Metrics collection. |
| **Loki** | Monitoring / Observability | Log aggregation. |
| **Jaeger / Tempo** | Monitoring / Observability | Distributed tracing. |
| **Grafana** | Monitoring / Observability | Dashboards. |

### External Services
| Element | Type | Description |
|---|---|---|
| **OAuth Providers** | External System | Google / GitHub — external identity. |
| **Email Service** | External System | SMTP provider — sends verification, alerts, notifications. |
| **Push Notification Service** | External System | Firebase / APNs — delivers push notifications to devices. |

### Infrastructure as Code (IaC)
| Element | Type | Description |
|---|---|---|
| **Terraform** | IaC | Infrastructure provisioning. |
| **Helm** | IaC | Kubernetes packages. |
| **Kustomize** | IaC | Configuration overlays. |
| **ApplicationSets** | IaC | Multi-env deployments. |
| **Argo Rollouts** | IaC | Blue/Green, Canary deployment strategies. |

---

## 🔗 Relationships

| From | To | Description | Protocol | Type |
|---|---|---|---|---|
| Users | Route53 (DNS) | Resolves domain | — | 🔵 Synchronous |
| Route53 (DNS) | CloudFront (CDN) | Routes | `HTTPS` | 🔵 Synchronous |
| Route53 (DNS) | Application Load Balancer | Routes | `HTTPS / WSS` | 🔵 Synchronous |
| CloudFront (CDN) | Amazon S3 (Static Website) | Serves static assets | — | 🔵 Synchronous |
| Application Load Balancer | NGINX Ingress Controller | Forwards traffic | — | 🔵 Synchronous |
| NGINX Ingress Controller | ASP.NET Core Monolith Pods | Routes | — | 🔵 Synchronous |
| NGINX Ingress Controller | Projection Worker Pods | Routes | — | 🔵 Synchronous |
| ASP.NET Core Monolith Pods | Projection Worker Pods | Communicates | — | 🟣 Asynchronous |
| ASP.NET Core / Projection Pods | PostgreSQL Primary / Read Replica | Reads / writes | — | 🔵 Synchronous |
| PostgreSQL Primary | PostgreSQL Read Replica | Replication | — | 🔴 Asynchronous |
| ASP.NET Core / Projection Pods | RabbitMQ | Publishes / consumes events | — | 🟣 Asynchronous |
| ASP.NET Core / Projection Pods | Redis | Caches data | — | 🟢 Asynchronous |
| ASP.NET Core / Projection Pods | Amazon S3 (File Storage) | Uploads / downloads files | — | 🔵 Synchronous |
| RabbitMQ | OpenSearch (Search Layer) | Indexes data | — | 🟣 Asynchronous |
| Application Layer Pods | OpenTelemetry Collector | Sends traces, metrics, logs | `OTLP` | 🟣 Asynchronous |
| OpenTelemetry Collector | Prometheus / Loki / Jaeger-Tempo | Exports telemetry | — | 🟣 Asynchronous |
| Prometheus / Loki / Jaeger-Tempo | Grafana | Feeds dashboards | — | 🟣 Asynchronous |
| ASP.NET Core Monolith Pods | OAuth Providers | Authenticates users | `HTTPS` | ⚫ Synchronous |
| ASP.NET Core Monolith Pods | Email Service | Sends emails | `SMTP` | ⚫ Asynchronous |
| ASP.NET Core Monolith Pods | Push Notification Service | Sends push notifications | `HTTPS` | ⚫ Asynchronous |
| Developer | GitHub Repository | git push | — | 🔵 Synchronous |
| GitHub Repository | GitHub Actions (CI) | Triggers pipeline | — | 🔵 Synchronous |
| GitHub Actions (CI) | Container Registry | Pushes Docker image | — | 🔵 Synchronous |
| GitHub Actions (CI) | GitOps Repository | Updates image tag | — | 🔵 Synchronous |
| Argo CD | GitOps Repository | Watches for changes | — | 🔵 Synchronous |
| Argo CD | Amazon EKS Cluster | Syncs desired state (GitOps) | — | 🔵 Synchronous |

---

## ✅ MVP Status

| Element | Status | Note |
|---|:---:|---|
| CI/CD Pipeline (GitHub Actions → Argo CD) | ✅ Confirmed | Already implemented in `argocd-demo`; applies to this repo's deployment |
| Amazon EKS Cluster | ✅ Confirmed | Hosting environment for the MVP Chat API |
| ASP.NET Core Monolith Pods | ⚠️ Simplified in MVP | MVP runs a single Chat API container, not the full CQRS monolith shown here |
| Projection Worker Pods | 🔜 Future | Not present in MVP |
| PostgreSQL Primary / Read Replica split | 🔜 Future | MVP uses a single PostgreSQL database |
| RabbitMQ | 🔜 Future | Not present in MVP |
| Redis (ElastiCache) | ✅ Confirmed | Used for caching & rate limiting in MVP |
| Amazon S3 | ✅ Confirmed | Used for file/media uploads in MVP |
| OpenSearch | 🔜 Future | Not present in MVP |
| CloudFront (CDN) | 🔜 Future | Documented for future consideration |
| Prometheus / Grafana / Loki | ✅ Confirmed | Documented and planned in the `argocd-demo` infra repo, not this repo |
| OpenTelemetry Collector / Jaeger-Tempo | 🔜 Future | Distributed tracing not yet implemented in `argocd-demo` |
| OAuth Providers | 🔜 Future | MVP uses self-issued JWT only |
| Email Service / Push Notification Service | 🔜 Future | Notifications are in-app only for now |
| Terraform / Helm / Kustomize / ApplicationSets / Argo Rollouts | ✅ Confirmed | Already present in `argocd-demo` |

---

## 🗂 Legend

| Symbol | Meaning |
|---|---|
| 🟪 Purple | User / Client, Edge / Network |
| 🟦 Blue | Compute (Application) |
| 🟧 Orange | Compute (Worker) |
| 🟨 Data Stores | PostgreSQL, etc. |
| 🟥 Red | Messaging |
| 🟩 Green | Cache |
| 🟪 Purple (Search) | Search |
| 🟫 Storage | S3 and similar |
| 🟦 Teal | Monitoring / Observability |
| ⬜ Gray | External System |
| ➡️ Solid arrow | Synchronous Communication |
| ➡️ Dashed arrow | Asynchronous / Event Communication |

---

## 📝 Notes

> This diagram represents the **Future architecture** deployment topology for the Global Chat Platform, built on top of the existing [`argocd-demo`](https://github.com/abdulrahman11a/argocd-demo) GitOps platform (Argo CD, Terraform, Helm, Kustomize).
>
> At the current MVP stage, the deployment pipeline (GitHub Actions → Container Registry → GitOps Repo → Argo CD → Kubernetes) is already real and confirmed. What's **not yet implemented** is the full application topology shown here: the CQRS split, Projection Worker pods, message broker, search engine, distributed tracing, and external OAuth/Email/Push integrations. This document is kept as an architectural reference for the team's future scale-up direction and is not part of the active MVP scope.
