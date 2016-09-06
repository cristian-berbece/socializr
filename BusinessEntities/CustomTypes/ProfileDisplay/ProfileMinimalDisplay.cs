using BusinessEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.CustomTypes.ProfileDisplay
{
    class ProfileMinimalDisplay
    {
        public int IdUser { get; set; }
        public long? IdProfilePhoto { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        public RelationshipStatus RelationshipStatus { get; set; }
    }
}
