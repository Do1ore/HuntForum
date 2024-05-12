namespace ProjectFuse.Models.Forum;

public class TopicView
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public string? AuthorName { get; set; }
    public string? AuthorLastName { get; set; }
    public bool HasWeaponLicence { get; set; }
    public long ForumMessagesCount { get; set; }

    public ICollection<ForumMessageView>? Messages { get; set; } = new List<ForumMessageView>();
}