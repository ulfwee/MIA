using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Dtos;
using MyWebApi.Entities;
using MyWebApi.Services;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly IMapper _mapper;

        public TodoItemController(ITodoItemService todoItemService, IMapper mapper)
        {
            _todoItemService = todoItemService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoItemService.GetAsync();
            var dtos = _mapper.Map<List<TodoItemDto>>(todos);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(string id)
        {
            var todo = await _todoItemService.GetAsync(id);
            if (todo == null) return NotFound("Завдання не знайдено.");
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (todo.UserId != userId) return Unauthorized("Доступ заборонено.");
            var dto = _mapper.Map<TodoItemDto>(todo);
            return Ok(dto);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetByUser()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var todos = await _todoItemService.GetByUserIdAsync(userId);
            var dtos = _mapper.Map<List<TodoItemDto>>(todos);
            return Ok(dtos);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(TodoItemDto dto)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var todo = _mapper.Map<TodoItem>(dto);
            todo.UserId = userId;
            await _todoItemService.CreateAsync(todo);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, _mapper.Map<TodoItemDto>(todo));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, TodoItemDto dto)
        {
            var existing = await _todoItemService.GetAsync(id);
            if (existing == null) return NotFound("Завдання не знайдено.");
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (existing.UserId != userId) return Unauthorized("Доступ заборонено.");
            var todo = _mapper.Map<TodoItem>(dto);
            todo.Id = id;
            await _todoItemService.UpdateAsync(todo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _todoItemService.GetAsync(id);
            if (existing == null) return NotFound("Завдання не знайдено.");
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (existing.UserId != userId) return Unauthorized("Доступ заборонено.");
            await _todoItemService.DeleteAsync(id);
            return NoContent();
        }
    }
}