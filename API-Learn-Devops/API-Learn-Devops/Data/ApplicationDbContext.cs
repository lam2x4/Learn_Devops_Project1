using API_Learn_Devops.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Learn_Devops.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> Todos { get; set; }
    }
}
