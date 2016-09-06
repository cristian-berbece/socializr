using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Socializr.Models.Comment
{
    public class CreateCommentModel
    {
        public long IdPost { get; set; }
        public int IdUser { get; set; }

        [Required]
        public string Body { get; set; }
    }
}