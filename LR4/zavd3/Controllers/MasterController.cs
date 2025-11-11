using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MastersController : ControllerBase
    {
        private readonly IMasterService _service;

        public MastersController(IMasterService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Master>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Master>> Get(int id)
        {
            try
            {
                var master = await _service.GetByIdAsync(id);
                return Ok(master);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Master>> Create(Master master)
        {
            try
            {
                await _service.AddAsync(master);
                return CreatedAtAction(nameof(Get), new { id = master.Id }, master);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Master master)
        {
            if (id != master.Id) return BadRequest("ID mismatch");
            await _service.UpdateAsync(master);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}