using System;
using System.Collections.Generic;

namespace APIproyectoUgel.Models;

public partial class MantenimientosProgramado
{
    public int IdManPro { get; set; }

    public int? IdEquipo { get; set; }

    public DateTime FechaManPro { get; set; }

    public string TipoManPro { get; set; } = null!;

    public string DescriManPro { get; set; } = null!;

    public string EstadoManPro { get; set; } = null!;

    public DateTime? FechaRealizado { get; set; }

    public virtual Equipo? IdEquipoNavigation { get; set; }
}
