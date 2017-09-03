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

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult submit_btn_contact(string name, string email, string subject, string message)
        {
            var obj = new db_connect();
            obj.Insert(name, email, subject, message);
            return RedirectToAction("Contact", "Home");
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
            ViewBag.total = list[0].Count();
            return View();
        }

        public ActionResult Disclaimer()
        {
            return View();
        }

        public ActionResult Booking()
        {
            return View();
        }

        public ActionResult submit_btn_booking(string date, string time_slot, string session_name)
        {
            var obj = new db_connect();
            obj.Insert_Booking(time_slot, session_name, date);
            return RedirectToAction("Booking","Home");
        }

        [HttpGet]
        public ActionResult get_time_slots(string date)
        {
            var obj = new db_connect();
            List<string>[] list_time = new List<string>[1];
            list_time = obj.time_slot_show(date);

            List<int> a = list_time[0].Select(int.Parse).ToList();
            List <int> b = new List<int> {10, 11, 12, 1, 2, 3, 4};

            List<int> c = b.Except(a).ToList();
            ViewBag.time_list = c;
            ViewBag.time_cnt = list_time[0].Count();
            return Json(new
            {
                c = c
            }, JsonRequestBehavior.AllowGet);
        }
    }
}