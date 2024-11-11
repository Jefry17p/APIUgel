using System;
using System.Collections.Generic;

namespace APIproyectoUgel.Models;

public partial class Equipo
{
    public int IdEquipo { get; set; }

    public string NombreEqui { get; set; } = null!;

    public string TipoEqui { get; set; } = null!;

    public string SerieEqui { get; set; } = null!;

    public string MarcaEqui { get; set; } = null!;

    public string ModeloEqui { get; set; } = null!;

    public string AreaEqui { get; set; } = null!;

    public DateTime? FechaAdquisicion { get; set; }

    public string EstadoEqui { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();

    public virtual ICollection<MantenimientosProgramado> MantenimientosProgramados { get; set; } = new List<MantenimientosProgramado>();
}
