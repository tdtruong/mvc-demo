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

        public ActionResult CategoryDetail(long cateId, int page = 1, int pageSize = 1)
        {
            var cate = new ProductCategoryDao().CategoryDetail(cateId);
            ViewBag.Category = cate;
            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(cateId, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Prev = page - 1;
            ViewBag.Next = page + 1;
            return View(model);
        }

        public ActionResult ProductDetail(long id)
        {
            var product = new ProductDao().Detail(id);
            ViewBag.Category = new ProductCategoryDao().CategoryDetail(product.CategoryID.Value);
            ViewBag.RelatedProduct = new ProductDao().ListRelatedProduct(id);
            return View(product);
        }

        public JsonResult ListName(string q)
        {
            var list = new ProductDao().ListName(q);
            return Json(new {
                data = list,
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1, int pageSize = 2)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Prev = page - 1;
            ViewBag.Next = page + 1;
            return View(model);
        }
    }
}