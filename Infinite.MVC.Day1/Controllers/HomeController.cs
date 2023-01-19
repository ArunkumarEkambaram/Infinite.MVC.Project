using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace Infinite.MVC.Day1.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
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

        //Home/MyView

        public RedirectResult MyUrl()
        {
            return Redirect("https://www.google.com");
        }

        public RedirectToRouteResult MyView()
        {            
            return RedirectToAction("About");
        }

        public ContentResult MyContent()
        {
            return Content("Hello World!!!");
        }

        public JsonResult MyJson()
        {
            var product = new { id = "P001", Name = "Pen", Price = 5000.6 };
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public string MyData()
        {
            return "Hello World!!!";
        }
    }
}