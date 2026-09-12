using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Data;
using ProductManagement.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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
        public ActionResult<string> Login(User user)
        {
            if(_context.users.Any(u => u.UserName == user.UserName && u.Password == user.Password))
            {
                var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim (ClaimTypes.Name, user.UserName)
                });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _jwt.Issuer,
                    Audience = _jwt.Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey)), SecurityAlgorithms.HmacSha256),
                    Subject = identity,
                    Expires = DateTime.Now.AddMinutes(_jwt.Lifetime)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var accessToken = tokenHandler.WriteToken(securityToken);
                return Ok(accessToken);
            }

            return BadRequest("Invalid username or password");
        }
    }
}
