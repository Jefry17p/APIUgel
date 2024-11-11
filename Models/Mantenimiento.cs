using System;
using System.Collections.Generic;

namespace APIproyectoUgel.Models;

public partial class Mantenimiento
{
    public int IdManteni { get; set; }

    public int? IdEquipo { get; set; }

    public int? IdUser { get; set; }

    public string TipoManteni { get; set; } = null!;

    public string DescriManteni { get; set; } = null!;

    public DateTime FechaManteni { get; set; }

    public double CostoManteni { get; set; }

    public virtual Equipo? IdEquipoNavigation { get; set; }

    public virtual Usuario? IdUserNavigation { get; set; }
}
