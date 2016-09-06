using System;
using System.Collections.Generic;

namespace BusinessEntities.Entities
{
    public partial class City
    {
        public City()
        {
            Users = new HashSet<User>();
        }

        public int IdCity { get; set; }
        public int IdCounty { get; set; }
        public string Name { get; set; }

        public virtual County County { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
