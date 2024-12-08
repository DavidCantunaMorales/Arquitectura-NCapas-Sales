using BLL;
using Entities;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestService.Controllers
{
    public class ProductController : ApiController, IProductService
    {

        [HttpGet]
        public List<Products> GetProducts()
        {
            var productLogic = new ProductLogic();
            var products = productLogic.RetrieveAllProducts();
            return products;
        }

        [HttpGet]
        public Products GetProductById(int id)
        {
            var productLogic = new ProductLogic();
            var product = productLogic.RetrieveById(id);
            return product;
        }


        [HttpPost]
        public Products CreateProduct(Products product)
        {
            var productLogic = new ProductLogic();
            var newProduct = productLogic.Create(product);
            return newProduct;
        }

        [HttpPut]
        public bool UpdateProduct(Products product)
        {
            var productLogic = new ProductLogic();
            var updatedProduct = productLogic.Update(product);
            return updatedProduct;
        }


        [HttpDelete]
        public bool DeleteProduct(int id)
        {
            var productLogic = new ProductLogic();
            var product = productLogic.Delete(id);
            return product;
        }
    }
}
