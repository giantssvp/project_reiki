using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_reiki.Models;
using System.Net.Mail;

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

        public ActionResult submit_btn_booking(string name_booking, string email_booking, string phone, string date, string time_slot, string session_name)
        {
            var obj = new db_connect();
            obj.Insert_Booking(name_booking, email_booking, phone, time_slot, session_name, date);

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("abcdtes26@gmail.com");
            mail.To.Add("abcdtes26@gmail.com");
            mail.Subject = "Booking : " + name_booking  + " : "+ date  + " : " + time_slot + " : " + session_name;
            mail.IsBodyHtml = true;
            string htmlBody;
            string am_pm = " PM";

            if(Int32.Parse(time_slot) == 10)
            {
                am_pm = " AM";
            }

            string time = "";
            int end_time = Int32.Parse(time_slot) + 1;
            if (Int32.Parse(time_slot) == 12)
            {
                time = time_slot + " - 1" + am_pm;
            }
            else
            {
                time = time_slot + " - " + end_time + am_pm;
            }

            htmlBody = "<html> <head>  </head> <body>" +
                        "<table border=\"1\" style=\"font - family:Georgia, Garamond, Serif; width: 100 %; color: blue; font - style:italic; \"> <tr bgcolor=\"#00FFFF\" align=\"center\"> <th> Name </th> <th> Email </th> <th> Phone </th> <th> Date </th> <th> Time </th> <th> Session </th>  </tr> <tr align=\"center\"> " +
                        "<td>" + name_booking + "</td>" +
                        "<td>" + email_booking + "</td>" +
                        "<td>" + phone + "</td>" +
                        "<td>" + date + "</td>" +
                        "<td>" + time + "</td>" +
                        "<td>" + session_name + "</td>" +
                        "</tr> </table> </body> </html> ";
           
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("abcdtes26@gmail.com", "9921642540");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

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