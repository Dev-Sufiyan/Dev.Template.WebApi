using Genie.Counter.Model.Entity;
using Genie.Counter.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Genie.Counter.WebApi;
[ApiController]
[Route("api/[controller]")]
public class CountController : BaseController<Count>
{
    public CountController(IRepositoryBase<Count> repository) : base(repository)
    {
    }
}
