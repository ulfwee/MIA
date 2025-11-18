namespace MyWebApi.Dtos
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // Plain password, хешується в service
    }
}