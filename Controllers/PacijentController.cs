using Dom_zdravlja.Models;
using Dom_zdravlja.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dom_zdravlja.Controllers
{
    public class PacijentController : Controller
    {
        private IJsonFileService<Pacijent> _pacijentService;
        private IJsonFileService<Lekar> _lekarService;
        private IJsonFileService<Termin> _terminService;

        public PacijentController()
        {
            _pacijentService = new JsonFileService<Pacijent>("~/App_Data/Pacijenti.json");
            _lekarService = new JsonFileService<Lekar>("~/App_Data/Lekari.json");
            _terminService = new JsonFileService<Termin>("~/App_Data/Termini.json");
        }

        public ActionResult Index()
        {
            var pacijent = Session["User"] as Pacijent;
            if (pacijent == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(pacijent.ZakazaniTermini);
        }

        public ActionResult IzborLekara()
        {
            var lekari = _lekarService.Read();
            return View(lekari);
        }

        [HttpPost]
        public ActionResult IzborTermina(string lekarKorisnickoIme)
        {
            var lekari = _lekarService.Read();
            var lekar = lekari.FirstOrDefault(l => l.KorisnickoIme == lekarKorisnickoIme);

            if (lekar == null)
            {
                return HttpNotFound("Lekar nije pronađen.");
            }

            return View(lekar);
        }

        [HttpPost]
        public ActionResult ZakaziTermin(string lekarKorisnickoIme, string datumVreme)
        {
            var pacijent = Session["User"] as Pacijent;
            if (pacijent == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var lekari = _lekarService.Read();
            var lekar = lekari.FirstOrDefault(l => l.KorisnickoIme == lekarKorisnickoIme);
            if (lekar != null)
            {
                var termin = lekar.Termini.FirstOrDefault(t => t.DatumVreme.ToString("dd'/'MM'/'yyyy HH:mm") == datumVreme && t.Status == "slobodan");
                if (termin != null)
                {
                    termin.Status = "zakazan";
                    termin.KorisnickoImePacijenta = pacijent.KorisnickoIme;
                    termin.Pacijent = pacijent;
                    pacijent.ZakazaniTermini.Add(termin);

                    if (!lekar.Pacijenti.Any(p => p.KorisnickoIme == pacijent.KorisnickoIme))
                    {
                        lekar.Pacijenti.Add(pacijent);
                    }

                    _pacijentService.Update(pacijent, p => p.KorisnickoIme == pacijent.KorisnickoIme);
                    _lekarService.Update(lekar, l => l.KorisnickoIme == lekar.KorisnickoIme);

                    return RedirectToAction("Index");
                }
            }

            ViewBag.ErrorMessage = "Termin nije dostupan.";
            return View("Index");
        }

        public ActionResult PregledTerapija()
        {
            var pacijent = Session["User"] as Pacijent;
            var terapije = pacijent.Terapije;
            return View(terapije);
        }
    }
}