using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POOII_T2_SanchezLozano_Lorena.Models;

public partial class Categorium
{
    public int IdCategoria { get; set; }
   
    public string? NombreCate { get; set; }
   
    public string? Descripcion { get; set; }

    public DateTime? FechaCreacionCat { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
