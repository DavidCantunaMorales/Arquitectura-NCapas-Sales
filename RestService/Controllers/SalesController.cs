using Entities;
using SLC;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestService.Models;
using SecurityLayer;

namespace RestService.Controllers
{
    public class SalesController : ApiController, IService
    {
        public const string AdminRole = "1";
        public const string UserRole = "2";
        public const string ViewerRole = "3";

        // Métodos para Categorías

        [HttpPost]
        public Categories CreateCategory(Categories newCategory)
        {
            try
            {
                // 1. Extraer el token de la solicitud
                var token = Request.Headers.Authorization?.Parameter;
                if (string.IsNullOrEmpty(token))
                {
                    return null; // Token no proporcionado
                }

                // 2. Validar el token
                var principal = JwtService.ValidateToken(token);
                if (principal == null)
                {
                    return null; // Token inválido
                }

                // 3. Verificar el rol del usuario
                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "rol");
                if (roleClaim == null)
                {
                    return null; // Rol no encontrado
                }

                var userRole = roleClaim.Value;

                if (userRole != AdminRole && userRole != UserRole)
                {
                    return null; // Solo Admin y User pueden crear
                }

                // Lógica para crear la categoría
                var categoryLogic = new CategoryLogic();
                var category = categoryLogic.Create(newCategory);
                return category; // Retorna la categoría creada
            }
            catch (Exception)
            {
                return null; // Si hay un error, devolver null
            }
        }

