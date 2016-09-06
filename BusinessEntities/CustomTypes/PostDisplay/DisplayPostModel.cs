using BusinessEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities.CustomTypes.PostDisplay
{
    public class DisplayPostModel
    {
        public long IdPost { get; set; }
        public int IdUser { get; set; }

        public string AuthorFullName { get; set; }

        public long? IdMedia { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsPublic { get; set; }

        public List<Comment> Comments { get; set; }
        public int LikeCount { get; set; }

        public bool? IsLikedByCurrentUser { get; set; }
    }
}