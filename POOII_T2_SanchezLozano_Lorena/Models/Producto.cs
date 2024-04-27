using System;
using System.Collections.Generic;

namespace POOII_T2_SanchezLozano_Lorena.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? NombrePro { get; set; }

    public string? DescripcionPro { get; set; }

    public int? Stock { get; set; }

    public decimal? Precio { get; set; }

    public DateTime? FechaCreacionPro { get; set; }

    public int? IdCategoria { get; set; }

    public virtual Categorium? oCategoria { get; set; }
}
