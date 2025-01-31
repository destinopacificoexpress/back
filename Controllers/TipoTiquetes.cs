using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DestinopacificoExpres.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DestinopacificoExpres.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoTiquetesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TipoTiquetesController(DatabaseContext context)
        {
            _context = context;
        }

        // POST: api/TipoTiquete
        [HttpPost]
        public ActionResult<TipoTiquete> CreateTipoTiquete(TipoTiquete TipoTiquete)
        {
            _context.TipoTiquete.Add(TipoTiquete);
            _context.SaveChanges();
            return CreatedAtAction(nameof(TipoTiquete), new { id = TipoTiquete.TipoTiqueteId }, TipoTiquete);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTiquete>>> GetTipoTiquetes()
        {
            var tipoTiquete = await _context.TipoTiquete.ToListAsync();
            return Ok(tipoTiquete);
        }

        // GET: api/TipoTiquete/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTiquete>> GetTipoTiquete(int id)
        {
            var TipoTiquete = await _context.TipoTiquete
                // .Include(u => u.UsuarioRoles)
                // .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.TipoTiqueteId == id);

            if (TipoTiquete == null)
            {
                return NotFound();
            }

            return Ok(TipoTiquete);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoTiquetes(int id, TipoTiquete TipoTiquete)
        {
            if (id != TipoTiquete.TipoTiqueteId)
            {
                return BadRequest();
            }

            _context.Entry(TipoTiquete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoTiquetesExists(id))
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
        private bool TipoTiquetesExists(int id)
        {
            return _context.TipoTiquete.Any(e => e.TipoTiqueteId == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoTiquete(int id)
        {
            var TipoTique = await _context.TipoTiquete.FindAsync(id);
            if (TipoTique == null)
            {
                return NotFound();
            }

            _context.TipoTiquete.Remove(TipoTique);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
