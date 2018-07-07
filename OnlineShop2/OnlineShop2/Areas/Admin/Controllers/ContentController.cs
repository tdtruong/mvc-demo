using Model.Dao;
using Model.EF;
using OnlineShop2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop2.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index()
        {
            var contentDao = new ContentDao();
            var model = contentDao.ListAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var contentDao = new ContentDao();
                model.MetaTitle = Converter.ConvertToUnSign(model.Name);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                var rs = contentDao.Insert(model);
                if (rs > 0)
                {
                    SetAlert("Insert success!", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Insert failed, check the contents again");
                }
            }
            SetViewBag();
            return View("Create");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var contentDao = new ContentDao();
            var content = contentDao.GetById(id);
            SetViewBag(content.CategoryID);
            return View(content);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {
                var contentDao = new ContentDao();
                var editedContent = contentDao.GetById(model.ID);
                if (editedContent == null)
                {
                    ViewBag.ErrorContent = "This content is not exists or deleted.";
                    return RedirectToAction("Index", "Content");
                } else
                {
                    model.MetaTitle = Converter.ConvertToUnSign(model.Name);
                    var rs = contentDao.Update(model);
                    if (rs)
                    {
                        return RedirectToAction("Index", "Content");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Can not update this content. Please check again.");
                    }
                }
            }
            SetViewBag(model.CategoryID);
            return View();
        }

        public ActionResult Delete(long id)
        {
            var contentDao = new ContentDao();
            var rs = contentDao.Delete(id);
            if (rs)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var contentDao = new ContentDao();
            var rs = contentDao.ChangeStatus(id);
            SetAlert("Status is changed successfully!", "success");
            return Json(new {
                status = rs
            });
        }

        public void SetViewBag(long? selectedId = null)
        {
            var categoryDao = new CategoryDao();
            var categories = categoryDao.ListAll();
            ViewBag.CategoryID = new SelectList(categories, "ID", "Name", selectedId);
        }
    }
}