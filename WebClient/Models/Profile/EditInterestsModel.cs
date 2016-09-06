using BusinessEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socializr.Models.Profile
{
    public class EditInterestsModel
    {
        public int IdUser { get; set; }

        public List<int> InterestIds { get; set; }
        public List<SelectListItem> InterestList {get; set;}
    }
}