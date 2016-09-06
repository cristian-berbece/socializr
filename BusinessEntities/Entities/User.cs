using System;
using System.Collections.Generic;

namespace BusinessEntities.Entities
{
    public partial class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
            OutRelationships = new HashSet<Relationship>();
            InRelationships = new HashSet<Relationship>();
            RequestedRelationships = new HashSet<FriendRequest>();
            ReceivedRelationships = new HashSet<FriendRequest>();
            Interests = new HashSet<Interest>();
            Likes = new HashSet<Like>();
            Medias = new HashSet<Media>();
            Posts = new HashSet<Post>();
        }

        public int IdUser { get; set; }
        public int? IdCity { get; set; }
        public long? IdProfilePhoto { get; set; }
        public string Email { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Password { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? IsMale { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Relationship> OutRelationships { get; set; }
        public virtual ICollection<Relationship> InRelationships { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<FriendRequest> RequestedRelationships { get; set; }
        public virtual ICollection<FriendRequest> ReceivedRelationships { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Media> Medias { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual Media ProfilePhoto { get; set; }
        public virtual City City { get; set; }

    }
}
