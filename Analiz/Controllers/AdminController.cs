using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analiz.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Analiz.Web.Controllers
{
    public class AdminController : Controller
    {
        private DataContext context;
        private IHostingEnvironment env;
        private IKullaniciRepository kullaniciRepository;
        private IAnalizRepository analizRepository;
        private IAdminRepository adminRepository;
        private IContact contact;
        private IMainBlogsRepository mainBlogsRepository;
        private IConfiguration configuration;
        string directory;
        public AdminController(DataContext _context, IHostingEnvironment _env, IMainBlogsRepository _mainBlogsRepository, IContact _contact, IConfiguration _configuration, IKullaniciRepository _kullaniciRepository, IAnalizRepository _analizRepository, IAdminRepository _adminRepository)
        {
            env = _env;
            directory = _env.ContentRootPath + "/Images/";
            adminRepository = _adminRepository;
            kullaniciRepository = _kullaniciRepository;
            analizRepository = _analizRepository;
            configuration = _configuration;
            contact = _contact;
            mainBlogsRepository = _mainBlogsRepository;
            context = _context;
        }
        public IActionResult Index(Admin kullanici)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var liste = context.admins.Where(i => i.Email == HttpContext.Session.GetString("AdminSession").ToString()).Single();
                kullanici.Id = liste.Id;
                kullanici.Adi = liste.Adi;
                kullanici.Email = liste.Email;
                kullanici.Soyadi = liste.Soyadi;
                kullanici.Parola = liste.Parola;
                kullanici.Unvan = liste.Unvan;

                return View(kullanici);
            }
        }
        public ActionResult Custom()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View(kullaniciRepository.GetKullanicis());
            }
            
        }

        public ActionResult Analiz()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View(analizRepository.GetAnalizs());
            }
            
        }

        public ActionResult Yönetici()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View(adminRepository.GetAdmins());
            }
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Admin kullanici)
        {
            var a = context.admins.Where(i => i.Email == kullanici.Email && i.Parola == kullanici.Parola).Count();
            
            if (a == 1)
            {
                var b = context.admins.Where(i => i.Email == kullanici.Email).Single();
                HttpContext.Session.SetString("AdminSession", b.Email);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Error = "Kullanıcı Adı veya Parola Hatalı";
                return View();
            }
        }
        public ActionResult Profile(Admin kullanici)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var liste = context.admins.Where(i => i.Email == HttpContext.Session.GetString("AdminSession").ToString()).Single();
                kullanici.Id = liste.Id;
                kullanici.Adi = liste.Adi;
                kullanici.Email = liste.Email;
                kullanici.Soyadi = liste.Soyadi;
                kullanici.Unvan = liste.Unvan;
                kullanici.Parola = liste.Parola;
                return View(kullanici);
            }
        }        
        public ActionResult Exit()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

        public IActionResult EditCustom(int Id)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View(kullaniciRepository.GetById(Id));
            }
            
        }
        [HttpPost]
        public IActionResult EditCustom(Kullanici kullanici)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                kullaniciRepository.UpdateKullanici(kullanici);
                return RedirectToAction(nameof(Custom));
            }

        }
        [HttpPost]
        public IActionResult DeleteCustom(int Id)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                kullaniciRepository.DeleteKullanici(Id);
                return RedirectToAction(nameof(Custom));
            }

        }
        public IActionResult EditAnaliz(int Id)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View(analizRepository.GetById(Id));
            }
            
        }
        [HttpPost]
        public IActionResult EditAnaliz(Analiz.Web.Models.Analiz analiz)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                analizRepository.UpdateAnaliz(analiz);
                return RedirectToAction(nameof(Analiz));
            }

        }
        [HttpPost]
        public IActionResult DeleteAnaliz(int Id)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                analizRepository.DeleteAnaliz(Id);
                return RedirectToAction(nameof(Analiz));
            }

        }
        public IActionResult CreateAnaliz()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }
            
        }
        [HttpGet]
        public IActionResult AddAnaliz()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public IActionResult AddAnaliz(Analiz.Web.Models.Analiz analiz)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                analizRepository.CreateAnaliz(analiz);
                return RedirectToAction(nameof(Analiz));
            }
            
        }
        public IActionResult CreateCustom()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult AddCustom()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public IActionResult AddCustom(Kullanici kullanici)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                kullaniciRepository.CreateKullanici(kullanici);
                return RedirectToAction(nameof(Custom));
            }
            
        }

        public IActionResult EditAdmin(int Id)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View(adminRepository.GetById(Id));
            }
            
        }
        [HttpPost]
        public IActionResult EditAdmin(Admin admin)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                adminRepository.UpdateAdmin(admin);
                return RedirectToAction(nameof(Yönetici));
            }

        }
        [HttpPost]
        public IActionResult DeleteAdmin(int Id)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                adminRepository.DeleteAdmin(Id);
                return RedirectToAction(nameof(Yönetici));
            }

        }
        public IActionResult CreateAdmin()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }
            
        }
        [HttpGet]
        public IActionResult AddAdmin()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public IActionResult AddAdmin(Admin admin)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                adminRepository.CreateAdmin(admin);
                return RedirectToAction(nameof(Yönetici));
            }
            
        }
        public IActionResult CreateMainBlog()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public ActionResult AddMainBlog()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult AddMainBlog(MainBlog2 mainBlog2)
        {
                var exention = Path.GetExtension(mainBlog2.Gorsel.FileName);
                var newimagename = Guid.NewGuid() + exention;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/img/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                mainBlog2.Gorsel.CopyTo(stream);
            MainBlog mainBlog = new MainBlog
            {
                Id = mainBlog2.Id,
                Baslik = mainBlog2.Baslik,
                Kategori = mainBlog2.Kategori,
                Tarih = mainBlog2.Tarih,
                Icerik = mainBlog2.Icerik,
                Yazar = mainBlog2.Yazar,
                Etiket = mainBlog2.Etiket,
                    Gorsel = newimagename

                };
                mainBlogsRepository.CreateMainBlog(mainBlog);
                return View("MainBlog", mainBlogsRepository.GetMainBlog());
            
        }
        public IActionResult Message()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                
                return View(contact.GetContact());
            }
            
        }
        public IActionResult EditMessage(int Id)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View(contact.GetById(Id));
            }

        }
        [HttpPost]
        public IActionResult EditMessage(Contact contact2)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                contact.UpdateContact(contact2);
                return RedirectToAction(nameof(Message));
            }

        }
        [HttpPost]
        public IActionResult DeleteMessage(int Id)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                contact.DeleteContact(Id);
                return RedirectToAction(nameof(Message));
            }

        }
        public IActionResult MainBlog()
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                return View(mainBlogsRepository.GetMainBlog());
            }
        }
        [HttpPost]
        public IActionResult DeleteMainBlog(int Id)
        {
            if (HttpContext.Session.GetString("AdminSession") == null)
            {
                return View("Login");
            }
            else
            {
                mainBlogsRepository.DeleteMainBlog(Id);
                return RedirectToAction(nameof(MainBlog));
            }

        }
    }

    }