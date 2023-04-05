using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public interface IAnalizRepository
    {
        IQueryable<Analiz> Analizs { get; }
        Analiz GetById(int AnalizId);
        IEnumerable<Analiz> GetAnalizs();
        void CreateAnaliz(Analiz newAnaliz);
        void UpdateAnaliz(Analiz updatedAnaliz);
        void DeleteAnaliz(int AnalizId);
    }
}
