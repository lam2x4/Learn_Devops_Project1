using System.ComponentModel.DataAnnotations;

namespace API_Learn_Devops.DTOs
{
    public class UpdateTodoDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
