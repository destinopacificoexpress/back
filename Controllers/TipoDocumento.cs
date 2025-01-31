
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DestinopacificoExpres.Data;

namespace DestinopacificoExpres.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TipoDocumentoController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/TipoDocumento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDocumento>>> GetTipoDocumentos()
        {
            var pasajeros = await _context.TipoDocumento.ToListAsync();
            return Ok(pasajeros);
        }
    }
}