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
        public async Task<IActionResult> GetAll()
        {
            var mastersDto = await _service.GetAllAsync();
            return Ok(mastersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var masterDto = await _service.GetByIdAsync(id);
            return masterDto is null ? NotFound() : Ok(masterDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MasterDto masterDto)
        {
            await _service.CreateAsync(masterDto);
            return CreatedAtAction(nameof(Get), new { id = masterDto.Id }, masterDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, MasterDto masterDto)
        {
            masterDto.Id = id;
            var result = await _service.UpdateAsync(masterDto);
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
