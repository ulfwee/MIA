using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private static List<Booking> bookings = new()
        {
            new Booking { Id = 1, MasterId = 2, Date = "22.11.2025", ServiceDetails = "Deck repairs", Status = Status.Confirmed },
            new Booking { Id = 2, MasterId = 1, Date = "10.11.2025", ServiceDetails = "Socket repairs", Status = Status.Pending }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetAll()
        {
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public ActionResult<Booking> GetById(int id)
        {
            var todo = bookings.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return NotFound($"Завдання з ID={id} не знайдено.");

            return Ok(todo);
        }

        [HttpPost]
        public IActionResult AddBooking([FromBody] Booking newBooking)
        {
            newBooking.Id = bookings.Count + 1;
            
            if (!Enum.IsDefined(typeof(Status), newBooking.Status))
                return BadRequest($"Invalid Difficulty value: {newBooking.Status}");

            bookings.Add(newBooking);

            return CreatedAtAction(nameof(GetById), new { id = newBooking.Id }, newBooking);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Booking updatedBooking)
        {
            var booking = bookings.FirstOrDefault(t => t.Id == id);

            if (booking == null)
                return NotFound($"Завдання з ID={id} не знайдено.");

            //if (!masters.Any(m => m.Id == updatedBooking.MasterId))
            //    return Results.BadRequest($"No master found with Id = {updatedBooking.MasterId}");


            booking.MasterId = updatedBooking.MasterId;
            booking.Date = updatedBooking.Date;
            booking.Status = updatedBooking.Status;
            booking.Status = updatedBooking.Status;


            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var booking = bookings.FirstOrDefault(t => t.Id == id);

            if (booking == null)
                return NotFound($"Завдання з ID={id} не знайдено.");

            bookings.Remove(booking);

            return NoContent();
        }
    }
}