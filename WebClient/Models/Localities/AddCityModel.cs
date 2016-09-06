using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Socializr.Models.Localities
{
    public class AddCityModel
    {
        [Required]
        public int CountyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}