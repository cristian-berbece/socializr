using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : Base.BaseRepository where T : class
    {
        private DbSet<T> Set;

        public GenericRepository(SocializrContext context) : base(context)
        {
             Set = Context.Set<T>();
        }

        public T GetFirst()
        {
            return Set.FirstOrDefault();
        }

        public List<T> GetAll()
        {
            return Set.ToList();
        }
    }
}
