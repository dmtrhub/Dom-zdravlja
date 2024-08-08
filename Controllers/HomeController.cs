using Dom_zdravlja.Models;
using Dom_zdravlja.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dom_zdravlja.Controllers
{
    public class HomeController : Controller
    {
        private IJsonFileService<Pacijent> _pacijentService;
        private IJsonFileService<Lekar> _lekarService;
        private IJsonFileService<Administrator> _administratorService;

        public HomeController()
        {
            _pacijentService = new JsonFileService<Pacijent>("~/App_Data/Pacijenti.json");
            _lekarService = new JsonFileService<Lekar>("~/App_Data/Lekari.json");
            _administratorService = new JsonFileService<Administrator>("~/App_Data/Administratori.json");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Učitavanje podataka o korisnicima
                var pacijenti = _pacijentService.Read();
                var lekari = _lekarService.Read();
                var administratori = _administratorService.Read();

                // Provera da li je korisnik pacijent
                var pacijent = pacijenti.Find(p => p.KorisnickoIme == model.KorisnickoIme && p.Sifra == model.Sifra);
                if (pacijent != null)
                {
                    // Postavi korisničke podatke u sesiju
                    Session["User"] = pacijent;
                    Session["Role"] = "Pacijent";
                    return RedirectToAction("Index", "Pacijent");
                }

                // Provera da li je korisnik lekar
                var lekar = lekari.Find(l => l.KorisnickoIme == model.KorisnickoIme && l.Sifra == model.Sifra);
                if (lekar != null)
                {
                    // Postavi korisničke podatke u sesiju
                    Session["User"] = lekar;
                    Session["Role"] = "Lekar";
                    return RedirectToAction("Index", "Lekar");
                }

                // Provera da li je korisnik administrator
                var administrator = administratori.Find(a => a.KorisnickoIme == model.KorisnickoIme && a.Sifra == model.Sifra);
                if (administrator != null)
                {
                    // Postavi korisničke podatke u sesiju
                    Session["User"] = administrator;
                    Session["Role"] = "Administrator";
                    return RedirectToAction("Index", "Administrator");
                }

                // Ako korisnik nije pronađen, vrati grešku
                ViewBag.ErrorMessage = "Neispravno korisničko ime ili šifra.";
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}