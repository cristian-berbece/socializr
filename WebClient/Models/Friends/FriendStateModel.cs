using BusinessEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Socializr.Models.Friends
{
    public class FriendStateModel
    {
        public int IdFirstUser { get; set; }
        public int IdSecondUser { get; set; }
        public RelationshipStatus Status { get; set; }
    }
}