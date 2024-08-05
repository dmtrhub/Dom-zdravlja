using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dom_zdravlja.Controllers
{
    public class PacijentController : Controller
    {
        // GET: Pacijent
        public ActionResult Index()
        {
            if(Session["UserRole"]?.ToString() != "Pacijent")
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}