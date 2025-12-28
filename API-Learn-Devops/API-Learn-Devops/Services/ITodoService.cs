using API_Learn_Devops.DTOs;

namespace API_Learn_Devops.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetAllAsync();
        Task<TodoDto?> GetByIdAsync(int id);
        Task<TodoDto> CreateAsync(CreateTodoDto createTodoDto);
        Task<bool> UpdateAsync(int id, UpdateTodoDto updateTodoDto);
        Task<bool> DeleteAsync(int id);
    }
}
