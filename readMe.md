# Online Shop Backend

## Tech Stack

| Layer            | Technology                                    |
| ---------------- | --------------------------------------------- |
| Framework        | ASP.NET Core 9                                |
| Language         | C# 12                                         |
| Database         | PostgreSQL                                    |
| ORM              | Entity Framework Core 9                       |
| Authentication   | JWT Bearer + Refresh Tokens (HttpOnly cookie) |
| Password Hashing | BCrypt                                        |
| API Docs         | Swagger / OpenAPI                             |
| Containerization | Docker + Docker Compose                       |

## Features

- JWT authentication with refresh token rotation
- Role-based access control (Customer / Admin)
- Product catalog with categories and image support
- Shopping cart with price snapshots
- Order lifecycle management (Created → Paid → Shipped → Delivered)
- Automatic admin seeding on first startup
- EF Core migrations applied automatically on startup

## Project Structure

```
api/
├── Data/
│   ├── AppDbContext.cs
│   └── Configurations/        # EF Core entity configurations
├── Models/                    # Domain entities
├── Extensions/                # Helpers (PasswordHasher, SkuGenerator, etc.)
└── Modules/
    ├── AuthModule/            # Login, logout, refresh token
    ├── UserModule/            # User CRUD, addresses
    ├── ProductModule/         # Product CRUD, SKU generation
    ├── CategoryModule/        # Category tree
    ├── CartModule/            # Cart and cart items
    └── OrderModule/           # Order creation and management
```

Each module follows the same internal structure:

```
SomeModule/
├── Api/           # Controller
├── Domain/        # Service interface + implementation
├── Repository/    # Repository interface + implementation
├── DTOs/          # Request and response models
└── Mapper/        # Entity ↔ DTO mapping
```

## Running Locally

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL instance

### 1. Clone the repository

```bash
git clone https://github.com/VolodymyrBiletskyi/online-shop-backend.git
cd online-shop-backend
```

### 2. Configure the environment

Create `api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=AppDb;Username=postgres;Password=your_password"
  },
  "JwtOptions": {
    "SecretKey": "your_secret_key_min_32_chars_long",
    "Issuer": "OnlineShop",
    "Audience": "OnlineShopUsers",
    "AccessTokenMinutes": 15
  },
  "AdminSeed": {
    "Email": "admin@example.com",
    "Password": "your_admin_password"
  }
}
```

### 3. Run

```bash
cd api
dotnet run
```

Migrations are applied automatically on startup. The URL is printed in the console. Swagger is at `/swagger`.

---

## Running with Docker Compose

### 1. Create a `.env` file in the project root

```env
POSTGRES_DB=AppDb
POSTGRES_USER=postgres
POSTGRES_PASSWORD=your_db_password

JWT_SECRET=your_secret_key_min_32_chars_long
JWT_ISSUER=OnlineShop
JWT_AUDIENCE=OnlineShopUsers

ADMIN_EMAIL=admin@example.com
ADMIN_PASSWORD=your_admin_password
```

### 2. Start

```bash
docker compose up --build
```

Run in the background:

```bash
docker compose up --build -d
```

Swagger will be available at `http://localhost:8080/swagger`.

### 3. Stop

```bash
docker compose down
```

To also remove the database volume:

```bash
docker compose down -v
```

---
