using Model.Dao;
using Model.EF;
using OnlineShop2.Common;
using OnlineShop2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop2.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var userDao = new UserDao();
                if (userDao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "The user name is exist.");
                }
                else if (userDao.CheckUserEmail(model.Email))
                {
                    ModelState.AddModelError("", "The email is exist.");
                }
                else
                {
                    var user = new User();
                    user.CreatedDate = DateTime.Now;
                    user.GroupID = "MEMBER";
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.Name = model.Name;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Phone = model.Phone;
                    user.Status = true;
                    var userId = userDao.Insert(user);
                    if (userId > 0)
                    {
                        ViewBag.RegisterStatus = "Register successed.";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.RegisterStatus = "Register failed.";
                    }
                }
            }
            return View();
        }
    }
}