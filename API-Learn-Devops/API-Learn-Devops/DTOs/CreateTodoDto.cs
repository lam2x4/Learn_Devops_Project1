using System.ComponentModel.DataAnnotations;
using API_Learn_Devops.Enums;

namespace API_Learn_Devops.DTOs
{
    public class CreateTodoDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Priority Priority { get; set; } = Priority.Medium;
    }
}
