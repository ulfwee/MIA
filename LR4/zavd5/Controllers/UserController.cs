using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usersDto = await _service.GetAllAsync();
            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var userDto = await _service.GetByIdAsync(id);
            return userDto is null ? NotFound() : Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            await _service.CreateAsync(userDto);
            return CreatedAtAction(nameof(Get), new { id = userDto.Id }, userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UserDto userDto)
        {
            userDto.Id = id;
            var result = await _service.UpdateAsync(userDto);
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
