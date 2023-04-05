using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analiz.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Analiz.Web.Controllers
{
    public class UserController : Controller
    {
        private DataContext context;
        private IKullaniciRepository kullaniciRepository;
        private IConfiguration configuration;
        private IContact contact;
        public UserController(DataContext _context, IConfiguration _configuration, IKullaniciRepository _kullaniciRepository, IContact _contact)
        {
            configuration = _configuration;
            context = _context;
            kullaniciRepository = _kullaniciRepository;
            contact = _contact;
        }

        public IActionResult Index(Kullanici kullanici)
        {
            if (HttpContext.Session.GetString("Oturum") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var liste = context.kullanicis.Where(i => i.Email == HttpContext.Session.GetString("Oturum").ToString()).Single();
                return View(kullaniciRepository.GetById(liste.Id));
            }
        }

        public ActionResult Profile(Kullanici kullanici)
        {
            if (HttpContext.Session.GetString("Oturum") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var liste = context.kullanicis.Where(i => i.Email == HttpContext.Session.GetString("Oturum").ToString()).Single();
                return View(kullaniciRepository.GetById(liste.Id));
            }
        }

        public ActionResult Exit()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        public IActionResult AdsControlMain()
        {
            var liste = context.kullanicis.Where(i => i.Email == HttpContext.Session.GetString("Oturum")).Single();
            DateTime date = DateTime.Now;

           if (date.Subtract(liste.OldLoginDate).TotalHours < 12)
            {
                return RedirectToAction(nameof(Show));
            }
            else
            {
                return RedirectToAction(nameof(Ads));
            }
        }
        public IActionResult AdsControlMain2()
        {
            var liste = context.kullanicis.Where(i => i.Email == HttpContext.Session.GetString("Oturum")).Single();
            DateTime date = DateTime.Now;
             if (date.Subtract(liste.OldAdsWatch).TotalHours < 24)
            {
                return RedirectToAction(nameof(Show2));
            }
            else
            {
                return RedirectToAction(nameof(Ads2));
            }
        }
        public ActionResult Ads()
        {

            if (HttpContext.Session.GetString("Oturum") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else 
            {
                return View();
            }
        }
        
        public ActionResult Contact()
        {
            if (HttpContext.Session.GetString("Oturum") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {                              
                return View();
            }

            }
        [HttpGet]
        public ActionResult Contact2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact2(Contact contact2)
        {
            contact2.KullaniciEmail = HttpContext.Session.GetString("Oturum");
            contact2.Cozum = false;


            context.contacts.Add(contact2);
            context.SaveChanges();
            return RedirectToAction(nameof(Contact));
        }
        public ActionResult Ads2()
        {
            if (HttpContext.Session.GetString("Oturum") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Show(Kullanici kullanici, Models.Analiz analiz)
        {
            if (HttpContext.Session.GetString("Oturum") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var liste = context.analizs.Where(i => i.Id > 0).Count();
                Random random = new Random();
                int a = random.Next(1, liste);
                var custom = context.kullanicis.Where(i => i.Email == HttpContext.Session.GetString("Oturum")).Single();
                var analiz2 = context.analizs.Where(i => i.Id == a).Single();
                DateTime date = DateTime.Now;

                kullanici.Id = custom.Id;
                kullanici.Adi = custom.Adi;
                kullanici.Email = custom.Email;
                kullanici.Soyadi = custom.Soyadi;
                kullanici.OldLoginDate = custom.OldLoginDate;
                kullanici.Parola = custom.Parola;
                kullanici.Validation = custom.Validation;
                kullanici.DogumTarihi = custom.DogumTarihi;
                kullanici.Burc = custom.Burc;
                if (date.Subtract(custom.OldLoginDate).TotalHours> 12)
                {
                    analiz.Id = analiz2.Id;
                    analiz.Kategori = analiz2.Kategori;
                    analiz.Aciklama = analiz2.Aciklama;
                    custom.OldLoginDate = date;
                    kullanici.OldLoginDate = date;
                    context.SaveChanges();
                    return View(Tuple.Create(kullanici, analiz));
                }
                else
                {
                    analiz.Aciklama = "12 saat içerisinde sadece bir analiz hakkınız bulunmaktadır. " +
                        "Bir adet reklam izleyerek yeni analiz hakkı alabilirsiniz. Reklam izlemek ister misiniz? ";
                    return View("ShowError", Tuple.Create(kullanici, analiz));
                }
            }          
        }
        public ActionResult Show2(Kullanici kullanici, Models.Analiz analiz)
        {
            DateTime date = DateTime.Now;
            var custom = context.kullanicis.Where(i => i.Email == HttpContext.Session.GetString("Oturum")).Single();
            if (HttpContext.Session.GetString("Oturum") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if(date.Subtract(custom.OldAdsWatch).TotalHours <24)
            {
                kullanici = context.kullanicis.Where(i => i.Email == HttpContext.Session.GetString("Oturum")).Single();
                analiz.Aciklama = "24 saat içerisinde sadece 1 adet reklam izleyerek ekstra analiz yapabilirsiniz. Günlük reklam izleme hakkınızı doldurduğunuz için şuan analiz yapamıyoruz. Lütfen sürenizin dolmasını bekleyin. Anlayışınız için teşekkür ederiz. Bir sonraki analizde görüşmek üzere.  ";
                return View("ShowError2", Tuple.Create(kullanici, analiz));
            }
            else
            {
                var liste = context.analizs.Where(i => i.Id > 0).Count();
                Random random = new Random();
                int a = random.Next(1, liste);
                var custom2 = context.kullanicis.Where(i => i.Email == HttpContext.Session.GetString("Oturum")).Single();
                var analiz2 = context.analizs.Where(i => i.Id == a).Single();
                

                kullanici.Id = custom.Id;
                kullanici.Adi = custom.Adi;
                kullanici.Email = custom.Email;
                kullanici.Soyadi = custom.Soyadi;
                kullanici.Parola = custom.Parola;
                kullanici.Validation = custom.Validation;
                kullanici.DogumTarihi = custom.DogumTarihi;
                kullanici.Burc = custom.Burc;
                kullanici.OldAdsWatch = date;
                analiz.Id = analiz2.Id;
                analiz.Kategori = analiz2.Kategori;
                analiz.Aciklama = analiz2.Aciklama;
                custom.OldAdsWatch = date;
                context.SaveChanges();
                return View(Tuple.Create(kullanici, analiz));

            }
        }
            public ActionResult ShowError()
        {
            return View();
        }
    }
}