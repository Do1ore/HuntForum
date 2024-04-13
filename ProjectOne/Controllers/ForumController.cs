using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ProjectFuse.Models;
using ProjectFuse.Models.Forum;

namespace ProjectFuse.Controllers;

// TODO Views, Improve logics
public class ForumController : Controller
{
    private readonly AppDbContext _context;

    public ForumController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult CreateTopic()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTopic(Topic topic)
    {
        if (ModelState.IsValid)
        {
            topic.CreatedAt = DateTime.Now;
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        return View(topic);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMessage(int topicId, string message)
    {
        var forumMessage = new ForumMessage
        {
            TopicId = topicId,
            Message = message,
            PostedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        };

        _context.ForumMessages.Add(forumMessage);
        await _context.SaveChangesAsync();
        return RedirectToAction("TopicDetails", new { id = topicId });
    }
    
    [HttpPost]
    public async Task<IActionResult> LikeMessage(int messageId)
    {
        var message = await _context.ForumMessages.FindAsync(messageId);
        if (message != null)
        {
            message.LikeCount += 1;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("TopicDetails", new { id = message?.TopicId });
    }

    public IActionResult TopicDetails(int? id)
    {
        throw new NotImplementedException();
    }
}