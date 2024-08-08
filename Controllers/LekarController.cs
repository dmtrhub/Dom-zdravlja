using Dom_zdravlja.Models;
using Dom_zdravlja.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Dom_zdravlja.Controllers
{
    public class LekarController : Controller
    {
        private IJsonFileService<Lekar> _lekarService;

        public LekarController()
        {
            _lekarService = new JsonFileService<Lekar>("~/App_Data/Lekari.json");
        }

        // GET: Lekar
        public ActionResult Index()
        {
            var lekar = Session["User"] as Lekar;
            if (lekar == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var termini = lekar.Termini;
            return View(termini);
        }

        // GET: Kreiranje termina
        public ActionResult KreirajTermin()
        {
            return View();
        }

        // POST: Kreiranje termina
        [HttpPost]
        public ActionResult KreirajTermin(Termin noviTermin)
        {
            var lekar = Session["User"] as Lekar;
            if (lekar == null)
            {
                return RedirectToAction("Login", "Home");
            }

            // Proverite da li je noviTermin null
            if (noviTermin == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Termin cannot be null");
            }

            noviTermin.Status = "slobodan";
            noviTermin.Lekar = lekar;

            // Proverite da li lekar.Termini nije null
            if (lekar.Termini == null)
            {
                lekar.Termini = new List<Termin>();
            }

            lekar.Termini.Add(noviTermin);

            _lekarService.Update(lekar, l => l.KorisnickoIme == lekar.KorisnickoIme);

            return RedirectToAction("Index");
        }
    }
}