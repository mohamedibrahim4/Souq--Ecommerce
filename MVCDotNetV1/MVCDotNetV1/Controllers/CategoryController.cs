using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDotNetV1.Models;

namespace MVCDotNetV1.Controllers
{
    public class categorycontroller : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // get: category
      
        public ActionResult index(string catname, string subcat)
        {
            var cat = db.Products.Where(s => s.Subcat.Maincat.maincatName == catname && s.Subcat.subcatName == subcat).ToList();


            return View(cat);
        }
        public ActionResult allcatogry(string catname)
        {

            var allcat = db.Products.Where(s => s.Subcat.Maincat.maincatName == catname).Select(e => e.Subcat.subcatName).Distinct().ToList();


            ViewBag.allcat = allcat;
            ViewBag.cat = catname;


            return View();

        }
    }
}