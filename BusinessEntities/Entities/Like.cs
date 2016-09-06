using System;

namespace BusinessEntities.Entities
{
    public partial class Like
    {
        public long IdPost { get; set; }
        public int IdUser { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
