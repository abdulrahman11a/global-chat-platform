# ADR 0004: In-app notifications only for the MVP

![Status](https://img.shields.io/badge/Status-Accepted-brightgreen)

## Context
A real chat platform typically also notifies users who are offline via email or push notification (e.g., "you were mentioned while away"). This is genuinely useful, but requires choosing an external provider (SendGrid/AWS SES, Firebase/APNs), deciding on send-timing logic (e.g., "if offline for X time and has unread messages"), and building the associated triggers — none of which are required for a minimum viable product.

## Decision
The MVP ships with **in-app notifications only** (e.g., a badge or in-app popup when the app is open). Email and Push Notification services are **not implemented** in the MVP.

## Consequences
- ✅ Removes a non-essential integration and its timing-logic complexity from the MVP scope.
- ✅ Keeps the Context and Container diagrams honest about what's actually running.
- ⚠️ Users who are offline will only see new messages when they reopen the app — no external nudge.
- ℹ️ Email/Push Notification Services remain documented in the diagrams with an explicit **"Future"** status, so the integration points are already designed when the team decides to build them.
