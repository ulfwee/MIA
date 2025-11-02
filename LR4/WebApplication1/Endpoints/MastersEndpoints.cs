using WebApplication1.Models;

namespace WebApplication1.Endpoints
{
    public static class MastersEndpoints
    {
        public static void MapMastersEndpoints(this WebApplication app)
        {
            var masters = new List<Master>()
            {
                new Master { Id = 1, Name = "Theodor", Category = "repair", Ranking = 4.5 },
                new Master { Id = 2, Name = "Samanta", Category = "repair", Ranking = 4.8 }
            };

            app.MapGet("/masters", () =>
            {
                return Results.Ok(masters);
            });

            app.MapGet("/masters/{id:int}", (int id) =>
            {
                var master = masters.FirstOrDefault(t => t.Id == id);
                return master is not null ? Results.Ok(master) : Results.NotFound();
            });

            app.MapPost("/masters", (Master newMaster) =>
            {
                if (string.IsNullOrWhiteSpace(newMaster.Name))
                    return Results.BadRequest("Поле 'Name' не може бути порожнім.");

                if (string.IsNullOrWhiteSpace(newMaster.Category))
                    return Results.BadRequest("Поле 'Category' не може бути порожнім.");
                
                newMaster.Id = masters.Max(t => t.Id) + 1;
                masters.Add(newMaster);

                return Results.Created($"/masters/{newMaster.Id}", newMaster);
            });

            app.MapPut("/masters/{id:int}", (int id, Master updatedMaster) =>
            {
                var master = masters.FirstOrDefault(t => t.Id == id);
                if (master is null) return Results.NotFound();

                if (string.IsNullOrWhiteSpace(updatedMaster.Name))
                    return Results.BadRequest("Поле 'Name' не може бути порожнім.");

                if (string.IsNullOrWhiteSpace(updatedMaster.Category))
                    return Results.BadRequest("Поле 'Category' не може бути порожнім.");

                master.Name = updatedMaster.Name;
                master.Category = updatedMaster.Category;
                master.Ranking = updatedMaster.Ranking;

                return Results.Ok(master);
            });

            app.MapDelete("/masters/{id:int}", (int id) =>
            {
                var master = masters.FirstOrDefault(t => t.Id == id);
                if (master is null) return Results.NotFound();

                masters.Remove(master);
                return Results.NoContent();
            });
        }
    }
}
