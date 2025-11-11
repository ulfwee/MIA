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

        public MastersController(IMasterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var master = await _service.GetByIdAsync(id);
            return master is null ? NotFound() : Ok(master);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Master master)
        {
            await _service.CreateAsync(master);
            return CreatedAtAction(nameof(Get), new { id = master.Id }, master);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Master master)
        {
            master.Id = id;
            var result = await _service.UpdateAsync(master);
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