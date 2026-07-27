# ADR 0006: CQRS + Event-driven design deferred to Future Architecture

![Status](https://img.shields.io/badge/Status-Accepted-brightgreen)

## Context
A full production-grade design was explored and refined across several iterations: separate write/read PostgreSQL databases, a RabbitMQ message broker, a Projection/Event Processor worker, an OpenSearch search engine, a CloudFront CDN, and a full EKS-based deployment pipeline with Argo CD, Prometheus/Grafana/Loki, and OpenTelemetry tracing. This is a strong, complete architecture — but it is a significant scale-up from the current MVP (see [ADR 0001](./0001-single-chat-api-container-for-mvp.md)).

## Decision
This full design is documented as the project's **Future Architecture** (`docs/architecture/context-diagram.png`, `container-diagram.png`, `component-diagram.png`, `deployment-diagram.png`), separate from the active MVP scope. It is a reference for the team's scale-up direction, not a current build target.

## Consequences
- ✅ The team has a clear, detailed target architecture to grow into, without blocking MVP delivery.
- ✅ Each diagram includes an explicit **MVP Status** table distinguishing what's confirmed vs. future, avoiding ambiguity for anyone reading the docs.
- ⚠️ Revisiting this ADR will be necessary once the MVP hits scaling limits (e.g., read-heavy load, search requirements, or need for async event processing).
- ℹ️ The CI/CD pipeline portion (GitHub Actions → Argo CD → EKS) is **already real** via [`argocd-demo`](https://github.com/abdulrahman11a/argocd-demo) — only the application-level topology (CQRS, message broker, search, tracing) remains future work.
