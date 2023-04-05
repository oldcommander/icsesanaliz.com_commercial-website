using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
   public interface IContact
    {
        IQueryable<Contact> Contacts{ get; }
        Contact GetById(int ContactId);
        IEnumerable<Contact> GetContact();
        void CreateContact(Contact newContact);
        void UpdateContact(Contact updatedAdmin);
        void DeleteContact(int AdminId);
    }
}
