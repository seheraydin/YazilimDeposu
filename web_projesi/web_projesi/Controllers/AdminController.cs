﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using web_projesi.Models;
using web_projesi.Models.DataContext;
using web_projesi.Models.Model;

namespace web_projesi.Controllers
{
    public class AdminController : Controller
    {


        YazilimDeposuDBContext db = new YazilimDeposuDBContext();

        // GET: Admin
        [Route("yonetimpaneli")]
        public ActionResult Index()
        {
            ViewBag.BlogSay = db.Blog.Count();
            ViewBag.KategoriSay = db.Kategori.Count();
            ViewBag.HizmetSay = db.Hizmet.Count();
            ViewBag.SiteAciklamaSay = db.Kimlik.Count();
            var sorgu = db.Admin.ToList();
            return View(sorgu);
        }

        //login action 
        [Route("/yonetimpaneli/giris")]
        public ActionResult Login()
        {
            return View();
        }

        //post metodu
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            
            //bize gelen e posta modelden gelen e posta eşleşme kontrolü
            var login = db.Admin.Where(x => x.Eposta == admin.Eposta).SingleOrDefault();
            if (login.Eposta == admin.Eposta && login.Sifre == Crypto.Hash(admin.Sifre, "MD5")) //şifreyi md5 olarak giriş yap
            {
                //oturum değişkeni oluştur-->session
                Session["adminid"] = login.AdminId;
                Session["eposta"] = login.Eposta;
                Session["yetki"] = login.Yetki;
                //Giriş yaptıktan sonra adminin indexine git
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Uyari = "Kullanıcı adı ya da şifre yanlış!";
            return View(admin);

        }
        public ActionResult Logout()
        {
            //oturum sonlandıryetki
            Session["adminid"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");

        }

        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifremiUnuttum(string eposta)
        {
            //girilen epostaya dair db de şifre var mı?
            var mail = db.Admin.Where(x => x.Eposta == eposta).SingleOrDefault();
            if (mail!=null)
            {
                //random şifre üret ve öd5 olarak db ye kaydet
                Random rnd = new Random();
                int yenisifre = rnd.Next();

                Admin admin = new Admin();
                mail.Sifre = Crypto.Hash(Convert.ToString(yenisifre),"MD5");
                db.SaveChanges();

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "yazilimdeposu09@gmail.com";
                WebMail.Password = "YazilimDeposu0935";
                WebMail.SmtpPort = 587;

                //girilen epostaya şifreyi md5 olmadan  mail gönder
                WebMail.Send(eposta, "Admin Panel Giriş Şifreniz", "Şifreniz:" + yenisifre);
                ViewBag.Uyari = "Şifreniz E-posta adresinize iletildi!";


            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu. Tekrar Deneyiniz";
            }
            
            return View();
        }
       
        public ActionResult Adminler()
        {
            return View(db.Admin.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
    
         [HttpPost]
        public ActionResult Create(Admin admin, string sifre, string eposta)
        {
            if (ModelState.IsValid)
            {
                admin.Sifre = Crypto.Hash(sifre, "MD5");
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Adminler");

            }

            return View();
        }
        public ActionResult Edit(int id)
        {
            var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
            return View(a);
        }
        [HttpPost]
        public ActionResult Edit(int id,Admin admin,string sifre,string eposta)
        {
            if (ModelState.IsValid)
            {
                var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
                a.Sifre = Crypto.Hash(sifre, "MD5");
                a.Eposta = admin.Eposta;
                a.Yetki = admin.Yetki;
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            
            return View(admin);//Hata varsa Admin modelini geri çevir
        }
        public ActionResult Delete(int id)
        {
            var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
            if (a != null)
            {
                db.Admin.Remove(a);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            
            return View();
        }
    }
}