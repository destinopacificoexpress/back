using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DestinopacificoExpres.Data;
namespace DestinopacificoExpres.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestConnectionController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TestConnectionController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                // Intenta ejecutar una consulta simple
                var usuarios = await _context.Usuarios.ToListAsync();
                return Ok(new { mensaje = "Conexión exitosa", usuarios });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error de conexión", error = ex.Message });
            }
        }
    }
}
