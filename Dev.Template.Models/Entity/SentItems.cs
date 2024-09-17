namespace Dev.Template.Model.Entity;
public class SentItems
{
    public int ProfileId { get; set; }
    public DateTime TimeStamp { get; set; }
    public int Count { get; set; }
    public UserProfile? UserProfile { get; set; }
}

