using System.Collections.Generic;
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
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;
        private readonly IMapper _mapper;

        public MasterController(IMasterService masterService, IMapper mapper)
        {
            _masterService = masterService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()  // Публічний доступ
        {
            var masters = await _masterService.GetAsync();
            var dtos = _mapper.Map<List<MasterDto>>(masters);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var master = await _masterService.GetAsync(id);
            if (master == null) return NotFound("Майстер не знайдено.");
            var dto = _mapper.Map<MasterDto>(master);
            return Ok(dto);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            if (!Enum.TryParse<Category>(category, true, out var cat)) return BadRequest("Неправильна категорія.");
            var masters = await _masterService.GetByCategoryAsync(cat);
            var dtos = _mapper.Map<List<MasterDto>>(masters);
            return Ok(dtos);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]  // Тільки адміни можуть створювати майстрів
        public async Task<IActionResult> Create(MasterDto dto)
        {
            var master = _mapper.Map<Master>(dto);
            await _masterService.CreateAsync(master);
            return CreatedAtAction(nameof(GetById), new { id = master.Id }, _mapper.Map<MasterDto>(master));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, MasterDto dto)
        {
            var existing = await _masterService.GetAsync(id);
            if (existing == null) return NotFound("Майстер не знайдено.");
            var master = _mapper.Map<Master>(dto);
            master.Id = id;
            await _masterService.UpdateAsync(master);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _masterService.GetAsync(id);
            if (existing == null) return NotFound("Майстер не знайдено.");
            await _masterService.DeleteAsync(id);
            return NoContent();
        }
    }
}