using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _service;

        public TodoItemsController(ITodoItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todosDto = await _service.GetAllAsync();
            return Ok(todosDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var todoDto = await _service.GetByIdAsync(id);
            return todoDto is null ? NotFound() : Ok(todoDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoItemDto todoDto)
        {
            await _service.CreateAsync(todoDto);
            return CreatedAtAction(nameof(Get), new { id = todoDto.Id }, todoDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, TodoItemDto todoDto)
        {
            todoDto.Id = id;
            var result = await _service.UpdateAsync(todoDto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
