using Entities;
using Newtonsoft.Json;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NSalesProxyService
{
    public class Proxy : IService
    {
        string BaseAddress = "http://localhost:59181";

        public async Task<T> SendPost<T, PostData>(string requestURI, PostData data)
        {
            T Result = default(T);
            using (var Client = new HttpClient())
            {
                try
                {
                    // URL Absoluto
                    requestURI = BaseAddress + requestURI;
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add
                     (new MediaTypeWithQualityHeaderValue("application/json"));
                    var JSONData = JsonConvert.SerializeObject(data);
                    HttpResponseMessage Response =
                     await Client.PostAsync(requestURI,
                    new StringContent(JSONData.ToString(),
                     Encoding.UTF8, "application/json"));
                    var ResultWebAPI = await Response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<T>(ResultWebAPI);
                }
                catch (Exception)
                {
                    // Manejar la excepción
                }
            }
            return Result;
        }

        public async Task<T> SendGet<T>(string requestURI)
        {
            T Result = default(T);
            using (var Client = new HttpClient())
            {
                try
                {
                    requestURI = BaseAddress + requestURI; // URL Absoluto
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("application/json"));
                    var ResultJSON = await Client.GetStringAsync(requestURI);
                    Result = JsonConvert.DeserializeObject<T>(ResultJSON);
                }
                catch (Exception)
                {
                    // Manejar la excepción
                }
            }
            return Result;
        }

        public async Task<Products> CreateProductAsync(Products newProduct)
        {
            return await SendPost<Products, Products>
            ("/api/Sales/CreateProduct", newProduct);
        }
        public Products CreateProduct(Products newProduct)
        {
            Products Result = null;
            // Ejecutar la tarea en un nuevo hilo
            // para que no se bloquee el hilo síncrono
            // con Wait esperamos la operación asíncrona
            Task.Run(async () => Result =
            await CreateProductAsync(newProduct)).Wait();
            return Result;
        }


        public async Task<Products> RetrieveProductIDAsync(int ID)
        {
            return await SendGet<Products>($"/api/Sales/RetrieveProductByID/{ID}");
        }
        public Products RetrieveProductID(int ID)
        {
            Products Result = null;
            Task.Run(async () =>
           Result = await RetrieveProductIDAsync(ID)).Wait();
            return Result;
        }


        public async Task<bool> UpdateProductAsync(Products productToUpdate)
        {
            return await SendPost<bool, Products>
            ("/api/Sales/UpdateProduct", productToUpdate);
        }
        public bool UpdateProduct(Products productToUpdate)
        {
            bool Result = false;
            Task.Run(async () => Result = await
            UpdateProductAsync(productToUpdate)).Wait();
            return Result;
        }


        public async Task<bool> DeleteProductAsync(int ID)
        {
            return await SendGet<bool>($"/api/Sales/DeleteProduct/{ID}");
        }
        public bool DeleteProduct(int ID)
        {
            bool Result = false;
            Task.Run(async () => Result = await DeleteProductAsync(ID)).Wait();
            return Result;
        }


        public async Task<List<Products>> FilterProductsByCategoryIDAsync(int ID)
        {
            return await SendGet<List<Products>>
            ($"/api/Sales/FilterProductsByCategoryID/{ID}");
        }
        public List<Products> FilterProductsByCategoryID(int ID)
        {
            List<Products> Result = null;
            Task.Run(async () => Result = await
            FilterProductsByCategoryIDAsync(ID)).Wait();
            return Result;
        }


        public async Task<List<Products>> RetrieveAllProductsAsync()
        {
            return await SendGet<List<Products>>("/api/Sales/RetrieveAllProducts");
        }
        public List<Products> RetrieveAllProducts()
        {
            List<Products> Result = null;
            Task.Run(async () => Result = await RetrieveAllProductsAsync()).Wait();
            return Result;
        }


        // CATEGORY METHODS

        public async Task<Categories> CreateCategoryAsync(Categories newCategory)
        {
            return await SendPost<Categories, Categories>
            ("/api/Sales/CreateCategory", newCategory);
        }
        public Categories CreateCategory(Categories newCategory)
        {
            Categories Result = null;
            Task.Run(async () => Result = await
           CreateCategoryAsync(newCategory)).Wait();
            return Result;
        }


        public async Task<Categories> RetrieveCategoryByIDAsync(int categoryID)
        {
            return await SendGet<Categories>($"/api/Sales/RetrieveCategoryByID/{categoryID}");
        }
        public Categories RetrieveCategoryID(int categoryID)
        {
            Categories Result = null;
            Task.Run(async () => Result = await RetrieveCategoryByIDAsync(categoryID)).Wait();
            return Result;
        }

        public async Task<bool> UpdateCategoryAsync(Categories categoryToUpdate)
        {
            return await SendPost<bool, Categories>("/api/Sales/UpdateCategory", categoryToUpdate);
        }
        public bool UpdateCategory(Categories categoryToUpdate)
        {
            bool Result = false;
            Task.Run(async () => Result = await UpdateCategoryAsync(categoryToUpdate)).Wait();
            return Result;
        }


        public async Task<bool> DeleteCategoryAsync(int categoryID)
        {
            return await SendGet<bool>($"/api/Sales/DeleteCategory/{categoryID}");
        }
        public bool DeleteCategory(int categoryID)
        {
            bool Result = false;
            Task.Run(async () => Result = await DeleteCategoryAsync(categoryID)).Wait();
            return Result;
        }


        public async Task<List<Categories>> RetrieveAllCategoriesAsync()
        {
            return await SendGet<List<Categories>>("/api/Sales/RetrieveAllCategories");
        }
        public List<Categories> RetrieveAllCategories()
        {
            List<Categories> Result = null;
            Task.Run(async () => Result = await RetrieveAllCategoriesAsync()).Wait();
            return Result;
        }
    }
}
