using API_Learn_Devops.DTOs;
using API_Learn_Devops.Entities;
using API_Learn_Devops.Repositories;

namespace API_Learn_Devops.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoDto>> GetAllAsync()
        {
            var todos = await _repository.GetAllAsync();
            return todos.Select(t => new TodoDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                Priority = t.Priority,
                CreatedAt = t.CreatedAt
            });
        }

        public async Task<TodoDto?> GetByIdAsync(int id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) return null;

            return new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                Priority = todo.Priority,
                CreatedAt = todo.CreatedAt
            };
        }

        public async Task<TodoDto> CreateAsync(CreateTodoDto createTodoDto)
        {
            var todo = new TodoItem
            {
                Title = createTodoDto.Title,
                Description = createTodoDto.Description,
                Priority = createTodoDto.Priority
            };

            var createdTodo = await _repository.AddAsync(todo);

            return new TodoDto
            {
                Id = createdTodo.Id,
                Title = createdTodo.Title,
                Description = createdTodo.Description,
                IsCompleted = createdTodo.IsCompleted,
                Priority = createdTodo.Priority,
                CreatedAt = createdTodo.CreatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateTodoDto updateTodoDto)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) return false;

            todo.Title = updateTodoDto.Title;
            todo.Description = updateTodoDto.Description;
            todo.IsCompleted = updateTodoDto.IsCompleted;
            todo.Priority = updateTodoDto.Priority;

            await _repository.UpdateAsync(todo);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<PagedResult<TodoDto>> SearchAsync(TodoQuery query)
        {
            var page = query.Page < 1 ? 1 : query.Page;
            var pageSize = query.PageSize <= 0 ? 10 : Math.Min(query.PageSize, 100);

            var (items, totalCount) = await _repository.SearchAsync(
                query.Search,
                query.Status,
                query.SortBy,
                query.SortOrder,
                page,
                pageSize);

            var resultItems = items.Select(t => new TodoDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                Priority = t.Priority,
                CreatedAt = t.CreatedAt
            });

            return new PagedResult<TodoDto>
            {
                Items = resultItems,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
