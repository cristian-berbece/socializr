using BusinessEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Repositories
    {
        private SocializrContext context;

        public Repositories()
        {
            this.context = new SocializrContext();
        }

        private UserRepository users;
        public UserRepository UserRepository
        {
            get
            {
                return users ?? (users = new UserRepository(context));
            }
        }

        private LocalitiesRepository localities;
        public LocalitiesRepository LocalitiesRepository
        {
            get
            {
                return localities ?? (localities = new LocalitiesRepository(context));
            }
        }

        private InterestRepository interests;
        public InterestRepository InterestRepository
        {
            get
            {
                return interests ?? (interests = new InterestRepository(context));
            }
        }

        private PostRepository posts;
        public PostRepository PostRepository
        {
            get
            {
                return posts ?? (posts = new PostRepository(context));
            }
        }

        private FriendshipRepository friends;
        public FriendshipRepository FriendshipRepository
        {
            get
            {
                return friends ?? (friends = new FriendshipRepository(context));
            }
        }


        private CommentLikeRepository commentsAndLikes;
        public CommentLikeRepository CommentLikeRepository
        {
            get
            {
                return commentsAndLikes ?? (commentsAndLikes = new CommentLikeRepository(context));
            }
        }
    }
}
