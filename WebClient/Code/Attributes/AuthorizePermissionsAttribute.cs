using BusinessEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socializr.Code.Attributes
{
    public class AuthorizePermissionsAttribute : AuthorizeAttribute
    {
        public AuthorizePermissionsAttribute(params Permissions[] permissions)
        {
            Roles = string.Join(",", permissions);
        }
    }
}