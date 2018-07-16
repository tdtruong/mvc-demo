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
    public class UserController : BaseController
    {
        private UserDao userDao;
        // GET: Admin/User - None paging
        //public ActionResult Index()
        //{
        //    userDao = new UserDao();
        //    var users = userDao.GetAll();
        //    return View(users);
        //}
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int pageNumber = 1, int pageSize = 10)
        {
            userDao = new UserDao();
            var users = userDao.Paging(searchString, pageNumber, pageSize);
            ViewBag.SearchString = searchString;
            return View(users);
        }


        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create() {
            return View();
        }
        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                userDao = new UserDao();
                // checking whether username is already taken
                var existence = userDao.GetByUserName(user.UserName);
                if (existence != null)
                {
                    ModelState.AddModelError("", "This username has been used, please choose another!");
                    return View("Create");
                }
                var hashPassword = Encryptor.MD5Hash(user.Password);
                user.Password = hashPassword;
                var rs = userDao.Insert(user);
                if (rs > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Insert failed!");
                }
            }
            return View("Create");
        }
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            userDao = new UserDao();
            var user = userDao.GetById(id);
            // check user is null

            return View(user);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userDao = new UserDao();
                    var rs = userDao.Update(user);
                    if (rs)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update failed. Please check the contents!");
                    }
                }
                return View("Edit");
            }
            catch
            {
                return View();
            }
        }

        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Delete(int id)
        {
            userDao = new UserDao();
            var rs = userDao.Delete(id);
            if (rs)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Details(int id)
        {
            userDao = new UserDao();
            var user = userDao.GetById(id);
            return View(user);
        }
    }
}