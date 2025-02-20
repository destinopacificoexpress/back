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
    public class LanchasController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public LanchasController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost("registrar-lancha")]
        public async Task<IActionResult> RegistrarLancha([FromBody] Lancha request)
        {
            _context.Lanchas.Add(request);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Lancha registrada exitosamente" });
        }


        // [HttpPost("crear-horario")]
        // public async Task<IActionResult> CrearHorario([FromBody] Viaje request)
        // {
        //     _context.Viajes.Add(request);
        //     await _context.SaveChangesAsync();
        //     return Ok(new { mensaje = "Horario creado correctamente" });
        // }

       
      
        // [HttpGet("horarios-lancha-hoy")]
        // public async Task<IActionResult> GetHorariosHoy()
        // {
        //     var hoy = DateTime.Today;
        //     var horarios = await _context.Viajes
        //         .Where(h => h.LanchaId != null && h.FechaViaje == hoy)
        //         .Select(h => new
        //         {
        //             h.ViajeId,
        //             h.FechaViaje,
        //             h.LugarPartidaId,
        //             h.HoraSalida,
        //             h.HoraRetorno,
        //             Destino = h.DestinoId
        //         }).ToListAsync();

        //     return Ok(horarios);
        // }


        // [HttpGet("horarios-lancha")]
        // public async Task<IActionResult> GetHorariosLanchas()
        // {

        //     var hoy = DateTime.Today;
        //     var horarios = await _context.Viajes
        //         .Where(h => h.LanchaId != null && h.FechaViaje >= hoy)
        //         .Select(h => new
        //         {
        //             h.ViajeId,
        //             h.FechaViaje,
        //             h.LugarPartidaId,
        //             h.HoraSalida,
        //             h.HoraRetorno,
        //             Destino = h.DestinoId
        //         }).ToListAsync();

        //     return Ok(horarios);
        // }


        // [HttpGet("horarios-sin-lancha")]
        // public async Task<IActionResult> GetHorariosSinLancha()
        // {
        //     var horarios = await _context.Viajes
        //         .Where(h => h.LanchaId == null && h.Estado == "Pendiente")
        //         .Select(h => new
        //         {
        //             h.ViajeId,
        //             h.FechaViaje,
        //             h.LugarPartidaId,
        //             h.HoraSalida,
        //             h.HoraRetorno,
        //             Destino = h.DestinoId
        //         }).ToListAsync();

        //     return Ok(horarios);
        // }


        [HttpPut("asignar-lancha/{horarioId}/{lanchaId}")]
        public async Task<IActionResult> AsignarLancha(int horarioId, int lanchaId)
        {
            var horario = await _context.Viajes.FindAsync(horarioId);
            if (horario == null) return NotFound("Horario no encontrado");

            var lancha = await _context.Lanchas.FindAsync(lanchaId);
            if (lancha == null) return NotFound("Lancha no encontrada");

            horario.LanchaId = lanchaId;
            horario.Estado = "Confirmado";

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Lancha asignada correctamente" });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lancha>>> GetLanchas()
        {
            return await _context.Lanchas.ToListAsync();
        }


    }
}