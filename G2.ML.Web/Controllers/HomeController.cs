using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G2.ML.Web.Controllers
{
	[AllowAnonymous]
    public class HomeController : Web.Infrastructure.Core.BaseController
    {
        public ActionResult Index()
        {
			return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
