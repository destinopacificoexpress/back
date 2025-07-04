using DestinopacificoExpres.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;


namespace DestinopacificoExpres.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TiquetesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TiquetesController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTiquete([FromBody] TiqueteRequest tiqueteDto)
        {
            try
            {

                if (!await _context.TipoTiquete.AnyAsync(t => t.TipoTiqueteId == tiqueteDto.TipoTiqueteId))
                    return BadRequest("TipoTiqueteId no existe.");
                if (!await _context.Agencias.AnyAsync(a => a.AgenciaId == tiqueteDto.AgenciaId))
                    return BadRequest("AgenciaId no existe.");
                if (!await _context.Grupos.AnyAsync(g => g.GrupoId == tiqueteDto.GrupoId))
                    return BadRequest("GrupoId no existe.");
                if (!await _context.Destinos.AnyAsync(d => d.DestinoId == tiqueteDto.InfoDestinoId))
                    return BadRequest("DestinoId no existe.");
                if (!await _context.FormasPago.AnyAsync(f => f.Id == tiqueteDto.FormaPagoId))
                    return BadRequest("FormaPagoId no existe.");
                if (!await _context.Pasajeros.AnyAsync(p => p.PasajeroId == tiqueteDto.PasajeroId))
                    return BadRequest("PasajeroId no existe.");
                if (tiqueteDto.FechaRetorno != null && tiqueteDto.FechaRetorno < tiqueteDto.FechaAbordo)
                    return BadRequest("FechaRetorno debe ser mayor o igual a FechaAbordo.");



                var tiquete = new Tiquete
                {
                    FechaExpedicion = tiqueteDto.FechaExpedicion,
                    NumeroTiquete = tiqueteDto.NumeroTiquete,
                    TipoTiqueteId = tiqueteDto.TipoTiqueteId,
                    AgenciaId = tiqueteDto.AgenciaId,
                    GrupoId = tiqueteDto.GrupoId,
                    InfoDestinoId = tiqueteDto.InfoDestinoId,
                    FechaAbordo = tiqueteDto.FechaAbordo,
                    HoraAbordo = tiqueteDto.HoraAbordo,
                    FechaRetorno = tiqueteDto.FechaRetorno,
                    HoraRetorno = tiqueteDto.HoraRetorno,
                    SoloIda = tiqueteDto.SoloIda,
                    CantidadPasajeros = tiqueteDto.CantidadPasajeros,
                    ValorSugerido = tiqueteDto.ValorSugerido,
                    ValorIndividual = tiqueteDto.ValorIndividual,
                    ExcesoEquipaje = tiqueteDto.ExcesoEquipaje,
                    TotalVenta = tiqueteDto.TotalVenta,
                    Descripcion = tiqueteDto.Descripcion,
                    FormaPagoId = tiqueteDto.FormaPagoId,
                    PasajeroId = tiqueteDto.PasajeroId,
                    ViajeId = tiqueteDto.ViajeId == 0 ? null : tiqueteDto.ViajeId
                };
                _context.Tiquetes.Add(tiquete);
                _context.SaveChanges();
                return CreatedAtAction(nameof(CrearTiquete), new { id = tiquete.TiqueteId }, tiquete);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error: {ex.InnerException?.Message}");
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tiquete>> GetTiquete(int id)
        {
            var tiquete = await _context.Tiquetes.FindAsync(id);

            if (tiquete == null)
            {
                return NotFound();
            }

            return tiquete;
        }

        // GET: api/tiquetes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetTiquetes()
        {
            try
            {
                var tiquetes = await _context.Tiquetes
                    .Select(T => new
                    {
                        T.TiqueteId,
                        T.FechaExpedicion,
                        T.NumeroTiquete,
                        T.TipoTiqueteId,
                        T.AgenciaId,
                        T.GrupoId,
                        T.InfoDestinoId,
                        T.FechaAbordo,
                        T.HoraAbordo,
                        T.FechaRetorno,
                        T.HoraRetorno,
                        T.SoloIda,
                        T.CantidadPasajeros,
                        T.ValorSugerido,
                        T.ValorIndividual,
                        T.ExcesoEquipaje,
                        T.TotalVenta,
                        T.Descripcion,
                        T.FormaPagoId,
                        T.PasajeroId,
                        T.ViajeId
                    })
                    .ToListAsync();

                if (tiquetes == null || tiquetes.Count == 0)
                    return NotFound("No se encontraron tiquetes.");

                return Ok(tiquetes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTiquete(int id, Tiquete tiquete)
        {
            if (id != tiquete.TiqueteId)
            {
                return BadRequest();
            }

            _context.Entry(tiquete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiqueteExists(id))
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

        [HttpGet("infos/{lugarPartidaId}")]
        public async Task<ActionResult<IEnumerable<InfoDestino>>> GetInfoDestinos(int lugarPartidaId)
        {
            var infoDestinos = await _context.Viajes
            .Where(d => d.LugarPartidaId == lugarPartidaId && d.FechaViaje.Date == DateTime.Today)
            .ToListAsync();

            if (infoDestinos == null || !infoDestinos.Any())
            {
                return Ok(new { Message = "No se encontraron destinos para el lugar de partida y fecha especificados." });
            }

            var viajeIds = infoDestinos.Select(v => v.ViajeId).ToList();
            var infoTiquetes = await _context.Tiquetes
            .Where(t => t.ViajeId.HasValue && viajeIds.Contains(t.ViajeId.Value) && t.FechaAbordo.Date == DateTime.Today)
            .ToListAsync();

            var viajeInfoList = infoDestinos.Select(v => new ViajeInfo
            {
                HoraSalida = v.HoraSalida.ToString(),
                CantidadPasajero = infoTiquetes.Where(t => t.ViajeId == v.ViajeId).Sum(t => t.CantidadPasajeros),
                CupoDisponible = _context.Lanchas.FirstOrDefault(l => l.LanchaId == v.LanchaId)?.Capacidad ?? 0 - infoTiquetes.Where(t => t.ViajeId == v.ViajeId).Sum(t => t.CantidadPasajeros),
                LimiteCupo = _context.Lanchas.FirstOrDefault(l => l.LanchaId == v.LanchaId)?.Capacidad ?? 0,
                KlsCarga = 0
            }).ToList();

            return Ok(viajeInfoList);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiquete(int id)
        {
            var tiquete = await _context.Tiquetes.FindAsync(id);
            if (tiquete == null)
            {
                return NotFound();
            }

            _context.Tiquetes.Remove(tiquete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TiqueteExists(int id)
        {
            return _context.Tiquetes.Any(e => e.TiqueteId == id);
        }

        [HttpGet("tiquetesAbordes/{lugarPartidaId}")]
        public async Task<ActionResult> GetTiquetesAbordes(int lugarPartidaId)
        {
            var tiquetes = await _context.Tiquetes
                .Where(t => t.GrupoId == lugarPartidaId && t.FechaAbordo.Date == DateTime.Today)
                .ToListAsync();

            var totalVentas = await _context.Tiquetes
                .Where(t => t.GrupoId == lugarPartidaId && t.FechaExpedicion.Date == DateTime.Today)
                .ToListAsync();

            var tiqueteRetorno = await _context.Tiquetes
                .Where(t => t.GrupoId != lugarPartidaId && t.FechaAbordo.Date == DateTime.Today)
                .ToListAsync();

            var totalVentasRetorno = await _context.Tiquetes
           .Where(t => t.GrupoId != lugarPartidaId && t.FechaExpedicion.Date == DateTime.Today)
           .ToListAsync();

            if (tiquetes == null || !tiquetes.Any())
            {
                return Ok(new { Message = "No se encontraron tiquetes para el lugar de partida y fecha especificados." });
            }

            var infoDestinos = new
            {
                totaltiqutes = tiquetes.Count,
                totaltiqutesRetorno = tiqueteRetorno.Count,
                totalVentas = totalVentas.Count,
                totalVentasRetorno = totalVentasRetorno.Count,
            };

            return Ok(infoDestinos);
        }

        [HttpGet("numeroTiquete/{iduser}")]
        public async Task<ActionResult<int>> GetNumeroTiquetes(int iduser)
        {
            try
            {
                var user = await _context.Usuarios
                    .Where(u => u.Id == iduser)
                    .Select(u => new { u.LugarSalidaId })
                    .FirstOrDefaultAsync();

                if (user == null)
                    return NotFound("Usuario no encontrado.");

                int lugarPartidaId = user?.LugarSalidaId ?? 0;

                if (lugarPartidaId <= 0)
                    return BadRequest("El usuario no tiene un lugar de salida válido.");

                var ultimoTiqueteStr = await _context.Tiquetes
                    .Where(d => d.GrupoId == lugarPartidaId)
                    .OrderByDescending(t => t.FechaExpedicion)
                    .Select(t => t.NumeroTiquete)
                    .FirstOrDefaultAsync();

                int numero = 1;
                if (!string.IsNullOrEmpty(ultimoTiqueteStr) && int.TryParse(ultimoTiqueteStr, out int ultimo))
                {
                    numero = ultimo + 1;
                }

                return Ok(numero);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }



        //     [HttpGet("{tiqueteId}/qr")]
        // public async Task<IActionResult> GenerarQRCode(int tiqueteId)
        // {
        //     // Obtener datos del tiquete desde la base de datos
        //     var tiquete = await _context.Tiquetes
        //         .Include(t => t.InfoDestinoId)
        //         .FirstOrDefaultAsync(t => t.TiqueteId == tiqueteId);

        //     if (tiquete == null)
        //         return NotFound("Tiquete no encontrado.");

        //     // Crear contenido del QR (puedes personalizarlo)
        //     var contenidoQR = $"Tiquete: {tiquete.NumeroTiquete}\n" + $"Destino: {tiquete.InfoDestinos.Nombre}\n" + $"Fecha: {tiquete.FechaAbordo:yyyy-MM-dd HH:mm}\n" + $"Valor: {tiquete.TotalVenta:C}";

        //     // Generar QR
        //     var qrCode = QRService.GenerarQRCode(contenidoQR);

        //     // Retornar QR como imagen PNG
        //     return File(qrCode, "image/png");
        // }
    }
}

public class TiqueteRequest
{
    public DateTime FechaExpedicion { get; set; }
    public string NumeroTiquete { get; set; } = string.Empty;

    public int TipoTiqueteId { get; set; }
    public int AgenciaId { get; set; }
    public int GrupoId { get; set; }
    public int InfoDestinoId { get; set; }
    public DateTime FechaAbordo { get; set; }
    public string HoraAbordo { get; set; }
    public DateTime? FechaRetorno { get; set; }

    public string HoraRetorno { get; set; }
    public bool SoloIda { get; set; }
    public int CantidadPasajeros { get; set; }
    public decimal ValorSugerido { get; set; }
    public decimal ValorIndividual { get; set; }
    public bool ExcesoEquipaje { get; set; }
    public decimal TotalVenta { get; set; }
    public string? Descripcion { get; set; }
    public int FormaPagoId { get; set; }
    public int PasajeroId { get; set; } // Asegúrate de incluir el PasajeroId si es necesario
    public int ViajeId { get; set; }

}


public class ViajeInfo
{
    public string HoraSalida { get; set; }
    public int CantidadPasajero { get; set; }
    public int CupoDisponible { get; set; }
    public int LimiteCupo { get; set; }
    public int KlsCarga { get; set; }
}