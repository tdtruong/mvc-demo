using Model.Dao;
using OnlineShop2.Areas.Admin.Models;
using OnlineShop2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop2.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userDao = new UserDao();
                var rs = userDao.Login(model.UserName, Encryptor.MD5Hash(model.Password), true);
                switch (rs)
                {
                    case 1: // login success
                        var user = userDao.GetByUserName(model.UserName);
                        var userLogin = new UserLogin();
                        userLogin.UserID = user.ID;
                        userLogin.UserName = user.UserName;
                        userLogin.GroupID = user.GroupID;
                        var credentials = userDao.GetUserCredential(user.UserName);
                        Session.Add(CommonConstants.CREDENTIAL_SESSION, credentials);
                        Session.Add(CommonConstants.USER_SESSION, userLogin);
                        return RedirectToAction("Index", "Home");
                    case 0: // account is not exist
                        ModelState.AddModelError("", "The account is not exist!");
                        break;
                    case -1: // password is not correct
                        ModelState.AddModelError("", "Password is not correct!");
                        break;
                    case -2: // account is locked
                        ModelState.AddModelError("", "The account is locked, please contact your administrator!");
                        break;
                    case -3: // account is not pervented by permission
                        ModelState.AddModelError("", "You don't have permission to access this area!");
                        break;
                    default:
                        break;
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}