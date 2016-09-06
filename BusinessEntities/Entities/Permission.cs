using System.Collections.Generic;

namespace BusinessEntities.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
        }

        public int IdPermission { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

    }
}
