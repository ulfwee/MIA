using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Dtos;
using MyWebApi.DTOs;
using MyWebApi.Entities;
using MyWebApi.Models;
using MyWebApi.Services;
using MyWebApi.Services.Interfaces;


namespace MyWebApi.Controller;

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public AuthController(IUserService userService, JwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher)
    {
        _userService = userService;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var existingUser = await _userService.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            return BadRequest("Користувач уже існує.");

        var user = new User
        {
            FullName = dto.FullName,
            Age = dto.Age,
            Email = dto.Email,
            Role = Roles.User,
            PasswordHash = _passwordHasher.HashPassword(dto.Password)
        };

        await _userService.CreateAsync(user);

        return Ok("Користувач зареєстрований.");
    }




    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userService.GetByEmailAsync(dto.Email);

        if (user == null)
            return Unauthorized("Неправильна пошта або пароль.");

        // 🔥 Перевірка пароля через Verify (НЕ порівнюємо хеші!)
        if (!_passwordHasher.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Неправильна пошта або пароль.");

        // 🔥 Генеруємо новий access token
        var accessToken = _jwtTokenGenerator.Generate(user);

        // 🔥 Генеруємо новий refresh token
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        // 🔥 Оновлюємо refresh token у БД
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userService.UpdateAsync(user);

        return Ok(new LoginResponse
        {
            Token = accessToken,
            RefreshToken = refreshToken,
            TokenExpiryTime = DateTime.UtcNow.AddMinutes(60),
            RefreshTokenExpiryTime = (DateTime)user.RefreshTokenExpiryTime!
        });
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromHeader(Name = "Authorization")] string authHeader, [FromBody] string refreshToken)
    {
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            return BadRequest("Missing or invalid Authorization header");

        var token = authHeader.Substring("Bearer ".Length);

        var principal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(token);

        if (principal == null)
            return BadRequest("Invalid refresh token");

        var id = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (id == null) throw new Exception("Invalid token claims");

        var user = await _userService.GetAsync(id);

        if (user == null ||
            user.RefreshToken != refreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return Unauthorized("Invalid refresh token");
        }

        var newAccessToken = _jwtTokenGenerator.Generate(user);
        var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userService.UpdateAsync(user);

        return Ok(new
        {
            token = newAccessToken,
            refreshToken = newRefreshToken,
            TokenExpiryTime = DateTime.UtcNow.AddMinutes(60),
            RefreshTokenExpiryTime = (DateTime)user.RefreshTokenExpiryTime
        });
    }

}
