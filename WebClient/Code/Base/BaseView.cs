using Socializr.Code.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socializr.Code.Base
{
    public abstract class BaseView<TModel> : WebViewPage<TModel>
    {
        protected new SessionWrapper Session { get { return SessionWrapper.Instance; } }
        protected ConfigWrapper Config { get { return ConfigWrapper.Instance; } }
    }
}