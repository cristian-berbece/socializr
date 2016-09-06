using System;
using BusinessEntities.Entities;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class Services
    {
        private Repositories repositories;

        public Services()
        {
            repositories = new Repositories();
        }

        private UserService users;
        public UserService UserService
        {
            get
            {
                return users ?? (users = new UserService(repositories));
            }
        }

        private LocalitiesService localities;
        public LocalitiesService LocalitiesService
        {
            get
            {

                return localities ?? (localities = new LocalitiesService(repositories));
            }
        }


        private InterestService interests;
        public InterestService InterestService
        {
            get
            {
                return interests ?? (interests = new InterestService(repositories));
            }
        }

        private PostService posts;
        public PostService PostService
        {
            get
            {
                return posts ?? (posts = new PostService(repositories));
            }
        }

        private FriendshipService friends;
        public FriendshipService FrienshipService
        {
            get
            {
                return friends ?? (friends = new FriendshipService(repositories));
            }
        }

        private  CommentLikeService  commsAndLikes { get; set; }
        public CommentLikeService CommentLikeService
        {
            get
            {
                return commsAndLikes ?? (commsAndLikes = new CommentLikeService(repositories));
            }
        }

    }
}
