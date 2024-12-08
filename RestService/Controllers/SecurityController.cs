using BLL;
using Entities;
using SecurityLayer;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestService.Controllers
{
    public class SecurityController : ApiController, ISecurityService
    {
        public const string AdminRole = "1"; // Solo el rol "1" (Administrador) tiene acceso

        // Validación del Token y Rol
        private bool IsAdmin(HttpRequestMessage request)
        {
            var token = request.Headers.Authorization?.Parameter;
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var principal = JwtService.ValidateToken(token);
            if (principal == null)
            {
                return false;
            }

            var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "rol");
            return roleClaim != null && roleClaim.Value == AdminRole;
        }

        // Método para crear un nuevo rol
        [HttpPost]
        public Roles CreateRole(Roles newRole)
        {
            if (!IsAdmin(Request))
            {
                return null; // Solo el administrador puede crear roles
            }

            var rolesLogic = new RolesLogic();
            return rolesLogic.Create(newRole);
        }

        // Método para obtener todos los roles
        [HttpGet]
        public List<Roles> RetrieveAllRoles()
        {
            if (!IsAdmin(Request))
            {
                return null; // Solo el administrador puede obtener todos los roles
            }

            var rolesLogic = new RolesLogic();
            return rolesLogic.RetrieveAllRoles();
        }

        // Método para obtener un rol por ID
        [HttpGet]
        public Roles RetrieveRoleID(int ID)
        {
            if (!IsAdmin(Request))
            {
                return null; // Solo el administrador puede obtener un rol por ID
            }

            var rolesLogic = new RolesLogic();
            return rolesLogic.RetrieveByID(ID);
        }

        // Método para actualizar un rol
        [HttpPost]
        public bool UpdateRole(Roles roleToUpdate)
        {
            if (!IsAdmin(Request))
            {
                return false; // Solo el administrador puede actualizar roles
            }

            var rolesLogic = new RolesLogic();
            return rolesLogic.Update(roleToUpdate);
        }

        // Método para eliminar un rol
        [HttpDelete]
        public bool DeleteRole(int ID)
        {
            if (!IsAdmin(Request))
            {
                return false; // Solo el administrador puede eliminar roles
            }

            var rolesLogic = new RolesLogic();
            return rolesLogic.Delete(ID);
        }

        // Método para crear un nuevo usuario
        [HttpPost]
        public Usuarios CreateUser(Usuarios newUser)
        {
            if (!IsAdmin(Request))
            {
                return null; // Solo el administrador puede crear usuarios
            }

            var userLogic = new UsuariosLogic();
            return userLogic.Create(newUser);
        }

        // Método para obtener todos los usuarios
        [HttpGet]
        public List<Usuarios> RetrieveAllUsers()
        {
            if (!IsAdmin(Request))
            {
                return null; // Solo el administrador puede obtener todos los usuarios
            }

            var userLogic = new UsuariosLogic();
            return userLogic.RetrieveAllUsers();
        }

        // Método para obtener un usuario por ID
        [HttpGet]
        public Usuarios RetrieveUserID(int userID)
        {
            if (!IsAdmin(Request))
            {
                return null; // Solo el administrador puede obtener un usuario por ID
            }

            var userLogic = new UsuariosLogic();
            return userLogic.RetrieveByID(userID);
        }

        // Método para actualizar un usuario
        [HttpPost]
        public bool UpdateUser(Usuarios userToUpdate)
        {
            if (!IsAdmin(Request))
            {
                return false; // Solo el administrador puede actualizar usuarios
            }

            var userLogic = new UsuariosLogic();
            return userLogic.Update(userToUpdate);
        }

        // Método para eliminar un usuario
        [HttpDelete]
        public bool DeleteUser(int userID)
        {
            if (!IsAdmin(Request))
            {
                return false; // Solo el administrador puede eliminar usuarios
            }

            var userLogic = new UsuariosLogic();
            return userLogic.Delete(userID);
        }
    }
}
