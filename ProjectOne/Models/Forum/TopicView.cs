namespace ProjectFuse.Models.Forum;

public class TopicView
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public string? AuthorName { get; set; }
    public long ForumMessagesCount { get; set; }

    public ICollection<ForumMessage>? Messages { get; set; }
}