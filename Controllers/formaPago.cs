using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DestinopacificoExpres.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class formaPago : ControllerBase
{
    private readonly DatabaseContext _context;

    public formaPago(DatabaseContext context)
    {
        _context = context;
    }

    // GET: api/FormaPago
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormaPago>>> GetFormaPago()
    {
        var FormaPago = await _context.FormasPago.ToListAsync();

        return Ok(FormaPago);
    }

    // GET: api/FormaPago/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FormaPago>> GetFormaPago(int id)
    {
        var FormaPago = await _context.FormasPago.FindAsync(id);

        if (FormaPago == null)
        {
            return NotFound();
        }

        return FormaPago;
    }

    // PUT: api/FormaPago/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFormaPago(int id, FormaPago FormaPago)
    {
        if (id != FormaPago.Id)
        {
            return BadRequest();
        }

        _context.Entry(FormaPago).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FormaPagoExists(id))
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

    private bool FormaPagoExists(int id)
    {
        return _context.FormasPago.Any(e => e.Id == id);
    }

    // POST: api/FormaPago
    [HttpPost]
    public async Task<ActionResult<FormaPago>> PostFormaPago(FormaPago FormaPago)
    {
        _context.FormasPago.Add(FormaPago);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFormaPago", new { id = FormaPago.Id }, FormaPago);
    }

    // DELETE: api/FormaPago/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<FormaPago>> DeleteFormaPago(int id)
    {
        var FormaPago = await _context.FormasPago.FindAsync(id);
        if (FormaPago == null)
        {
            return NotFound();
        }

        _context.FormasPago.Remove(FormaPago);
        await _context.SaveChangesAsync();

        return FormaPago;
    }
}
