using JqueryAjaxApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JqueryAjaxApp.Models.ViewModels;
using System.Text.Json;

namespace JqueryAjaxApp.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly OkulDbContext ctx;

        public OgrenciController(OkulDbContext context)
        {
            ctx = context;
        }

        public ViewResult Index()//Action
        {
            return View("AnaSayfa");
        }


        [HttpGet]
        public IActionResult OgrenciDetay(int id)
        {
            var ogr = ctx.Ogrenciler.Find(id);
            return View(ogr);
        }

        [HttpPost]
        public IActionResult OgrenciDetay(Ogrenci ogr)
        {
            ctx.Entry(ogr).State = EntityState.Modified;
            ctx.SaveChanges();
            return RedirectToAction("OgrenciListe");
        }

        public ViewResult OgrenciListe()
        {
           
            var lst = ctx.Ogrenciler.ToList();
            return View(lst);

        }


        [HttpGet]
        public ViewResult OgrenciEkle()
        {
            return View();
        }

        
        [HttpPost]
        public ViewResult OgrenciEkle(Ogrenci ogr)
        {
            int sonuc = 0;
            if (ogr != null)
            {

                ctx.Ogrenciler.Add(ogr);
                sonuc = ctx.SaveChanges();

            }

            if (sonuc > 0)
            {
                TempData["sonuc"] = true;
            }
            else
            {
                TempData["sonuc"] = false;
            }
            return View();
        }

        public IActionResult OgrenciSil(int id)
        {
            var ogr = ctx.Ogrenciler.Find(id);
            ctx.Ogrenciler.Remove(ogr);
            ctx.SaveChanges();

            return RedirectToAction(nameof(OgrenciListe));
        }

       
        [HttpGet]
        public IActionResult OgrenciListeAjax()
        {
            var ogr = ctx.Ogrenciler.ToList();
            return Json(ogr);

        }

        [HttpGet]
        public IActionResult OgrenciGetirAjax(int id)
        {
            var ogr = ctx.Ogrenciler.Find(id);
            if (ogr == null)
            {
                return NotFound();
            }
            return Json(ogr);

        }

    
        [HttpPost]
        public IActionResult OgrenciEkleAjax([FromBody] Ogrenci ogr)
        {
            if (ogr == null)
            {
                return BadRequest();
            }

            ctx.Ogrenciler.Add(ogr);
            int sonuc = ctx.SaveChanges();
            if (sonuc > 0)
            {
                return Json(new { success = true, message = "Öğrenci başarıyla eklendi.", data = ogr });
            }
            else
            {
                return Json(new { success = false, message = "Öğrenci eklenemedi." });
            }

        }

    
        [HttpPost]
        public IActionResult OgrenciGuncelleAjax([FromBody] Ogrenci ogr)
        {
            if (ogr == null)
            {
                return BadRequest();
            }

            var mevcutOgrenci = ctx.Ogrenciler.Find(ogr.OgrenciId);
            if (mevcutOgrenci == null)
            {
                return NotFound();
            }

            mevcutOgrenci.Ad = ogr.Ad;
            mevcutOgrenci.Soyad = ogr.Soyad;

            ctx.Entry(mevcutOgrenci).State = EntityState.Modified;
            int sonuc = ctx.SaveChanges();

            if (sonuc > 0)
            {
                return Json(new { success = true, message = "Öğrenci başarıyla güncellendi.", data = mevcutOgrenci });
            }
            else
            {
                return Json(new { success = false, message = "Öğrenci güncellenemedi." });
            }
        }



        [HttpPost]
        public IActionResult OgrenciSilAjax([FromBody] int id)
        {

            var ogr = ctx.Ogrenciler.Find(id);
            if (ogr == null)
            {
                return NotFound();
            }

            ctx.Ogrenciler.Remove(ogr);
            int sonuc = ctx.SaveChanges();

            if (sonuc > 0)
            {
                return Json(new { success = true, message = "Öğrenci başarıyla silindi." });
            }
            else
            {
                return Json(new { success = false, message = "Öğrenci silinemedi." });
            }

        }
    }
}
