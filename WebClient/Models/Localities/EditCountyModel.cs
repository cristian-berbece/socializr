using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Socializr.Models.Localities
{
    public class EditCountyModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(2)]
        public string ShortName { get; set; }

        [Required]
        public int CountyId { get; set; }
    }
}