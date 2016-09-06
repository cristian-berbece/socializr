using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Socializr.Code.Base;
using Socializr.Code.Wrappers;
using Socializr.Code.Attributes;
using BusinessEntities.Enums;

namespace Socializr.Controllers
{
    [AuthorizePermissions(Permissions.AccessPublicApp)]
    public class HomeController : BaseController
    {
        
        public ActionResult Index()
        {
            //sadasd
            return View();
        }

        [HttpGet]
        public ActionResult AdminMenu()
        {
            return View();
        }
    }
}