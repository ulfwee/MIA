using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesDemoController : ControllerBase
    {
        // Лише Admin
        [Authorize(Roles = nameof(Roles.Admin))]
        [HttpGet("admin")]
        public IActionResult OnlyAdmins() => Ok("Admin access granted");

        // Admin або Manager
        [Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.Manager)}")]
        [HttpGet("management")]
        public IActionResult ManagersAndAdmins() => Ok("Manager/Admin access granted");

        // Дивимось claims
        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            var name = User.Identity?.Name;
            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            return Ok(new { name, roles });
        }
    }
}
