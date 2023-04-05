using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public class EfAdminRepository : IAdminRepository
    {
        private DataContext context;
        public EfAdminRepository(DataContext _context)
        {
            context = _context;
        }

        public IEnumerable<Admin> Admins => context.admins;
        IQueryable<Admin> IAdminRepository.Admins => context.admins;

        public void CreateAdmin(Admin newAdmin)
        {
            context.admins.Add(newAdmin);
            context.SaveChanges();
        }

        public void DeleteAdmin(int AdminId)
        {
            var entity = context.admins.Find(AdminId);
            context.admins.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<Admin> GetAdmins()
        {
            return context.admins.ToList();
        }

        public Admin GetById(int AdminId)
        {
            return context.admins.Find(AdminId);
        }

        public void UpdateAdmin(Admin updatedAdmin)
        {
            context.admins.Update(updatedAdmin);
            context.SaveChanges();
        }
    }
}
