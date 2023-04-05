using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public class EfContactRepository : IContact
    {
        private DataContext context;
        public EfContactRepository(DataContext _context)
        {
            context = _context;
        }
        public IEnumerable<Contact> Contacts => context.contacts;
        IQueryable<Contact> IContact.Contacts => context.contacts;

        public void CreateContact(Contact newContact)
        {
            context.contacts.Add(newContact);
            context.SaveChanges();
        }

        public void DeleteContact(int AdminId)
        {
            var entity = context.contacts.Find(AdminId);
            context.contacts.Remove(entity);
            context.SaveChanges();
        }

        public Contact GetById(int ContactId)
        {
            return context.contacts.Find(ContactId);
        }

        public IEnumerable<Contact> GetContact()
        {
            return context.contacts.ToList();
        }

        public void UpdateContact(Contact updatedAdmin)
        {
            context.contacts.Update(updatedAdmin);
            context.SaveChanges();
        }
    }
}
