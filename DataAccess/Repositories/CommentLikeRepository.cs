using BusinessEntities.CustomTypes.CommentDisplay;
using BusinessEntities.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
namespace DataAccess.Repositories
{
    public class CommentLikeRepository : BaseRepository
    {
        public CommentLikeRepository(SocializrContext context) : base(context)
        {
        }

        public bool UserLikesPost(int userId, long postId)
        {
            return Context.Likes
                 .Where(like => like.IdUser == userId && like.IdPost == postId)
                 .Any();
        }

        public void AddComment(Comment comment)
        {
            Context.Comments.Add(comment);
            Context.SaveChanges();
        }

        public List<DisplayCommentModel> GetComments(int postId, int pageNumber, int resultsPerPage)
        {
            return Context.Comments
                .AsNoTracking()
                .Include(comm => comm.User)
                .Where(comm => comm.IdPost == postId)
                .OrderByDescending(comm => comm.CreatedOn)
                .Skip((pageNumber - 1) * resultsPerPage)
                 .Take(resultsPerPage)
                 .Select(comm => new DisplayCommentModel()
                 {
                     Body = comm.Body,
                     CreatedOn = comm.CreatedOn,
                     IdUser = comm.IdUser,
                     IdPost = postId,
                     AuthorFullName = comm.User.FName + " " + comm.User.LName
                 }).ToList();
        }

        public DisplayCommentModel GetLatestComment(int postId)
        {
            return Context.Comments
                .AsNoTracking()
                .Include(comm => comm.User)
                .Where(comm => comm.IdPost == postId)
                .OrderByDescending(comm => comm.CreatedOn)
                .Select(comm => new DisplayCommentModel()
                {
                    Body = comm.Body,
                    CreatedOn = comm.CreatedOn,
                    IdUser = comm.IdUser,
                    IdPost = postId,
                    AuthorFullName = comm.User.FName + " " + comm.User.LName
                }).First();

        }

        public void AddLike(Like like)
        {
            Context.Likes.Add(like);
            Context.SaveChanges();
        }

        public void DeleteLike(Like like)
        {
            Context.Likes.Attach(like);
            Context.Likes.Remove(like);
            Context.SaveChanges();
        }

    }
}
