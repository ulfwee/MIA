using WebApplication1.Models;


namespace WebApplication1.Endpoints
{
    public static class TodoEndpoints
    {
        public static void MapTodoEndpoints(this WebApplication app)
        {
            var todoItems = new List<TodoItem>()
            {
                new TodoItem { Id = 1, Name = "Learn .NET 8 Minimal APIs", IsComplete = true },
                new TodoItem { Id = 2, Name = "Build a Simple To-Do API", IsComplete = false }
            };

            app.MapGet("/todos", () =>
            {
                return Results.Ok(todoItems);
            });

            app.MapGet("/todos/{id:int}", (int id) =>
            {
                var todo = todoItems.FirstOrDefault(t => t.Id == id);
                return todo is not null ? Results.Ok(todo) : Results.NotFound();
            });

            app.MapPost("/todos", (TodoItem newTodo) =>
            {
                if (!ValidateTodo(newTodo, out var error))
                    return Results.BadRequest(error);

                newTodo.Id = todoItems.Count + 1;
                todoItems.Add(newTodo);
                return Results.Created($"/todos/{newTodo.Id}", newTodo);
            });

            app.MapPut("/todos/{id:int}", (int id, TodoItem updatedTodo) =>
            {
                var todo = todoItems.FirstOrDefault(t => t.Id == id);
                if (todo is null) return Results.NotFound();

                todo.Name = updatedTodo.Name;
                todo.IsComplete = updatedTodo.IsComplete;

                return Results.Ok(todo);
            });

            app.MapDelete("/todos/{id:int}", (int id) =>
            {
                var todo = todoItems.FirstOrDefault(t => t.Id == id);
                if (todo is null) return Results.NotFound();

                todoItems.Remove(todo);
                return Results.NoContent();
            });

            bool ValidateTodo(TodoItem todo, out string? error)
            {
                if (string.IsNullOrWhiteSpace(todo.Name))
                {
                    error = "Назва не може бути порожньою.";
                    return false;
                }

                if (todo.Name.Length < 3)
                {
                    error = "Назва має бути не коротша за 3 символи.";
                    return false;
                }

                error = null;
                return true;
            }
        }
    }
}
