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

        Task<(IEnumerable<TodoItem> Items, int TotalCount)> SearchAsync(
            string? search,
            string? status,
            string? sortBy,
            string? sortOrder,
            int page,
            int pageSize);
    }
}
