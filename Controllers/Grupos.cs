using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DestinopacificoExpres.Data;
using Microsoft.EntityFrameworkCore;

namespace DestinopacificoExpres.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GruposController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public GruposController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grupos>>> GetGruposConDestinos()
        {
            var result = await _context.Grupos
            .Select(g => new
            {
                GrupoId = g.GrupoId,
                NombreGrupo = g.NombreGrupo,
                InfoDestino = g.InfoDestino.Select(d => new
                {
                    Id = d.Id,
                    Nombre = d.Nombre
                }).ToList()
            })
            .ToListAsync();

            return Ok(result);
        }


        // GET: api/Grupos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grupos>> GetGrupos(int id)
        {
            var grupo = await _context.Grupos
                .FirstOrDefaultAsync(u => u.GrupoId == id);

            var result = await _context.Grupos

                       .Select(g => new
                       {
                           GrupoId = g.GrupoId,
                           NombreGrupo = g.NombreGrupo,
                           InfoDestino = g.InfoDestino.Select(d => new
                           {
                               Id = d.Id,
                               Nombre = d.Nombre
                           }).ToList()
                       }).FirstOrDefaultAsync(u => u.GrupoId == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrupos(int id, Grupos grupos)
        {
            if (id != grupos.GrupoId)
            {
                return BadRequest();
            }

            _context.Entry(grupos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GruposExists(id))
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
        private bool GruposExists(int id)
        {
            return _context.Grupos.Any(e => e.GrupoId == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupos(int id)
        {
            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }

            _context.Grupos.Remove(grupo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}