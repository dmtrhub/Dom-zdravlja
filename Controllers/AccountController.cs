using Dom_zdravlja.Models;
using Dom_zdravlja.Models.Login;
using Dom_zdravlja.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dom_zdravlja.Controllers
{
    public class AccountController : Controller
    {
        private readonly JsonFileService<Pacijent> _pacijentService;
        private readonly JsonFileService<Lekar> _lekarService;
        private readonly JsonFileService<Administrator> _administratorService;
            
        public AccountController()
        {
            _pacijentService = new JsonFileService<Pacijent>(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Pacijenti.json"));
            _lekarService = new JsonFileService<Lekar>(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Lekari.json"));
            _administratorService = new JsonFileService<Administrator>(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Administratori.json"));
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pacijenti = _pacijentService.GetAll() ?? new List<Pacijent>();
                var lekari = _lekarService.GetAll() ?? new List<Lekar>();
                var administratori = _administratorService.GetAll() ?? new List<Administrator>();


                var pacijent = pacijenti.FirstOrDefault(p => p.KorisnickoIme == model.KorisnickoIme && p.Sifra == model.Sifra);
                var lekar = lekari.FirstOrDefault(l => l.KorisnickoIme == model.KorisnickoIme && l.Sifra == model.Sifra);
                var administrator = administratori.FirstOrDefault(a => a.KorisnickoIme == model.KorisnickoIme && a.Sifra == model.Sifra);

                if (pacijent != null)
                {
                    // Log in as Pacijent
                    Session["UserRole"] = "Pacijent";
                    Session["Username"] = pacijent.KorisnickoIme;
                    return RedirectToAction("Index", "Pacijent");
                }
                else if (lekar != null)
                {
                    // Log in as Lekar
                    Session["UserRole"] = "Lekar";
                    Session["Username"] = lekar.KorisnickoIme;
                    return RedirectToAction("Index", "Lekar");
                }
                else if (administrator != null)
                {
                    // Log in as Administrator
                    Session["UserRole"] = "Administrator";
                    Session["Username"] = administrator.KorisnickoIme;
                    return RedirectToAction("Index", "Administrator");
                }
                else
                {
                    ModelState.AddModelError("", "Neispravno korisničko ime ili šifra.");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear(); // Clear all session data
            return RedirectToAction("Login", "Account");
        }
    }
}