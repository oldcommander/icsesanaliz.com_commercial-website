using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Analiz.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Analiz.Web.Controllers
{
    public class HomeController : Controller
    {
        private DataContext context;
        private IConfiguration configuration;
        private IMainBlogsRepository mainBlogsRepository;
        public HomeController(DataContext _context, IConfiguration _configuration, IMainBlogsRepository _mainBlogsRepository)
        {
            mainBlogsRepository = _mainBlogsRepository;
            configuration = _configuration;
            context = _context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Kullanici kullanici)
        {
            string con = configuration.GetConnectionString("DataConnection");
            bool b = true;
            var a = context.kullanicis.Where(i => i.Email == kullanici.Email && i.Parola==kullanici.Parola).Count();
            

            if(a==1)
            {
                var query2 = context.kullanicis.Where(i => i.Email == kullanici.Email).Single();
                b = query2.Validation;

               if (b==false)
                {
                    HttpContext.Session.SetString("EmailValidation", kullanici.Email);
                    
                    Random rnd = new Random();
                    int code = rnd.Next(100000, 999999);

                    var liste = context.kullanicis.Where(i => i.Email == kullanici.Email).Single();
                    liste.Code = code;
                    context.SaveChanges();
                    
                    SmtpClient sc = new SmtpClient();
                    sc.Port = 587;
                    sc.Host = "smtp.icsesanaliz.com";
                    sc.EnableSsl = false;
                    sc.Credentials = new NetworkCredential("info@icsesanaliz.com", "Ahmet44.@");

                    // sistem için oluşturulmuş mail hesabından kullanıcının hesabına veritabanındaki parolası iletilecektir.

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("info@icsesanaliz.com", "İçsesanaliz - Email Doğrulama Servisi");
                    mail.To.Add(kullanici.Email);
                    mail.IsBodyHtml = true;
                    mail.Body = "Değerli kullanıcımız, hizmetlerimizden faydalanmanız için Email adresinizi doğrulamanız gerekmektedir. Lütfen size gönderilen kodu kimseyle paylaşmayınız. Email doğrulama kodunuz: " + code + "";
                    mail.Subject = "Email Doğrulama";
                    sc.Send(mail);
                    return RedirectToAction(nameof(Email));
                }
                else
                {
                    HttpContext.Session.SetString("Oturum",kullanici.Email);
                    return RedirectToAction("Index", "User");
                }
                
            }
            else
            {
                ViewBag.Validation = "Kullanıcı Adı veya Parola Hatalı";
                return View();
            }
            
        }
        public IActionResult SıngUp()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SıngUp2()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult SıngUp2(Kullanici kul)
        {
            DateTime date = DateTime.Now;
            kul.Validation = false;
            var a = context.kullanicis.Where(i => i.Email == kul.Email).Count();           
            if (a==1)
            {
                Error e = new Error();
                e.ErrorMesage = "Girdiğiniz email adresi sisteme kayıtlı. Lütfen farklı bir email adresi giriniz";
                ViewBag.Error = e.ErrorMesage.ToString();
                return View("SıngUp");
            }
            else if (date.Subtract(kul.DogumTarihi).TotalDays<4745)
            {
                Error e = new Error();
                e.ErrorMesage = "Koşullarımız gereği sitemize 13 yaşından küçük kullanıcılar üye olamaz.";
                ViewBag.Error2 = e.ErrorMesage.ToString();
                return View("SıngUp");
            }
            else
            {
                kul.Code = 0;
                context.kullanicis.Add(kul);
                context.SaveChanges();
                return View("Login");
            }

        }

        public ActionResult Koşullar()
        {
            return View();
        }
        public ActionResult Email()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Email2()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Email2(EmailCustom email)
        {

            var liste = context.kullanicis.Where(i => i.Email == HttpContext.Session.GetString("EmailValidation")).Single();
             

            if (liste.Code==email.Code)
            {
                liste.Validation = true;
                context.SaveChanges();
                HttpContext.Session.SetString("Oturum", liste.Email);
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.EmailError = "Girdiğiniz doğrulama kodu yanlış. Lütfen kontrol edip tekrar deneyiniz.";
                return View(nameof(Email));
            }
            
        }
        public ActionResult Help()
        {
            return View();
        }
        public ActionResult Forgot()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ForgotEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotEmail(Kullanici kullanici)
        {
            var liste = context.kullanicis.Where(i => i.Email == kullanici.Email).Count();
            if (liste == 1)
            {
                HttpContext.Session.SetString("ForgotUser", kullanici.Email);

                Random rnd = new Random();
                int code = rnd.Next(100000, 999999);
                var liste2 = context.kullanicis.Where(i => i.Email == kullanici.Email).Single();
                liste2.Code = code;
                context.SaveChanges();

                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.icsesanaliz.com";
                sc.EnableSsl = false;
                sc.Credentials = new NetworkCredential("info@icsesanaliz.com", "Ahmet44.@");

                // sistem için oluşturulmuş mail hesabından kullanıcının hesabına veritabanındaki parolası iletilecektir.

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("info@icsesanaliz.com", "İçsesanaliz - Email Doğrulama Servisi");
                mail.To.Add(kullanici.Email);
                mail.IsBodyHtml = true;
                mail.Body = "Değerli kullanıcımız, parolanıza erişmek için email adresinizi doğrulamanız gerekmektedir. Lütfen size gönderilen kodu kimseyle paylaşmayınız. Email doğrulama kodunuz: " + code + "";
                mail.Subject = "Email Doğrulama";
                sc.Send(mail);
                return RedirectToAction(nameof(ForgotVerify));
            }
            else
            {
                ViewBag.ForgotEmailError = "Girdiğiniz email sisteme kayıtlı değil. Lütfen bilgilerinizi kontrol ediniz.";
                return View("Forgot");
            }

        }
        public IActionResult ForgotVerify()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ForgotEmailVerify()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotEmailVerify(EmailCustom email)
        {
            var verify = context.kullanicis.Where(i => i.Email == HttpContext.Session.GetString("ForgotUser")).Single();
            
            if (verify.Code == email.Code)
            {
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.icsesanaliz.com";
                sc.EnableSsl = false;
                sc.Credentials = new NetworkCredential("info@icsesanaliz.com", "Ahmet44.@");

                // sistem için oluşturulmuş mail hesabından kullanıcının hesabına veritabanındaki parolası iletilecektir.

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("info@icsesanaliz.com", "İçsesanaliz - Parola Hatırlatma Servisi");
                mail.To.Add(verify.Email);
                mail.IsBodyHtml = true;
                mail.Body = "Değerli kullanıcımız, lütfen size gönderilen parolanızı kimseyle paylaşmayınız. Parolanız: " + verify.Parola + "";
                mail.Subject = "Parola Hatırlatma";
                sc.Send(mail);
                ViewBag.VerifyMessage = "Parolanız email adresinize gönderilmiştir.";
                return View("Login");
            }
            else
            {
                ViewBag.EmailError = "Girdiğiniz doğrulama kodu yanlış. Lütfen kontrol edip tekrar deneyiniz.";
                return View(nameof(ForgotVerify));
            }
            
        }
        public ActionResult BlogList()
        {
            return View(mainBlogsRepository.GetMainBlog());
        }

        [HttpGet]
        public IActionResult BlogShow()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BlogShow(int Id)
        {
            MainBlog mainBlog = new MainBlog();
            var liste = context.mainblogs.Where(i => i.Id == Id).Single();
            mainBlog.Id = liste.Id;
            mainBlog.Gorsel = liste.Gorsel;
            mainBlog.Icerik = liste.Icerik;
            mainBlog.Kategori = mainBlog.Kategori;
            mainBlog.Tarih = liste.Tarih;
            mainBlog.Yazar = liste.Yazar;
            mainBlog.Baslik = liste.Baslik;
            mainBlog.Etiket = liste.Etiket;
            return View(mainBlog);
        }

        public ActionResult About()
        {
            return View();
        }

    }
}