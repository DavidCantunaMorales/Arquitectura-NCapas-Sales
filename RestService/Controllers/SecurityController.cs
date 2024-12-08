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
    public class SecurityController : ApiController, ISecurityService
    {
        [HttpPost]
        public Roles CreateRole(Roles newRole)
        {
            var rolesLogic = new RolesLogic();
            var roles = rolesLogic.Create(newRole);
            return roles;
        }

        [HttpGet]
        public List<Roles> RetrieveAllRoles()
        {
            var rolesLogic = new RolesLogic();
            var roles = rolesLogic.RetrieveAllRoles();
            return roles;
        }

        [HttpGet]
        public Roles RetrieveRoleID(int ID)
        {
            var rolesLogic = new RolesLogic();
            var roles = rolesLogic.RetrieveByID(ID);
            return roles;
        }

        [HttpPost]
        public bool UpdateRole(Roles roleToUpdate)
        {
            var rolesLogic = new RolesLogic();
            var roles = rolesLogic.Update(roleToUpdate);
            return roles;
        }

        [HttpDelete]
        public bool DeleteRole(int ID)
        {
            var rolesLogic = new RolesLogic();
            var roles = rolesLogic.Delete(ID);
            return roles;
        }


        [HttpPost]
        public Usuarios CreateUser(Usuarios newUser)
        {
            var userLogic = new UsuariosLogic();
            var user = userLogic.Create(newUser);
            return user;
        }

        [HttpGet]
        public List<Usuarios> RetrieveAllUsers()
        {
            var userLogic = new UsuariosLogic();
            var user = userLogic.RetrieveAllUsers();
            return user;
        }

        [HttpGet]
        public Usuarios RetrieveUserID(int userID)
        {
            var userLogic = new UsuariosLogic();
            var user = userLogic.RetrieveByID(userID);
            return user;
        }

        [HttpPost]
        public bool UpdateUser(Usuarios userToUpdate)
        {
            var userLogic = new UsuariosLogic();
            var user = userLogic.Update(userToUpdate);
            return user;
        }

        [HttpDelete]
        public bool DeleteUser(int userID)
        {
            var userLogic = new UsuariosLogic();
            var user = userLogic.Delete(userID);
            return user;
        }
    }
}
