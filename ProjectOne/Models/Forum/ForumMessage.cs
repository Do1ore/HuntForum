using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectFuse.Areas.Identity.Data;

namespace ProjectFuse.Models.Forum;

[Table("ForumMessage")]
public class ForumMessage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ForumMessageId { get; set; }
    [Column(TypeName = "nvarchar(1500)")]
    public string? Message { get; set; }

    public DateTime PostedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long LikeCount { get; set; } = 0;
    
    [ForeignKey("Topic")]
    public int TopicId { get; set; }
    public Topic? Topic { get; set; }
    
    [ForeignKey("ProjectOneUser")]
    public string? UserId { get; set; }
    public ProjectOneUser? User { get; set; }
}