using Microsoft.AspNetCore.Mvc.Rendering;

namespace POOII_T2_SanchezLozano_Lorena.Models.ViewModels
{
    public class ProductoVM
    {
        public Producto oProducto { get; set; }
        public List<SelectListItem> oListaCategoria { get; set; }

    }
}
