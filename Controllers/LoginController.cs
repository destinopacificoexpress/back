
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

            var user = _context.Usuarios.SingleOrDefault(u => u.Username == usuario.Username);

            if (user == null)
            {
                user = _context.Usuarios.SingleOrDefault(u => u.Email == usuario.Email);
            }

            if (user == null || !BCrypt.Net.BCrypt.Verify(usuario.Password, user.Password))
            {
                return Unauthorized("Usuario y/o contraseña incorrectos." + user);
            }

            var tokenService = _config["JwtSettings:SecretKey"];
            if (tokenService != null)
            {
                string secretKey = tokenService; // Cambiar por una clave más segura y mantenerla en un lugar seguro
                var token = _tokenService.GenerateToken(user.Username, secretKey);

                var Session = new Sesion
                {
                    UsuarioId = user.Id,
                    Token = token,
                    FechaInicio = DateTime.Now,
                    FechaExpiracion = DateTime.Now.AddHours(2), // Define el tiempo de expiración que desees
                    Activa = true
                };

                _context.Sesiones.Add(Session);
                _context.SaveChanges();
                return Ok(new { Session = Session });
            }
            else
            {
                return Unauthorized("Verificar SecretKey");
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistroRequest usuario)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el email ya está registrado
            var user = _context.Usuarios.SingleOrDefault(u => u.Email == usuario.Email);
            if (user != null)
            {
                return BadRequest("El email ya está registrado.");
            }

            // Verificar si el nombre de usuario ya está registrado
            user = _context.Usuarios.SingleOrDefault(u => u.Username == usuario.Username);
            if (user != null)
            {
                return BadRequest("El Usuario ya está registrado.");
            }

            // Crear un nuevo usuario
            var usuarioRegister = new Usuario
            {
                Username = usuario.Username,
                Email = usuario.Email,
                FechaCreacion = DateTime.Now,
                Name = usuario.Name,
                LastName = usuario.LastName,
                // Hashear la contraseña principal
                Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password),
                Second_Password = BCrypt.Net.BCrypt.HashPassword(usuario.SecondPassword),
                RoleId = usuario.RoleId,
                LugarSalidaId = usuario.LugarSalidaId
            };

            // Guardar el nuevo usuario en la base de datos
            _context.Usuarios.Add(usuarioRegister);
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

    public int LugarSalidaId { get; set; }
}
