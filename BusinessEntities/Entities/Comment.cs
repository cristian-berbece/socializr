using System;

namespace BusinessEntities.Entities
{
    public partial class Comment
    {
        public long IdComment { get; set; }
        public long IdPost { get; set; }
        public int IdUser { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
