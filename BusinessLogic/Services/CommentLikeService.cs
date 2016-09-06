using BusinessLogic.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories;
using BusinessEntities.Entities;
using BusinessEntities.CustomTypes.CommentDisplay;

namespace BusinessLogic.Services
{
    public class CommentLikeService : BaseService
    {
        public CommentLikeService(Repositories repositories) : base(repositories)
        {
        }

        public bool UserLikesPost(int userId, long postId)
        {
            return Repositories.CommentLikeRepository.UserLikesPost(userId, postId);
        }

        public void AddLike(Like like)
        {
            if (!UserLikesPost(like.IdUser, like.IdPost))
                Repositories.CommentLikeRepository.AddLike(like);
        }

        public void DeleteLike(Like like)
        {
            if (UserLikesPost(like.IdUser, like.IdPost))
                Repositories.CommentLikeRepository.DeleteLike(like);
        }

        public void AddComment(Comment comment)
        {
            Repositories.CommentLikeRepository.AddComment(comment);
        }

        public List<DisplayCommentModel> GetComments(int postId, int pageNumber, int resultsPerPage)
        {
            return Repositories.CommentLikeRepository.GetComments(postId, pageNumber, resultsPerPage);
        }

        public DisplayCommentModel GetLatestComment(int postId)
        {
            return Repositories.CommentLikeRepository.GetLatestComment(postId);
        }

    }
}
