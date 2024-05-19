using CIoTDSystem.Data;
using CIoTDSystem.Models;
using CIoTDSystem.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CIoTDSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dbContext;

        public AuthController(IConfiguration configuration, DataContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(User request)
        {
            var existingUser = _dbContext.User.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser != null)
            {
                return BadRequest("Este usuário já existe.");
            }

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Status = request.Status,
                IsAdmin = request.IsAdmin,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
            };

            _dbContext.User.Add(newUser);
            _dbContext.SaveChanges();

            return Ok("Usuário criado com sucesso!");
        }

        [HttpPost("login")]
        public ActionResult Login(UserDTO request)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return BadRequest("Senha incorreta.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            // @TODO

            return Ok("Logout successful");
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
