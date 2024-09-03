using Genie.Counter.DBContext;
using Genie.Counter.Model.Entity;

namespace Genie.Counter.Repository
{
    public class CountHistoryRepository : RepositoryBase<CountHistory>, ICountHistoryRepository
    {
        public CountHistoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
