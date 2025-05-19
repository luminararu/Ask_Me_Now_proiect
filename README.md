# Ask_Me_Now — ASP.NET Core MVC Q&A Web Application

This is a demo Q&A (Question & Answer) web application, similar to StackOverflow. Users can register, post questions, and answer other users' questions. The project is built with ASP.NET Core MVC and Entity Framework Core.

---

## Requirements

- Visual Studio 2022 or later
- .NET 6 SDK or higher
- SQL Server Express or LocalDB (installed with Visual Studio by default)

---

## How to Run the Project

### 1. Open the Solution

- Open the file `Ask_Me_Now.sln` using Visual Studio.

---

### 2. Create the Database (First-time Setup)

1. In Visual Studio, go to:
   `Tools` → `NuGet Package Manager` → `Package Manager Console`

2. In the console that appears at the bottom (`PM>` prompt), type the following commands:

   ```powershell
   Add-Migration InitialCreate
   Update-Database
