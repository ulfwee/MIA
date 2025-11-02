using System.Text.RegularExpressions;
using WebApplication1.Models;

namespace WebApplication1.Endpoints
{
    public static class BookingEndpoints
    {
        public static void MapBookingEndpoints(this WebApplication app)
        {
            var bookings = new List<Booking>()
            {
                new Booking { Id = 1, MasterId = 2, Date = "11.12.2025", ServiceDetails = "socket changing", Status = "pending" },
                new Booking { Id = 2, MasterId = 1, Date = "25.11.2025", ServiceDetails = "kitchen faucet repair", Status = "confirmed" }
            };

            app.MapGet("/bookings", () =>
            {
                return Results.Ok(bookings);
            });

            app.MapGet("/bookings/{id:int}", (int id) =>
            {
                var booking = bookings.FirstOrDefault(t => t.Id == id);
                return booking is not null ? Results.Ok(booking) : Results.NotFound();
            });

            app.MapPost("/bookings", (Booking newBooking, List<Master> masters, List<Booking> bookings) =>
            {
                if (!masters.Any(m => m.Id == newBooking.MasterId))
                    return Results.BadRequest($"No master found with Id = {newBooking.MasterId}");

                if (string.IsNullOrWhiteSpace(newBooking.ServiceDetails))
                    return Results.BadRequest("Поле 'Servicedetails' не може бути порожнім.");

                if (string.IsNullOrWhiteSpace(newBooking.Date))
                    return Results.BadRequest("Поле 'Date' не може бути порожнім.");
                if (newBooking.Date.Length < 10)
                    return Results.BadRequest("Дата має містити щонайменше 3 символи.");
                var datePattern = @"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.\d{4}$";
                if (!Regex.IsMatch(newBooking.Date, datePattern))
                    return Results.BadRequest("Невірний формат дати. Використовуйте формат dd.MM.yyyy (наприклад, 05.11.2025).");

                if (string.IsNullOrWhiteSpace(newBooking.Status))
                    return Results.BadRequest("Поле 'Status' не може бути порожнім.");

                newBooking.Id = bookings.Max(t => t.Id) + 1;
                bookings.Add(newBooking);

                return Results.Created($"/bookings/{newBooking.Id}", newBooking);
            });

            app.MapPut("/bookings/{id:int}", (int id, Booking updatedBooking, List<Master> masters) =>
            {
                var booking = bookings.FirstOrDefault(t => t.Id == id);
                if (booking is null) return Results.NotFound();

                if (!masters.Any(m => m.Id == updatedBooking.MasterId))
                    return Results.BadRequest($"No master found with Id = {updatedBooking.MasterId}");

                if (string.IsNullOrWhiteSpace(updatedBooking.ServiceDetails))
                    return Results.BadRequest("Поле 'Servicedetails' не може бути порожнім.");

                if (string.IsNullOrWhiteSpace(updatedBooking.Date))
                    return Results.BadRequest("Поле 'Date' не може бути порожнім.");
                if (updatedBooking.Date.Length < 10)
                    return Results.BadRequest("Дата має містити щонайменше 10 символи.");
                var datePattern = @"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.\d{4}$";
                if (!Regex.IsMatch(updatedBooking.Date, datePattern))
                    return Results.BadRequest("Невірний формат дати. Використовуйте формат dd.MM.yyyy (наприклад, 05.11.2025).");

                if (string.IsNullOrWhiteSpace(updatedBooking.Status))
                    return Results.BadRequest("Поле 'Status' не може бути порожнім.");

                booking.MasterId = updatedBooking.MasterId;
                booking.Date = updatedBooking.Date;
                booking.ServiceDetails = updatedBooking.ServiceDetails;
                booking.Status = updatedBooking.Status;

                return Results.Ok(booking);
            });

            app.MapDelete("/bookings/{id:int}", (int id) =>
            {
                var booking = bookings.FirstOrDefault(t => t.Id == id);
                if (booking is null) return Results.NotFound();

                bookings.Remove(booking);
                return Results.NoContent();
            });
        }
    }
}
