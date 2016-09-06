using System.Collections.Generic;

namespace BusinessEntities.Entities
{
    public partial class Interest
    {
        public Interest()
        {
            Users = new HashSet<User>();
        }
        public int IdInterest { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
