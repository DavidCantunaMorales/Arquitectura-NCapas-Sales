using Entities;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NSalesMVCPLS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index(int? id) // Cambia a int? (nullable)
        {
            if (id == null)
            {
                return View();
            }

            var Proxy = new BLL.ProductLogic();
            var Products = Proxy.FilterByCategoryID(id.Value); // Usamos id.Value ya que sabemos que no es null ahora

            if (Products == null || !Products.Any())
            {
                // Si no hay productos, muestra un mensaje o redirige
                ViewBag.Message = "No se encontraron productos para esta categoría.";
                return View("ProductList", new List<Entities.Products>());
            }

            // Renderizar la vista de lista de productos
            return View("ProductList", Products);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var Proxy = new BLL.ProductLogic();
            var Model = Proxy.RetrieveByID(id);
            return View(Model);
        }

        public ActionResult CUD(int id = 0)
        {
            var Proxy = new BLL.ProductLogic();
            var Model = new Products();
            if (id != 0)
            {
                Model = Proxy.RetrieveByID(id);
            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult CUD(Products newProduct,
            string CreateBtn, string UpdateBtn, string DeleteBtn)
        {
            Products Product;
            var Proxy = new BLL.ProductLogic();
            ActionResult Result = View();
            if (CreateBtn != null) // ¿Crear un producto? 
            {
                Product = Proxy.Create(newProduct);
                if (Product != null)
                {
                    Result = RedirectToAction("CUD", new { id = Product.ProductID });
                }
            }
            else if (UpdateBtn != null) // ¿Modificar un producto? 
            {
                var IsUpdate = Proxy.Update(newProduct);
                if (IsUpdate)
                {
                    Result = Content("El producto se ha actualizado");
                }

            }
            else if (DeleteBtn != null) // ¿Eliminar un producto? 
            {
                var DeletedProduct = Proxy.Delete(newProduct.ProductID);
                if (DeletedProduct)
                {
                    Result = Content("El producto se ha eliminado");
                }
            }
            return Result;
        }

    }
}