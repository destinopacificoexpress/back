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
    public class DestinosController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DestinosController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDestinos()
        {
            var getDestinos = await _context.Destinos.ToListAsync();
            return Ok(getDestinos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDestinosId(int id)
        {
            var getDestinos = await _context.Destinos.FirstOrDefaultAsync(u => u.DestinoId == id);

            return Ok(getDestinos);
        }

        [HttpPost]  
        public async Task<IActionResult> PostDestinos([FromBody] Destino request)
        {
            _context.Destinos.Add(request);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Destino registrado exitosamente" });
        }

        [HttpPut("{DestinoId}")]
        public async Task<IActionResult> PutDestinos(int DestinoId, [FromBody] Destino destinos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var destinoPut = await _context.Destinos.AsNoTracking().FirstOrDefaultAsync(v => v.DestinoId == DestinoId);
            if (destinoPut == null) return NotFound("Destino no encontrado");

            _context.Destinos.Update(destinos);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Destino actualizado exitosamente" });
        }

    }
}