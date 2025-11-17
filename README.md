# Store API - E-Commerce Shopping Cart System

## 🎯 Overview

This project is designed to demonstrate **SOLID principles** and **3-Tier Architecture** in a real-world ASP.NET Core application. It implements a fully functional e-commerce shopping cart system with user authentication, product management, shopping cart operations, and order processing.

## ✨ Features

### Core Functionality

#### 🔐 Authentication & Authorization
- JWT-based authentication with bearer tokens
- Role-based authorization (Admin and User roles)
- Secure password hashing using ASP.NET Core Identity
- Token expiration (10-minute lifetime)

#### 👥 User Management
- User registration with profile information
- User authentication (login/logout)
- Profile viewing and updating
- Admin user creation
- User data isolation (users can only access their own data)

#### 🛍️ Product Catalog
- Browse products with pagination
- View detailed product information
- 32+ pre-loaded sample products (electronics, gaming, cameras, etc.)

#### 🛒 Shopping Cart
- Create multiple shopping carts per user
- Add products to cart with quantities
- Automatic quantity updates for duplicate products
- Remove individual items from cart
- Delete entire carts
- Real-time cart total calculation
- Product validation before adding to cart

#### 📦 Order Management
- Convert shopping cart to order
- View order history with pagination
- View individual order details
- Automatic cost calculations:
  - Item subtotal
  - Delivery cost ($3.99)
  - Tax (configurable)
  - Total amount

#### 📊 Admin Features
- View any user's details
- Create admin accounts
- Generate sales reports:
  - Custom date ranges
  - Multiple aggregation intervals (Daily, Monthly, Yearly)
  - Revenue and order volume analytics

#### 🔧 Technical Features
- Full async/await support with cancellation tokens
- RESTful API design
- Swagger/OpenAPI documentation
- Request validation using FluentValidation
- Standardized error responses (RFC 7807 Problem Details)
- Transaction support for data consistency
- Complex SQL with JSON aggregation

---

## 🛠️ Technology Stack

### Backend Framework
- **ASP.NET Core 7.0** - Modern, cross-platform web framework
- **C# 11** - Latest C# language features

### Database
- **PostgreSQL 14.8** - Robust relational database
- **Dapper 2.1** - High-performance micro-ORM
- **Npgsql 8.0** - PostgreSQL data provider for .NET

### Authentication & Security
- **JWT (JSON Web Tokens)** - Stateless authentication
- **ASP.NET Core Identity** - Password hashing and security
- **Microsoft.IdentityModel.Tokens** - Token validation

### Validation & Documentation
- **FluentValidation 11.9** - Fluent validation library
- **Swashbuckle 6.5** - Swagger/OpenAPI generation
- **Microsoft.AspNetCore.OpenApi** - OpenAPI support

### Database Management
- **Docker & Docker Compose** - Containerized PostgreSQL
- **DbUp** - Database migration tool

### Development Tools
- **Visual Studio 2022** / **VS Code** - IDEs
- **Postman** - API testing (collection included)

---

## 🏗️ Architecture

This project follows a **3-Tier Architecture** pattern with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────────────┐
│                      PRESENTATION TIER                          │
│                        Store.Api                                │
│                                                                 │
│  • Controllers (HTTP endpoints)                                 │
│  • Request/Response DTOs                                        │
│  • Input Validators (FluentValidation)                          │
│  • JWT Authentication & Authorization                           │
│  • Swagger/OpenAPI Documentation                                │
│  • API Mappers (DTOs ↔ Domain Models)                          │
└────────────────────┬────────────────────────────────────────────┘
                     │ depends on
                     ↓
┌─────────────────────────────────────────────────────────────────┐
│                       BUSINESS TIER                             │
│                    Store.Application                            │
│                                                                 │
│  • Business Services (CartService, OrderService, etc.)          │
│  • Domain Models (Cart, Order, Product, User)                   │
│  • Business Rules & Logic                                       │
│  • Service Interfaces (ICartService, IOrderService, etc.)       │
│  • Password Hashing Service                                     │
│  • Application Mappers (Domain ↔ Data Models)                  │
└────────────────────┬────────────────────────────────────────────┘
                     │ depends on
                     ↓
