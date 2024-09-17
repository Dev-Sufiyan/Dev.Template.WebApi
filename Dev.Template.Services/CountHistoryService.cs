using Dev.Template.Repositories;
using Genesis.Repositories;
using Dev.Template.Model.Entity;
using Dev.Template.DBContext;

namespace Dev.Template.Repositories
{
    public class CountHistoryRepositories : RepositoriesBase<CountHistory>, ICountHistoryRepositories
    {
        public CountHistoryRepositories(AppDbContext context) : base(context)
        {
        }
    }
}
