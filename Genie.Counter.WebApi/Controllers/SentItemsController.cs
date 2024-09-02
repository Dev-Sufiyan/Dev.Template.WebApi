using Genie.Counter.Model.Entity;
using Genie.Counter.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Genie.Counter.WebApi;
[ApiController]
[Route("api/[controller]")]
public class SentItemsController : BaseController<SentItems>
{
    public SentItemsController(IRepositoryBase<SentItems> repository) : base(repository)
    {
    }
}
