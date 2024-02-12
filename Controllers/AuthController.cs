using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ILogger<UserService> _logger;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                // Aquí puedes devolver un BadRequest con los detalles de los errores de validación
                return BadRequest(ModelState);
            }

            var user = await _userService.RegisterUser(userRegisterDto);

            if (user == null)
            {
                return BadRequest("No se pudo registrar el usuario.");
            }

            // No devuelvas la contraseña o el hash de la contraseña
            user.PasswordHash = null;

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            Console.WriteLine($"Intento de login con Email: {userLoginDto.Email}"); // Asumiendo que has cambiado a Email

            var user = await _userService.ValidateUserCredentials(userLoginDto);

            if (user == null)
            {
                Console.WriteLine("Credenciales inválidas.");
                return Unauthorized("Credenciales inválidas.");
            }

            var token = GenerateJwtToken(user);
            Console.WriteLine("Login exitoso, token generado.");

            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email), // Usamos el Email como sujeto del token
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar el Token.");

                throw new ApplicationException("Error al generar el Token.", ex);
            }
        }
    }
}
