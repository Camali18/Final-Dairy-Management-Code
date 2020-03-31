using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DairyManagementSystem.Models;
using DataAccessLayer;

namespace DairyManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                DAL d = new DAL();
                LoginData ld = d.Login(model.UserName, model.Password);
                if (ld != null)
                {
                    if (ld.role == "Manager")
                        return RedirectToAction("Manager", "Main");
                    else if (ld.role == "Vendor")
                    {
                        Session["VID"] = ld.uid;
                        return RedirectToAction("VendorView", "Vendor");
                    }
                    else if (ld.role == "Feed Supplier")
                        return RedirectToAction("FeedIndex", "Feed");     
                }
                ViewBag.msg = "alert('Error in login...Enter correct credentials')";
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
        
    }
}