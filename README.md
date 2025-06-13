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
```

# CodePulse – Clean Architecture Blog API

CodePulse is a clean-architecture RESTful API built with ASP.NET Core 8, designed to power a blog platform with secure user management, blog publishing, and modern API standards.

## Features

- **Layered architecture** (API, Application, Domain, Infrastructure)
- **JWT authentication** with Reader and Writer roles
- **API versioning** support (`/api/v1/...`, `/api/v2/...`)
- **Token Bucket Rate Limiting** per authenticated user
- **Swagger UI** for interactive API documentation
- **Postman collection** included for easy local testing
- **Entity Framework Core + SQL Server**
- **Seeds default users and roles** for quick startup
- **Dockerized** using Docker Compose for full development environment orchestration

---

## Architecture Overview

This project follows the Clean Architecture pattern:
- **API**: ASP.NET Core 8 Web API project (entry point, controllers, versioning, Swagger).
- **Application**: Business logic, use cases, DTOs, validation.
- **Domain**: Core entities, enums, interfaces, domain logic.
- **Infrastructure**: Data access (EF Core), external services, persistence, authentication.

---

## Running the Application with Docker Compose

The repository includes a `docker-compose.yaml` file that orchestrates the following services:
- **SQL Server**: Database container with persistent storage.
- **API**: ASP.NET Core 8 backend, built and run from source.
- **UI**: Angular frontend (optional, if present in the repo).

### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop) installed
- [Git](https://git-scm.com/) installed

### Quick Start

1. **Clone the repository:**
   ```sh
   git clone <this-repo-url>
   cd ServicesV2
   ```

2. **Configure environment variables:**
   - Edit the `.env` file to set your database password and connection string if needed.

3. **Build and start all services:**
   ```sh
   docker compose up --build
   ```

4. **Access the services:**
   - **API Swagger UI:** [http://localhost:5000/swagger](http://localhost:5000/swagger)
   - **Angular UI:** [http://localhost:4200](http://localhost:4200) (if included)
   - **SQL Server:** Accessible on `localhost:1433` (use credentials from `.env`)

5. **Test the API:**
   - Use Swagger UI or import the provided Postman collection to test endpoints.
   - Default seeded users:
     - Writer: `writer@codepulse.com` / `Writer@123`
     - Reader: `reader@codepulse.com` / `Reader@123`

6. **Stopping the environment:**
   ```sh
   docker compose down
   ```

---

## Notes

- The API and database are fully containerized for easy local development and testing.
- For production, update environment variables and connection strings as needed.
- The solution is ready for deployment to cloud environments (e.g., Azure, AWS) using container registries and orchestration services.

---

## Contributing

Feel free to open issues or submit pull requests for improvements or bug fixes.

---

## License

MIT License
