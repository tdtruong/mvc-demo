using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop2.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleID { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (UserLogin)HttpContext.Current.Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return false;
            }
            var credential = this.GetUserCredential(session.UserName);
            if ((credential != null && credential.Contains(this.RoleID)) || session.GroupID == Constants.ADMIN_GROUP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var session = (UserLogin)HttpContext.Current.Session[CommonConstants.USER_SESSION];
            if (session != null && (session.GroupID == Constants.ADMIN_GROUP || session.GroupID == Constants.MOD_GROUP))
            {
                filterContext.Result = new ViewResult { ViewName = "~/Areas/Admin/Views/Shared/401.cshtml" };
            } else
            {
                filterContext.Result = new ViewResult { ViewName = "~/Views/Error/401.cshtml" };
            }
        }

        private List<string> GetUserCredential(string userName)
        {
            var credential = (List<string>)HttpContext.Current.Session[CommonConstants.CREDENTIAL_SESSION];
            return credential;
        }
    }
}