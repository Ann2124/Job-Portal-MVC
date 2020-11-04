using Job_Portal_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job_Portal_MVC.Controllers
{
    public class HomeController : Controller
    {
        JobPortalContext db = new JobPortalContext();
        public ActionResult Index()
        {
            return View(db.Openings.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}