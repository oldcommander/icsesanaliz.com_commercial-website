using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Email { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Burc { get; set; }
        public string Parola { get; set; }
        public bool Validation { get; set; }
        public string Cinsiyet { get; set; }
        public string Hobi { get; set; }
        public string Egitim { get; set; }
        public string Muzik { get; set; }
        public bool AdsWatch { get; set; }
        public DateTime OldAdsWatch { get; set; }
        public DateTime OldLoginDate { get; set; }

        public int Code { get; set; }
    }
}
