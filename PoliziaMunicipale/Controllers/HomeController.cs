using PoliziaMunicipale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliziaMunicipale.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.VerbaliByT = DB.getCountVerbaliByTrasgressore();
            ViewBag.PuntiByT = DB.getPuntiByTrasgressore();
            ViewBag.Mag10Punti = DB.getTrasgressoriMag10Punti();
            ViewBag.AmountMag400 = DB.getImportoMag400();
            return View();
        }
    }
}