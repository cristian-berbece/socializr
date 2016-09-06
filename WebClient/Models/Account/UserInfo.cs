using BusinessEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Socializr.Models
{
    public class UserInfo
    {
        public int IdUser { get; set; }
        public int? IdCity { get; set; }
        public long? IdProfilePhoto { get; set; }
        public string Email { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? IsMale { get; set; }
        public bool IsPublic { get; set; }

        public List<Permissions> Permissions { get; set; }
    }
}