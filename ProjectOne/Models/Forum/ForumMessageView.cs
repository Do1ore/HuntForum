namespace ProjectFuse.Models.Forum;

public class ForumMessageView
{
    public int ForumMessageId { get; set; }
    public string? Message { get; set; }

    public DateTime PostedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long LikeCount { get; set; } = 0;
    public int TopicId { get; set; }

    public UserView? User { get; set; }
}