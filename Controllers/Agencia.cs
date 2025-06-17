using DestinopacificoExpres.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DestinopacificoExpres.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AgenciaController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AgenciaController(DatabaseContext context)
        {
            _context = context;
        }

        // Obtener todas las agencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agencias>>> GetAgencias()
        {
            return await _context.Agencias.ToListAsync();
        }

        // Obtener una agencia por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Agencias>> GetAgencia(int id)
        {
            var agencia = await _context.Agencias.FindAsync(id);

            if (agencia == null)
            {
                return NotFound();
            }

            return agencia;
        }

        // Crear una nueva agencia
        [HttpPost]
        public async Task<ActionResult<Agencias>> CreateAgencia(Agencias agencia)
        {
            _context.Agencias.Add(agencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAgencia), new { id = agencia.AgenciaId }, agencia);
        }

        // Actualizar una agencia existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgencia(int id, Agencias agencia)
        {
            if (id != agencia.AgenciaId)
            {
                return BadRequest();
            }

            _context.Entry(agencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Agencias.Any(e => e.AgenciaId == id))
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

        // Eliminar una agencia
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgencia(int id)
        {
            var agencia = await _context.Agencias.FindAsync(id);
            if (agencia == null)
            {
                return NotFound();
            }

            _context.Agencias.Remove(agencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("existe-nit/{nit}")]
        public async Task<ActionResult<bool>> ExisteNit(string nit)
        {
            var existe = await _context.Agencias.AnyAsync(a => a.Nit == nit);
            return Ok(existe);
        }
    }
}