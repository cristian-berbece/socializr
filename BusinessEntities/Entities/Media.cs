using System;

namespace BusinessEntities.Entities
{
    public partial class Media
    {
        public long IdMedia { get; set; }
        public long? IdAlbum { get; set; }
        public int IdUser { get; set; }
        public string Caption { get; set; }
        public byte Type { get; set; }
        public byte[] MediaContent { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Album Album { get; set; }
        public virtual User Author { get; set; }
    }
}
