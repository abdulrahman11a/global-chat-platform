# ADR 0003: Observability stack lives in the infra repo, not here

![Status](https://img.shields.io/badge/Status-Accepted-brightgreen)

## Context
Monitoring the Chat Platform requires a Prometheus/Grafana/Loki stack. The existing [`argocd-demo`](https://github.com/abdulrahman11a/argocd-demo) repository already manages infrastructure (Terraform, Helm, Kustomize, Argo CD) for the broader platform, including security tooling, but had no monitoring stack defined at the time this was raised.

## Decision
The Observability stack (Prometheus, Grafana, Loki, and later OpenTelemetry/Jaeger-Tempo) is planned and owned by the **`argocd-demo`** infra repository, not the `global-chat-platform` repository — since it is infrastructure-layer concern, not application-layer.

## Consequences
- ✅ Keeps the Chat Platform repo focused only on what it's directly responsible for (its own containers and code).
- ✅ Avoids duplicating infra concerns across multiple repos.
- ⚠️ Anyone wanting to change dashboards, alerts, or tracing configuration should go to `argocd-demo`, not this repo.
- ℹ️ The Chat Platform's Deployment Diagram still **shows** the observability stack (for context), but marks it as owned elsewhere.
