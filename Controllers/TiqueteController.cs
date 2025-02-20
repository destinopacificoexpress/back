using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DestinopacificoExpres.Data;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;


namespace DestinopacificoExpres.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                    ViajeId = tiqueteDto.ViajeId
                };
                _context.Tiquetes.Add(tiquete);
                _context.SaveChanges();
                return CreatedAtAction(nameof(CrearTiquete), new { id = tiquete.TiqueteId }, tiquete);
            }
            catch (DbUpdateException  ex)
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
        public  ActionResult<IEnumerable<Tiquete>> GetTiquetes()
        {

                var tiquetes =  _context.Tiquetes.Select(T => new
                {
                    TiqueteId = T.TiqueteId,
                    FechaExpedicion = T.FechaExpedicion,
                    NumeroTiquete = T.NumeroTiquete,
                    TipoTiqueteId = T.TipoTiqueteId,
                    AgenciaId = T.AgenciaId,
                    GrupoId = T.GrupoId,
                    InfoDestinoId = T.InfoDestinoId,
                    FechaAbordo = T.FechaAbordo,
                    HoraAbordo = T.HoraAbordo,
                    FechaRetorno = T.FechaRetorno,
                    HoraRetorno = T.HoraRetorno,
                    SoloIda = T.SoloIda,
                    CantidadPasajeros = T.CantidadPasajeros,
                    ValorSugerido = T.ValorSugerido,
                    ValorIndividual = T.ValorIndividual,
                    ExcesoEquipaje = T.ExcesoEquipaje,
                    TotalVenta = T.TotalVenta,
                    Descripcion = T.Descripcion,
                    FormaPagoId = T.FormaPagoId,
                    PasajeroId = T.PasajeroId,
                    ViajeId = T.ViajeId
                })
                .ToList();
            return Ok(tiquetes);
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
    public string HoraAbordo{ get; set; }
    public DateTime? FechaRetorno { get; set; }
    
    public string HoraRetorno{ get; set; }
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
