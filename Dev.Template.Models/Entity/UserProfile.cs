using System.ComponentModel.DataAnnotations;
namespace Dev.Template.Model.Entity;
public class UserProfile
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MobileNo { get; set; }
}


