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
- **Docker support with stable SQL Server healthcheck and reliable container orchestration**

---

## 🧱 Technologies Used

- ASP.NET Core 8
- EF Core
- SQL Server
- Identity + JWT
- Swagger / Swashbuckle
- Rate Limiting (`Microsoft.AspNetCore.RateLimiting`)
- API Versioning (`Microsoft.AspNetCore.Mvc.Versioning`)
- Docker (with improved containerization setup)

---

## 🛠️ Recent Improvements

- ✅ Improved SQL Server healthcheck using TCP port check for better stability
- ✅ Removed unused `DB_USER` and `DB_PASSWORD` from environment files
- ✅ Cleaned up `docker-compose.yaml` and `.env` for clarity and best practices
- ✅ Ensured API and SQL Server containers restart reliably with correct environment variables

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