        [HttpGet]
        public List<Categories> RetrieveAllCategories()
        {
            try
            {
                // Validación de rol
                var token = Request.Headers.Authorization?.Parameter;
                if (string.IsNullOrEmpty(token))
                {
                    return null; // Token no proporcionado
                }

                var principal = JwtService.ValidateToken(token);
                if (principal == null)
                {
                    return null; // Token inválido
                }

                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "rol");
                if (roleClaim.Value == ViewerRole || roleClaim.Value == AdminRole || roleClaim.Value == UserRole)
                {
                    var categoryLogic = new CategoryLogic();
                    var categories = categoryLogic.RetrieveAllCategories();
                    return categories;
                }
                else
                {
                    return null; // Si no tiene permiso, retornar null
                }
            }
            catch (Exception)
            {
                return null; // En caso de error, retorna null
            }
        }

        [HttpGet]
        [Route("api/Sales/RetrieveCategoryID/{categoryID}")]
        public Categories RetrieveCategoryID(int categoryID)
        {
            try
            {
                // Validación de rol
                var token = Request.Headers.Authorization?.Parameter;
                if (string.IsNullOrEmpty(token))
                {
                    return null; // Token no proporcionado
                }

                var principal = JwtService.ValidateToken(token);
                if (principal == null)
                {
                    return null; // Token inválido
                }

                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "rol");
                if (roleClaim.Value == ViewerRole || roleClaim.Value == AdminRole || roleClaim.Value == UserRole)
                {
                    var categoryLogic = new CategoryLogic();
                    var category = categoryLogic.RetrieveByID(categoryID);
                    return category; // Solo GET permitido para ViewerRole
                }
                else
                {
                    return null; // Si no tiene permisos, retornar null
                }
            }
            catch (Exception)
            {
                return null; // En caso de error, retorna null
            }
        }

        [HttpPost]
        public bool UpdateCategory(Categories categoryToUpdate)
        {
            try
            {
                // Validación de rol
                var token = Request.Headers.Authorization?.Parameter;
                if (string.IsNullOrEmpty(token))
                {
                    return false; // Token no proporcionado
                }

                var principal = JwtService.ValidateToken(token);
                if (principal == null)
                {
                    return false; // Token inválido
                }

                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "rol");
                if (roleClaim == null || (roleClaim.Value != AdminRole && roleClaim.Value != UserRole))
                {
                    return false; // Solo Admin y User pueden actualizar
                }

                var categoryLogic = new CategoryLogic();
                var result = categoryLogic.Update(categoryToUpdate);
                return result; // Retorna el resultado de la actualización
            }
            catch (Exception)
            {
                return false; // En caso de error, retorna false
            }
        }

        [HttpDelete]
        [Route("api/Sales/DeleteCategory/{categoryID}")]
        public bool DeleteCategory(int categoryID)
        {
            try
            {
                // Validación de rol
                var token = Request.Headers.Authorization?.Parameter;
                if (string.IsNullOrEmpty(token))
                {
                    return false; // Token no proporcionado
                }

                var principal = JwtService.ValidateToken(token);
                if (principal == null)
                {
                    return false; // Token inválido
                }

                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "rol");
                if (roleClaim == null || (roleClaim.Value != AdminRole && roleClaim.Value != UserRole))
                {
                    return false; // Solo Admin y User pueden eliminar
                }

                var categoryLogic = new CategoryLogic();
                var result = categoryLogic.Delete(categoryID);
                return result; // Retorna el resultado de la eliminación
            }
            catch (Exception)
            {
                return false; // En caso de error, retorna false
            }
        }

        // Métodos para Productos

        [HttpPost]
        public Products CreateProduct(Products newProduct)
        {
            try
            {
                // Validación de rol
                var token = Request.Headers.Authorization?.Parameter;
                if (string.IsNullOrEmpty(token))
                {
                    return null; // Token no proporcionado
                }

                var principal = JwtService.ValidateToken(token);
                if (principal == null)
                {
                    return null; // Token inválido
                }

                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "rol");
                if (roleClaim == null || (roleClaim.Value != AdminRole && roleClaim.Value != UserRole))
                {
                    return null; // Solo Admin y User pueden crear productos
                }

                var productLogic = new ProductLogic();
                var product = productLogic.Create(newProduct);
                return product; // Retorna el producto creado
            }
            catch (Exception)
            {
                return null; // Si hay un error, devolver null
            }
        }

        // Metodo para obtener todos los productos
        [HttpGet]
        public List<Products> RetrieveAllProducts()
        {
            // 1. Extraer el token de la solicitud
            var token = Request.Headers.Authorization?.Parameter;
            if (string.IsNullOrEmpty(token))
            {
                return null; // Si no hay token, devolver null
            }

            // 2. Validar el token
            var principal = JwtService.ValidateToken(token);
            if (principal == null)
            {
                return null; // Si el token no es válido, devolver null
            }

            // 3. Verificar el rol del usuario
            var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "rol");
            if (roleClaim == null || (roleClaim.Value != AdminRole && roleClaim.Value != UserRole && roleClaim.Value != ViewerRole))
            {
                return null; // Si el rol no es válido o no tiene permisos para realizar la acción, devolver null
            }

            try
            {
                var productLogic = new ProductLogic();
                var products = productLogic.RetrieveAllProducts();
                return products; // Devuelve todos los productos
            }
            catch (Exception)
            {
                return null; // En caso de error, retorna null
            }
        }

        // Metodo para obtener un producto por ID
        [HttpGet]
        public Products RetrieveProductID(int ID)
        {
            // 1. Extraer el token de la solicitud
            var token = Request.Headers.Authorization?.Parameter;
            if (string.IsNullOrEmpty(token))
            {
                return null; // Si no hay token, devolver null
            }

            // 2. Validar el token
            var principal = JwtService.ValidateToken(token);
            if (principal == null)
            {
                return null; // Si el token no es válido, devolver null
            }

            // 3. Verificar el rol del usuario
            var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "rol");
            if (roleClaim == null || (roleClaim.Value != AdminRole && roleClaim.Value != UserRole && roleClaim.Value != ViewerRole))
            {
                return null; // Si el rol no es válido o no tiene permisos para realizar la acción, devolver null
            }

            try
            {
                var productLogic = new ProductLogic();
                var result = productLogic.RetrieveByID(ID);
                return result; // Devuelve el producto por ID
            }
            catch (Exception)
            {
                return null; // En caso de error, retorna null
            }
        }

        [HttpPost]
        public bool UpdateProduct(Products productToUpdate)
        {
            try
            {
                // Validación de rol
                var token = Request.Headers.Authorization?.Parameter;
                if (string.IsNullOrEmpty(token))
                {
                    return false; // Token no proporcionado
                }

                var principal = JwtService.ValidateToken(token);
                if (principal == null)
                {
                    return false; // Token inválido
                }

                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "rol");
                if (roleClaim == null || (roleClaim.Value != AdminRole && roleClaim.Value != UserRole))
                {
                    return false; // Solo Admin y User pueden actualizar productos
                }

                var productLogic = new ProductLogic();
                var result = productLogic.Update(productToUpdate);
                return result; // Retorna el resultado de la actualización
            }
            catch (Exception)
            {
                return false; // En caso de error, retorna false
            }
        }

        [HttpDelete]
        public bool DeleteProduct(int ID)
        {
            try
            {
                // Validación de rol
                var token = Request.Headers.Authorization?.Parameter;
                if (string.IsNullOrEmpty(token))
                {
                    return false; // Token no proporcionado
                }

                var principal = JwtService.ValidateToken(token);
                if (principal == null)
                {
                    return false; // Token inválido
                }

                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "rol");
                if (roleClaim == null || (roleClaim.Value != AdminRole && roleClaim.Value != UserRole))
                {
                    return false; // Solo Admin y User pueden eliminar productos
                }

                var productLogic = new ProductLogic();
                var result = productLogic.Delete(ID);
                return result; // Retorna el resultado de la eliminación
            }
            catch (Exception)
            {
                return false; // En caso de error, retorna false
            }
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
