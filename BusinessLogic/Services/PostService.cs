using BusinessLogic.Services.Base;
using System;
using System.Collections.Generic;
using DataAccess.Repositories;
using BusinessEntities.Entities;
using BusinessEntities.CustomTypes.CommentDisplay;
using BusinessEntities.CustomTypes.PostDisplay;

namespace BusinessLogic.Services
{
    public class PostService : BaseService
    {
        public PostService(Repositories repositories) : base(repositories)
        {
        }

        #region Post CRUD

        public void AddPost(Post post)
        {
            Repositories.PostRepository.AddPost(post);
        }

        public Post GetPost(int postId)
        {
            return Repositories.PostRepository.GetPost(postId);
        }

        public void DeletePost(int idPost)
        {
            Repositories.PostRepository.DeletePost(idPost);
        }

        public void EditPost(Post post)
        {
            Repositories.PostRepository.EditPost(post);
        }

        public int GetPostCount(int authorId)
        {
            return Repositories.PostRepository.GetPostCount(authorId);
        }

        #endregion


        public List<DisplayPostListModel> GetPostList(int idUser, int pageNumber, int resultsPerPage)
        {
            return Repositories.PostRepository.GetPostList(idUser, pageNumber, resultsPerPage);
        }

        public List<DisplayPostModel> GetFeedByAuthor(int authorId, int viewerId, int pageNumber, int resultNumber)
        {
            return Repositories.PostRepository.GetFeedByAuthor(authorId, viewerId, pageNumber, resultNumber);
        }

        public int GetPostCountByUser(int authorId, int viewerId)
        {
            return Repositories.PostRepository.GetPostCountByUser(authorId, viewerId);
        }

        public List<DisplayPostModel> GetFeedForViewer(int viewerId, int pageNumber, int resultNumber)
        {
            return Repositories.PostRepository.GetFeedForViewer(viewerId, pageNumber, resultNumber);


        }
    }
}
