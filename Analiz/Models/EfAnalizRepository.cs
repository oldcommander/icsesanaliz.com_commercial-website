using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public class EfAnalizRepository : IAnalizRepository
    {
        DataContext context;
        public EfAnalizRepository(DataContext _context)
        {
            context = _context;
        }
        public IEnumerable<Analiz> Analizs => context.analizs;

        IQueryable<Analiz> IAnalizRepository.Analizs => context.analizs;

        public void CreateAnaliz(Analiz newAnaliz)
        {
            context.analizs.Add(newAnaliz);
            context.SaveChanges();
        }

        public void DeleteAnaliz(int AnalizId)
        {
            var entity = context.analizs.Find(AnalizId);
            context.analizs.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<Analiz> GetAnalizs()
        {
            return context.analizs.ToList();
        }

        public Analiz GetById(int AnalizId)
        {
            return context.analizs.Find(AnalizId);
        }

        public void UpdateAnaliz(Analiz updatedAnaliz)
        {
            context.analizs.Update(updatedAnaliz);
            context.SaveChanges();
        }
    }
}
