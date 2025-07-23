# Field Management System API (.NET 8, In-Memory)

This is a simple RESTful API built with **ASP.NET Core 8** and **Entity Framework Core (In-Memory)**  
It demonstrates basic CRUD operations for managing users.

---

## ✨ Features

- Built with .NET 8 minimal hosting model
- Uses EF Core with `UseInMemoryDatabase` — **no setup or SQL Server required**
- Fully RESTful API (GET, POST, PUT, DELETE)
- Easy to clone and run

---

## 🛠️ Technologies

- ASP.NET Core 8 (Web API)
- Entity Framework Core
- In-Memory Database Provider

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

---

### 📦 Run the project

1. Clone the repo:
   ```bash
   git clone https://github.com/ofekdinis/field-management-system.git
   cd field-management-system

---
──────────────────────────────────────────────────────────────
💡 OPTIONAL: How to switch from In-Memory DB to SQL Server
──────────────────────────────────────────────────────────────

This project uses EF Core's In-Memory provider by default.

To use Microsoft SQL Server instead:

1. Install the required NuGet package:
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer

2. Replace the in-memory DB line in Program.cs:
  - From:
       builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseInMemoryDatabase("FieldDb"));
  - To:
       builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

3. Add a connection string in appsettings.json:
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FieldDb;Trusted_Connection=True;"
     }
   }

4. Run database migrations:
   dotnet ef migrations add InitialCreate
   dotnet ef database update

Now the app will use SQL Server instead of the in-memory DB.
