using BusinessEntities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socializr.Models.Profile
{
    public class EditProfileModel
    {
        public int IdUser { get; set; }

        [Display(Name = "City")]
        public int? IdCity { get; set; }

        [Display(Name = "County")]
        public int? IdCounty { get; set; }

        public long? IdProfilePhoto { get; set; }

        [Display(Name = "Sex")]
        public bool? IsMale { get; set; }

        [Display(Name = "Profile privacy")]
        public bool? IsPublic { get; set; }

        public string CityName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Interests")]
        public List<int> InterestIds { get; set; }
        public List<SelectListItem> SelectedInterests { get; set; }


        //Data Storage
        public List<SelectListItem> CountiesSelectItems { get; set; }
        public List<SelectListItem> CitiesSelectItems { get; set; }

    }
}