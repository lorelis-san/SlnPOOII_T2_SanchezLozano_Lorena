using Microsoft.AspNetCore.Mvc.Rendering;

namespace POOII_T2_SanchezLozano_Lorena.Models.ViewModels
{
    public class CategoriaVM
    {
        public Categorium oCategoria { get; set; }
        public List<SelectListItem> ListaCategoria { get; set; }
    }
}