using Microsoft.AspNetCore.Mvc;
using APIproyectoUgel.Models; // Cambia según tu espacio de nombres
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoUgel.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly DbAae202Dbugelproyecto01Context _context;

        public EquiposController(DbAae202Dbugelproyecto01Context context)
        {
            _context = context;
        }

        // GET: api/equipos
        [HttpGet]
        public ActionResult<IEnumerable<Equipo>> GetEquipos()
        {
            return _context.Equipos.ToList();
        }

        // GET: api/equipos/5
        [HttpGet("{id}")]
        public ActionResult<Equipo> GetEquipo(int id)
        {
            var equipo = _context.Equipos.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }
            return equipo;
        }

        // POST: api/equipos
        [HttpPost]
        public ActionResult<Equipo> PostEquipo(Equipo equipo)
        {
            _context.Equipos.Add(equipo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEquipo), new { id = equipo.IdEquipo }, equipo);
        }

        // PUT: api/equipos/5
        [HttpPut("{id}")]
        public IActionResult PutEquipo(int id, Equipo equipo)
        {
            if (id != equipo.IdEquipo)
            {
                return BadRequest();
            }

            _context.Entry(equipo).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(id))
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

        // DELETE: api/equipos/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEquipo(int id)
        {
            var equipo = _context.Equipos.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }

            _context.Equipos.Remove(equipo);
            _context.SaveChanges();

            return NoContent();
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.IdEquipo == id);
        }
    }
}
