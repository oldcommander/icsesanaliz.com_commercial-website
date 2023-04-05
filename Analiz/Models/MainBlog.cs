using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public class MainBlog
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Kategori { get; set; }
        public DateTime Tarih { get; set; }
        public string Icerik { get; set; }
        public string Yazar { get; set; }
        public string Etiket { get; set; }
        public string Gorsel { get; set; }
    }
}
