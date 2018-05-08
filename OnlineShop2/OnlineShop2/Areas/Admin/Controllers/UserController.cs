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
        // GET: Admin/User
        public ActionResult Index()
        {
            userDao = new UserDao();
            var users = userDao.GetAll();
            return View(users);
        }

        [HttpGet]
        public ActionResult Create() {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                userDao = new UserDao();
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
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            userDao = new UserDao();
            var user = userDao.GetById(id);
            // check user is null

            return View(user);
        }

        [HttpPost]
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
    }
}