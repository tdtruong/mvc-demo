using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop2.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var dao = new ProductCategoryDao();
            var list = dao.ListAll();
            return PartialView(list);
        }

        public ActionResult CategoryDetail(long cateId)
        {
            var cate = new ProductCategoryDao().CategoryDetail(cateId);
            return View(cate);
        }

        public ActionResult ProductDetail(long id)
        {
            var product = new ProductDao().Detail(id);
            return View(product);
        }
    }
}