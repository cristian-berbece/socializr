using DataAccess.Repositories;

namespace BusinessLogic.Services.Base
{
    public class BaseService
    {
        protected Repositories Repositories;

        public BaseService(Repositories repositories)
        {
            Repositories = repositories;
        }
    }
}
