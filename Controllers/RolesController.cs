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
    public class RolesController : ControllerBase
    {

        private readonly DatabaseContext _context;

        public RolesController(DatabaseContext context)
        {
            _context = context;
        }

       [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRoles(int id)
        {
            var rol = await _context.Roles
                // .Include(u => u.roles)
                // .ThenInclude(ur => ur.RoleId = RoleId)
                .FirstOrDefaultAsync(u => u.RoleId == id);

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }
    }
}