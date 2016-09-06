using System;

namespace BusinessEntities.Entities
{
    public partial class Relationship
    {
        public int IdUser1 { get; set; }
        public int IdUser2 { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
    }
}
