using API_Learn_Devops.Entities;

namespace API_Learn_Devops.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(int id);
        Task<TodoItem> AddAsync(TodoItem todo);
        Task UpdateAsync(TodoItem todo);
        Task DeleteAsync(int id);
    }
}
