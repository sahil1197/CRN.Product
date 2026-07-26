# CRN Product REST API

A RESTful Product Management API built with **.NET 8** following **Clean Architecture** principles.

---

## Features

- CRUD Operations for Products
- JWT Authentication
- Refresh Token Support
- Role-Based Authorization
- API Versioning
- Repository Pattern
- Unit of Work
- FluentValidation
- AutoMapper
- Global Exception Middleware
- Serilog Logging
- Health Checks
- Swagger Documentation
- Docker Support
- Unit & Integration Tests

---

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- AutoMapper
- FluentValidation
- Serilog
- Swagger
- Docker
- xUnit
- Moq

---

## Project Structure

```
src/
│
├── CRN.Product.Api
├── CRN.Product.Application
├── CRN.Product.Domain
└── CRN.Product.Infrastructure

tests/

├── CRN.Product.Api.Tests
├── CRN.Product.Application.Tests
└── CRN.Product.Infrastructure.Tests
```

---

## Architecture

```
Client
    │
    ▼
ASP.NET Core Web API
    │
Authentication (JWT)
    │
Application Layer
    │
Repository + Unit of Work
    │
Entity Framework Core
    │
SQL Server
```

---

## API Features

- Get Products
- Get Product By Id
- Create Product
- Update Product
- Delete Product
- User Login
- Refresh Token

---

## Authentication

Default User

Username

```
admin
```

Password

```
Admin@123
```

Login Endpoint

```
POST /api/v1/auth/login
```

---

## Running the Application

Clone the repository

```bash
git clone https://github.com/yourusername/CRN-Product-API.git
```

Navigate to the project

```bash
cd CRN-Product-API
```

Update the SQL Server connection string in `appsettings.json`.

Run

```bash
dotnet restore
dotnet build
dotnet run
```

---

## Swagger

```
https://localhost:44368
```

---

## Running Tests

```bash
dotnet test
```

---

## Docker

Build and run

```bash
docker compose up --build
```

---

## Author

Kishore Baradkar