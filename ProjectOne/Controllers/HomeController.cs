using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectFuse.Models;
using ProjectFuse.Services;
using ProjectFuse.ViewModels.News;

namespace ProjectFuse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewsManagerService _newsManagerService;
        private static NewsViewModel? NewsViewModel { get; set; }
        
        public HomeController(ILogger<HomeController> logger, NewsManagerService newsManagerService)
        {
            _logger = logger;
            _newsManagerService = newsManagerService;
        }

        public async Task<IActionResult> Index()
        {
            NewsViewModel = new();
            List<Article?>? news = await _newsManagerService.GetNewsAsync();
            if (news is null)
            {
                NewsViewModel.Articles = new List<Article?>();
            }
            else
            {
                NewsViewModel.Articles = news;
            }
            return View(NewsViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public async Task<IActionResult> NewsDetails(string title)
        {
            var selectedNews = NewsViewModel?.Articles?.FirstOrDefault(i => i?.Title == title) ?? throw new ArgumentNullException("NewsViewModel?.Articles?.FirstOrDefault(i => i?.Title == title)");
            ViewBag.Title = "Новости";
            ViewBag.Secondary = "Детальная информация";

            return View(selectedNews);
        }
        
        public IActionResult AllNews()
        {
            ViewBag.Title = "Новости";
            if (NewsViewModel is null)
            {
                ViewBag.Secondary = "Новостей не найдено";
            }
            else
            {
                if (NewsViewModel.Articles != null) ViewBag.Secondary = "Найдено: " + NewsViewModel.Articles.Count;
            }


            return View(NewsViewModel?.Articles);
        }
    }
}