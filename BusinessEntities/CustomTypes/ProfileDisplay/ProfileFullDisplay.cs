using BusinessEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.CustomTypes.ProfileDisplay
{
    public class ProfileFullDisplay
    {
        public int IdUser { get; set; }
        public long? IdProfilePhoto { get; set; }
        public string Email { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? IsMale { get; set; }
        public List<string> Interests { get; set; }
        public string CityName { get; set; }
        public string CountyName { get; set; }
        
        public RelationshipStatus RelationshipStatus { get; set; }
    }
}
