using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop2.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var contact = new ContactDao().GetContact();
            return View(contact);
        }

        public JsonResult Send(string name, string phone, string address, string email, string content)
        {
            var fb = new Feedback();
            fb.Name = name;
            fb.Phone = phone;
            fb.Address = address;
            fb.Email = email;
            fb.Content = content;
            fb.CreatedDate = DateTime.Now;
            var fbId = new ContactDao().InsertFeedback(fb);
            if (fbId > 0)
            {
                return Json(new {
                    success = true
                });
            }
            else
            {
                return Json(new
                {
                    success = false
                });
            }
        }
    }
}