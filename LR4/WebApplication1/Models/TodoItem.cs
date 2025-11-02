namespace WebApplication1.Models
{
    public class TodoItem
    {
        public int Id { get; set; }           // Унікальний ідентифікатор
        public string Name { get; set; }      // Назва завдання
        public bool IsComplete { get; set; }  // Статус виконання
    }   
}
