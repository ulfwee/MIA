using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static List<User> users = new()
        {
            new User { Id = 1, Name = "Mary", Age = 32, Email = "mary33@gmail.com" },
            new User { Id = 2, Name = "Dorothy", Age = 54, Email = "dorothy22@gmail.com" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var todo = users.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return NotFound($"Користувача з ID={id} не знайдено.");

            return Ok(todo);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User newUser)
        {
            newUser.Id = users.Count + 1;
            users.Add(newUser);

            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(t => t.Id == id);

            if (user == null)
                return NotFound($"Користувач з ID={id} не знайдено.");

            user.Name = updatedUser.Name;
            user.Age = updatedUser.Age;
            user.Email = updatedUser.Email;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(t => t.Id == id);

            if (user == null)
                return NotFound($"Користувач з ID={id} не знайдено.");

            users.Remove(user);

            return NoContent();
        }
    }
}