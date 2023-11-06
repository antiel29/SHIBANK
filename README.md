# SHIBANK - Secure Home Investment Bank
A Homebanking REST API that enables user registration, account and card creation, fund transfers, and secure transactions with others.

# Technologies:
- **Backend:** Developed using **.NET 7.0**.
- **Security:** Utilizing **ASP.Net Core Identity** for user roles managment and authentication.
- **Authentication:** Implementing **JWT** for secure user authentication using Bearer type providing access to api.
- **Containerization:** Using **Docker** to containerize the app.
- **Frontend:** In progress, being developed with **Angular**.
- **Database:** Storing data in **SQL Server**.
- **ORM:** Employing **Entity Framework** as the Object-Relational framework for database interaction.
- **Queries:** Using **LINQ** for quering and manipulating data.

# Features:
- **Swagger Documentation:** Integrated with Swashbuckle.
- **Seed:** System for seeding initial data and try the app.
- **Personalized Token Logout Middleware(Token Blacklist):** Allow users log out tokens stay in unauthorized access.
- **Role-Based Authorization:** Differentiating between admin and user roles with distinct permissions.
- **Automatic Interest Generation Service:** Automatically calculates and adds interest to saving accounts.
