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

        public ActionResult Detail(long id)
        {
            var content = new ContentDao().GetById(id);
            var tags = new ContentDao().ListAllTag(content.ID);
            ViewBag.Tags = tags;
            return View(content);
        }

        public ActionResult TagDetail(string id, int page = 1, int pageSize = 5)
        {
            var model = new ContentDao().GetAllContentByTag(id, page, pageSize);
            ViewBag.Tag = new ContentDao().GetTagById(id);
            return View(model);
        }
    }
}