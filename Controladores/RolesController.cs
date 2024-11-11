using Microsoft.AspNetCore.Mvc;
using APIproyectoUgel.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoUgel.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DbAae202Dbugelproyecto01Context _context;

        public RolesController(DbAae202Dbugelproyecto01Context context)
        {
            _context = context;
        }

        // GET: api/roles
        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetRoles()
        {
            return _context.Roles.ToList();
        }

        // GET: api/roles/5
        [HttpGet("{id}")]
        public ActionResult<Role> GetRole(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            return role;
        }

        // POST: api/roles
        [HttpPost]
        public ActionResult<Role> PostRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRole), new { id = role.IdRol }, role);
        }

        // PUT: api/roles/5
        [HttpPut("{id}")]
        public IActionResult PutRole(int id, Role role)
        {
            if (id != role.IdRol)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // DELETE: api/roles/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            _context.SaveChanges();

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.IdRol == id);
        }
    }
}
