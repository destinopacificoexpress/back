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
    public class HorariosController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public HorariosController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
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

        // [HttpPost("generar-horarios")]
        // public async Task<IActionResult> GenerarHorariosDiarios(DateTime dateTime)
        // {

        //     // var hoy = DateTime.Today;
        //     var hoy = dateTime;
        //     // var diaSemana = hoy.DayOfWeek.ToString(); // "Monday", "Tuesday", etc.

        //     // Verificar si los horarios ya fueron creados hoy
        //     var horariosExistentes = await _context.Viajes.AnyAsync(h => h.FechaViaje == hoy);

        //     if (!horariosExistentes)
        //     {
        //         var horarios = new[]
        //         {
        //             new Viaje { LugarPartidaId = 7, DestinoId = 1,FechaViaje= hoy, HoraSalida = new TimeSpan(8, 0, 0), Estado = "Pendiente"}, // 8 AM 
        //             new Viaje { LugarPartidaId = 7, DestinoId = 1,FechaViaje= hoy, HoraSalida = new TimeSpan(13, 0, 0), Estado = "Pendiente"},// 1 PM 
        //             new Viaje { LugarPartidaId = 7, DestinoId = 1,FechaViaje= hoy, HoraSalida = new TimeSpan(16, 0, 0), Estado = "Pendiente"},  // 4 PM 

        //             new Viaje { LugarPartidaId = 1, DestinoId = 7,FechaViaje= hoy, HoraSalida = new TimeSpan(10, 30, 0), Estado = "Pendiente"}, // 10:30 AM
        //             new Viaje { LugarPartidaId = 1, DestinoId = 7,FechaViaje= hoy, HoraSalida = new TimeSpan(13, 0, 0), Estado = "Pendiente"},// 1 PM
        //             new Viaje { LugarPartidaId = 1, DestinoId = 7,FechaViaje= hoy, HoraSalida = new TimeSpan(16, 0, 0), Estado = "Pendiente"} // 4 pm
        //         };

        //         _context.Viajes.AddRange(horarios);
        //         await _context.SaveChangesAsync();
        //         return Ok("Horarios generados correctamente.");
        //     }

        //     return BadRequest("Los horarios ya han sido generados para hoy.");
        // }

[HttpPost("generar-horarios")]
        public async Task<IActionResult> GenerarHorariosDiarios(Viaje horario)
        {
            
            if (horario.LanchaId != 0)
            {

            }
            // var hoy = DateTime.Today;
            // var hoy = dateTime;
            // var diaSemana = hoy.DayOfWeek.ToString(); // "Monday", "Tuesday", etc.

            // Verificar si los horarios ya fueron creados hoy
            var horariosExistentes = await _context.Viajes.AnyAsync(h => h.FechaViaje == horario.FechaViaje);

            if (!horariosExistentes)
            {
                var horarios = new[]
                {
                    new Viaje { LugarPartidaId = horario.LugarPartidaId, DestinoId = horario.DestinoId,FechaViaje= horario.FechaViaje, HoraSalida = new TimeSpan(8, 0, 0), Estado = "Pendiente"}, // 8 AM 
            
                };

                _context.Viajes.AddRange(horarios);
                await _context.SaveChangesAsync();
                return Ok("Horarios generados correctamente.");
            }
            

            return BadRequest("Los horarios ya han sido generados para hoy.");
        }
        [HttpGet("{LugarPartidaId}")]
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

    }
}
