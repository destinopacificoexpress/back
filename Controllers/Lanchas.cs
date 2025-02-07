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


        [HttpPost("crear-horario")]
        public async Task<IActionResult> CrearHorario([FromBody] Viaje request)
        {
            _context.Viajes.Add(request);
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Horario creado correctamente" });
        }

        [HttpPost("generar-horarios")]
        public async Task<IActionResult> GenerarHorariosDiarios()
        {
            var hoy = DateTime.Today;
            // var diaSemana = hoy.DayOfWeek.ToString(); // "Monday", "Tuesday", etc.

            // Verificar si los horarios ya fueron creados hoy
            var horariosExistentes = await _context.Viajes.AnyAsync(h => h.FechaViaje == hoy);

            if (!horariosExistentes)
            {
                var horarios = new[]
                {
                    new Viaje { LugarPartidaId = 7, DestinoId = 1,FechaViaje= hoy, HoraSalida = new TimeSpan(8, 0, 0), Estado = "Pendiente"}, // 8 AM 
                    new Viaje { LugarPartidaId = 7, DestinoId = 1,FechaViaje= hoy, HoraSalida = new TimeSpan(13, 0, 0), Estado = "Pendiente"},// 1 PM 
                    new Viaje { LugarPartidaId = 7, DestinoId = 1,FechaViaje= hoy, HoraSalida = new TimeSpan(16, 0, 0), Estado = "Pendiente"},  // 4 PM 

                    new Viaje { LugarPartidaId = 1, DestinoId = 7,FechaViaje= hoy, HoraSalida = new TimeSpan(10, 30, 0), Estado = "Pendiente"}, // 10:30 AM
                    new Viaje { LugarPartidaId = 1, DestinoId = 7,FechaViaje= hoy, HoraSalida = new TimeSpan(13, 0, 0), Estado = "Pendiente"},// 1 PM
                    new Viaje { LugarPartidaId = 1, DestinoId = 7,FechaViaje= hoy, HoraSalida = new TimeSpan(16, 0, 0), Estado = "Pendiente"} // 4 pm
                };

                _context.Viajes.AddRange(horarios);
                await _context.SaveChangesAsync();
                return Ok("Horarios generados correctamente.");
            }

            return BadRequest("Los horarios ya han sido generados para hoy.");
        }

        [HttpGet("horarios/{LugarPartidaId}")]
        public async Task<IActionResult> GetHorariosLugarPartidaId(int LugarPartidaId)
        {
            var hoy = DateTime.Today;
            var horarios = await _context.Viajes
                .Where(h => h.LugarPartidaId == LugarPartidaId && h.FechaViaje >= hoy)
                .Select(h => new
                {
                    h.ViajeId,
                    h.FechaViaje,
                    h.LugarPartidaId,
                    h.HoraSalida,
                    h.HoraRetorno,
                    Destino = h.DestinoId
                }).ToListAsync();

            return Ok(horarios);
        }

        [HttpGet("horarios")]
        public async Task<IActionResult> GetHorarios()
        {
            var hoy = DateTime.Today;
            var horarios = await _context.Viajes
                .Where(h => h.FechaViaje >= hoy)
                .Select(h => new
                {
                    h.ViajeId,
                    h.FechaViaje,
                    h.LugarPartidaId,
                    h.HoraSalida,
                    h.HoraRetorno,
                    Destino = h.DestinoId
                }).ToListAsync();

            return Ok(horarios);
        }


        [HttpGet("horarios-lancha-hoy")]
        public async Task<IActionResult> GetHorariosHoy()
        {
            var hoy = DateTime.Today;
            var horarios = await _context.Viajes
                .Where(h => h.LanchaId != null && h.FechaViaje == hoy)
                .Select(h => new
                {
                    h.ViajeId,
                    h.FechaViaje,
                    h.LugarPartidaId,
                    h.HoraSalida,
                    h.HoraRetorno,
                    Destino = h.DestinoId
                }).ToListAsync();

            return Ok(horarios);
        }


        [HttpGet("horarios-lancha")]
        public async Task<IActionResult> GetHorariosLanchas()
        {

            var hoy = DateTime.Today;
            var horarios = await _context.Viajes
                .Where(h => h.LanchaId != null && h.FechaViaje >= hoy)
                .Select(h => new
                {
                    h.ViajeId,
                    h.FechaViaje,
                    h.LugarPartidaId,
                    h.HoraSalida,
                    h.HoraRetorno,
                    Destino = h.DestinoId
                }).ToListAsync();

            return Ok(horarios);
        }


        [HttpGet("horarios-sin-lancha")]
        public async Task<IActionResult> GetHorariosSinLancha()
        {
            var horarios = await _context.Viajes
                .Where(h => h.LanchaId == null && h.Estado == "Pendiente")
                .Select(h => new
                {
                    h.ViajeId,
                    h.FechaViaje,
                    h.LugarPartidaId,
                    h.HoraSalida,
                    h.HoraRetorno,
                    Destino = h.DestinoId
                }).ToListAsync();

            return Ok(horarios);
        }


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