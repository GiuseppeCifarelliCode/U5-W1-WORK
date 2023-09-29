using PoliziaMunicipale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliziaMunicipale.Controllers
{
    public class ViolazioneController : Controller
    {
        // GET: Violazione
        public ActionResult Create()
        {
            ViewBag.AllViolazioni = DB.getAllViolazioni();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Violazione v)
        {
            if (ModelState.IsValid)
            {
                DB.AggiungiViolazione(v.Description);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.AllViolazioni = DB.getAllViolazioni();
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Violazione v = DB.getViolazioneById(id);
            return View(v);
        }
        [HttpPost]
        public ActionResult Edit(Violazione v)
        {
            if(ModelState.IsValid)
            {
                DB.UpdateViolazione(v.Id,v.Description);
                return RedirectToAction("Index","Home");
            } else return View(v);
        }

        public ActionResult Delete(int id)
        {
            DB.RemoveViolazione(id);
            return RedirectToAction("Index", "Home");
        }
    }
}