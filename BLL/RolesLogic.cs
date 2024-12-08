using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RolesLogic
    {
        public Roles Create(Roles newRole)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                newRole = r.Create(newRole);
            }
            return newRole;
        }

        public Roles RetrieveByID(int roleID)
        {
            Roles result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Retrieve<Roles>(ro => ro.RoleID == roleID);
            }
            return result;
        }

        public bool Update(Roles roleToUpdate)
        {
            bool result = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Update(roleToUpdate);
            }
            return result;
        }

        public bool Delete(int roleID)
        {
            bool result = false;
            var role = RetrieveByID(roleID);
            if (role != null)
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    result = r.Delete(role);
                }
            }
            return result;
        }

        public List<Roles> RetrieveAllRoles()
        {
            List<Roles> result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.RetrieveAll<Roles>();
            }
            return result;
        }
    }
}
