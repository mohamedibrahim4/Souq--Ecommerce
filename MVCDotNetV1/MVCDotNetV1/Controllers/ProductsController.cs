using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCDotNetV1.Models;


namespace MVCDotNetV1.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Subcat.Maincat);
            return View(products.ToList());
        }


        [HttpPost]
        public ActionResult Index(string Search)
        {


            return View(db.Products.Where(x => x.ProductName.StartsWith(Search) || Search == null).ToList());

        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            
            ViewBag.maincatID = new SelectList(db.Maincats, "maincatID", "maincatName");
              ViewBag.subcatId = new SelectList(db.Subcats, "subcatId", "subcatName");
            
            




            ViewBag.ProductID = new SelectList(db.OrderDetails, "OrderDetailID", "ShippingType");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create( Product product,maincat c,subcat s, HttpPostedFileBase Image)
        {

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                string Imgname = product.ProductID.ToString()+ "." + Image.FileName.Split('.')[1];
                Image.SaveAs(Server.MapPath("~/images/") + Imgname);
                product.ImagePath = Imgname;

                product.subcatId =s.subcatId;
                //product.Subcat.Maincat = c;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.ProductID = new SelectList(db.OrderDetails, "OrderDetailID", "ShippingType", product.ProductID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.maincatID = new SelectList(db.Maincats, "maincatID", "maincatName");
            ViewBag.subcatId = new SelectList(db.Subcats, "subcatId", "subcatName");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.OrderDetails, "OrderDetailID", "ShippingType", product.ProductID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,ImagePath,MainCategory ,SubCategory,Description,Price,Discount,MadeIn,SupplierName,Weight,AvailableQuantity,OrderDetailID")] Product product, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                string Imgname = product.ProductID.ToString() + "." + Image.FileName.Split('.')[1];
                Image.SaveAs(Server.MapPath("~/images/") + Imgname);
                product.ImagePath = Imgname;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.OrderDetails, "OrderDetailID", "ShippingType", product.ProductID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public JsonResult GetSubcatList(int categoryId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<subcat> Sublist = db.Subcats.Where(e => e.maincatID == categoryId).ToList();
            return Json(Sublist, JsonRequestBehavior.AllowGet);
        }
    }
}
