using Analiz.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public interface IKullaniciRepository
    {
        IQueryable<Kullanici> Kullanicis { get; }
        Kullanici GetById(int KullaniciId);
        IEnumerable<Kullanici> GetKullanicis();
        void CreateKullanici(Kullanici newKullanici);
        void UpdateKullanici(Kullanici updatedKullanici);
        void DeleteKullanici(int KullaniciId);
    }
}
