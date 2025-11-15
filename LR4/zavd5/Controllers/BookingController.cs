using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookingsDto = await _service.GetAllAsync();
            return Ok(bookingsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var bookingDto = await _service.GetByIdAsync(id);
            return bookingDto is null ? NotFound() : Ok(bookingDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingDto bookingDto)
        {
            await _service.CreateAsync(bookingDto);
            return CreatedAtAction(nameof(Get), new { id = bookingDto.Id }, bookingDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, BookingDto bookingDto)
        {
            bookingDto.Id = id;
            var result = await _service.UpdateAsync(bookingDto);
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