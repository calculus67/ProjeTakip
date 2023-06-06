﻿using Microsoft.Ajax.Utilities;
using PROJECTTRACKING.Models.DataContext;
using PROJECTTRACKING.Models.ProjeTakip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJECTTRACKING.Controllers
{
    public class PersonelProjeController : Controller
    {
        private ProjeTakipDBContext db = new ProjeTakipDBContext();
        public ActionResult Index()
        {
            var projelistele = db.personelProjeleris.ToList();
            return View(projelistele);
        }

        public ActionResult Create()
        {
            ViewBag.PersonelBilgileriId = new SelectList(db.PersonelBilgileris, "PersonelBilgileriId", "AdSoyad");
            return View();
        }
        [HttpPost]
        public ActionResult Create(PersonelProjeleri projeObj, int[] PersonelBilgileriId)
        {

            foreach (var x in PersonelBilgileriId)
            {
                projeObj.PersonelBilgileris.Add(db.PersonelBilgileris.Find(x));
            }
            projeObj.OlusturmaTarihi = DateTime.Now;
            db.personelProjeleris.Add(projeObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var projeObj = db.personelProjeleris.Find(id);
            return View(projeObj);
        }
        [HttpPost]


        public ActionResult Edit(PersonelProjeleri projeObj)
        {
            var projeDbObj = db.personelProjeleris.Find(projeObj.PersonelProjeId);
            projeDbObj.ProjeAciklama = projeObj.ProjeAciklama;
            projeDbObj.ProjeBaslik = projeObj.ProjeBaslik;
            projeDbObj.TamamlanmaOrani = projeObj.TamamlanmaOrani;
            projeDbObj.OncelikDurumu = projeObj.OncelikDurumu;
            projeDbObj.TamamlanmaTarihi = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Tamamla(int id)
        {
            var projeObj = db.personelProjeleris.Find(id);
            projeObj.TamamlanmaDurumu = true;
            projeObj.TamamlanmaOrani = 100;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}