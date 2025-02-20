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
    [Route("api/[controller]")]
    public class viajesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public viajesController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostViajes([FromBody] Viaje request)
        {
            _context.Viajes.Add(request);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Lancha registrada exitosamente" });
        }

        [HttpGet]
        public async Task<IActionResult> GetViajes()
        {
            var getViajes = await _context.Viajes.ToListAsync();
            return Ok(getViajes);
        }

        [HttpGet("hora/{horaSalida}")]
        public async Task<IActionResult> GetViajesHora(string horaSalida)
        {
            if (!TimeSpan.TryParse(horaSalida, out var horaSalidas))
            {
                return BadRequest("Formato de hora inválido");
            }
            var getViajes = await _context.Viajes.FirstOrDefaultAsync(u => u.HoraSalida == horaSalidas && u.Estado == "Activo" && u.FechaViaje == DateTime.Now.Date);

            return Ok(getViajes);
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetViajesId(int id)
        {
            var getViajes = await _context.Viajes.FirstOrDefaultAsync(u => u.ViajeId == id);

            return Ok(getViajes);
        }

        [HttpPut("{ViajeId}")]
        public async Task<IActionResult> PutViajes(int ViajeId, [FromBody] Viaje viajes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var viajePut = await _context.Viajes.AsNoTracking().FirstOrDefaultAsync(v => v.ViajeId == ViajeId);
            if (viajePut == null) return NotFound("Viaje no encontrado");

            viajePut.LugarPartidaId = viajes.LugarPartidaId;
            viajePut.DestinoId = viajes.DestinoId;
            viajePut.FechaViaje = viajes.FechaViaje;
            viajePut.HoraSalida = viajes.HoraSalida;
            viajePut.Estado = viajes.Estado;
            viajePut.LanchaId = viajes.LanchaId;

            _context.Entry(viajePut).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Viajes.Any(e => e.ViajeId == ViajeId))
                {
                    return NotFound("Viaje no encontrado");
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { mensaje = "Viaje Editado exitosamente" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteViaje(int id)
        {
            var Viaje = _context.Viajes.Find(id);
            if (Viaje == null)
                return NotFound();
            _context.Viajes.Remove(Viaje);
            _context.SaveChanges();
            return NoContent();
        }
    }
}