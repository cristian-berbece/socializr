using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Enums
{
    public enum RelationshipStatus
    {
        IsSameUser = 0,
        NoRelationship = 1,
        RequestSentByFirstUser = 2,
        RequestReceivedByFirstUser = 3,
        Friends = 4,
        NoPossibleRelation = 5
    }
}
