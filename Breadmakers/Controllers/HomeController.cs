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

        public ActionResult Membership()
        {
            ViewBag.Message = "Your Membership page.";

            return View();
        }

        public ActionResult Gallery(string id)
        {
            ViewBag.Message = "Your Gallery page.";
            ViewBag.GalleryPath = id;

            // get comp code, last but one element
            // "~|Content|BAT|BAT2005|Gallery"
            string[] aPath = id.Split('|');
            ViewBag.CodeName = aPath[aPath.Length-2];

            return View();
        }
    }
}