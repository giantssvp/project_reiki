using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using db;

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

        public ActionResult submit_btn_contact(string name, string email, string comment_type, string subject, string message)
        {
            var obj = new db_connect();
            obj.Insert(name, email, comment_type, subject, message);

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("relay-hosting.secureserver.net");

            mail.From = new MailAddress("admin@mokshhealing.com.au");
            mail.To.Add("admin@mokshhealing.com.au");
            mail.Subject = "Contact : " + name + " : " + comment_type;
            mail.IsBodyHtml = true;
            string htmlBody;

            htmlBody = "<html> <head>  </head> <body>" +
                        "<table border=\"1\" style=\"font - family:Georgia, Garamond, Serif; width: 100 %; color: blue; font - style:italic; \"> <tr bgcolor=\"#00FFFF\" align=\"center\"> <th> Name </th> <th> Email </th> <th> Comment Type </th> <th> Subject </th> <th> Message </th>  </tr> <tr align=\"center\"> " +
                        "<td>" + name + "</td>" +
                        "<td>" + email + "</td>" +
                        "<td>" + comment_type + "</td>" +
                        "<td>" + subject + "</td>" +
                        "<td>" + message + "</td>" +
                        "</tr> </table> </body> </html> ";

            mail.Body = htmlBody;
            SmtpServer.Credentials = new System.Net.NetworkCredential("admin@mokshhealing.com.au", "Mokshhealing@123");

            SmtpServer.Send(mail);

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
            var msg = "";
            var ret = obj.Insert_Booking(name_booking, email_booking, phone, time_slot, session_name, date);

            if (ret == 0)
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("relay-hosting.secureserver.net");

                mail.From = new MailAddress("admin@mokshhealing.com.au");
                mail.To.Add("admin@mokshhealing.com.au");
                mail.Subject = "Booking : " + name_booking + " : " + date + " : " + time_slot + " : " + session_name;
                mail.IsBodyHtml = true;
                string htmlBody;
                string am_pm = " PM";

                if (Int32.Parse(time_slot) == 10)
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
                SmtpServer.Credentials = new System.Net.NetworkCredential("admin@mokshhealing.com.au", "Mokshhealing@123");

                SmtpServer.Send(mail);

                msg = "Your appoinment is booked successfully. Thank You!!";
            }
            else if (ret == 1062)
            {
                msg = "This slot is already booked by another user, Please book another time slot. Thank You!!";
            }
            else
            {
                msg = "There is some issue with booking, Please try again. Thank You!!";
            }
            TempData["AlertMessage"] = msg;
            return RedirectToAction("Booking", "Home");
        }

        [HttpGet]
        public ActionResult get_time_slots(string date)
        {
            var obj = new db_connect();
            List<string>[] list_time = new List<string>[1];
            list_time = obj.time_slot_show(date);

            List<int> a = list_time[0].Select(int.Parse).ToList();
            List<int> b = new List<int> { 10, 11, 12, 1, 2, 3, 4 };

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