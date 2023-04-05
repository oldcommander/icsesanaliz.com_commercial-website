using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public class EfKullaniciRepository : IKullaniciRepository
    {
        private DataContext context;
        public EfKullaniciRepository(DataContext _context)
        {
            context = _context;
        }
        public IEnumerable<Kullanici> Kullanicis => context.kullanicis;

        IQueryable<Kullanici> IKullaniciRepository.Kullanicis => context.kullanicis;

        public void CreateKullanici(Kullanici newKullanici)
        {
            context.kullanicis.Add(newKullanici);
            context.SaveChanges();
        }

        public void DeleteKullanici(int KullaniciId)
        {
            var entity = context.kullanicis.Find(KullaniciId);
            context.kullanicis.Remove(entity);
            context.SaveChanges();
        }

        public Kullanici GetById(int KullaniciId)
        {
            return context.kullanicis.Find(KullaniciId);
        }

        public IEnumerable<Kullanici> GetKullanicis()
        {
            return context.kullanicis.ToList();
        }

        public void UpdateKullanici(Kullanici updatedKullanici)
        {
            context.kullanicis.Update(updatedKullanici);
            context.SaveChanges();
        }
    }
}
