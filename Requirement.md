Role: You are a senior full-stack developer and software architect.
Objective: Build a production-ready Fullstack To-Do Application using ASP.NET Core Web API (Code-First) for the backend, React for the frontend, and PostgreSQL as the database.

📌 BACKEND REQUIREMENTS (ASP.NET Core Web API)

Framework: ASP.NET Core Web API (.NET 8)

Architecture:

Controllers

Services

Repositories

DTOs

Clean Architecture principles

ORM:

Entity Framework Core (Code-First)

Database:

PostgreSQL

Use Npgsql.EntityFrameworkCore.PostgreSQL

Entity:

TodoItem

Id (int, primary key)

Title (string, required, max length 200)

Description (string, optional)

IsCompleted (bool)

CreatedAt (DateTime, UTC)

DbContext:

ApplicationDbContext

Migrations:

Enable EF Core migrations

Show how to create and apply migrations

API Endpoints (RESTful):

GET /api/todos

GET /api/todos/{id}

POST /api/todos

PUT /api/todos/{id}

DELETE /api/todos/{id}

Features:

Data validation using Data Annotations

Proper HTTP status codes

Global exception handling middleware

Enable CORS for React

Swagger/OpenAPI enabled

Configuration:

PostgreSQL connection string in appsettings.json

Environment-based configuration

Folder structure must be clean and professional

📌 FRONTEND REQUIREMENTS (React)

Framework: React

Setup: Vite or Create React App

Language: JavaScript or TypeScript

UI Features:

Create new todo

Display list of todos

Mark todo as completed

Delete todo

State Management:

React Hooks (useState, useEffect)

API Communication:

Axios

UX:

Loading indicator

Error handling

Simple, clean, responsive UI

Environment:

.env file for API base URL

📌 INTEGRATION DETAILS

Backend runs on http://localhost:5000

Frontend runs on http://localhost:5173 (or default React port)

Frontend consumes backend REST APIs

CORS configured correctly

📌 DELIVERABLES

Backend project structure

Backend source code:

Entity

DbContext

Repository

Service

Controller

EF Core migration commands

Frontend project structure

React components and API service

PostgreSQL setup instructions

Step-by-step instructions to run:

Database

Backend

Frontend

Sample API request/response

README.md content

📌 OUTPUT FORMAT

Clear section headings

Code blocks for all source code

Concise explanations for each major part

Best practices and notes

Important constraints:

Use Code-First approach only

Follow clean code and REST best practices

Assume the user is a junior developer learning full-stack development