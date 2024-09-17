using Dev.Template.Model.Entity;
using Dev.Template.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Template.WebApi;
[ApiController]
[Route("api/[controller]")]
public class SentItemsController : GenController<SentItems>
{
    public SentItemsController(IRepositoriesBase<SentItems> Repositories) : base(Repositories)
    {
    }
}
