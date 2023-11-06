# SHIBANK - Secure Home Investment Bank
A Homebanking REST API that enables user registration, account and card creation, fund transfers, and secure transactions with others.

## Table of Contents
- [Technologies](#technologies)
- [Features](#features)
- [Requirements](#requirements)
- [Configuration](#configuration)
- [Usage](#usage)

## Technologies
- **Backend:** Developed using **.NET 7.0**.
- **Security:** Utilizing **ASP.Net Core Identity** for user roles managment and authentication.
- **Authentication:** Implementing **JWT** for secure user authentication using Bearer type providing access to api.
- **Containerization:** Using **Docker** to containerize the app.
- **Frontend:** In progress, being developed with **Angular**.
- **Database:** Storing data in **SQL Server**.
- **ORM:** Employing **Entity Framework** as the Object-Relational framework for database interaction.
- **Queries:** Using **LINQ** for quering and manipulating data.

## Features
- **Swagger Documentation:** Integrated with Swashbuckle.
- **Seed:** System for seeding initial data and try the app.
- **Personalized Token Logout Middleware(Token Blacklist):** Allow users log out tokens stay in unauthorized access.
- **Role-Based Authorization:** Differentiating between admin and user roles with distinct permissions.
- **Automatic Interest Generation Service:** Automatically calculates and adds interest to saving accounts.
- **Self Signed Certificate:** For https.

## Requirements
- [.NET 7.0](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/products/docker-desktop/) (Optional: only if you plan to containerize).
  
## Configuration

**Clone the repository**
1. Clone the repository to your local machine
```bash
git clone https://github.com/antiel29/SHIBANK.git
```
**Install dependacys**
1. Navigate to the backend directory
```bash
cd SHIBANK/SHIBANK_BackEnd
```
2. Install the dependacys
```bash
dotnet restore
```
**Configure Certificate,JWT and Database**
1. Open the `appsettings.json` file and update the database connection string to point to your SQL server instance.

2. In the same file,configure the JWT settings such as issuer,audience,key.

3. In the same file, configure the Kestrel settings such as port and url.

**Seeding**

1. Navigate to the backend directory and  in the nugget terminal
```bash
Add-Migration InitialCreate
```
```bash
Update-Database
```
```bash
dotnet run seeddata
```

## Usage
1. Run in the SHIBANK_Backend directory.
```bash
dotnet run
```
2. Access the API documentation at http://localhost:yourport/swagger to explore the available endpoints and make requests.
