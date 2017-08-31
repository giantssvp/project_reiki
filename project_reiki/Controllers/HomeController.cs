using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_reiki.Models;

namespace project_reiki.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(string name, string email, string subject, string message)
        {
            //System.Windows.Forms.MessageBox.Show(name + email + subject + message);
            //ViewBag.Message = "Your contact page.";
            var obj = new db_connect();
            obj.Insert(name, email, subject, message);
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Testimony()
        {
            var obj = new db_connect();
            List<string>[] list = new List<string>[3];
            list = obj.testimony_show(0);
            ViewBag.list = list;
            return View();
        }

        public ActionResult Disclaimer()
        {
            return View();
        }

        public ActionResult Booking()
        {
            //var obj = new db_connect();
            //obj.Insert();
            return View();
        }
    }
}