using Microsoft.Ajax.Utilities;
using PoliziaMunicipale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliziaMunicipale.Controllers
{
    public class TrasgressoreController : Controller
    {
        // GET: Trasgressore
       public ActionResult Create()
        {
            ViewBag.AllTrasgressori = DB.getAllTrasgressori();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Trasgressore t)
        {
            if (ModelState.IsValid)
            {
                DB.AggiungiTrasgressore(t.Surname, t.Name, t.Address, t.City, t.CAP, t.CF);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.AllTrasgressori = DB.getAllTrasgressori();
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Trasgressore t = DB.getTrasgressoreById(id);
            return View(t);
        }
        [HttpPost]
        public ActionResult Edit(Trasgressore t)
        {
            if (ModelState.IsValid)
            {
                DB.UpdateTrasgressore(t.Id, t.Surname, t.Name, t.Address, t.City, t.CAP, t.CF);
                return RedirectToAction("Index", "Home");
            }
            else return View(t);
        }

        public ActionResult Delete(int id)
        {
            DB.RemoveTrasgressore(id); 
            return RedirectToAction("Index","Home");
        }

    }
}