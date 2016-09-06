using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Socializr.Models.Localities
{
    public class CreateCountyModel
    {
        [Required]
        public string Name { get; set; }

        [MaxLength(2)]
        public string ShortName { get; set; }
    }
}