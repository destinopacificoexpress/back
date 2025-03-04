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
    public class AprobadorController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AprobadorController(DatabaseContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetCorteciasAprobador()
        {
            var getAprobadores = await _context.Aprobadores.ToListAsync();
            return Ok(getAprobadores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCorteciasAprobadorId(int id)
        {
            var getAprobadores = await _context.Aprobadores.FirstOrDefaultAsync(u => u.aprobadorId == id);

            return Ok(getAprobadores);
        }

        [HttpPost]
        public async Task<IActionResult> PostCorteciasAprobador([FromBody] Aprobador request)
        {
            _context.Aprobadores.Add(request);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Aprobador registrado exitosamente" });
        }

        [HttpPut("{aprobadorId}")]
        public async Task<IActionResult> PutCorteciasAprobador(int aprobadorId, [FromBody] Aprobador aprobador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aprobadorPut = await _context.Aprobadores.AsNoTracking().FirstOrDefaultAsync(v => v.aprobadorId == aprobadorId);
            if (aprobadorPut == null) return NotFound("Aprobador no encontrado");

            _context.Aprobadores.Update(aprobador);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Aprobador actualizado exitosamente" });
        }



    }
}