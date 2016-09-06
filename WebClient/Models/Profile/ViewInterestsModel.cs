using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socializr.Models.Profile
{
    public class ViewInterestsModel
    {
        public List<int> InterestIds { get; set; }
        public int IdUser { get; set; }
        public List<SelectListItem> SelectedInterests { get; set; }
    }
}