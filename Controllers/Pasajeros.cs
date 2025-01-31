
using DestinopacificoExpres.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class PasajeroController : ControllerBase
{
    private readonly DatabaseContext _context;

    public PasajeroController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pasajero>>> GetPasajeros()
    {
        //  return await _context.Pasajeros.ToListAsync();
        // var pasajeros = await _pasajerosService.ObtenerPasajerosAsync();
        var pasajeros = await _context.Pasajeros.ToListAsync();
        return Ok(pasajeros);
    }

    // GET: api/Pasajero/{id}
    [HttpGet("{id}")]
    public ActionResult<Pasajero> GetPasajero(int id)
    {
        var pasajero = _context.Pasajeros.Find(id);
        if (pasajero == null)
            return NotFound();

        return Ok(pasajero);
    }

    // GET: api/Pasajero/{id}
    [HttpGet("Documento/{documento}")]
    public async Task<ActionResult<Pasajero>> GetPasajeroDocumento(string documento)
    {
        // var pasajero =  _context.Pasajeros.FirstOrDefaultAsync(p => p.Documento == documento);
         var pasajero = await _context.Pasajeros
        .Where(p => p.Documento == documento)
        .Select(p => new Pasajero
        {
            PasajeroId = p.PasajeroId,
            TipoDocumentoId=p.TipoDocumentoId,
            Documento = p.Documento,
            Nombre = p.Nombre,
            Apellido = p.Apellido,
            Edad = p.Edad,
            Email =p.Email,
            Telefono = p.Telefono,
            Direccion = p.Direccion,
            Fecha_Registro = p.Fecha_Registro,
            Activo= p.Activo
        })
        .FirstOrDefaultAsync();
        return Ok(pasajero);
    }


    // POST: api/Pasajero
    [HttpPost]
    public ActionResult<Pasajero> CreatePasajero(Pasajero Pasajeros)
    {
        _context.Pasajeros.Add(Pasajeros);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetPasajero), new { id = Pasajeros.PasajeroId }, Pasajeros);
    }

    // PUT: api/Pasajero/{id}
    [HttpPut("{id}")]
    public IActionResult UpdatePasajero(int id, Pasajero pasajero)
    {
        if (id != pasajero.PasajeroId)
            return BadRequest();

        var existingPasajero = _context.Pasajeros.Find(id);
        if (existingPasajero == null)
            return NotFound();

        // Update properties
        existingPasajero.TipoDocumentoId = pasajero.TipoDocumentoId;
        existingPasajero.Documento = pasajero.Documento;
        existingPasajero.Nombre = pasajero.Nombre;
        existingPasajero.Apellido = pasajero.Apellido;
        existingPasajero.Edad = pasajero.Edad;
        existingPasajero.Email = pasajero.Email;
        existingPasajero.Telefono = pasajero.Telefono;
        existingPasajero.Direccion = pasajero.Direccion;
        existingPasajero.Fecha_Registro = pasajero.Fecha_Registro;
        existingPasajero.Activo= pasajero.Activo;
        
        _context.SaveChanges();
        return NoContent();
    }

    // DELETE: api/Pasajero/{id}
    [HttpDelete("{id}")]
    public IActionResult DeletePasajero(int id)
    {
        var pasajero = _context.Pasajeros.Find(id);
        if (pasajero == null)
            return NotFound();

        _context.Pasajeros.Remove(pasajero);
        _context.SaveChanges();
        return NoContent();
    }
    
}