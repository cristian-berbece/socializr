using BusinessEntities.CustomTypes;
using BusinessEntities.CustomTypes.PostDisplay;
using BusinessEntities.Entities;
using BusinessEntities.Enums;
using Socializr.Code.Attributes;
using Socializr.Code.Base;
using Socializr.Code.Pagination;
using Socializr.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Socializr.Controllers
{
    [AuthorizePermissions(Permissions.AccessPublicApp)]
    public class PostController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("ManagePosts");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreatePostModel();
            model.IsPublic = true;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreatePostModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new Post()
                {
                    IdUser = Session.CurrentUser.IdUser,
                    Body = model.Body,
                    Title = model.Title,
                    IsPublic = model.IsPublic,
                };
                //Very wierd hack
                //Done because the DateTime type in C# has better precision than the date time column type in sql
                var now = DateTime.Now;
                var date = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                post.CreatedOn = date;
                Services.PostService.AddPost(post);
            }
            return RedirectToAction("ManagePosts");
        }

        [HttpGet]
        public ActionResult ManagePosts()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List(int pageNumber = 1)
        {
            int resultsPerPage = Config.ResultNumber;
            var id = Session.CurrentUser.IdUser;
            List<DisplayPostListModel> list = Services.PostService.GetPostList(id, pageNumber, resultsPerPage);
            ViewBag.PageNumber = pageNumber;
            return View(list);
        }

        [HttpPost]
        public ActionResult DeletePost(int idPost)
        {
            Services.PostService.DeletePost(idPost);
            return Json(new { success = true, responseText = "Post successfuly deleted!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var post = Services.PostService.GetPost(id);
            if (post == null)
                return HttpNotFound();

            if (post.IdUser != Session.CurrentUser.IdUser)
                return HttpNotFound();

            var model = new EditPostModel()
            {
                Body = post.Body,
                IsPublic = post.IsPublic,
                Title = post.Title,
                PostId = post.IdPost
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditPostModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new Post()
                {
                    Body = model.Body,
                    Title = model.Title,
                    IsPublic = model.IsPublic,
                    IdPost = model.PostId
                };
                Services.PostService.EditPost(post);
            }
            return RedirectToAction("ManagePosts");
        }

        [HttpGet]
        public ActionResult UserPosts(int id, int pageNumber = 1)
        {
            var authorId = id;
            var viewerId = Session.CurrentUser.IdUser;

            var postList = Services.PostService.GetFeedByAuthor(authorId, viewerId, pageNumber, Config.ResultNumber);
            var pager = new Pager<DisplayPostModel>()
            {
                CurrentPageNumber = pageNumber,
                ElementList = postList,
                ResultsPerPage = Config.ResultNumber,
                TotalResultNumber = Services.PostService.GetPostCountByUser(authorId, viewerId)
            };

            if (pager.isEmptyPage())
            {
                if (pager.LastPageNumber > 0)
                    return RedirectToAction("UserPosts", new { id = id, pageNumber = pager.LastPageNumber });

                else
                    return View("UserHasNoPosts");
            }

            return View(pager);
        }

        [HttpGet]
        public ActionResult Feed(int pageNumber = 1)
        {
            var viewerId = Session.CurrentUser.IdUser;
            var postList = Services.PostService.GetFeedForViewer(viewerId, pageNumber, Config.ResultNumber);

            var pager = new Pager<DisplayPostModel>()
            {
                CurrentPageNumber = pageNumber,
                ElementList = postList,
                ResultsPerPage = Config.ResultNumber,
                TotalResultNumber = 100
            };

            if (pager.isEmptyPage())
            {
                if (pager.LastPageNumber > 0)
                    return RedirectToAction("Feed", new { pageNumber = pager.LastPageNumber });
                else
                    return View("UserHasNoPosts");
            }

            return View(pager);
        }

    }
}