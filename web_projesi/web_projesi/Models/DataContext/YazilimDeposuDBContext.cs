﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using web_projesi.Models.Model;

namespace web_projesi.Models.DataContext
{
    public class YazilimDeposuDBContext:DbContext
    {
        //ilk yüklendiğinde oluşan tablolar
        public YazilimDeposuDBContext():base("YazilimDeposuWebDB")
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Hizmet> Hizmet { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Kimlik> Kimlik { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Slider> Slider {get; set; }
        public DbSet<Yorum> Yorum { get; set; }

    }
}