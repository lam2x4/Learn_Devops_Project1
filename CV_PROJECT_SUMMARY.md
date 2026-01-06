# Full-Stack DevOps Todo Application

## Project Overview
A production-ready, cloud-deployed task management application demonstrating modern full-stack development and DevOps practices. The project features a RESTful API backend, responsive React frontend, and enterprise-grade deployment architecture using AWS infrastructure.

**Live Demo:** Deployed on AWS EC2 (Backend) + Vercel (Frontend)  
**Source Code:** [GitHub Repository Link]

---

## Technical Stack

### Backend
- **Framework:** ASP.NET Core 8.0 Web API
- **Architecture:** Clean Architecture (Controllers → Services → Repositories → Data)
- **ORM:** Entity Framework Core (Code-First approach)
- **Database:** PostgreSQL (AWS RDS)
- **API Documentation:** Swagger/OpenAPI

### Frontend
- **Framework:** React 19 with Vite
- **State Management:** React Hooks (useState, useEffect)
- **HTTP Client:** Axios
- **UI Features:** Responsive design, pagination, search & filter, real-time updates

### DevOps & Infrastructure
- **Containerization:** Docker & Docker Compose
- **Cloud Platform:** AWS (EC2, RDS)
- **Frontend Hosting:** Vercel (CDN deployment)
- **CI/CD:** Automated deployments with Docker containerization
- **Database Migration:** Entity Framework Core Migrations

---

## Key Features Implemented

### Backend API
- ✅ **RESTful Endpoints:** Complete CRUD operations (GET, POST, PUT, DELETE)
- ✅ **Advanced Querying:** Search, filtering, sorting, and pagination
- ✅ **Data Validation:** Using Data Annotations and DTOs
- ✅ **Global Exception Handling:** Custom middleware for error management
- ✅ **CORS Configuration:** Secure cross-origin resource sharing
- ✅ **Database Seeding:** Automated data initialization

### Frontend Application
- ✅ **Task Management:** Create, update, complete, and delete tasks
- ✅ **Search & Filter:** Real-time search with status filtering (All/Active/Completed)
- ✅ **Pagination:** Efficient data loading with page navigation
- ✅ **Responsive UI:** Modern, user-friendly interface
- ✅ **Error Handling:** User-friendly error messages and loading states

### DevOps Implementation
- ✅ **Docker Multi-stage Builds:** Optimized container images
- ✅ **Docker Compose Orchestration:** Multi-container application management
- ✅ **AWS EC2 Deployment:** Ubuntu server hosting with security groups
- ✅ **AWS RDS Integration:** Managed PostgreSQL database service
- ✅ **Vercel Deployment:** Frontend deployed to global CDN
- ✅ **Environment Configuration:** Separate development/production settings

---

## Architecture Design

```
┌─────────────────────┐
│  Frontend (Vercel)  │ ← React + Vite
│  Global CDN         │
└──────────┬──────────┘
           │ HTTPS API Calls
           ↓
┌─────────────────────┐
│  Backend (AWS EC2)  │ ← .NET 8 Web API
│  Docker Container   │
└──────────┬──────────┘
           │
           ↓
┌─────────────────────┐
│  Database (AWS RDS) │ ← PostgreSQL
│  Managed Service    │
└─────────────────────┘
```

---

## Project Structure

```
Project-Devops/
├── API-Learn-Devops/              # Backend .NET Solution
│   ├── Controllers/               # API Endpoints
│   ├── Services/                  # Business Logic Layer
│   ├── Repositories/              # Data Access Layer
│   ├── DTOs/                      # Data Transfer Objects
│   ├── Entities/                  # Database Models
│   ├── Middleware/                # Custom Middleware
│   ├── Migrations/                # EF Core Migrations
│   └── Dockerfile                 # Backend containerization
├── frontend/                      # React Application
│   ├── src/
│   │   ├── components/            # Reusable UI Components
│   │   ├── services/              # API Integration Layer
│   │   └── App.jsx                # Main Application
│   └── vercel.json                # Deployment configuration
├── docker-compose.yml             # Container orchestration
└── Documentation/                 # Deployment guides
```

