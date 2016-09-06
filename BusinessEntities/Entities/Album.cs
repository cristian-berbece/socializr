using System;
using System.Collections.Generic;

namespace BusinessEntities.Entities
{
    public partial class Album
    {
        public Album()
        {
            Medias = new HashSet<Media>();
        }

        public long IdAlbum { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Media> Medias { get; set; }
    }
}
