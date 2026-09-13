# Product Management API

A RESTful Web API for managing products and categories, built with ASP.NET Core and Entity Framework Core.

This project was built as a hands-on backend project to practice building APIs and applying common ASP.NET Core concepts.

## Features

* Product and category CRUD operations
* Filter products by category
* DTOs for API requests and responses
* JWT Bearer Authentication
* Permission-based Authorization
* Custom Middleware
* Custom Action Filters
* Request and execution-time logging
* Model validation
* Swagger / OpenAPI
* Entity Framework Core with SQL Server
* Seed data and EF Core migrations
* Dependency Injection

## Technologies

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* LINQ
* JWT
* Swagger / OpenAPI

## Authentication & Authorization

The API uses JWT Bearer Authentication.

After a successful login, an access token is generated and used to access protected endpoints.

The project also implements permission-based authorization. Users can have different permissions, including:

* Read Products
* Add Products
* Edit Products
* Delete Products

Permissions are checked based on the authenticated user's ID stored in the JWT claims.

## API Overview

### Authentication

* `POST /api/Auth` — Login and generate a JWT access token

### Products

* `GET /api/Products` — Get all products
* `GET /api/Products/{id}` — Get a product by ID
* `GET /api/Products?categoryId={id}` — Get products by category
* `POST /api/Products` — Add a product
* `PUT /api/Products/{id}` — Update a product
* `DELETE /api/Products/{id}` — Delete a product

### Categories

* `GET /api/Categories` — Get all categories
* `GET /api/Categories/{id}` — Get a category by ID
* `POST /api/Categories` — Create a category
* `PUT /api/Categories/{id}` — Update a category
* `DELETE /api/Categories/{id}` — Delete a category

## Getting Started

### Prerequisites

* .NET 8 SDK
* SQL Server
* Visual Studio or another .NET-compatible IDE

### Configuration

Configure the database connection and JWT settings in your development configuration.

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "your-connection-string"
}
```

```json
"Jwt": {
  "Issuer": "your-issuer",
  "Audience": "your-audience",
  "Lifetime": 30,
  "SigningKey": "your-secret-key"
}
```

Sensitive configuration values should not be committed to the repository.

### Run the Project

1. Clone the repository.
2. Configure the database connection.
3. Apply the EF Core migrations.
4. Run the application.
5. Open Swagger to test the API.
