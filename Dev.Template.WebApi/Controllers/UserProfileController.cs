
namespace Dev.Template.WebApi;
[ApiController]
[Route("api/[controller]")]
public class UserProfileController : GenController<UserProfile>
{
    public UserProfileController(IRepositoriesBase<UserProfile> Repositories) : base(Repositories)
    {
    }
}
