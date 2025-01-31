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
}
