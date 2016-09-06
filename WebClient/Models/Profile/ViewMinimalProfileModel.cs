using BusinessEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Socializr.Models.Profile
{
    public class ViewMinimalProfileModel
    {
        public int IdUser { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public RelationshipStatus RelationshipStatus { get; set; }
        public long? IdProfilePhoto { get; set; }

    }
}