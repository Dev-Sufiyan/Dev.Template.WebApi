using Dev.Template.Model.Entity;
using Dev.Template.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Template.WebApi;
[ApiController]
[Route("api/[controller]")]
public class CountController : GenController<Count>
{
    public CountController(IRepositoriesBase<Count> Repositories) : base(Repositories)
    {
    }
}