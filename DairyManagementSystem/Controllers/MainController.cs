using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DairyManagementSystem.Models;
using DataAccessLayer;

namespace DairyManagementSystem.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Manager()
        {
            ViewBag.msg = "Manager functionality...";
            DatabaseClass ctx = new DatabaseClass();
            ManagerModel model = new ManagerModel();
            var res = from l in ctx.Logins
                      where l.Role == "Vendor"
                      select new { VendorName = l.UserName, UserId = l.Uid };
            List<SelectListItem> vend = new List<SelectListItem>();
            foreach (var i in res)
                vend.Add(new SelectListItem() { Text = i.VendorName, Value = i.UserId.ToString() });
            ViewBag.vend = vend;
            Session["VendorId"] = vend;
            var r = from l in ctx.PriceMaintains
                    select new { fatperc = l.FatPercent, fatpercid = l.PriceId };
            List<SelectListItem> fat = new List<SelectListItem>();
            foreach (var i in r)
                fat.Add(new SelectListItem() { Text = i.fatperc, Value = i.fatpercid.ToString() });
            Session["fatperc"] = fat;
            List<SelectListItem> time = new List<SelectListItem>();
            time.Add(new SelectListItem { Text = "Morning", Value = "Morning" });
            time.Add(new SelectListItem { Text = "Evening", Value = "Evening" });
            ViewBag.timing = time;
            Session["timing"] = time;
            ViewBag.fat = fat;
            return View();
        }
        [HttpPost]
        public ActionResult Manager(ManagerModel model)
        {
            //DatabaseClass ctx = new DatabaseClass();
            Session["VendorId"] = model.UserId;
            Session["fatperc"] = model.fat;
            Session["timing"] = model.timing;

            DAL d = new DAL();
            MilkDailyData m = d.MilkData(model.UserId, model.quantity, model.fat, model.timing, model.date);
            return RedirectToAction("Manager", "Main");
        }

        public ActionResult AddNewVendor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewVendor(AddVendorModel model)
        {
            if (ModelState.IsValid)
            {
                DAL d = new DAL();
                AddVendorData a = d.VendorDetails(model.username, model.password, model.address);
                return RedirectToAction("Manager", "Main");
            }
            else 
            {
                return View();
            }
        }

        public ActionResult ViewHistory()
        {
            DatabaseClass ctx = new DatabaseClass();
            HistoryModel model = new HistoryModel();
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
        public ActionResult ViewHistory(HistoryModel model)
        {
            Session["VendId"] = model.UserId;
            return RedirectToAction("ViewHistoryNew", "Main");
        }

        public ActionResult ViewHistoryNew(HistoryModel model)
        {
            DatabaseClass ctx = new DatabaseClass();
            int v = int.Parse(Session["VendId"].ToString());
            ViewBag.msg = "History viewed";
            //To view the data from database
            string vnm = (from k in ctx.Logins
                          where k.Uid == v
                          select k.UserName).FirstOrDefault().ToString();
            string vname = vnm.ToString();

            var req = from i in ctx.Products
                      where i.VendorUid == v
                      select i;

            List<HistoryModel> mod = new List<HistoryModel>();
            foreach (var i in req)
            {
                HistoryModel dv = new HistoryModel();
                dv.VendorName = vname;
                dv.quantity = i.MilkQuantity;
                dv.fat = (from k in ctx.PriceMaintains
                          where k.PriceId == i.FatPercentId
                          select k.FatPercent).FirstOrDefault().ToString();
                //dv.fatid = i.FatPercentId;
                dv.timing = i.Time;
                dv.date = i.Date;
                dv.Price = i.Price;
                mod.Add(dv);
            }
            return View(mod);
        }

        public ActionResult GenerateSlip()
        {
            DatabaseClass ctx = new DatabaseClass();
            HistoryModel model = new HistoryModel();
            var res = from l in ctx.Logins
                      where l.Role == "vendor"
                      select new { VendorName = l.UserName, UserId = l.Uid };
            List<SelectListItem> vend = new List<SelectListItem>();
            foreach (var i in res)
                vend.Add(new SelectListItem() { Text = i.VendorName, Value = i.UserId.ToString() });
            ViewBag.vend = vend;
            Session["VendeId"] = vend;
            return View();
        }
        [HttpPost]
        public ActionResult GenerateSlip(GenerateModel model)
        {
            Session["VendeId"] = model.UserId;
            return RedirectToAction("GenerateSlipNew", "Main");
        }
        public ActionResult GenerateSlipNew()
        {
            DatabaseClass ctx = new DatabaseClass();
            int v = int.Parse(Session["VendeId"].ToString());
            //To view the data from database
            string vnm = (from k in ctx.Logins
                          where k.Uid == v
                          select k.UserName).FirstOrDefault().ToString();
            string vname = vnm.ToString();
            var req = from i in ctx.Products
                      where i.VendorUid == v
                      select i;
            int sum = 0;
            List<GenerateModel> mod = new List<GenerateModel>();
            foreach (var i in req)
            {
                GenerateModel dv = new GenerateModel();
                dv.VendorName = vname;
                dv.quantity = i.MilkQuantity;
                dv.fat = (from k in ctx.PriceMaintains
                          where k.PriceId == i.FatPercentId
                          select k.FatPercent).FirstOrDefault().ToString();
                dv.timing = i.Time;
                dv.date = i.Date;
                dv.Price = i.Price;
                sum += i.Price;
                mod.Add(dv);
            }
            ViewBag.sum = sum;
            return View(mod);
        }
        public ActionResult AddVendorPaySlip()
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
        public ActionResult AddVendorPaySlip(VendorPayModel model)
        {
            Session["month"] = model.month;
            Session["year"] = model.year;
            Session["vendor"] = model.UserId;
            DAL d = new DAL();
            ManagerPaytoVendData m = d.VendorPayDetails(model.month, model.year, model.UserId);
            return RedirectToAction("ViewVendorPaySlipFinal", "Main");
        }
        public ActionResult ViewVendorPaySlipFinal()
        {
            DatabaseClass ctx = new DatabaseClass();
            int v = int.Parse(Session["vendor"].ToString());
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
            //ViewBag.msg = " Gross Amount Paid to Vendor..Thank You";
            return View(mod);
            //return RedirectToAction("ViewMsg", "Main");
        }
        [HttpPost]
        public ActionResult ViewVendorPaySlipFinal(VendorPayModel model)
        {
            
            return RedirectToAction("ViewMsg","Main");
        }
        public ActionResult ViewMsg()
        {
            ViewBag.msg = " Gross Amount Paid to Vendor..Thank You";
            return View();
        }

    }
}

