using System;
using System.Collections.Generic;

namespace APIproyectoUgel.Models;

public partial class Usuario
{
    public int IdUser { get; set; }

    public string NombreUser { get; set; } = null!;

    public string ContraUser { get; set; } = null!;

    public DateTime? FechaCreacionUser { get; set; }

    public int? IdRol { get; set; }

    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
