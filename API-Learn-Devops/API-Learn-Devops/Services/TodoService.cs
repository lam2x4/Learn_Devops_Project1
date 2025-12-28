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
                CreatedAt = todo.CreatedAt
            };
        }

        public async Task<TodoDto> CreateAsync(CreateTodoDto createTodoDto)
        {
            var todo = new TodoItem
            {
                Title = createTodoDto.Title,
                Description = createTodoDto.Description
            };

            var createdTodo = await _repository.AddAsync(todo);

            return new TodoDto
            {
                Id = createdTodo.Id,
                Title = createdTodo.Title,
                Description = createdTodo.Description,
                IsCompleted = createdTodo.IsCompleted,
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
    }
}
