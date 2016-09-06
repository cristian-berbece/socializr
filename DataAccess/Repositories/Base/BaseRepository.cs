namespace DataAccess.Repositories.Base
{
   public class BaseRepository
    {
        protected SocializrContext Context;

        public BaseRepository(SocializrContext context)
        {
            this.Context = context;
        }
    }
}
