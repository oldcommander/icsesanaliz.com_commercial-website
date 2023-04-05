using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string KullaniciEmail { get; set; }
        public string Kategori { get; set; }
        public string Mesaj { get; set; }
        public bool Cozum { get; set; }
    }
}
