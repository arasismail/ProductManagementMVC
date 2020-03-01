using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductManagementMVC.Models.EntityFramework;
using ProductManagementMVC.ViewMoldes;
using System.Data.Entity;



namespace ProductManagementMVC.Controllers
{

    
    public class UrunController : Controller
    {
        ProductManagementDbEntities db = new ProductManagementDbEntities();
        public ActionResult Index()
        {
            var model = db.Urun.Include(x => x.Kategori).ToList();
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "A")]
        public ActionResult Kaydet()
        {
            var model1 = new UrunFormViewMoleds()
            {
                Kategoriler = db.Kategori.ToList(),
                Urun=new Urun()
            };
            return View("UrunForm",model1);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Urun urun)
        {
            if (!ModelState.IsValid)
            {
                var model = new UrunFormViewMoleds()
                {
                    Kategoriler = db.Kategori.ToList(),
                    Urun = urun
                };
                return View("UrunForm",model);
            }
            if (urun.UrunId == 0) 
            {
                db.Urun.Add(urun);
            }
            else
            {
                db.Entry(urun).State = System.Data.Entity.EntityState.Modified;
            }
            
            db.SaveChanges();
           return RedirectToAction("Index","Urun");
        }
        public ActionResult Guncelle(int id)
        {
            var model = db.Urun.Find(id);
            if (model == null)
                return HttpNotFound();
            var model2 = new UrunFormViewMoleds()
            {
                Kategoriler = db.Kategori.ToList(),
                Urun = model
            };
            return View("UrunForm",model2);
        }
        public ActionResult Sil(int id)
        {
            var silinecekUrun = db.Urun.Find(id);
            if (silinecekUrun == null)
                return HttpNotFound();
            db.Urun.Remove(silinecekUrun);
            db.SaveChanges();
            return RedirectToAction("Index", "Urun");
        }
    }
}