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
    public class CorteciasController : ControllerBase
    {

        private readonly DatabaseContext _context;

        public CorteciasController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCortecias()
        {
            var getCortecias = await _context.Cortesias.ToListAsync();
            return Ok(getCortecias);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCorteciasId(int id)
        {
            var getCortecias = await _context.Cortesias.FirstOrDefaultAsync(u => u.CortesiaId == id);

            return Ok(getCortecias);
        }
        

        [HttpPost]
        public async Task<IActionResult> PostCortecias([FromBody] Cortesia request)
        {
            _context.Cortesias.Add(request);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Cortesia registrada exitosamente" });
        }

        [HttpPut("{CortesiaId}")]
        public async Task<IActionResult> PutCortecias(int CortesiaId, [FromBody] Cortesia cortesias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cortesiaPut = await _context.Cortesias.AsNoTracking().FirstOrDefaultAsync(v => v.CortesiaId == CortesiaId);
            if (cortesiaPut == null) return NotFound("Cortesia no encontrada");

            _context.Cortesias.Update(cortesias);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Cortesia actualizada exitosamente" });
        }

    }
}