using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop2.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index(int pageNumber = 1, int pageSize = 4)
        {
            var model = new ContentDao().Paging(pageNumber, pageSize);
            return View(model);
        }
    }
}