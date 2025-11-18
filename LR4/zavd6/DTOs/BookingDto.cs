namespace MyWebApi.Dtos
{
    public class BookingDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }  // Для API, якщо потрібно показувати
        public string MasterId { get; set; }
        public DateTime Date { get; set; }  // DateTime для JSON
        public string ServiceDetails { get; set; }  // Перейменував з "Details" для точності
        public string Status { get; set; }  // Строкове представлення енума
    }
}