using System;
using System.Collections.Generic;

namespace BusinessEntities.Entities
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
        }

        public long IdPost { get; set; }
        public int IdUser { get; set; }
        public long? IdMedia { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsPublic { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual User Author { get; set; }
    }
}
