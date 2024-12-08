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
    public class CategoryController : ApiController, ICategoryService
    {
        [HttpPost]
        public Categories CreateCategory(Categories category)
        {
            var categoryLogic = new CategoryLogic();
            var createdCategory = categoryLogic.Create(category);
            return createdCategory;
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var categoryLogic = new CategoryLogic();
            var result = categoryLogic.Delete(id);
            return result;
        }

        [HttpGet]
        public Categories GetCategoryById(int id)
        {
            var categoryLogic = new CategoryLogic();
            var category = categoryLogic.RetrieveById(id);
            return category;
        }

        [HttpPut]
        public bool UpdateCategory(Categories category)
        {
            var categoryLogic = new CategoryLogic();
            var result = categoryLogic.Update(category);
            return result;
        }

        [HttpGet]
        public List<Categories> GetAllCategories()
        {
            var categoryLogic = new CategoryLogic();
            var categories = categoryLogic.RetrieveAll();
            return categories;
        }
    }
}
