using PoliziaMunicipale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliziaMunicipale.Controllers
{
    public class VerbaleController : Controller
    {
        // GET: Verbale
        public List<Trasgressore> t = DB.getAllTrasgressori();
        public List<Violazione> v = DB.getAllViolazioni();
        public List<SelectListItem> trasgressori
        {
            get
            {
                List<SelectListItem> listT = new List<SelectListItem>();
                foreach (Trasgressore tr in t)
                {
                    SelectListItem item = new SelectListItem { Text= tr.Surname +" "+ tr.Name, Value = tr.Id.ToString()};
                    listT.Add(item);
                }
                return listT;
            }
        }
        public List<SelectListItem> violazioni
        {
            get
            {
                List<SelectListItem> listV = new List<SelectListItem>();
                foreach (Violazione vi in v)
                {
                    SelectListItem item = new SelectListItem { Text = vi.Description, Value = vi.Id.ToString() };
                    listV.Add(item);
                }
                return listV;
            }
        }
        public ActionResult Create()
        {
            ViewBag.ListaTrasgressori = trasgressori;
            ViewBag.ListaViolazioni = violazioni;
            ViewBag.ListaVerbali = DB.getAllVerbali();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Verbale v)
        {
            if (ModelState.IsValid)
            {
                DB.AggiungiVerbale(v.DataViolazione, v.IndirizzoViolazione, v.Agente, v.DataVerbale, v.Importo, v.PuntiTolti, v.IdTrasgressore, v.IdViolazione);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ListaTrasgressori = trasgressori;
                ViewBag.ListaViolazioni = violazioni;
                ViewBag.ListaVerbali = DB.getAllVerbali();
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ListaTrasgressori = trasgressori;
            ViewBag.ListaViolazioni = violazioni;
            Verbale v = DB.getVerbaleById(id);
            return View(v);
        }

        [HttpPost]
        public ActionResult Edit(Verbale v)
        {
            if (ModelState.IsValid)
            {
                DB.UpdateVerbale(v.Id, v.DataViolazione, v.IndirizzoViolazione, v.Agente, v.DataVerbale, v.Importo, v.PuntiTolti, v.IdTrasgressore, v.IdViolazione);
                return RedirectToAction("Index", "Home");
            } else return View(v); 
        }

        public ActionResult Remove(int id)
        {
            DB.RemoveVerbale(id); 
            return RedirectToAction("Index","Home");
        }
    }
}