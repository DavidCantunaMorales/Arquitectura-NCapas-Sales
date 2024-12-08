using Entities;
using SLC;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestService.Controllers
{
    public class SalesController : ApiController, IService
    {
        // Metodos para Categorias
        [HttpPost]
        public Categories CreateCategory(Categories newCategory)
        {
            var categoryLogic = new CategoryLogic();
            var category = categoryLogic.Create(newCategory);
            return category;
        }

        [HttpGet]
        public List<Categories> RetrieveAllCategories()
        {
            var categoryLogic = new CategoryLogic();
            var categories = categoryLogic.RetrieveAllCategories();
            return categories;
        }

        [HttpGet]
        [Route("api/Sales/RetrieveCategoryID/{categoryID}")]
        public Categories RetrieveCategoryID(int categoryID)
        {
            var categoryLogic = new CategoryLogic();
            var categories = categoryLogic.RetrieveByID(categoryID);
            return categories;
        }

        [HttpPost]
        public bool UpdateCategory(Categories categoryToUpdate)
        {
            var categoryLogic = new CategoryLogic();
            var result = categoryLogic.Update(categoryToUpdate);
            return result;
        }

        [HttpDelete]
        [Route("api/Sales/DeleteCategory/{categoryID}")]
        public bool DeleteCategory(int categoryID)
        {
            var categoryLogic = new CategoryLogic();
            var result = categoryLogic.Delete(categoryID);
            return result;
        }

        // Metodos para Productos

        [HttpPost]
        public Products CreateProduct(Products newProduct)
        {
            var productLogic = new ProductLogic();
            var product = productLogic.Create(newProduct);
            return product;
        }

        [HttpGet]
        public List<Products> RetrieveAllProducts()
        {
            var productLogic = new ProductLogic();
            var products = productLogic.RetrieveAllProducts();
            return products;
        }

        [HttpGet]
        public Products RetrieveProductID(int ID)
        {
            var productLogic = new ProductLogic();
            var result = productLogic.RetrieveByID(ID);
            return result;
        }

        [HttpPost]
        public bool UpdateProduct(Products productToUpdate)
        {
            var productLogic = new ProductLogic();
            var result = productLogic.Update(productToUpdate);
            return result;
        }

        [HttpDelete]
        public bool DeleteProduct(int ID)
        {
            var productLogic = new ProductLogic();
            var result = productLogic.Delete(ID);
            return result;
        }

        [HttpGet]
        public List<Products> FilterProductsByCategoryID(int ID)
        {
            var productLogic = new ProductLogic();
            var result = productLogic.FilterByCategoryID(ID);
            return result;
        }
    }
}
