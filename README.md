# CodePulse API

CodePulse is a clean-architecture RESTful API built with **ASP.NET Core 8**, designed for managing blog posts, categories, and user authentication. It supports JWT-based authentication and includes features like rate limiting, API versioning, and role-based access control.

---

## 🚀 Features

- Clean Architecture with layered separation (API / Application / Domain / Infrastructure)
- ASP.NET Core 8 + Entity Framework Core
- JWT Authentication
- Role-based Authorization (Reader, Writer)
- Token Bucket Rate Limiting (per user)
- API Versioning (v1, v2)
- Swagger UI for interactive documentation
- SQL Server database
- Docker support (in progress)

---

## 🧱 Technologies Used

- ASP.NET Core 8
- EF Core
- SQL Server
- Identity + JWT
- Swagger / Swashbuckle
- Rate Limiting (`Microsoft.AspNetCore.RateLimiting`)
- API Versioning (`Microsoft.AspNetCore.Mvc.Versioning`)
- Docker (for containerization)

---

## 🏗️ Project Structure

```bash
CodePulse.API/
│
├── CodePulse.API/                # API Layer (Controllers, Middlewares, etc.)
├── CodePulse.Application/       # Application Layer (Services, Interfaces, DTOs)
├── CodePulse.Domain/            # Domain Layer (Entities, Enums, etc.)
├── CodePulse.Infrastructure/    # Infrastructure Layer (EF DbContext, Auth, Logging)
├── CodePulse.Persistence/       # Data access and persistence
├── README.md
└── ...
