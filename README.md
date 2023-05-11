# Expenses Tracker App (Backend)

This is the README file for the backend of the Expenses Tracker App, a C# application designed to manage expenses. The backend is responsible for handling data storage, retrieval, and processing.

## Features

- User authentication and authorization
- Expense creation, retrieval, update, and deletion
- Category management
- Filtering and sorting expenses
- Reporting and statistics generation

## Technologies Used

The backend of the Expenses Tracker App is built using the following technologies:

- C# programming language
- ASP.NET Core framework
- Entity Framework Core for data access and ORM
- SQL Server for data storage
- JWT (JSON Web Tokens) for authentication and authorization
- Swagger for API documentation
- Unit testing frameworks (e.g., NUnit, xUnit) for testing

## Getting Started

To set up and run the Expenses Tracker App backend, follow these steps:

1. Clone the repository:
git clone [https://github.com/your/repository.git](https://github.com/Larajihed/Expenses-Tracker-App)

2. Install the required dependencies using NuGet:
dotnet restore

3. Create a SQL Server database and update the connection string in the `appsettings.json` file:

```json
"ConnectionStrings": {
    "DefaultConnection": "YourConnectionString"
}
```
4. Apply the database migrations to create the required tables:
dotnet ef database update

5. Build the project:
dotnet build

6. Run the application:
dotnet run

7. Access the API documentation using Swagger by navigating to:
[Access the API documentation using Swagger by navigating to:
](http://localhost:5000/swagger)

8-Start making API requests to manage expenses.

## API Endpoints
The following API endpoints are available for managing expenses:

GET /api/expenses: Retrieve a list of all expenses.
GET /api/expenses/{id}: Retrieve details of a specific expense.
POST /api/expenses: Create a new expense.
PUT /api/expenses/{id}: Update details of a specific expense.
DELETE /api/expenses/{id}: Delete a specific expense.
GET /api/categories: Retrieve a list of all categories.
POST /api/categories: Create a new category.

## Security
The Expenses Tracker App backend utilizes JWT for authentication and authorization. When making API requests, include an authorization header with a valid JWT token:
Authorization: Bearer {your_token}
To obtain a JWT token, send a POST request to the /api/auth/login endpoint with valid user credentials. The response will contain the JWT token.




## Contributing
Contributions to the Expenses Tracker App are welcome! If you find any issues or have ideas for improvements, please submit an issue or pull request to the repository.

## License
The Expenses Tracker App is open source and released under the MIT License.

## Acknowledgements
This Expenses Tracker App backend was created by Jihed Larayedh.




