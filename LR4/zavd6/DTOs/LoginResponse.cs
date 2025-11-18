namespace MyWebApi.DTOs;

public class LoginResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime TokenExpiryTime { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}