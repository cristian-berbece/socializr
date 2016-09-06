using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Entities;
using System.Data.Entity;
using BusinessEntities.CustomTypes;
using BusinessEntities.CustomTypes.CommentDisplay;
using BusinessEntities.CustomTypes.PostDisplay;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class PostRepository : BaseRepository
    {
        public PostRepository(SocializrContext context) : base(context)
        {
        }

        #region Post Crud

        public void AddPost(Post post)
        {
            Context.Posts.Add(post);
            Context.SaveChanges();
        }

        public Post GetPost(int postId)
        {
            return Context.Posts
                 .AsNoTracking()
                 .Include(post => post.Comments)
                 .Where(post => post.IdPost == postId)
                 .SingleOrDefault();
        }

        public void EditPost(Post editedPost)
        {
            var oldPost = Context.Posts
                .Where(post => post.IdPost == editedPost.IdPost)
                .SingleOrDefault();

            if (oldPost == null)
                return;

            oldPost.Title = editedPost.Title;
            oldPost.Body = editedPost.Body;
            oldPost.IsPublic = editedPost.IsPublic;

            Context.SaveChanges();
        }

        /*
      * I am deleting a Post, which has some "one to many" relationships: Likes and Comments
      * So I will first delete those, and then I will delete the post object
      * Hope it works
      */
        public void DeletePost(int idPost)
        {
            var postToDelete = Context.Posts
                .Where(post => post.IdPost == idPost)
                .SingleOrDefault();

            if (postToDelete == null)
            {
                return;
            }

            Context.Comments
                .RemoveRange(Context.Comments
                .Where(comm => comm.IdPost == idPost));

            Context.Likes
                .RemoveRange(Context.Likes
                .Where(like => like.IdPost == idPost));

            Context.Posts.Remove(postToDelete);
            Context.SaveChanges();
        }




        #endregion

        /// <summary>
        /// Returns the total number of posts by an author that the viewer may see.
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="viewerId"></param>
        /// <returns></returns>
        public int GetPostCountByUser(int authorId, int viewerId)
        {
            Expression<Func<Post, bool>> whereClause =
               post => post.IdUser == authorId &&
                  (
                      post.IdUser == viewerId ||
                      post.IsPublic == true ||
                      post.Author.InRelationships.Where(relation => relation.IdUser2 == viewerId).Any()
                  );

            return Context.Posts
               .AsNoTracking()
               .Include(p => p.Author.InRelationships)
               .Where(whereClause)
               .Count();
        }

        /// <summary>
        /// Returns the number of posts a user has written
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        public int GetPostCount(int authorId)
        {
            return Context.Posts
               .AsNoTracking()
               .Where(post => post.IdUser == authorId)
               .Count();
        }

        /// <summary>
        /// Returns a list containing summaries of all posts written by an author, with pagination
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="pageNumber"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        public List<DisplayPostListModel> GetPostList(int idUser, int pageNumber, int resultsPerPage)
        {
            return Context.Posts
               .AsNoTracking()
               .Where(post => post.IdUser == idUser)
               .OrderByDescending(post => post.CreatedOn)
               .Skip((pageNumber - 1) * resultsPerPage)
               .Take(resultsPerPage)
               .Select(post => new DisplayPostListModel()
               {
                   CreatedOn = post.CreatedOn,
                   IdPost = post.IdPost,
                   IdUser = post.IdUser,
                   Title = post.Title
               }).ToList();
        }

        /// <summary>
        /// Returns the display model for one post, identified by id
        /// Tells whether the viewer likes the post or not
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="viewerId"></param>
        /// <returns></returns>
        public DisplayPostModel GetPostDisplay(int postId, int viewerId)
        {
            return Context.Posts
                .AsNoTracking()
                .Include(post => post.Comments)
                .Where(post => post.IdPost == postId)
                .Select(post => new DisplayPostModel()
                {
                    Body = post.Body,
                    Title = post.Title,
                    IdUser = post.IdUser,
                    IdPost = post.IdPost,
                    CreatedOn = post.CreatedOn,
                    IsPublic = post.IsPublic,
                    IdMedia = post.IdMedia,
                    LikeCount = post.Likes.Count(),
                    Comments = post.Comments.OrderByDescending(c => c.CreatedOn).Take(10).ToList(),
                    AuthorFullName = post.Author.FName + " " + post.Author.LName,
                    IsLikedByCurrentUser = post.Likes.Where(like => like.IdUser == viewerId).Any()
                })
                .SingleOrDefault();
        }

        /// <summary>
        /// Returns a paginated list of all the posts written by the specified author, on the condition that the viewer has access to these posts
        /// Current viewing conditions( with "OR" between them) : 
        ///     1. Author and viewer are same person
        ///     2. Post is public 
        ///     3. Author and viewer are friends
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="viewerId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        public List<DisplayPostModel> GetFeedByAuthor(int authorId, int viewerId, int pageNumber, int resultsPerPage)
        {
            Expression<Func<Post, bool>> whereClause =
                post => post.IdUser == authorId &&
                   (
                       post.IdUser == viewerId ||
                       post.IsPublic == true ||
                       post.Author.InRelationships.Where(relation => relation.IdUser2 == viewerId).Any()
                   );

            var posts = Context.Posts
                .AsNoTracking()
                .Include(post => post.Comments)
                .Include(post => post.Author.InRelationships)
                .Where(whereClause)
                .OrderByDescending(post => post.CreatedOn)
                .Skip((pageNumber - 1) * resultsPerPage)
                .Take(resultsPerPage);

            return posts.Select(post => new DisplayPostModel()
            {
                Body = post.Body,
                Title = post.Title,
                IdUser = post.IdUser,
                IdPost = post.IdPost,
                CreatedOn = post.CreatedOn,
                IsPublic = post.IsPublic,
                IdMedia = post.IdMedia,
                LikeCount = post.Likes.Count(),
                //Comments = post.Comments.OrderByDescending(c => c.CreatedOn).Take(10).ToList(),
                AuthorFullName = post.Author.FName + " " + post.Author.LName,
                IsLikedByCurrentUser = post.Likes.Where(like => like.IdUser == viewerId).Any()
            })
            .ToList();
        }

        public List<DisplayPostModel> GetFeedForViewer(int viewerId, int pageNumber, int resultNumber)
        {
            Expression<Func<Post, bool>> whereClause = post =>
                post.IdUser != viewerId &&
                (
                post.IsPublic == true ||
                post.Author.InRelationships.Where(relation => relation.IdUser2 == viewerId).Any()
                );

            var posts = Context.Posts
                .AsNoTracking()
                .Include(post => post.Comments)
                .Include(post => post.Author.InRelationships)
                .Include(post => post.Likes)
                .Where(whereClause)
                .OrderByDescending(post => post.CreatedOn)
                .OrderByDescending(post => post.Likes.Count())
                .Skip((pageNumber - 1) * pageNumber)
                .Take(resultNumber);

            return posts.Select(post => new DisplayPostModel()
            {
                Body = post.Body,
                Title = post.Title,
                IdUser = post.IdUser,
                IdPost = post.IdPost,
                CreatedOn = post.CreatedOn,
                IsPublic = post.IsPublic,
                IdMedia = post.IdMedia,
                LikeCount = post.Likes.Count(),
                //Comments = post.Comments.OrderByDescending(c => c.CreatedOn).Take(1).ToList(),
                AuthorFullName = post.Author.FName + " " + post.Author.LName,
                IsLikedByCurrentUser = post.Likes.Where(like => like.IdUser == viewerId).Any()
            })
            .ToList();
        }
    }
}
