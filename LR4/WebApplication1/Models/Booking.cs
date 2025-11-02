namespace WebApplication1.Models
{
    public class Booking
    {       
        public int Id { get; set; }
        public int MasterId { get; set; }
        public string Date { get; set; }
        public string ServiceDetails { get; set; }
        public string Status { get; set; }
    }
}
