using Model.Dao;
using OnlineShop2.Common;
using OnlineShop2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            ViewBag.Title = ConfigurationManager.AppSettings["HomeTitle"];
            ViewBag.Keywords = ConfigurationManager.AppSettings["HomeKeywords"];
            ViewBag.Description = ConfigurationManager.AppSettings["HomeDescription"];
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

        [ChildActionOnly]
        public ActionResult CartHeader()
        {
            var cart = Session[CommonConstants.CartSession];
            var listItem = new List<CartItem>();
            if (cart != null)
            {
                listItem = (List<CartItem>)cart;
            }
            return PartialView(listItem);
        }
    }

}