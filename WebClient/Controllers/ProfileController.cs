using BusinessEntities.Entities;
using BusinessEntities.Enums;
using Socializr.Code.Attributes;
using Socializr.Code.Base;
using Socializr.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socializr.Controllers
{
    [AuthorizePermissions(Permissions.AccessPublicApp)]
    public class ProfileController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("ViewProfile", new { id = Session.CurrentUser.IdUser });
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var userId = Session.CurrentUser.IdUser;
            var user = Services.UserService.GetUserById(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            var model = new EditProfileModel()
            {
                Birthday = user.Birthday,
                //Email = user.Email,
                FName = user.FName,
                LName = user.LName,
                IdCity = user.IdCity,
                IdCounty = user.IdCity == null ? (int?)null : user.City.IdCounty,
                IdUser = user.IdUser,
                IsMale = user.IsMale,
                IsPublic = user.IsPublic,
                CityName = user.City == null ? null : user.City.Name,
                SelectedInterests = Services.InterestService
                    .GetUserInterests(userId)
                    .Select(i => new SelectListItem()
                    {
                        Text = i.Name,
                        Value = i.IdInterest.ToString()
                    })
                    .ToList()
            };

            model.CountiesSelectItems = Services.LocalitiesService
                .GetCounties()
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.IdCounty.ToString()
                })
                .ToList();

            model.CitiesSelectItems = new List<SelectListItem>();
            model.CitiesSelectItems.Add(new SelectListItem()
            {
                Text = model.CityName,
                Value = model.IdCity.ToString(),
            });

            //return Json(model, JsonRequestBehavior.AllowGet);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Birthday = model.Birthday,
                    IdCity = model.IdCity,
                    FName = model.FName,
                    LName = model.LName,
                    IsMale = model.IsMale,
                    IsPublic = model.IsPublic ?? true,
                    IdUser = model.IdUser
                };
                //return Json(model, JsonRequestBehavior.AllowGet);
                Services.UserService.SaveUserChanges(user);
                Services.InterestService.SetInterestList(model.IdUser, model.InterestIds);

            }
            return RedirectToAction("Edit");
        }

        [HttpGet]
        public ActionResult ViewProfile(int id)
        {
            var user = Services.UserService.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (Services.UserService.HasAccessToProfile(id, Session.CurrentUser.IdUser))
            {
                var model = new ViewFullProfileModel()
                {
                    Birthday = user.Birthday,
                    CityName = user.City == null ? string.Empty : user.City.Name,
                    CountyName = user.City == null ? string.Empty : user.City.County.Name,
                    Email = user.Email,
                    FName = user.FName,
                    LName = user.LName,
                    IdUser = user.IdUser,
                    Interests = user.Interests.Select(i => i.Name).ToList(),
                    IdProfilePhoto = user.IdProfilePhoto ?? 0,
                    IsMale = user.IsMale,
                };
                return View("ViewProfile", model);
            }
            else
            {
                var model = new ViewMinimalProfileModel()
                {
                    IdProfilePhoto = user.IdProfilePhoto ?? 0,
                    FName = user.FName,
                    LName = user.LName,
                    IdUser = user.IdUser,
                };
                return View("ViewMinimalProfile", model);
            }
        }

        [HttpGet]
        public ActionResult SearchUsers(string query)
        {
            var list = Services.UserService.SearchUsers(query, Config.ResultNumber)
               .Select(user => new
               {
                   id = user.IdUser,
                   text = user.FName + " " + user.LName
               });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}