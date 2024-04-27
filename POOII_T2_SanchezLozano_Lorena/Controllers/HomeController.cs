using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using POOII_T2_SanchezLozano_Lorena.Models;
using System.Diagnostics;
using POOII_T2_SanchezLozano_Lorena.Models.ViewModels;


namespace POOII_T2_SanchezLozano_Lorena.Controllers
{
    public class HomeController : Controller
    {
        private readonly Dbt2Context _DBContext;
        public HomeController(Dbt2Context DBContext)
        {
            _DBContext = DBContext;
        }

        public IActionResult Index()
        {
            //Obtiene el listado de los productos y retorna a la vista
            List<Producto> lista = _DBContext.Productos.Include(c => c.oCategoria).ToList();
            return View(lista);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //CRUD DE LA ENTIDAD PRODUCTO

        [HttpGet]
        public IActionResult Producto_Detalle(int idProducto)
        {
            //Instancia de la clase ProductoVM
            ProductoVM oProductoVM = new ProductoVM()
            {
                oProducto = new Producto(),
                //Se obtiene la lista de las categorías
                oListaCategoria = _DBContext.Categorias.Select(categoria => new SelectListItem()
                {
                    Text = categoria.NombreCate,
                    Value = categoria.IdCategoria.ToString()
                }).ToList()
            };

            //Si el idProducto es encontrado, será para editar  
            if (idProducto != 0)
            {
                oProductoVM.oProducto = _DBContext.Productos.Find(idProducto);
            }


            return View(oProductoVM);
        }

        [HttpPost]
        public IActionResult Producto_Detalle(ProductoVM oProductoVM)
        {
            //Si el idProducto que se obtiene desde el ProductoVM no existe, se añade; en cambio, se edita.
            if (oProductoVM.oProducto.IdProducto == 0)
            {
                _DBContext.Productos.Add(oProductoVM.oProducto);
            }
            else
            {
                _DBContext.Productos.Update(oProductoVM.oProducto);
            }

            _DBContext.SaveChanges();
            //Se retorna a la vista de listado
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar(int idProducto)
        {
            //Obtiene el producto para eliminar y muestra la vista Eliminar
            Producto oProducto = _DBContext.Productos.Include(c => c.oCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();
            return View(oProducto);
        }

        [HttpPost]
        public IActionResult Eliminar(Producto oProducto)
        {
            //Elimina el producto de la bd y guarda cambios,luego retorna a la vista principal (listado)
            _DBContext.Productos.Remove(oProducto);
            _DBContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        //CRUD DE LA ENTIDAD CATEGORÍA

        public IActionResult IndexCategoria()
        {
            //Obtiene el listado de las categorías
            List<Categorium> lista = _DBContext.Categorias.ToList();
            return View(lista);
        }


        [HttpGet]
        public IActionResult Categoria_Detalle(int idCategoria)
        {
            //Instancia de la clase CategoríaVM
            CategoriaVM categoriaVM = new CategoriaVM()
            {
                oCategoria = new Categorium(),
                //Se obtiene la lista de las categorías
                ListaCategoria = _DBContext.Categorias.Select(categoria => new SelectListItem()
                {
                    Text = categoria.NombreCate,
                    Value = categoria.IdCategoria.ToString()
                }).ToList()
            };
            //Si la categoría existe, se retorna a la vista de editar; sino a la vista para crear
            if (idCategoria != 0)
            {
                categoriaVM.oCategoria = _DBContext.Categorias.Find(idCategoria);
            }

            return View(categoriaVM);
        }

        [HttpPost]
        public IActionResult Categoria_Detalle(CategoriaVM categoriaVM)
        {

            //Si no se encuentra el idCategoría, se crea; en cambio, se edita
            if (categoriaVM.oCategoria.IdCategoria == 0)
            {
                _DBContext.Categorias.Add(categoriaVM.oCategoria);
            }
            else
            {
                _DBContext.Categorias.Update(categoriaVM.oCategoria);
            }

            _DBContext.SaveChanges();
            //Retorna a la vista de listado

            return RedirectToAction("IndexCategoria", "Home");
        }

        [HttpGet]
        public IActionResult EliminarCat(int idCategoria)
        {
            //Se obtiene el registro de la categoría según su id
            Categorium categorium = _DBContext.Categorias.Where(c => c.IdCategoria == idCategoria).FirstOrDefault();
            return View(categorium);

        }

        [HttpPost]
        public IActionResult EliminarCat(Categorium categorium)
        {

            // Para eliminar la categoría debemos obtener los productos que tienen cierta categoría
            //para que también se eliminen ya que es una FK.
            var productosConCategoria = _DBContext.Productos.Where(p => p.IdCategoria == categorium.IdCategoria).ToList();

            // Eliminar los productos asociados
            _DBContext.Productos.RemoveRange(productosConCategoria);
            //Elimina la categoría
            _DBContext.Categorias.Remove(categorium);
            //Guarda los cambios
            _DBContext.SaveChanges();
            //Retorna a la vista
            return RedirectToAction("IndexCategoria", "Home");
        }



        ///


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
