using BotDetect.Web.Mvc;
using Facebook;
using Model.Dao;
using Model.EF;
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
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Incorrect CAPTCHA code!")]
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userDao = new UserDao();
                var rs = userDao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                switch (rs)
                {
                    case 1: // login success
                        var user = userDao.GetByUserName(model.UserName);
                        var userLogin = new UserLogin();
                        userLogin.UserID = user.ID;
                        userLogin.UserName = user.UserName;
                        Session.Add(CommonConstants.USER_SESSION, userLogin);
                        return Redirect("/");
                    case 0: // account is not exist
                        ModelState.AddModelError("", "The account is not exist!");
                        break;
                    case -1: // password is not correct
                        ModelState.AddModelError("", "Password is not correct!");
                        break;
                    case -2: // account is locked
                        ModelState.AddModelError("", "The account is locked, please contact your administrator!");
                        break;
                    default:
                        break;
                }
            }
            return View(model);
        }

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string firstName = me.first_name;
                string middleName = me.middle_name;
                string lastName = me.last_name;

                var user = new User();
                user.UserName = email;
                user.Email = email;
                user.CreatedDate = DateTime.Now;
                user.Name = firstName + " " + middleName + " " + lastName;
                user.Status = true;
                user.GroupID = "MEMBER";
                user.Phone = "0123456789";
                user.Password = Encryptor.MD5Hash("123456");
                var userId = new UserDao().InsertForFacebook(user);
                if (userId > 0)
                {
                    var userLogin = new UserLogin();
                    userLogin.UserID = user.ID;
                    userLogin.UserName = user.UserName;
                    Session.Add(CommonConstants.USER_SESSION, userLogin);
                }
            }
            return Redirect("/");
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }
    }
}