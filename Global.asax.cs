using Dom_zdravlja.Models;
using Dom_zdravlja.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Mvc5;

namespace Dom_zdravlja
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var adminService = new JsonFileService<Administrator>("~/App_Data/Administratori.json");
            var lekarService = new JsonFileService<Lekar>("~/App_Data/Lekari.json");
            var pacijentService = new JsonFileService<Pacijent>("~/App_Data/Pacijenti.json");
        }
    }
}
