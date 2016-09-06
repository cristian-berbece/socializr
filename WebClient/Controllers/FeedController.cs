using BusinessEntities.CustomTypes.PostDisplay;
using BusinessEntities.Enums;
using Socializr.Code.Attributes;
using Socializr.Code.Base;
using Socializr.Code.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socializr.Controllers
{
    [AuthorizePermissions(Permissions.AccessPublicApp)]
    public class FeedController : BaseController
    {
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
                    return RedirectToAction("Feed", new { pageNumber = 1 });
                else
                    return View("UserHasNoPosts");
            }
            return View(pager);
        }

        [HttpGet]
        public ActionResult GetFeedPosts(int pageNumber = 1)
        {
            var viewerId = Session.CurrentUser.IdUser;
            var postList = Services.PostService.GetFeedForViewer(viewerId, pageNumber, Config.ResultNumber);

            var resultList = postList.Select(p => new
            {
                body = p.Body,
                idPost = p.IdPost,
                title = p.Title,
                idAuthor = p.IdUser,
                authorName = p.AuthorFullName,
                date = p.CreatedOn.ToShortDateString().ToString(),
                likeCount = p.LikeCount,
                isLiked = p.IsLikedByCurrentUser,
            })
            .ToList();

            return Json(resultList, JsonRequestBehavior.AllowGet);

        }


    }
}