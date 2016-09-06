using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socializr.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote("IsEmailAvailable", "Api",ErrorMessage ="Email not avaialble")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Password { get; set; }


    }
}