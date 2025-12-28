using API_Learn_Devops.Entities;

namespace API_Learn_Devops.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure the database is created (optional, if you rely on migrations)
            // context.Database.EnsureCreated();

            // Look for any todos.
            if (context.Todos.Any())
            {
                return;   // DB has been seeded
            }

            var todos = new TodoItem[]
            {
                new TodoItem 
                { 
                    Title = "Setup Project Structure", 
                    Description = "Initialize the solution and Git repository", 
                    IsCompleted = true, 
                    CreatedAt = DateTime.UtcNow.AddDays(-2) 
                },
                new TodoItem 
                { 
                    Title = "Implement Backend API", 
                    Description = "Create CRUD endpoints using .NET 8", 
                    IsCompleted = true, 
                    CreatedAt = DateTime.UtcNow.AddDays(-1) 
                },
                new TodoItem 
                { 
                    Title = "Develop Frontend", 
                    Description = "Build React application with Vite", 
                    IsCompleted = false, 
                    CreatedAt = DateTime.UtcNow 
                },
                new TodoItem 
                { 
                    Title = "Configure Docker", 
                    Description = "Create Dockerfile and docker-compose.yml", 
                    IsCompleted = false, 
                    CreatedAt = DateTime.UtcNow 
                },
                new TodoItem 
                { 
                    Title = "Setup CI/CD Pipeline", 
                    Description = "Automate testing and deployment", 
                    IsCompleted = false, 
                    CreatedAt = DateTime.UtcNow 
                }
            };

            context.Todos.AddRange(todos);
            context.SaveChanges();
        }
    }
}
