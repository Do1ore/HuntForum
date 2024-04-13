using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using ProjectFuse.Areas.Identity.Data;

namespace ProjectFuse.Models.Forum;

public class Topic
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TopicId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string? UserId { get; set; } 
    
    public ICollection<ForumMessage>? Messages { get; set; }
}