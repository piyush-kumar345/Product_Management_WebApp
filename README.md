# Product Management WebApp

A simple product management web application built with ASP.NET Core MVC and Razor views. Authenticated users can register, sign in, and manage products from a dashboard.

## Features

- User registration
- User login and logout
- JWT-based authentication stored in an HTTP-only cookie
- Protected product dashboard
- View all products
- Add products
- Edit products
- Delete products
- SQL Server database integration
- Entity Framework Core migrations
- Responsive user interface with Bootstrap

## Technology Stack

- .NET 10
- ASP.NET Core MVC
- C#
- Razor views
- Entity Framework Core 10
- SQL Server
- JWT Bearer authentication
- Bootstrap 5
- HTML and CSS

## Project Structure

```text
Product_Management_WebApp/
├── Controllers/
│   ├── AuthController.cs
│   └── DashboardController.cs
├── Data/
│   └── AppDbConetxt.cs
├── Dto/
│   ├── ProductDto.cs
│   └── UserDto.cs
├── Migrations/
│   └── Entity Framework Core migration files
├── Models/
│   ├── Product.cs
│   └── User.cs
├── Views/
│   ├── Auth/
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   ├── Dashboard/
│   │   ├── Index.cshtml
│   │   ├── ProductForm.cshtml
│   │   └── UpdateProductForm.cshtml
│   └── Shared/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
├── Program.cs
├── appsettings.json
└── Product_Management_WebApp.csproj
```

## Prerequisites

Install the following before running the application:

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server or SQL Server Express
- Visual Studio 2026 or another compatible .NET IDE
- The Entity Framework Core command-line tools

Install the Entity Framework Core CLI tool if it is not already available:

```powershell
dotnet tool install --global dotnet-ef
```

## Getting Started

### 1. Clone the repository

```powershell
git clone https://github.com/piyush-kumar345/Product_Management_WebApp.git
cd Product_Management_WebApp
```

### 2. Configure the database

The application uses SQL Server. Configure the connection string through .NET User Secrets or an environment-specific configuration file instead of committing local or production credentials to source control.

From the project directory, initialize User Secrets if needed:

```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefalutConn" "Server=localhost\\SQLEXPRESS;Database=ProductManagement;Trusted_Connection=True;TrustServerCertificate=True;"
```

The current application reads the connection string using the configuration key `ConnectionStrings:DefalutConn`. Keep this key aligned with `Program.cs`, or rename it consistently in both the application and configuration when correcting the existing spelling.

### 3. Restore dependencies

```powershell
dotnet restore
```

### 4. Apply database migrations

```powershell
dotnet ef database update
```

### 5. Build the application

```powershell
dotnet build
```

### 6. Run the application

```powershell
dotnet run
```

Alternatively, open `Product_Management_WebApp.slnx` in Visual Studio and run the project with `F5` or `Ctrl+F5`.

## Using the Application

1. Open the application in a browser.
2. Register a user account.
3. Sign in with the registered email and password.
4. Open the authenticated dashboard.
5. Add a product with its name, description, price, and color.
6. Edit or delete products from the product list.
7. Use the logout option to end the session.

## Data Model

### User

The `User` entity contains:

- `ID`
- `Username`
- `Email`
- `Password`

### Product

The `Product` entity contains:

- `Id`
- `ProductName`
- `Description`
- `Price`
- `Color`

Database schema changes are managed with Entity Framework Core migrations in the `Migrations` directory.

## Authentication

The application uses JWT Bearer authentication. After a successful login, a JWT is written to an HTTP-only cookie named `Jwt_Key`. Requests to the dashboard require authentication.

For production deployments, configure the signing key securely through User Secrets, environment variables, or a managed secret store. Do not commit the signing key to the repository.

## Security Considerations

This project is suitable as a learning or demonstration application. Before using it in production, consider the following improvements:

- Hash and salt passwords with a proven password-hashing solution instead of storing plain-text passwords.
- Move database connection strings and JWT signing keys out of source control.
- Use a strong, environment-specific JWT signing key.
- Require HTTPS in every deployed environment.
- Add antiforgery protection to form submissions.
- Use POST requests for destructive operations such as deleting products.
- Add server-side validation and consistent error handling.
- Add authorization policies or roles if multiple permission levels are required.
- Avoid using `TrustServerCertificate=True` unless it is appropriate for the target environment.

## Future Improvements

Possible future enhancements include:

- ASP.NET Core Identity integration
- Password reset and email verification
- Role-based authorization
- Product search, filtering, and pagination
- Product image upload
- Automated unit and integration tests
- Docker support
- Continuous integration and deployment
- Structured logging and centralized error handling
- REST API support

## Project Status

The repository currently focuses on the core authentication and product CRUD workflow. Automated tests, Docker configuration, CI/CD workflows, and an explicit open-source license should be added as the project evolves.

## License

No license is currently included. Add an appropriate open-source license before allowing others to reuse, modify, or distribute the project.
