using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dom_zdravlja.Controllers
{
    public class LekarController : Controller
    {
        // GET: Lekar
        public ActionResult Index()
        {
            if(Session["UserRole"]?.ToString() != "Lekar")        
                return RedirectToAction("Login", "Home");          

            return View();
        }
    }
}