using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.CustomTypes.CommentDisplay
{
    public class DisplayCommentModel
    {
        public int IdUser { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public int IdPost { get; set; }
        public string AuthorFullName { get; set; }
    }
}
