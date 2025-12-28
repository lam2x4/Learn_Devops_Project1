# Todo App Backend (ASP.NET Core Web API)

## Prerequisites
- .NET 8 SDK
- PostgreSQL Database

## Setup Instructions

### 1. Database Configuration
Open `appsettings.json` and update the `ConnectionStrings:DefaultConnection` with your PostgreSQL credentials:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=TodoApp;Username=postgres;Password=YOUR_ACTUAL_PASSWORD"
}
```

### 2. Apply Migrations
Open a terminal in the project directory (`API-Learn-Devops`) and run:

```powershell
# Install EF Core tools if you haven't (global)
dotnet tool install --global dotnet-ef

# Create initial migration
dotnet ef migrations add InitialCreate

# Create database and apply schema
dotnet ef database update
```

### 3. Run the Application
```powershell
dotnet run
```

The API will start (usually on `http://localhost:5000` or `https://localhost:5001`).
Swagger UI will be available at: `http://localhost:5xxx/swagger/index.html`

## API Endpoints
- `GET /api/todos`: Get all todos
- `GET /api/todos/{id}`: Get a specific todo
- `POST /api/todos`: Create a new todo
- `PUT /api/todos/{id}`: Update a todo
- `DELETE /api/todos/{id}`: Delete a todo
