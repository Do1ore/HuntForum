using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFuse.Areas.Identity.Data;
using ProjectFuse.Models;
using ProjectFuse.Models.Forum;

namespace ProjectFuse.Controllers;

// TODO Views, Improve logics
public class ForumController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<ProjectOneUser> _userManager;

    public ForumController(AppDbContext context, UserManager<ProjectOneUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var topics = _context.Topics.ToList();
        var topicViewList = new List<TopicView>();
        foreach (var topic in topics)
        {
            topicViewList.Add(new TopicView
            {
                Title = topic.Title,
                Content = topic.Content,
                CreatedAt = topic.CreatedAt,
                ForumMessagesCount =
                    (long)_context.ForumMessages?.Where(m => m.TopicId == topic.TopicId).ToList().Count!,
                AuthorName = _userManager.Users
                    .Where(u => u.Id == topic.UserId)
                    .Select(u => u.UserName)
                    .FirstOrDefault()
            });
        }

        return View(topicViewList);
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
            topic.CreatedAt = DateTime.UtcNow;
            topic.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
            PostedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
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

    [HttpGet]
    public async Task<IActionResult> TopicDetails(int? id)
    {
        var topic = await _context.Topics.FirstOrDefaultAsync(i => i.TopicId == id);
        var topicViewModel = new TopicView
        {
            Title = topic?.Title,
            Content = topic?.Content,
            CreatedAt = topic!.CreatedAt,
            ForumMessagesCount = (long)_context.ForumMessages?.Where(m => m.TopicId == topic.TopicId).ToList().Count!,
            AuthorName = _userManager.Users
                .Where(u => u.Id == topic.UserId)
                .Select(u => u.UserName)
                .FirstOrDefault(),
            Messages = await _context.ForumMessages.Where(i => i.TopicId == id).ToListAsync()
        };
        return View(topicViewModel);
    }
}