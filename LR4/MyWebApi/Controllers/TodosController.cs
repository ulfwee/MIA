using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> todos = new()
        {
            new TodoItem { Id = 1, Name = "Вивчити ASP.NET Core Web API", IsComplete = true },
            new TodoItem { Id = 2, Name = "Створити власний ToDo контролер", IsComplete = false }
        };

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return NotFound($"Завдання з ID={id} не знайдено.");

            return Ok(todo);
        }

        [HttpPost]
        public ActionResult<TodoItem> Create(TodoItem newTodo)
        {
            newTodo.Id = todos.Count + 1;
            todos.Add(newTodo);

            return CreatedAtAction(nameof(GetById), new { id = newTodo.Id }, newTodo);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TodoItem updatedTodo)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return NotFound($"Завдання з ID={id} не знайдено.");

            todo.Name = updatedTodo.Name;
            todo.IsComplete = updatedTodo.IsComplete;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return NotFound($"Завдання з ID={id} не знайдено.");

            todos.Remove(todo);

            return NoContent();
        }
    }
}