┌─────────────────────────────────────────────────────────────────┐
│                         DATA TIER                               │
│                   Store.Infrastructure                          │
│                                                                 │
│  • Repository Implementations (CartRepository, etc.)            │
│  • Database Connection (Npgsql)                                 │
│  • Data Access (Dapper - SQL queries)                           │
│  • Database Models/Records                                      │
│  • Transaction Management                                       │
│  • Repository Interfaces (IReadCart, IWriteCart, etc.)          │
└────────────────────┬────────────────────────────────────────────┘
                     │
                     ↓
              ┌──────────────┐
              │  PostgreSQL  │
              │   Database   │
              └──────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                    CROSS-CUTTING CONCERNS                       │
│                       Store.Common                              │
│                                                                 │
│  • Result Pattern (SuccessResult, ErrorResult, etc.)            │
│  • Utility Helpers (ArgumentUtils)                              │
│  • Shared Extensions                                            │
│  • Common Models (Error, Paged<T>)                              │
│                                                                 │
│  Used by: Api, Application, and Infrastructure                  │
└─────────────────────────────────────────────────────────────────┘
```

### Architecture Diagram - Dependency Flow

```
        Store.Api (Presentation)
              ↓
      Store.Application (Business)
              ↓
    Store.Infrastructure (Data)
              ↓
          PostgreSQL

    Store.Common (shared by all layers)
```

### Layer Responsibilities

#### 1. **Store.Api** (Presentation Tier)
- **Purpose**: Handle HTTP communication and user interface concerns
- **Responsibilities**:
  - HTTP endpoint routing via Controllers
  - Request/Response handling and mapping
  - Input validation using FluentValidation
  - JWT authentication and authorization configuration
  - Swagger/OpenAPI documentation generation
  - Converting API contracts (DTOs) to domain models
- **Dependencies**: Depends on `Store.Application` and `Store.Common`
- **Technology**: ASP.NET Core Web API, Swagger, FluentValidation

#### 2. **Store.Application** (Business Tier)
- **Purpose**: Contains all business logic and rules
- **Responsibilities**:
  - Business logic implementation via Services
  - Domain model definitions (Cart, Order, Product, User)
  - Business rule enforcement and validation
  - Orchestrating data access through repositories
  - Password hashing and security logic
  - Converting domain models to data models
- **Dependencies**: Depends on `Store.Infrastructure` and `Store.Common`
- **Technology**: Pure C# business logic, ASP.NET Identity for passwords

#### 3. **Store.Infrastructure** (Data Tier)
- **Purpose**: Handle all data persistence and database operations
- **Responsibilities**:
  - Repository pattern implementations
  - Database connection management
  - SQL query execution using Dapper
  - Data persistence and retrieval
  - Transaction management
  - Converting database records to domain models
- **Dependencies**: Depends on `Store.Common` only
- **Technology**: Dapper (micro-ORM), Npgsql, PostgreSQL

#### 4. **Store.Common** (Cross-Cutting Concerns)
- **Purpose**: Shared utilities and patterns used across all layers
- **Responsibilities**:
  - Result pattern for type-safe operation results
  - Utility helper functions
  - Extension methods
  - Shared models and interfaces
- **Dependencies**: None (no dependencies on other projects)
- **Technology**: Pure C# utilities
- Maps data models to database records

#### 4. **Store.Common** (Shared Layer)
- Result pattern implementation
- Utility functions
- Extension methods
- Shared constants
- Cross-cutting concerns

---

## 🚀 Getting Started

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/)
- [Postman](https://www.postman.com/) (optional, for API testing)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/YOUR_USERNAME/store-api.git
   cd store-api
   ```

2. **Set up the database**
   ```bash
   cd db
   docker-compose up --build -d
   ```

3. **Run database migrations**
   ```bash
   dotnet tool restore
   dotnet dbup upgrade local.yml -e local.env --ensure
   ```

4. **Navigate to the API project**
   ```bash
   cd ../src/Store.Api
   ```

5. **Restore dependencies**
   ```bash
   dotnet restore
   ```

6. **Run the application**
   ```bash
   dotnet run
   ```

7. **Access the API**
   - Swagger UI: http://localhost:5010/swagger/index.html
   - API Base URL: http://localhost:5010

### Default User Accounts

#### Admin User
- **Email**: `admin.user@solid.com`
- **Password**: `solidrocks`
- **Permissions**: Full access to all endpoints including admin features