---

## Technical Highlights

### Clean Architecture Implementation
- **Separation of Concerns:** Controllers, Services, Repositories pattern
- **Dependency Injection:** Built-in ASP.NET Core DI container
- **DTO Pattern:** Decoupled API contracts from database entities
- **Interface-based Design:** Testable and maintainable code

### Database Design
- **Code-First Approach:** Entity Framework Core migrations
- **Data Annotations:** Built-in validation at entity level
- **Automatic Migrations:** Applied on application startup
- **UTC Timestamps:** Consistent timezone handling

### DevOps Best Practices
- **Infrastructure as Code:** Docker Compose configuration
- **Multi-stage Docker Builds:** Reduced image size and security
- **Environment Variables:** Externalized configuration
- **Cloud-native Deployment:** Leveraging AWS managed services
- **Continuous Deployment:** Automated builds on Vercel

---

## Deployment Process

### Backend Deployment (AWS EC2)
1. Provisioned Ubuntu EC2 instance (t3.small)
2. Configured security groups for HTTP/HTTPS access
3. Installed Docker and Docker Compose
4. Built and deployed containerized .NET application
5. Connected to AWS RDS PostgreSQL database
6. Configured automatic database migrations on startup

### Frontend Deployment (Vercel)
1. Connected GitHub repository to Vercel
2. Configured build settings (Vite, React)
3. Set environment variables for API endpoints
4. Deployed to global CDN with automatic HTTPS
5. Implemented CORS configuration for secure API access

### Database Deployment (AWS RDS)
1. Created managed PostgreSQL instance
2. Configured VPC security groups
3. Established secure connection from EC2
4. Migrated data using pg_dump/restore
5. Updated connection strings in backend configuration

---

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todos` | Retrieve all tasks |
| GET | `/api/todos/{id}` | Retrieve specific task |
| GET | `/api/todos/search` | Advanced search with pagination |
| POST | `/api/todos` | Create new task |
| PUT | `/api/todos/{id}` | Update existing task |
| DELETE | `/api/todos/{id}` | Delete task |

---

## Skills Demonstrated

### Backend Development
- RESTful API design and implementation
- Clean Architecture and design patterns
- Entity Framework Core and Code-First migrations
- Dependency injection and service lifetime management
- Custom middleware development
- Data validation and error handling

### Frontend Development
- Modern React with Hooks
- Component-based architecture
- State management and side effects
- HTTP client integration
- Responsive UI/UX design
- Error handling and loading states

### DevOps & Cloud
- Docker containerization and multi-stage builds
- Docker Compose orchestration
- AWS EC2 instance management
- AWS RDS database configuration
- Security group and networking configuration
- Environment configuration management
- Vercel deployment and CDN optimization

### Database Management
- PostgreSQL database design
- Code-First migrations
- Data seeding and initialization
- Database backup and restoration
- Cloud database migration (Docker → RDS)

---

## Challenges Solved

1. **CORS Configuration:** Implemented secure cross-origin requests between Vercel and EC2
2. **Database Migration:** Successfully migrated from containerized Postgres to AWS RDS
3. **Environment Management:** Configured separate settings for development/production
4. **Docker Optimization:** Used multi-stage builds to reduce image size
5. **Global Exception Handling:** Implemented centralized error management middleware

---

## Future Enhancements
- Implement authentication and authorization (JWT)
- Add unit and integration tests
- Set up CI/CD pipeline with GitHub Actions
- Implement caching layer (Redis)
- Add real-time updates using SignalR
- Migrate to Kubernetes for orchestration

---

## Technologies & Tools

**Languages:** C#, JavaScript  
**Frameworks:** ASP.NET Core 8.0, React 19, Entity Framework Core  
**Database:** PostgreSQL  
**Cloud Services:** AWS (EC2, RDS), Vercel  
**DevOps:** Docker, Docker Compose  
**Tools:** Git, Swagger, Vite, Axios  
**Patterns:** Repository Pattern, Service Layer, DTO Pattern, Clean Architecture
