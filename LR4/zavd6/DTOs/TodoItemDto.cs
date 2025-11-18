
namespace MyWebApi.Dtos
{
    public class TodoItemDto
    {
        public string Id { get; set; }
        public string Task { get; set; }
        public bool IsComplete { get; set; }  // Перейменував з "IsCompleted" для консистентності з entity
    }
}