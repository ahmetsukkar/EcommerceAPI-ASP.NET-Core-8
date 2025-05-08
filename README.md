# 🛒 Ecommerce API - ASP.NET Core 8

This is a fully functional **ECommerce API** project built with modern backend technologies. The project follows **Onion Architecture** principles for maintainability and scalability, and includes features like **JWT authentication**, **Redis caching**, and **Stripe integration** for payment processing.

---

## 🚀 Technologies Used

- **.NET 8.0** – Main backend framework
- **MSSQL** – Relational database
- **Entity Framework Core (EF Core)** – ORM for database operations
- **Onion Architecture** – Clean separation of concerns
- **JWT Authentication** – Secure token-based auth system
- **Redis** – In-memory caching for performance
- **Stripe** – Payment gateway integration

---

## 📂 Project Structure
```bash
EcommerceAPI-ASP.NET-Core-8/
│
├── Ecom.API/              # Web API (entry point)
│
├── Ecom.Core/             # Domain layer: Entities, Interfaces, DTOs
│
└── Ecom.Infrastructure/   # Infrastructure layer: EF Core, Redis, Stripe, etc.
```

## 🔐 Authentication

The API uses **JWT (JSON Web Tokens)** to secure protected routes. Users must log in and pass a valid token in the `Authorization` header.

---

## 💳 Payments

Stripe integration is used to handle payments. Test keys are loaded securely via `appsettings.Development.json` and **excluded from GitHub**.

---

## 🧠 Caching

**Redis** is used to cache frequently accessed data, improving response times and reducing DB load.

---

## 🔧 Getting Started

1. **Clone the repo**
   ```bash
   git clone https://github.com/ahmetsukkar/EcommerceAPI-ASP.NET-Core-8.git
   cd EcommerceAPI-ASP.NET-Core-8

2. **Setup your local database (SQL Server required)**

3. **Add local config:** Create a file `appsettings.Development.json` with your DB, JWT, and Stripe keys:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionString"
  },
  "JwtSettings": {
    "Key": "YourJwtSecretKey",
    "Issuer": "YourIssuer",
    "Audience": "YourAudience"
  },
  "Stripe": {
    "SecretKey": "sk_test_...",
    "PublishableKey": "pk_test_..."
  }
}
```

4. **Run the project:**  
Use the following commands to build and run the API project locally:

```bash
dotnet build
dotnet run --project src/Ecom.API
```

