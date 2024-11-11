using Microsoft.AspNetCore.Mvc;
using APIproyectoUgel.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoUgel.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientosController : ControllerBase
    {
        private readonly DbAae202Dbugelproyecto01Context _context;

        public MantenimientosController(DbAae202Dbugelproyecto01Context context)
        {
            _context = context;
        }

        // GET: api/mantenimientos
        [HttpGet]
        public ActionResult<IEnumerable<Mantenimiento>> GetMantenimientos()
        {
            try
            {
                // Incluir la información del equipo y el usuario si existen
                var mantenimientos = _context.Mantenimientos
                                             .Include(m => m.IdEquipoNavigation)
                                             .Include(m => m.IdUserNavigation)
                                             .ToList();

                if (mantenimientos == null || !mantenimientos.Any())
                {
                    return NotFound("No se encontraron mantenimientos.");
                }

                return Ok(mantenimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/mantenimientos/5
        [HttpGet("{id}")]
        public ActionResult<Mantenimiento> GetMantenimiento(int id)
        {
            try
            {
                var mantenimiento = _context.Mantenimientos
                                            .Include(m => m.IdEquipoNavigation)
                                            .Include(m => m.IdUserNavigation)
                                            .FirstOrDefault(m => m.IdManteni == id);

                if (mantenimiento == null)
                {
                    return NotFound($"Mantenimiento con ID {id} no encontrado.");
                }

                return Ok(mantenimiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/mantenimientos
        [HttpPost]
        public ActionResult<Mantenimiento> PostMantenimiento(Mantenimiento mantenimiento)
        {
            if (mantenimiento == null)
            {
                return BadRequest("El mantenimiento no puede ser nulo.");
            }

            try
            {
                _context.Mantenimientos.Add(mantenimiento);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetMantenimiento), new { id = mantenimiento.IdManteni }, mantenimiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el mantenimiento: {ex.Message}");
            }
        }

        // PUT: api/mantenimientos/5
        [HttpPut("{id}")]
        public IActionResult PutMantenimiento(int id, Mantenimiento mantenimiento)
        {
            if (id != mantenimiento.IdManteni)
            {
                return BadRequest("El ID del mantenimiento no coincide.");
            }

            _context.Entry(mantenimiento).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MantenimientoExists(id))
                {
                    return NotFound($"Mantenimiento con ID {id} no encontrado.");
                }
                else
                {
                    return StatusCode(500, "Error de concurrencia al actualizar el mantenimiento.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/mantenimientos/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMantenimiento(int id)
        {
            try
            {
                var mantenimiento = _context.Mantenimientos.Find(id);
                if (mantenimiento == null)
                {
                    return NotFound($"Mantenimiento con ID {id} no encontrado.");
                }

                _context.Mantenimientos.Remove(mantenimiento);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el mantenimiento: {ex.Message}");
            }
        }

        // Método auxiliar para verificar la existencia de un mantenimiento
        private bool MantenimientoExists(int id)
        {
            return _context.Mantenimientos.Any(e => e.IdManteni == id);
        }

        // GET: api/mantenimientos/porEquipo/1
        [HttpGet("porEquipo/{idEquipo}")]
        public ActionResult<IEnumerable<Mantenimiento>> GetMantenimientosPorEquipo(int idEquipo)
        {
            try
            {
                var mantenimientos = _context.Mantenimientos
                                             .Include(m => m.IdEquipoNavigation)
                                             .Where(m => m.IdEquipo == idEquipo)
                                             .ToList();

                if (mantenimientos == null || !mantenimientos.Any())
                {
                    return NotFound($"No se encontraron mantenimientos para el equipo con ID {idEquipo}.");
                }

                return Ok(mantenimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
