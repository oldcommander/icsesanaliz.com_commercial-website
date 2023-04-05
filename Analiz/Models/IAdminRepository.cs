using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public interface IAdminRepository
    {
        IQueryable<Admin> Admins { get; }
        Admin GetById(int AdminId);
        IEnumerable<Admin> GetAdmins();
        void CreateAdmin(Admin newAdmin);
        void UpdateAdmin(Admin updatedAdmin);
        void DeleteAdmin(int AdminId);
    }
}
