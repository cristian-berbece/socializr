using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Socializr.Code.Base;
using Socializr.Models;
using BusinessEntities.Entities;
using Socializr.Code.Wrappers;
using System.Web.Security;
using BusinessEntities.Enums;

namespace Socializr.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Destroy();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = null, string message = null)
        {
            var model = new LoginViewModel();
            model.Message = message;
            model.ReturnUrl = ReturnUrl;
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            else
            {
                var user = Services.UserService.GetByEmailAndPassword(model.Email, model.Password);
                if (user != null)
                {
                    SaveUserInSession(user);
                    return RedirectAfterLogIn(model.ReturnUrl);
                }
                else
                {
                    model.BadCredentials = true;
                    return View(model);
                }
            }
        }

        /// <summary>
        /// Takes the returnUrl from the LogIn model and decides where to redirect the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ActionResult RedirectAfterLogIn(string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            else
            {
                return Redirect(returnUrl);
            }
        }

        /// <summary>
        /// Takes the User entity and saves it in the session
        /// Assumes that the user entity is valid and exists in the database
        /// DOES NOT perform validation of the user object
        /// </summary>
        /// <param name="model"></param>
        private void SaveUserInSession(User user)
        {
            var userInfo = new UserInfo()
            {
                IdUser = user.IdUser,
                Email = user.Email,
                FName = user.FName,
                LName = user.LName,
                Permissions = user.Roles
                            .SelectMany(u => u.Permissions.Select(p => (Permissions)p.IdPermission))
                            .ToList()

            };
            Session.CurrentUser = userInfo;
            FormsAuthentication.SetAuthCookie(user.Email, true);
        }

        [HttpGet]
        public ActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Email = model.Email,
                    FName = model.FirstName,
                    LName = model.LastName,
                    Password = model.Password,
                };

                Services.UserService.RegisterPublicUser(user);
                return RedirectToAction("Login", "Account", new { message = "Welcome to SocializR. Please log in" });
            }
            else
            {
                return View(model);
            }
        }


    }
}