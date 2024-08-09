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
        private IJsonFileService<Pacijent> _pacijentService;

        public LekarController()
        {
            _lekarService = new JsonFileService<Lekar>("~/App_Data/Lekari.json");
            _pacijentService = new JsonFileService<Pacijent>("~/App_Data/Pacijenti.json");
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

            foreach (var termin in termini)
            {
                if (termin.Pacijent != null && !lekar.Pacijenti.Any(p => p.KorisnickoIme == termin.Pacijent.KorisnickoIme))
                {
                    lekar.Pacijenti.Add(termin.Pacijent);
                }
            }

            _lekarService.Update(lekar, l => l.KorisnickoIme == lekar.KorisnickoIme);
            return View(lekar);
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

        public ActionResult IzborPacijentaZaTerapiju()
        {
            var lekar = Session["User"] as Lekar;
            var pacijenti = lekar.Pacijenti;
            return View(pacijenti);
        }


        public ActionResult PrepisivanjeTerapije(string pacijentKorisnickoIme)
        {
            var lekar = Session["User"] as Lekar;
            var pacijenti = _pacijentService.Read();
            var pacijent = pacijenti.FirstOrDefault(p => p.KorisnickoIme == pacijentKorisnickoIme);

            if (pacijent == null)
            {
                return HttpNotFound();
            }

            var terapija = new Terapija
            {
                Lekar = lekar,
                Datum = DateTime.Now,
                Pacijent = pacijent
            };

            return View(terapija);
        }


        [HttpPost]
        public ActionResult PrepisivanjeTerapije(Terapija model)
        {
            var lekar = Session["User"] as Lekar;
            var pacijenti = _pacijentService.Read();
            var pacijent = pacijenti.FirstOrDefault(p => p.KorisnickoIme == model.Pacijent.KorisnickoIme);

            if(pacijent == null || !pacijent.ZakazaniTermini.Any(t => t.Lekar.KorisnickoIme == lekar.KorisnickoIme && t.DatumVreme < DateTime.Now))
            {
                ViewBag.ErrorMessage = "Pacijent nije vaš ili nije imao termin kod vas u prošlosti.";
                return View(model);
            }

            var novaTerapija = new Terapija
            {
                Lekar = lekar,
                Datum = DateTime.Now,
                Opis = model.Opis,
                Pacijent = pacijent,
                Naziv = model.Naziv
            };

            pacijent.Terapije.Add(novaTerapija);
            _pacijentService.Update(pacijent, p => p.KorisnickoIme == pacijent.KorisnickoIme);

            lekar.Terapije.Add(novaTerapija);
            _lekarService.Update(lekar, l => l.KorisnickoIme == lekar.KorisnickoIme);

            return RedirectToAction("Index");
        }

        public ActionResult PregledTerapija()
        {
            var lekar = Session["User"] as Lekar;
            var terapije = lekar.Terapije;
            return View(terapije);
        }
    }
}