namespace MyWebApi.Dtos
{
    public class MasterDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }  // Перейменував з "Name" для консистентності з entity
        public string Category { get; set; }  // Строкове представлення енума
        public double Ranking { get; set; }  // Виправив з "Details" -> "Ranking"
    }
}