# 🚀 CodePulse API

**CodePulse** is a clean-architecture RESTful API built with **ASP.NET Core 8**, designed for managing blog posts, categories, and user authentication. It supports **JWT authentication**, **role-based access control**, **rate limiting**, **API versioning**, and includes **Swagger UI** for interactive API documentation.

---

## ✨ Features

- ✅ Clean Architecture (API / Application / Domain / Infrastructure)
- ✅ ASP.NET Core 8 + Entity Framework Core
- ✅ JWT Authentication with Identity
- ✅ Role-based Authorization (Reader, Writer)
- ✅ Token Bucket Rate Limiting (per user)
- ✅ API Versioning (v1, v2)
- ✅ Swagger UI for interactive API testing
- ✅ SQL Server database
- ✅ Full Docker support with health checks & environment configuration

---

## 🧱 Technologies Used

- ASP.NET Core 8
- EF Core
- SQL Server
- ASP.NET Core Identity + JWT
- Swagger / Swashbuckle
- Rate Limiting (`Microsoft.AspNetCore.RateLimiting`)
- API Versioning (`Microsoft.AspNetCore.Mvc.Versioning`)
- Docker + Docker Compose

---

## 🔧 Project Structure

```bash
CodePulse.API/
│
├── CodePulse.API/                # API Layer (Controllers, Middlewares, Program.cs)
├── CodePulse.Application/       # Application Layer (Interfaces, DTOs, Business Logic)
├── CodePulse.Domain/            # Domain Layer (Entities, Enums, Interfaces)
├── CodePulse.Infrastructure/    # Infrastructure Layer (EF DbContext, JWT, Logging)
├── CodePulse.Persistence/       # Persistence Layer (Repositories, EF Configs)
├── docker-compose.yml           # Docker Compose setup
├── .env.example                 # Example environment config
├── README.md
└── ...
