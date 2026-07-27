<div align="center">

# 🌐 System Context Diagram
### Global Chat Platform

*Shows the Global Chat Platform and its interactions with external systems*

![Software System](https://img.shields.io/badge/Level-C4%20Context-blue)
![Status](https://img.shields.io/badge/Status-MVP%20In%20Progress-yellow)

<img width="1130" height="953" alt="System Context Diagram" src="https://github.com/user-attachments/assets/05053574-f6f6-4ab1-9af4-153685adf097" />

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
| **User** | Person | A person who sends messages, joins groups, shares files, and sends voice notes. |
| **Global Chat Platform** | Software System | Lets users communicate in real-time through group-based conversations. |
| **Email Service** | External System | Sends email notifications to users (e.g., verification, password reset, alerts). |
| **Push Notification Service** | External System | Delivers push notifications to users' devices. |
| **Object Storage (S3)** | External System | Stores uploaded files, images, videos, and voice notes. |

---

## 🔗 Relationships

| From | To | Description | Protocol | Type |
|---|---|---|---|---|
| User | Global Chat Platform | Uses | `HTTPS` / `WSS` | 🔵 Synchronous |
| Global Chat Platform | Email Service | Sends notification emails via | `SMTP` | 🟣 Asynchronous |
| Email Service | User | Sends e-mails to | `SMTP` | 🟣 Asynchronous |
| Global Chat Platform | Push Notification Service | Sends push notifications via | `APNS` / `FCM` | 🟣 Asynchronous |
| Global Chat Platform | Object Storage (S3) | Uploads / retrieves media via pre-signed URLs | `HTTPS` | 🔵 Synchronous |

---

## ✅ MVP Status

| Element | Status | Note |
|---|:---:|---|
| User | ✅ Confirmed | Core actor |
| Global Chat Platform | ✅ Confirmed | Core system |
| Object Storage (S3) | ✅ Confirmed | Used for file/media uploads |
| Email Service | 🔜 Future | Notifications are in-app only for now; email delivery not implemented in current MVP |
| Push Notification Service | 🔜 Future | Same as above — in-app notifications only at MVP stage |

---

## 🗂 Legend

| Symbol | Meaning |
|---|---|
| 🟦 Solid box, blue border | Our System (Software System) |
| ⬜ Solid box, black border | External System |
| ➡️ Dashed arrow | Data/Request (Synchronous) or Notification/Message (Asynchronous) |

---

## 📝 Notes

> The Global Chat Platform is the central system that users interact with. It integrates with external systems for email delivery, push notifications, and media storage.
>
> At the current MVP stage, only **Object Storage (S3)** is an active integration — **Email** and **Push Notification** services are documented here for architectural completeness but are deferred to a future iteration.
