# SHIBANK - Secure Home Investment Bank
SHIBANK is a Homebanking REST API that enables user registration, account and card creation, fund transfers, and secure transactions with others.

<h3 align="center">Video demostration</h3>

<div align="center">
  <a href="https://youtu.be/q7-9pfgNk0I?si=8BN4XlEjC09cDv4L"><img src="https://raw.githubusercontent.com/antiel29/SHIBANK/main/SHIBANK_FrontEnd/src/assets/images/youtube_preview.png" alt="Youtube image"></a>
</div>

## Table of Contents
- [Technologies](#technologies)
- [Features](#features)
- [Requirements](#requirements)
- [Configuration](#configuration)
- [Usage](#usage)

## Technologies
- **Backend:** Developed using **.NET 7.0**.
- **Security:** Utilizes **ASP.Net Core Identity** for user role managment and authentication.
- **Authentication:** Implements **JWT** for secure user authentication, providing access to the API with Bearer tokens.
- **Containerization:** Utilizes **Docker** for containerizing the app.
- **Frontend:** In progress, being developed with **Angular**.
- **Database:** Stores data in **SQL Server**.
- **ORM:** Employs **Entity Framework** as the Object-Relational framework for database interaction.
- **Queries:** Using **LINQ** for querying and manipulating data.

## Features
- **Swagger Documentation:** Integrated with Swashbuckle.
- **Seed:** Provides a system for seeding initial data.
- **Personalized Token Logout Middleware(Token Blacklist):** Allow users to log out and invalidate tokens to prevent unauthorized access.
- **Role-Based Authorization:** Differentiating between admin and user roles with distinct permissions.
- **Automatic Interest Generation Service:** Automatically calculates and adds interest to saving accounts.
- **Self Signed Certificate:** Supports HTTPS.

## Requirements
- [.NET 7.0](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/products/docker-desktop/) (Optional: only if you plan to containerize).
  
## Configuration

**Clone the repository**

1. Clone the repository to your local machine.
```bash
git clone https://github.com/antiel29/SHIBANK.git
```

**Install dependacys**

1. Navigate to the backend directory.
```bash
cd SHIBANK/SHIBANK_BackEnd
```

2. Install the necessary dependencies.
```bash
dotnet restore
```

**Configure Certificate, JWT, and Database**
1. Open the `appsettings.json` file and update the following settings:
- Database connection string to point to your SQL server instance.
- JWT settings, including issuer, audience, and key.
- Kestrel settings, such as port and URL.

**Seeding Data**

1. In the backend directory, perform the following:

```bash
dotnet ef Add-Migration InitialCreate
```

```bash
dotnet ef Update-Database
```

```bash
dotnet run seeddata
```

## Usage

1. Run the application from the SHIBANK_Backend directory.

```bash
dotnet run
```

2. Access the API documentation at http://localhost:yourport/swagger to explore the available endpoints and make requests.
