using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectFuse.Areas.Identity.Data;

namespace ProjectFuse.Models.Forum;

public class Topic
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TopicId { get; set; }
    [Column(TypeName = "nvarchar(150)")]
    public string? Title { get; set; }
    [Column(TypeName = "nvarchar(1500)")]
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    
    [ForeignKey("ProjectOneUser")]
    public string? UserId { get; set; } 
    public ProjectOneUser? User { get; set; }
    
    public ICollection<ForumMessage>? Messages { get; set; }
}