using Socializr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Socializr.Code.Wrappers
{
    public class SessionWrapper
    {
        private static SessionWrapper instance;
        public static SessionWrapper Instance
        {
            get
            {
                return instance ?? (instance = new SessionWrapper());
            }
        }

        private SessionWrapper()
        {
        }

        public void Destroy()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        public UserInfo CurrentUser
        {
            get
            {
                return HttpContext.Current.Session["CurrentUser"] as UserInfo;
            }

            set
            {
                HttpContext.Current.Session.Add("CurrentUser", value);
            }
        }
    }
}