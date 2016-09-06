using BusinessEntities.CustomTypes;
using BusinessEntities.CustomTypes.CommentDisplay;
using BusinessEntities.Entities;
using Socializr.Code.Base;
using Socializr.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socializr.Controllers
{
    public class ApiController : BaseController
    {
        public ActionResult IsEmailAvailable(string Email)
        {
            return Json(Services.UserService.IsEmailAvailable(Email), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Return a JSON which contains of arrays of elements which will be consumed by Select2
        /// The data fields will be configured just as Select 2 likes them
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetCitiesByCountyId(int id)
        {
            var cities = Services.LocalitiesService.GetCitiesByCountyId(id);
            var citiesData = cities.Select(city => new
            {
                id = city.IdCity,
                text = city.Name
            });

            return Json(citiesData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInterests()
        {
            var list = Services.InterestService.GetAllInterests()
                .Select(i => new
                {
                    id = i.IdInterest,
                    text = i.Name
                });

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SearchInterests(string searchTerm)
       {
            var list = Services.InterestService.SearchInterests(searchTerm)
                .Select(i => new
                {
                    id = i.IdInterest,
                    text = i.Name
                });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddLike(int idPost)
        {
            var like = new Like()
            {
                CreatedOn = DateTime.Now,
                IdPost = idPost,
                IdUser = Session.CurrentUser.IdUser,
            };
            Services.CommentLikeService.AddLike(like);
            return Json(new { success = true, responseText = "Like successfuly sent!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteLike(int idPost)
        {
            var like = new Like()
            {
                IdPost = idPost,
                IdUser = Session.CurrentUser.IdUser,
            };
            Services.CommentLikeService.DeleteLike(like);


            return Json(new { success = true, responseText = "Like successfuly sent!" }, JsonRequestBehavior.AllowGet);
        }

    }
}