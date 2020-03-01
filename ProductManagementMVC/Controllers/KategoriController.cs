using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductManagementMVC.Models.EntityFramework;

namespace ProductManagementMVC.Controllers
{
    public class KategoriController : Controller
    {
        ProductManagementDbEntities db = new ProductManagementDbEntities();
        
        public ActionResult Index()
        {
            var model = db.Kategori.ToList();
            return View(model);
        }
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult Kaydet()
        {   
            return View("KategoriForm", new Kategori());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Kategori model)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriForm");
            }
            if (model.KategoriId == 0) 
            {
                db.Kategori.Add(model);
            }
            else
            {
                var guncellenecekKategori = db.Kategori.Find(model.KategoriId);
                if (guncellenecekKategori == null)
                    return HttpNotFound();
                guncellenecekKategori.KategoriAd = model.KategoriAd;
            }
            db.SaveChanges();
            return RedirectToAction("Index","Kategori");
        }
        public ActionResult Guncelle(int id)
        {
            var model = db.Kategori.Find(id);
            if (model == null)
                return HttpNotFound();
            return View("KategoriForm",model);
        }
        public ActionResult Sil(int id)
        {
            var model = db.Kategori.Find(id);
            if (model == null)
                return HttpNotFound();
            db.Kategori.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult git()
        {
            return View("Index");
        }
    }
}