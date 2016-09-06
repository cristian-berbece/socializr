using System;
using System.Collections.Generic;

namespace BusinessEntities.Entities
{
    public partial class County
    {
        public County()
        {
            Cities = new HashSet<City>();
        }

        public int IdCounty { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
