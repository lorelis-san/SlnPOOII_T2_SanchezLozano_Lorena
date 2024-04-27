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
            ProductoVM oProductoVM = new ProductoVM()
            {
                oProducto = new Producto(),
                oListaCategoria = _DBContext.Categorias.Select(categoria => new SelectListItem()
                {
                    Text = categoria.Descripcion,
                    Value = categoria.IdCategoria.ToString()
                }).ToList()
            };
            if (idProducto != 0)
            {
                oProductoVM.oProducto = _DBContext.Productos.Find(idProducto);
            }


            return View(oProductoVM);
        }

        [HttpPost]
        public IActionResult Producto_Detalle(ProductoVM oProductoVM)
        {
            if (oProductoVM.oProducto.IdProducto == 0)
            {
                _DBContext.Productos.Add(oProductoVM.oProducto);
            }
            else
            {
                _DBContext.Productos.Update(oProductoVM.oProducto);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar(int idProducto)
        {
            Producto oProducto = _DBContext.Productos.Include(c => c.oCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();
            return View(oProducto);
        }

        [HttpPost]
        public IActionResult Eliminar(Producto oProducto)
        {
            _DBContext.Productos.Remove(oProducto);
            _DBContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        //CRUD DE LA ENTIDAD CATEGORÍA

        public IActionResult IndexCategoria()
        {
            List<Categorium> lista = _DBContext.Categorias.ToList();
            return View(lista);
        }


        [HttpGet]
        public IActionResult Categoria_Detalle(int idCategoria)
        {
            CategoriaVM categoriaVM = new CategoriaVM()
            {
                oCategoria = new Categorium(),
                ListaCategoria = _DBContext.Categorias.Select(categoria => new SelectListItem()
                {
                    Text = categoria.NombreCate,
                    Value = categoria.IdCategoria.ToString()
                }).ToList()
            };

            if (idCategoria != 0)
            {
                categoriaVM.oCategoria = _DBContext.Categorias.Find(idCategoria);
            }

            return View(categoriaVM);
        }

        [HttpPost]
        public IActionResult Categoria_Detalle(CategoriaVM categoriaVM)
        {
            if (categoriaVM.oCategoria.IdCategoria == 0)
            {
                _DBContext.Categorias.Add(categoriaVM.oCategoria);
            }
            else
            {
                _DBContext.Categorias.Update(categoriaVM.oCategoria);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("IndexCategoria", "Home");
        }

        [HttpGet]
        public IActionResult EliminarCat(int idCategoria)
        {
            Categorium categorium = _DBContext.Categorias.Where(c => c.IdCategoria == idCategoria).FirstOrDefault();
            return View(categorium);
        }

        [HttpPost]
        public IActionResult EliminarCat(Categorium categorium)
        {
            _DBContext.Categorias.Remove(categorium);
            _DBContext.SaveChanges();

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
