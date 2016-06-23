using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //Session["MysteriousMessage"] = "I am your father";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "My contact page.";

            return View();
        }
        [Authorize]
        public ActionResult Application()
        {
            return View();
        }
        [Authorize]
        public ActionResult Custom()
        {
            return View();
        }
    }
}