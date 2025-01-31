
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DestinopacificoExpres.Data;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
namespace DestinopacificoExpres.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _config;
        private readonly TokenService _tokenService;
        public LoginController(DatabaseContext context, IConfiguration config, TokenService tokenService)
        {
            _context = context;
            _config = config;
            _tokenService = tokenService;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest usuario)
        {
            var user = _context.Usuarios.SingleOrDefault(u => u.Email == usuario.Email);

            if (user == null)
            {
                user = _context.Usuarios.SingleOrDefault(u => u.Username == usuario.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(usuario.Password, user.Password))
                {
                    return Unauthorized("usaurio y contraseña incorrectos.");
                }
                var tokenService = _config["JwtSettings:SecretKey"];
                if (tokenService != null)
                {
                    string secretKey = tokenService; // Cambiar por una clave más segura y mantenerla en un lugar seguro
                    var token = _tokenService.GenerateToken(usuario.Username, secretKey);

                    var sesion = new Sesion
                    {
                        UsuarioId = user.Id,
                        Token = token,
                        FechaInicio = DateTime.Now,
                        FechaExpiracion = DateTime.Now.AddHours(1), // Define el tiempo de expiración que desees
                        Activa = true
                    };

                    _context.Sesiones.Add(sesion);
                    _context.SaveChanges();
                    return Ok(new { Sesion = sesion });
                }
                else
                {
                    return Unauthorized("Verificar SecretKey");
                }
            }
            else
            {
                if (user == null || !BCrypt.Net.BCrypt.Verify(usuario.Password, user.Password))
                {
                    return Unauthorized("usaurio y contraseña incorrectos.");
                }
                var tokenService = _config["JwtSettings:SecretKey"];
                if (tokenService != null)
                {
                    string secretKey = tokenService; // Cambiar por una clave más segura y mantenerla en un lugar seguro
                    var token = _tokenService.GenerateToken(usuario.Email, secretKey);
                    var sesion = new Sesion
                    {
                        UsuarioId = user.Id,
                        Token = token,
                        FechaInicio = DateTime.Now,
                        FechaExpiracion = DateTime.Now.AddHours(1), // Define el tiempo de expiración que desees
                        Activa = true
                    };

                    _context.Sesiones.Add(sesion);
                    _context.SaveChanges();
                    return Ok(new { Sesion = sesion });
                }
                else
                {
                    return Unauthorized("Verificar SecretKey");
                }
            }
            // return Ok("Login exitoso.");
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistroRequest usuario)
        {

            // _context.Usuarios.Any(u => u.Email == usuario.Email)
            var user = _context.Usuarios.SingleOrDefault(u => u.Email == usuario.Email);

            // Verificar si el email ya está registrado
            if (user != null)
            {
                return BadRequest("El email ya está registrado.");
            }

            user = _context.Usuarios.SingleOrDefault(u => u.Username == usuario.Username);

            if (user != null)
            {
                return BadRequest("El Usuario ya está registrado.");
            }
            //fecha de Hoy

            var usaurioRegister = new Usuario
            {
                Username = usuario.Username,
                Email = usuario.Email,
                FechaCreacion = DateTime.Now,
                Name = usuario.Name,
                LastName = usuario.LastName,
                // Hashea la contraseña principal
                Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password),
                Second_Password = BCrypt.Net.BCrypt.HashPassword(usuario.SecondPassword),
                RoleId = usuario.RoleId
            };

            // Guarda en la base de datos
            _context.Usuarios.Add(usaurioRegister);
            _context.SaveChanges();

            return Ok("Usuario registrado con éxito.");

        }

    }
}

public class LoginRequest
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public required string Password { get; set; } // Asegúrate de que la contraseña se compare adecuadamente
}


public class RegistroRequest
{
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string SecondPassword { get; set; }
    public required string Email { get; set; }
    // public DateTime FechaCreacion { get; set; } // Puedes usar esta propiedad si es necesario
    public required int RoleId { get; set; }
}
