using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DestinopacificoExpres.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace DestinopacificoExpres.Controllers
{
    [Route("api/[controller]")]
    public class PasajerosCorteciaController : Controller
    {
        private readonly DatabaseContext _context;

        public PasajerosCorteciaController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCorteciasPasajeros()
        {
            var getPasajerosCortecias = await _context.PasajerosCortecias.ToListAsync();
            return Ok(getPasajerosCortecias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCorteciasPasajerosId(int id)
        {
            var getPasajerosCortecias = await _context.PasajerosCortecias.FirstOrDefaultAsync(u => u.PasajerosCorteciasId == id);

            return Ok(getPasajerosCortecias);
        }

        [HttpPost]
        public async Task<IActionResult> PostCorteciasPasajeros([FromBody] PasajerosCortecia request)
        {
            _context.PasajerosCortecias.Add(request);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Pasajero de cortesia registrado exitosamente" });
        }

        [HttpPut("{PasajerosCorteciasId}")]
        public async Task<IActionResult> PutCorteciasPasajeros(int PasajerosCorteciasId, [FromBody] PasajerosCortecia pasajerosCortecias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pasajerosCorteciasPut = await _context.PasajerosCortecias.AsNoTracking().FirstOrDefaultAsync(v => v.PasajerosCorteciasId == PasajerosCorteciasId);
            if (pasajerosCorteciasPut == null) return NotFound("Pasajero de cortesia no encontrado");

            _context.PasajerosCortecias.Update(pasajerosCortecias);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Pasajero de cortesia actualizado exitosamente" });
        }
    }
}