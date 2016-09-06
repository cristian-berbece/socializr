using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.CustomTypes.PostDisplay
{
    public class DisplayPostListModel
    {
        public long IdPost { get; set; }
        public int IdUser { get; set; }

        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
