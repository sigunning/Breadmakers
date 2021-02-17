using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Breadmakers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Events()
        {
            ViewBag.Message = "Your Events page.";

            return View();
        }

        public ActionResult Brethren()
        {
            ViewBag.Message = "Your Members page.";

            return View();
        }

    }
}