using BusinessEntities.CustomTypes;
using BusinessEntities.CustomTypes.PostDisplay;
using BusinessEntities.Entities;
using Socializr.Code.Base;
using Socializr.Models.Comment;
using Socializr.Models.Post;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Socializr.Controllers
{
    public class CommentController : BaseController
    {
      
        [HttpPost]
        public ActionResult AddComment(CreateCommentModel model)
        {
            var comment = new Comment()
            {
                Body = model.Body,
                CreatedOn = DateTime.Now,
                IdPost = model.IdPost,
                IdUser = Session.CurrentUser.IdUser,
            };

            Services.CommentLikeService.AddComment(comment);
            return Json(new { success = true, responseText = "Comment successfuly sent!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetLatestComment(int postId)
        {
            var c = Services.CommentLikeService.GetLatestComment(postId);
            var result = new
            {
                idAuthor = c.IdUser,
                body = c.Body,
                date = c.CreatedOn.ToShortDateString(),
                authorName = c.AuthorFullName
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult GetComments(int postId, int pageNumber = 1)
        {
            var comments = Services.CommentLikeService.GetComments(postId, pageNumber, Config.CommentsPerLoad);
            //comments.Reverse();
            var result = comments.Select(c => new
            {
                idAuthor = c.IdUser,
                body = c.Body,
                date = c.CreatedOn.ToShortDateString(),
                authorName = c.AuthorFullName
            }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}