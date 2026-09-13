using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Data;
using ProductManagement.DTOs;
using ProductManagement.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtOptions _jwt;

        public AuthController(AppDbContext context, JwtOptions jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpPost]
        public ActionResult<string> Login(LoginDto user)
        {

            var dbUser = _context.users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

            if (dbUser != null)
            {
                var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim (ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                    new Claim (ClaimTypes.Name, dbUser.UserName)
                });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _jwt.Issuer,
                    Audience = _jwt.Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey)), SecurityAlgorithms.HmacSha256),
                    Subject = identity,
                    Expires = DateTime.UtcNow.AddMinutes(_jwt.Lifetime)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var accessToken = tokenHandler.WriteToken(securityToken);
                return Ok(accessToken);
            }

            return Unauthorized("Invalid username or password");
        }
    }
}
