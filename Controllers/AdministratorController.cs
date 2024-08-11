using Dom_zdravlja.Models;
using Dom_zdravlja.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dom_zdravlja.Controllers
{
    public class AdministratorController : Controller
    {
        private IJsonFileService<Pacijent> _pacijentService;

        public AdministratorController()
        {
            _pacijentService = new JsonFileService<Pacijent>("~/App_Data/Pacijenti.json");
        }

        // GET: Administrator
        public ActionResult Index()
        {
            var admin = Session["User"] as Administrator;
            if (admin == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var pacijenti = _pacijentService.Read();
            return View(pacijenti);

        }

        public ActionResult KreirajPacijenta()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KreirajPacijenta(Pacijent noviPacijent)
        {
            var pacijenti = _pacijentService.Read();

            bool korisnickoImePostoji = pacijenti.Any(p => p.KorisnickoIme == noviPacijent.KorisnickoIme);
            bool emailPostoji = pacijenti.Any(p => p.Email == noviPacijent.Email);
            bool jmbgPostoji = pacijenti.Any(p => p.JMBG == noviPacijent.JMBG);

            if(korisnickoImePostoji || emailPostoji || jmbgPostoji)
            {
                ViewBag.ErrorMessage = "Korisničko ime, E-mail ili JMBG već postoji.";
                return View(noviPacijent);
            }

            pacijenti.Add(noviPacijent);
            _pacijentService.Write(pacijenti);

            return RedirectToAction("Index");
        }
    }
}