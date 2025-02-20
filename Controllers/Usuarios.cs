using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DestinopacificoExpres.Data;   // Asegúrate de cambiarlo por tu namespace

namespace DestinopacificoExpres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuarios : ControllerBase
    {
        private readonly DatabaseContext _context;

        public Usuarios(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Sesiones) // Incluye los roles
                                          // .ThenInclude(ur => ur.SesionId)    // Incluye la entidad de rol
                .ToListAsync();

            return Ok(usuarios);
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                // .Include(u => u.roles)
                // .ThenInclude(ur => ur.RoleId = RoleId)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuarios(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
         private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var User = await _context.Usuarios.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(User);
            await _context.SaveChangesAsync(); 

            return NoContent();
        }
        // Puedes agregar más métodos para crear, actualizar y eliminar usuarios
    }
}
