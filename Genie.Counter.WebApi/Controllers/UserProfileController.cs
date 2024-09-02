using Genie.Counter.Model.Entity;
using Genie.Counter.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Genie.Counter.WebApi;
[ApiController]
[Route("api/[controller]")]
public class UserProfileController : BaseController<UserProfile>
{
    public UserProfileController(IRepositoryBase<UserProfile> repository) : base(repository)
    {
    }
}
