namespace MyWebApi.Models
{
    public enum Status
    {
        Confirmed,
        Pending,
        Cancelled,
        Competed
    }
    public class Booking
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public string Date { get; set; }
        public string ServiceDetails { get; set; }
        public Status Status { get; set; }
    }
}
