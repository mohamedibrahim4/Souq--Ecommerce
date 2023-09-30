using Microsoft.AspNet.Identity;
using MVCDotNetV1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDotNetV1.Controllers
{
    [AllowAnonymous]
    public class AdminCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        // GET: AdminCart
        [Authorize]
        public ActionResult CartAdded()
        {
            var userID = User.Identity.GetUserId();
            var userName = User.Identity.Name;

            var cart = db.Carts.Find(userID);

            if (cart != null && !cart.CartContant.Equals("[]"))
            {
                var cartAdmin = new CartAdmin()
                {
                    AddTime = DateTime.Now,
                    BuyerID = userID,
                    BuyerName = userName,
                    CartContent = cart.CartContant,
                    IsActionTaken = false,
                    IsConfirmed = false
                };

                db.CartAdmins.Add(cartAdmin);
                db.Carts.Remove(cart);
                db.SaveChanges();
            }

            return Json(cart.CartContant, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "Seller")]
        public ActionResult GetCart()
        {
            var allOrders = db.CartAdmins
                              .OrderBy(ca => ca.IsActionTaken)
                              .ToList();

            return View(allOrders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("GetCart")]
        public ActionResult GetCartConfirmation(Dictionary<string, bool> confirmationIDs)
        {
            if (ModelState.IsValid)
            {
                var ordersWithNoActions = db.CartAdmins
                                            .Where(ca => ca.IsActionTaken == false)
                                            .ToList();

                var values = confirmationIDs.Values.ToList();

                for (int i = 0; i < confirmationIDs.Count; i++)
                {
                    ordersWithNoActions[i].IsConfirmed = values[i];
                    ordersWithNoActions[i].IsActionTaken = true;
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetCustomerOrders()
        {
            var userID = User.Identity.GetUserId();

            var userOrders = db.CartAdmins
                               .Where(ca => ca.BuyerID == userID)
                               .OrderBy(ca => ca.IsActionTaken)
                               .ToList();

            return View(userOrders);
        }
    }
}