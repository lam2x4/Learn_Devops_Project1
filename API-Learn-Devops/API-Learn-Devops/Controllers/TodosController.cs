using API_Learn_Devops.DTOs;
using API_Learn_Devops.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Learn_Devops.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodosController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet("search")]
        public async Task<ActionResult<PagedResult<TodoDto>>> Search([FromQuery] TodoQuery query)
        {
            var result = await _service.SearchAsync(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetAll()
        {
            var todos = await _service.GetAllAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetById(int id)
        {
            var todo = await _service.GetByIdAsync(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoDto>> Create(CreateTodoDto createTodoDto)
        {
            var todo = await _service.CreateAsync(createTodoDto);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTodoDto updateTodoDto)
        {
            var result = await _service.UpdateAsync(id, updateTodoDto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
