using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dom_zdravlja.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Index()
        {
            if(Session["UserRole"]?.ToString() != "Administrator")
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}