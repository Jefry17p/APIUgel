using Microsoft.AspNetCore.Mvc;
using APIproyectoUgel.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoUgel.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientosProgramadosController : ControllerBase
    {
        private readonly DbAae202Dbugelproyecto01Context _context;

        public MantenimientosProgramadosController(DbAae202Dbugelproyecto01Context context)
        {
            _context = context;
        }

        // GET: api/mantenimientosprogramados
        [HttpGet]
        public ActionResult<IEnumerable<MantenimientosProgramado>> GetMantenimientosProgramados()
        {
            // Incluye la información del equipo en la consulta
            var mantenimientos = _context.MantenimientosProgramados
                                         .Include(mp => mp.IdEquipoNavigation)
                                         .ToList();
            return mantenimientos;
        }

        // GET: api/mantenimientosprogramados/5
        [HttpGet("{id}")]
        public ActionResult<MantenimientosProgramado> GetMantenimientoProgramado(int id)
        {
            var mantenimientoProgramado = _context.MantenimientosProgramados
                                                  .Include(mp => mp.IdEquipoNavigation) // Incluye la información del equipo
                                                  .FirstOrDefault(mp => mp.IdManPro == id);

            if (mantenimientoProgramado == null)
            {
                return NotFound();
            }

            return mantenimientoProgramado;
        }

        // POST: api/mantenimientosprogramados
        [HttpPost]
        public ActionResult<MantenimientosProgramado> PostMantenimientoProgramado(MantenimientosProgramado mantenimientoProgramado)
        {
            if (mantenimientoProgramado == null || string.IsNullOrWhiteSpace(mantenimientoProgramado.TipoManPro))
            {
                return BadRequest("El mantenimiento programado no es válido o falta información obligatoria.");
            }

            _context.MantenimientosProgramados.Add(mantenimientoProgramado);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMantenimientoProgramado), new { id = mantenimientoProgramado.IdManPro }, mantenimientoProgramado);
        }

        // PUT: api/mantenimientosprogramados/5
        [HttpPut("{id}")]
        public IActionResult PutMantenimientoProgramado(int id, MantenimientosProgramado mantenimientoProgramado)
        {
            if (id != mantenimientoProgramado.IdManPro)
            {
                return BadRequest("El ID del mantenimiento no coincide.");
            }

            _context.Entry(mantenimientoProgramado).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MantenimientoProgramadoExists(id))
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

        // DELETE: api/mantenimientosprogramados/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMantenimientoProgramado(int id)
        {
            var mantenimientoProgramado = _context.MantenimientosProgramados.Find(id);
            if (mantenimientoProgramado == null)
            {
                return NotFound();
            }

            _context.MantenimientosProgramados.Remove(mantenimientoProgramado);
            _context.SaveChanges();

            return NoContent();
        }

        private bool MantenimientoProgramadoExists(int id)
        {
            return _context.MantenimientosProgramados.Any(e => e.IdManPro == id);
        }

        // PUT: api/mantenimientosprogramados/{id}/marcarRealizado
        [HttpPut("{id}/marcarRealizado")]
        public async Task<IActionResult> MarcarMantenimientoRealizado(int id, [FromBody] DateTime? fechaRealizado)
        {
            var mantenimiento = await _context.MantenimientosProgramados
                                              .Include(mp => mp.IdEquipoNavigation) // Incluye la información del equipo
                                              .FirstOrDefaultAsync(mp => mp.IdManPro == id);

            if (mantenimiento == null)
            {
                return NotFound();
            }

            mantenimiento.EstadoManPro = "Realizado";
            mantenimiento.FechaRealizado = fechaRealizado ?? DateTime.UtcNow; // Usa la fecha proporcionada o la actual

            _context.MantenimientosProgramados.Update(mantenimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
