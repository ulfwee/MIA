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
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]  // Тільки авторизовані користувачі
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingService.GetAsync();
            var dtos = _mapper.Map<List<BookingDto>>(bookings);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(string id)
        {
            var booking = await _bookingService.GetAsync(id);
            if (booking == null) return NotFound("Бронювання не знайдено.");
            // Перевірка, чи належить користувачу (опціонально)
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (booking.UserId != userId) return Unauthorized("Доступ заборонено.");
            var dto = _mapper.Map<BookingDto>(booking);
            return Ok(dto);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetByUser()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var bookings = await _bookingService.GetByUserIdAsync(userId);
            var dtos = _mapper.Map<List<BookingDto>>(bookings);
            return Ok(dtos);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BookingDto dto)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var booking = _mapper.Map<Booking>(dto);
            booking.UserId = userId;  // Встановлюємо UserId з токена
            await _bookingService.CreateAsync(booking);
            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, _mapper.Map<BookingDto>(booking));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, BookingDto dto)
        {
            var existing = await _bookingService.GetAsync(id);
            if (existing == null) return NotFound("Бронювання не знайдено.");
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (existing.UserId != userId) return Unauthorized("Доступ заборонено.");
            var booking = _mapper.Map<Booking>(dto);
            booking.Id = id;
            await _bookingService.UpdateAsync(booking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _bookingService.GetAsync(id);
            if (existing == null) return NotFound("Бронювання не знайдено.");
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (existing.UserId != userId) return Unauthorized("Доступ заборонено.");
            await _bookingService.DeleteAsync(id);
            return NoContent();
        }
    }
}