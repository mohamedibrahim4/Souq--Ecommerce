using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDotNetV1.Models;
using Microsoft.AspNet.Identity;


namespace MVCDotNetV1.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        
        ApplicationDbContext db = new ApplicationDbContext();

     
        //private static string cartJson;

        [HttpGet]
        public ActionResult IsAuthorized()
        {
            if (User.Identity.IsAuthenticated)
                return Json(new { isAuthorized = true }, JsonRequestBehavior.AllowGet);

            return Json(new { isAuthorized = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[Authorize]
        public ActionResult Add()
        {
            string requestContent;

            using (StreamReader streamReader = new StreamReader(HttpContext.Request.InputStream))
            {
                requestContent = streamReader.ReadToEnd();
            }

            //cartJson = name;
            var userId = User.Identity.GetUserId();
            var cart = db.Carts.Find(userId);

            // if the user doesn't have a cart
            if (cart == null)
            {
                Cart c = new Cart() { CartID = userId, CartContant = requestContent };
                db.Carts.Add(c);
            }
            else
            {
                // the user already has cart
                cart.CartContant = requestContent;
            }

            db.SaveChanges();

            return Json(requestContent, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var userId = User.Identity.GetUserId();
            var cart = db.Carts.Find(userId);

            if (cart == null)
                return Json(new { isValid = false, cartContent = "null" }, JsonRequestBehavior.AllowGet);

            return Json(new { isValid = true, cartContent = cart.CartContant }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            //int p = db.Products.Max(a => a.ProductID);
            var items = db.Products.OrderByDescending(u => u.ProductID).Take(4).ToList();
            var discount = db.Products.OrderByDescending(u => u.Discount).Take(4).ToList();
            var min = db.Products.OrderBy(u => u.AvailableQuantity).Take(4).ToList();
            var offer = db.Offers.OrderBy(u=>u.ProductID).Take(4).ToList();
             ViewBag.offer= offer;
            ViewBag.min = min;
            ViewBag.discount = discount;



            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult search(string Search)
        {


            return View(db.Products.Where(x => x.ProductName.StartsWith(Search) || Search == null).ToList());

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        

        public  ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

           
            return  View();
        }

    }
}
