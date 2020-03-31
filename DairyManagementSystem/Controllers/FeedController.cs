using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DairyManagementSystem.Models;

namespace DairyManagementSystem.Controllers
{
    public class FeedController : Controller
    {
        private DatabaseClass db = new DatabaseClass();
        public ActionResult FeedIndex()
        {
            return View(db.FeedItems.ToList());
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedItem feedItem = db.FeedItems.Find(id);
            if (feedItem == null)
            {
                return HttpNotFound();
            }
            return View(feedItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeedItemId,ItemName,ItemPrice,ItemDescription,ItemStock")] FeedItem feedItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("FeedIndex");
            }
            return View(feedItem);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedItem feedItem = db.FeedItems.Find(id);
            if (feedItem == null)
            {
                return HttpNotFound();
            }
            return View(feedItem);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeedItem feedItem = db.FeedItems.Find(id);
            db.FeedItems.Remove(feedItem);
            db.SaveChanges();
            return RedirectToAction("FeedIndex");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //Add Feed Items - manual code
        public ActionResult AddFeedItem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFeedItem(AddFeedModel model)
        {
            if (ModelState.IsValid)
            {
                DAL d = new DAL();
                AddFeedData a = d.FeedDetails(model.Name, model.Price, model.Description, model.Stock);
                return RedirectToAction("FeedIndex", "Feed");
            }
            else
            {
                return View();
            }
        }
        //Add Feed Transaction Details - manual code
        public ActionResult AddFeedTrans()
        {
            DatabaseClass ctx = new DatabaseClass();
            AddFeedTransModel model = new AddFeedTransModel();
            var res = from l in ctx.Logins
                      where l.Role == "Vendor"
                      select new { VendorName = l.UserName, UserId = l.Uid };
            List<SelectListItem> vend = new List<SelectListItem>();
            foreach (var i in res)
                vend.Add(new SelectListItem() { Text = i.VendorName, Value = i.UserId.ToString() });
            ViewBag.vend = vend;
            Session["vendor"] = vend;
            var r = from l in ctx.FeedItems
                    select new { itemname = l.ItemName, feedid = l.FeedItemId };
            List<SelectListItem> feed = new List<SelectListItem>();
            foreach (var i in r)
                feed.Add(new SelectListItem() { Text = i.itemname, Value = i.feedid.ToString() });
            Session["feeditem"] = feed;
            ViewBag.feed = feed;
            return View();
        }
        [HttpPost]
        public ActionResult AddFeedTrans(AddFeedTransModel model)
        {
            DAL d = new DAL();
            Session["vendor"] = model.VendorId;
            Session["feeditem"] = model.ItemId;
            AddFeedTransData at = d.TransDetails(model.ItemId, model.Quantity, model.Date, model.VendorId);
            return RedirectToAction("AddFeedTrans", "Feed");
        }
        //View Feed Transaction Details - manual code
        public ActionResult ViewFeedTransold()
        {
            DatabaseClass ctx = new DatabaseClass();
            AddFeedTransModel model = new AddFeedTransModel();
            var res = from l in ctx.Logins
                      where l.Role == "vendor"
                      select new { VendorName = l.UserName, UserId = l.Uid };
            List<SelectListItem> vend = new List<SelectListItem>();
            foreach (var i in res)
                vend.Add(new SelectListItem() { Text = i.VendorName, Value = i.UserId.ToString() });
            ViewBag.vend = vend;
            Session["VendId"] = vend;
            return View();
        }
        [HttpPost]
        public ActionResult ViewFeedTransold(AddFeedTransModel model)
        {
            Session["VendId"] = model.UserId;
            return RedirectToAction("ViewFeedTrans", "Feed");
        }

        public ActionResult ViewFeedTrans(AddFeedTransModel model)
        {
            DatabaseClass ctx = new DatabaseClass();
            int v = int.Parse(Session["VendId"].ToString());
            ViewBag.msg = "History viewed";
            //To view the data from database
            string vnm = (from k in ctx.Logins
                          where k.Uid == v
                          select k.UserName).FirstOrDefault().ToString();
            string vname = vnm.ToString();

            var req = from i in ctx.FeedTransactions
                      where i.VendorId == v
                      select i;

            List<AddFeedTransModel> mod = new List<AddFeedTransModel>();
            foreach (var i in req)
            {
                AddFeedTransModel dv = new AddFeedTransModel();
                dv.VendorName = vname;
                dv.ItemName = i.ItemName;
                dv.Quantity = i.ItemQuantity;
                dv.Amount = i.ItemAmount;
                dv.Date = i.FeedTransDate;
                mod.Add(dv);
            }
            return View(mod);
        }
        //Add Feed Supplier Payment slip Details for manager and vendor - manual code
        public ActionResult AddSuppPayment()
        {
            DatabaseClass ctx = new DatabaseClass();
            List<SelectListItem> month = new List<SelectListItem>();
            month.Add(new SelectListItem { Text = "January", Value = "1" });
            month.Add(new SelectListItem { Text = "February", Value = "2" });
            month.Add(new SelectListItem { Text = "March", Value = "3" });
            month.Add(new SelectListItem { Text = "April", Value = "4" });
            month.Add(new SelectListItem { Text = "May", Value = "5" });
            month.Add(new SelectListItem { Text = "June", Value = "6" });
            month.Add(new SelectListItem { Text = "July", Value = "7" });
            month.Add(new SelectListItem { Text = "August", Value = "8" });
            month.Add(new SelectListItem { Text = "September", Value = "9" });
            month.Add(new SelectListItem { Text = "October", Value = "10" });
            month.Add(new SelectListItem { Text = "November", Value = "11" });
            month.Add(new SelectListItem { Text = "December", Value = "12" });
            ViewBag.month = month;
            Session["month"] = month;
            List<SelectListItem> year = new List<SelectListItem>();
            year.Add(new SelectListItem { Text = "2020", Value = "2020" });
            year.Add(new SelectListItem { Text = "2021", Value = "2021" });
            year.Add(new SelectListItem { Text = "2022", Value = "2022" });
            year.Add(new SelectListItem { Text = "2023", Value = "2023" });
            ViewBag.year = year;
            Session["year"] = year;
            var res = from l in ctx.Logins
                      where l.Role == "Vendor"
                      select new { VendorName = l.UserName, UserId = l.Uid };
            List<SelectListItem> vend = new List<SelectListItem>();
            foreach (var i in res)
                vend.Add(new SelectListItem() { Text = i.VendorName, Value = i.UserId.ToString() });
            ViewBag.vend = vend;
            Session["vendor"] = vend;
            return View();
        }
        [HttpPost]
        public ActionResult AddSuppPayment(SupplierPayModel model)
        {
            Session["month"] = model.month;
            Session["year"] = model.year;
            Session["vendor"] = model.UserId;
            DAL d = new DAL();
            SupplierPayData m = d.PaymentDetails(model.month, model.year, model.UserId);
            return RedirectToAction("ViewSuppPayment", "Feed");
        }
        public ActionResult ViewSuppPayment(SupplierPayModel model)
        {
            DatabaseClass ctx = new DatabaseClass();
            int v = int.Parse(Session["vendor"].ToString());
            //To view the data from database
            string vnm = (from k in ctx.Logins
                          where k.Uid == v
                          select k.UserName).FirstOrDefault().ToString();
            string vname = vnm.ToString();
            var req = ctx.FeedSupplierPayments.Where(i => i.VendorId == v).Select(i => i);
            List<SupplierPayModel> mod = new List<SupplierPayModel>();
            foreach (var i in req)
            {
                SupplierPayModel dv = new SupplierPayModel();
                dv.VendorName = vname;
                dv.year = i.Year;
                dv.month = i.Month;
                dv.Amount = i.TotalAmount;
                mod.Add(dv);
            }
            return View(mod);
        }



    }
}
