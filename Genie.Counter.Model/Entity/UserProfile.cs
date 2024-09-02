using System.ComponentModel.DataAnnotations;
namespace Genie.Counter.Model.Entity;
public class UserProfile
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MobileNo { get; set; }
}


