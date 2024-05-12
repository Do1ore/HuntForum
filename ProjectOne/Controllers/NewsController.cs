using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFuse.Mapper;
using ProjectFuse.Models;
using ProjectFuse.ViewModels.News;

namespace ProjectFuse.Controllers
{
    [Authorize(Roles = "admin")]
    public class NewsController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<NewsController> _logger;

        public NewsController(AppDbContext dbContext, ILogger<NewsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public async Task<IActionResult> ManageNewsSettings()
        {
            var newSettings = await _dbContext.NewsApiSettings.FirstOrDefaultAsync();
            if (newSettings == null)
            {
                return View(new NewsSettingsViewModel());
            }

            var settingsViewModel = NewsMapper.MapToNewsSettingsViewModel(newSettings);

            return View(settingsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ManageNewsSettings(NewsSettingsViewModel? newsSettings, string? sources,
            string? domains)
        {
            if (newsSettings is null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (sources is not null)
                newsSettings.Sourses = sources;

            if (domains is not null)
            {
                newsSettings.Domains = domains;
            }

            NewsApiSettingsModel toUpdate = new();
            if (!_dbContext.NewsApiSettings.Any())
                await _dbContext.NewsApiSettings.AddAsync(NewsMapper.MapToNewsApiSettingsModel(newsSettings));
            else
            {
                int id = await _dbContext.NewsApiSettings.Select(x => x.Id).FirstAsync();
                toUpdate = NewsMapper.MapToNewsApiSettingsModel(newsSettings);
                toUpdate.Id = id;
                _dbContext.NewsApiSettings.Update(toUpdate);
            }

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("News settings was changed: {@NewsSettings}", toUpdate);
            return RedirectToAction("Index", "Home");
        }
    }
}