﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Socializr.Models.Post
{
    public class EditPostModel
    {
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Body { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public long PostId { get; set; }
    }
}