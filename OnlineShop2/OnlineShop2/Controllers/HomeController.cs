using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDao().ListAll();
            var productDao = new ProductDao();
            ViewBag.ListNewProducts = productDao.ListNewProduct(4);
            ViewBag.ListFeatureProducts = productDao.ListFeatureProduct(4);
            return View();
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var menuDao = new MenuDao();
            var list = menuDao.GetByGroupId(1);
            return PartialView(list);
        }

        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var menuDao = new MenuDao();
            var list = menuDao.GetByGroupId(2);
            return PartialView(list);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var footerDao = new FooterDao();
            var footer = footerDao.GetFooter();
            return PartialView(footer);
        }
    }

}