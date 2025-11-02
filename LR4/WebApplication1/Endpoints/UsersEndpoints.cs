using System.Text.RegularExpressions;
using WebApplication1.Models;

namespace WebApplication1.Endpoints
{
    public static class UsersEndpoints
    {
        public static void MapUsersEndpoints(this WebApplication app)
        {
            var users = new List<User>()
            {
                new User { Id = 1, Name = "Clara", Age = 21, Email = "clara123@gmail.com" },
                new User { Id = 2, Name = "Thomas", Age = 34, Email = "thomasredish@gmail.com" }
            };

            app.MapGet("/users", () =>
            {
                return Results.Ok(users);
            });

            app.MapGet("/users/{id:int}", (int id) =>
            {
                var user = users.FirstOrDefault(t => t.Id == id);
                return user is not null ? Results.Ok(user) : Results.NotFound();
            });

            app.MapPost("/users", (User newUser) =>
            {
                if (string.IsNullOrWhiteSpace(newUser.Name))
                    return Results.BadRequest("Поле 'Name' не може бути порожнім.");

                if (string.IsNullOrWhiteSpace(newUser.Email))
                    return Results.BadRequest("Поле 'Email' не може бути порожнім.");
                if (newUser.Email.Length < 11)
                    return Results.BadRequest("Email має містити щонайменше 3 символи.");
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(newUser.Email, emailPattern, RegexOptions.IgnoreCase))
                    return Results.BadRequest("Невірний формат Email.");

                newUser.Id = users.Max(t => t.Id) + 1;
                users.Add(newUser);
                return Results.Created($"/todos/{newUser.Id}", newUser);
            });

            app.MapPut("/users/{id:int}", (int id, User updatedUser) =>
            {
                var user = users.FirstOrDefault(t => t.Id == id);
                if (user is null) return Results.NotFound();

                if (string.IsNullOrWhiteSpace(updatedUser.Name))
                    return Results.BadRequest("Поле 'Name' не може бути порожнім.");

                if (string.IsNullOrWhiteSpace(updatedUser.Email))
                    return Results.BadRequest("Поле 'Email' не може бути порожнім.");
                if (updatedUser.Email.Length < 11)
                    return Results.BadRequest("Email має містити щонайменше 11 символи.");
                
                user.Name = updatedUser.Name;
                user.Age = updatedUser.Age;
                user.Email = updatedUser.Email;

                return Results.Ok(user);
            });

            app.MapDelete("/users/{id:int}", (int id) =>
            {
                var user = users.FirstOrDefault(t => t.Id == id);
                if (user is null) return Results.NotFound();

                users.Remove(user);
                return Results.NoContent();
            });


        }
    }
}
