using System.ComponentModel.DataAnnotations;
using API_Learn_Devops.Enums;

namespace API_Learn_Devops.Entities
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public Priority Priority { get; set; } = Priority.Medium;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