#### Regular User
- **Email**: `normal.user@example.com`
- **Password**: `solidrocks`
- **Permissions**: Access to own cart, orders, and profile

---

## 🎨 Design Patterns

### 1. Repository Pattern
Abstracts data access logic behind interfaces:
```csharp
public interface IReadCart
{
    Task<CartRecord> GetCartAsync(int userId, int cartId, CancellationToken ct);
}

public interface IWriteCart
{
    Task<int?> CreateCartAsync(int userId, CartRecord cart, CancellationToken ct);
    Task<bool> AddToCartAsync(int cartId, ItemRecord item, CancellationToken ct);
}
```

**Benefits:**
- Separation of read and write operations
- Easy to test with mocks
- Flexible data source changes

### 2. Result Pattern
Type-safe operation results without exceptions:
```csharp
public abstract class Result<T>
{
    public T Data { get; }
    public bool Success { get; }
}

// Specific result types
- SuccessResult<T>
- NotFoundResult<T>
- InvalidResult<T>
- ErrorResult<T>
```

**Benefits:**
- Explicit error handling
- No hidden control flow
- Better performance (no exception overhead)

### 3. Service Layer Pattern
Business logic separated from controllers:
```csharp
public class CartService : ICartService
{
    // Validates business rules
    // Orchestrates repositories
    // Returns Result<T>
}
```

### 4. Dependency Injection
All dependencies injected via constructors:
```csharp
public CartController(
    ICartService cartService,
    IValidator<CartRequest> cartValidator,
    IValidator<ItemRequest> itemValidator)
{
    _cartService = cartService.NotNull();
    _cartValidator = cartValidator.NotNull();
    _itemValidator = itemValidator.NotNull();
}
```

### 5. Mapper Pattern
Separates data transfer concerns:
- **API Layer**: API contracts ↔ Domain models
- **Application Layer**: Domain models ↔ Database records

### 6. Strategy Pattern (Implicit)
Report generation uses different strategies:
```csharp
return reportInterval switch
{
    ReportInterval.Day => new SuccessResult<IEnumerable<OrderReport>>(report.MapDays()),
    ReportInterval.Month => new SuccessResult<IEnumerable<OrderReport>>(report.MapMonths()),
    ReportInterval.Year => new SuccessResult<IEnumerable<OrderReport>>(report.MapYears()),
    _ => throw new ArgumentException("Invalid report interval")
};
```

---

## 🔒 Security

### Authentication
- **JWT Tokens**: Stateless, scalable authentication
- **Token Signing**: HMAC SHA-512 signature algorithm
- **Token Claims**: User ID, email, roles
- **Token Expiration**: 10 minutes (configurable)

### Authorization
- **Role-Based Access Control**: Admin vs. User roles
- **Resource Ownership**: Users can only access their own data
- **Attribute-Based**: `[Authorize]` and `[Authorize(Roles = "Admin")]`

### Password Security
- **Password Hashing**: ASP.NET Core Identity PasswordHasher
- **Salted Hashes**: Unique salt per password
- **PBKDF2**: 10,000+ iterations

### Best Practices Implemented
- ✅ No sensitive data in logs
- ✅ HTTPS support
- ✅ SQL injection prevention (parameterized queries)
- ✅ Input validation
- ✅ CORS configuration
- ✅ Secure headers

### ⚠️ Security Notes for Production
- Change default JWT secret key
- Use environment variables for secrets
- Enable HTTPS only
- Implement rate limiting
- Add refresh tokens
- Increase token expiration appropriately
- Implement password complexity requirements
- Add account lockout policies
- Enable audit logging

---

## 🧪 Testing

### Manual Testing
1. **Postman Collection**: Import `SOLID Shopping Cart.postman_collection.json`
2. **Swagger UI**: Interactive testing at `/swagger/index.html`

### Testing Workflow
1. Login via `/auth` endpoint
2. Copy the JWT token
3. Add to Authorization header: `Bearer {token}`
4. Test other endpoints

### Recommended Test Scenarios
- ✅ User registration and login
- ✅ Create cart and add products
- ✅ Verify cart total calculation
- ✅ Convert cart to order
- ✅ Access control (try accessing other users' data)
- ✅ Admin features (reports, user management)
- ✅ Validation errors
- ✅ Not found scenarios

---

**Happy Coding! 🚀**
