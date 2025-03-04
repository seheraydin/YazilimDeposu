﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using web_projesi.Models.DataContext;
using PagedList;
using PagedList.Mvc;
using web_projesi.Models.Model;

namespace web_projesi.Controllers
{
    public class HomeController : Controller
    {
        private YazilimDeposuDBContext db = new YazilimDeposuDBContext();
        // GET: Home
        public ActionResult Index()
        {

            ViewBag.Hizmetler = db.Hizmet.ToList();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Kategori = db.Kategori.ToList().OrderByDescending(x => x.KategoriId);
            ViewBag.Kimlik = db.Kimlik.ToList();

            return View();
        }

        public ActionResult SliderPartial() //Slider tablosundaki menüleri getir.
        {

            //slider en son eklenen resmi önce göster
            return View(db.Slider.ToList());
        }
        public ActionResult HizmetPartial()//Hizmetler tablosundaki menüleri getir.
        {
            return View(db.Hizmet.ToList());
        }

        public ActionResult Hakkimizda()
        {
            ViewBag.Hizmetler = db.Hizmet.ToList();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Kategori = db.Kategori.ToList().OrderByDescending(x => x.KategoriId);
            ViewBag.Kimlik = db.Kimlik.ToList();
            return View(db.Hakkimizda.SingleOrDefault());
        }
        public ActionResult Hizmetlerimiz()
        {
            ViewBag.Hizmetler = db.Hizmet.ToList();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Kategori = db.Kategori.ToList().OrderByDescending(x => x.KategoriId);
            ViewBag.Kimlik = db.Kimlik.ToList();
            return View(db.Hizmet.ToList());
        }
        public ActionResult Iletisim()
        {
            ViewBag.Hizmetler = db.Hizmet.ToList();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Kategori = db.Kategori.ToList().OrderByDescending(x => x.KategoriId);
            ViewBag.Kimlik = db.Kimlik.ToList();
            return View(db.Iletisim.SingleOrDefault());
        }
        public ActionResult Hackathon()
        {
            ViewBag.Hizmetler = db.Hizmet.ToList();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Kategori = db.Kategori.ToList().OrderByDescending(x => x.KategoriId);
            ViewBag.Kimlik = db.Kimlik.ToList();
            return View();
        }
        public ActionResult DuyuruBasvuruları()
        {
            ViewBag.Hizmetler = db.Hizmet.ToList();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Kategori = db.Kategori.ToList().OrderByDescending(x => x.KategoriId);
            ViewBag.Kimlik = db.Kimlik.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult DuyuruBasvuruları(string AdSoyad=null,string email=null, string Konu=null,string Mesaj=null,string Number=null)
        {
            if (AdSoyad != null && email != null && Konu !=null && Mesaj != null && Number !=null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "yazilimdeposu09@gmail.com";
                WebMail.Password = "YazilimDeposu0935";
                WebMail.SmtpPort = 587;
                WebMail.Send("yazilimdeposu09@gmail.com", Konu , email + "   " + Mesaj);
           

            }
          
            ViewBag.Hizmetler = db.Hizmet.ToList();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Kategori = db.Kategori.ToList().OrderByDescending(x => x.KategoriId);
            ViewBag.Kimlik = db.Kimlik.ToList();
            return View();
        }

        public ActionResult Blog(int Page = 1)
        {
            ViewBag.Hizmetler = db.Hizmet.ToList();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Kategori = db.Kategori.ToList().OrderByDescending(x => x.KategoriId);
            ViewBag.Kimlik = db.Kimlik.ToList();
            return View(db.Blog.Include("Kategori").OrderByDescending(x => x.BlogId).ToPagedList(Page, 4));

        }
        public ActionResult KategoriBlog(int id, int Page = 1)
        {
            ViewBag.Hizmetler = db.Hizmet.ToList();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Kategori = db.Kategori.ToList().OrderByDescending(x => x.KategoriId);
            ViewBag.Kimlik = db.Kimlik.ToList();
            var b = db.Blog.Include("Kategori").OrderByDescending(x=>x.BlogId).Where(x => x.Kategori.KategoriId == id).ToPagedList(Page,4);
            return View(b);      
        }

        public ActionResult BlogDetay(int id)
        {
            ViewBag.Hizmetler = db.Hizmet.ToList();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Kategori = db.Kategori.ToList().OrderByDescending(x => x.KategoriId);
            ViewBag.Kimlik = db.Kimlik.ToList();
            var b = db.Blog.Include("Kategori").Include("Yorums").Where(x => x.BlogId == id).SingleOrDefault();
            return View(b);
        }

        public JsonResult YorumYap(string adsoyad,string eposta,string icerik,int blogId)
        {
            if(icerik == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorum.Add(new Yorum { AdSoyad = adsoyad, Eposta = eposta, Icerik = icerik, BlogId = blogId, Onay = false});
            db.SaveChanges();
            ViewBag.Hizmetler = db.Hizmet.ToList();
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Kategori = db.Kategori.ToList().OrderByDescending(x => x.KategoriId);
            ViewBag.Kimlik = db.Kimlik.ToList();
            return Json(false,JsonRequestBehavior.AllowGet);
        }


        public ActionResult BlogKategoriPartial()
        {
            return PartialView(db.Kategori.Include("Blogs").ToList().OrderBy(x=>x.KategoriAd));
        }

        public ActionResult BlogKayitPartial()
        {
            return PartialView(db.Blog.ToList().OrderBy(x => x.BlogId));
        }
    }
}