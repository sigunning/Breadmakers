using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Breadmakers.Models;
using Breadmakers.ViewModels;

namespace Breadmakers.Controllers
{
    public class BatwaiiiController : Controller
    {
        private List<Batwaiii> _lstBatwaiii;

        public BatwaiiiController()
        {
            _lstBatwaiii = new List<Batwaiii>()
            {
                new Batwaiii {id=1,Key="BAT2013", Name = "BATWAIII 2013" },
                new Batwaiii {id=2,Key="BAT2014", Name = "BATWAIII 2014",
                    Location = "Suffolk",
                    MonthYear = "Nov 2014",
                    Accomodation = "Bucket and Spade Cottage"},
                new Batwaiii {id=3,Key="BAT2015", Name = "BATWAIII 2015" }
            };
        }

        public ActionResult BatList()
        {



            var batwaiiiViewModel = new BatwaiiiViewModel
            {
                //
                BatwaiiiList = _lstBatwaiii
            };


            return View(batwaiiiViewModel);
        }

        public ActionResult BatDetail(string id)
        {
            Batwaiii bat = _lstBatwaiii.SingleOrDefault(o => o.Key == id);

            bat.ImgChamp = string.Concat( "~/Content/Images/BATWAIII/",bat.Key,"/presentation.jpg");

            // read PostScript text
            string filePath = string.Concat("~/Content/Images/BATWAIII/", bat.Key, "/postscript.htm");

            using (StreamReader sr = new StreamReader(Server.MapPath(filePath)))
            {
                bat.PostScript = sr.ReadToEnd();
            }
                
            

            var batwaiiiViewModel = new BatwaiiiViewModel
            {
                Batwaiii = bat
            };

            return View(batwaiiiViewModel);
        }
    }
}