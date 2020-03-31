using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DairyManagementSystem.Models;
using DataAccessLayer;

namespace DairyManagementSystem.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult VendorView(HistoryModel model)
        {
            DatabaseClass ctx = new DatabaseClass();
            int v = int.Parse(Session["VID"].ToString());
            //To view the data from database
            string vnm = (from k in ctx.Logins
                          where k.Uid == v
                          select k.UserName).FirstOrDefault().ToString();
            string vname = vnm.ToString();
            var req = ctx.Products.Where(i => i.VendorUid == v).Select(i => i);
            List<HistoryModel> mod = new List<HistoryModel>();
            foreach (var i in req)
            {
                HistoryModel m = new HistoryModel();
                m.VendorName = vname;
                m.quantity = i.MilkQuantity;
                m.fat = (from k in ctx.PriceMaintains
                         where k.PriceId == i.FatPercentId
                         select k.FatPercent).FirstOrDefault().ToString();
                m.date = i.Date;
                m.timing = i.Time;
                m.Price = i.Price;
                mod.Add(m);
            }
            return View(mod);
        }
        public ActionResult ViewFeedItem(AddFeedTransModel model)
        {
            DatabaseClass ctx = new DatabaseClass();
            int v = int.Parse(Session["VID"].ToString());
            //To view the data from database
            string vnm = (from k in ctx.Logins
                          where k.Uid == v
                          select k.UserName).FirstOrDefault().ToString();
            string vname = vnm.ToString();
            //var req = ctx.Products.Where(i => i.VendorUid == v).Select(i => i);
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
        public ActionResult ViewPaymentSlip(VendorPayModel model)
        {
            DatabaseClass ctx = new DatabaseClass();
            int v = int.Parse(Session["VID"].ToString());
            //To view the data from database
            string vnm = (from k in ctx.Logins
                          where k.Uid == v
                          select k.UserName).FirstOrDefault().ToString();
            string vname = vnm.ToString();

            var req = ctx.VenderPayments.Where(i => i.VendorId == v).Select(i => i);

            List<VendorPayModel> mod = new List<VendorPayModel>();
            foreach (var i in req)
            {
                VendorPayModel dv = new VendorPayModel();
                dv.VendorName = vname;
                dv.year = i.Year;
                dv.month = i.Month;
                dv.MilkAmount = i.MilkAmount;
                dv.FeedAmount = i.FeedAmount;
                dv.GrossAmount = i.GrossAmount;
                mod.Add(dv);
            }
            return View(mod);
        }

    }
}