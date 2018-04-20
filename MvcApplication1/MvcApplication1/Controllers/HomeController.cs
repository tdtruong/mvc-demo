using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //ViewBag.HomeMessage = "Sử dụng ViewBag để hiển thị message";
            var message = new MessageModel();
            message.Welcome = "Sử dụng MessageModel để hiển thị message";
            return View(message);
        }

    }
}
