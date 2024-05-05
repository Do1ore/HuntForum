using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectFuse.Models;
using ProjectFuse.ViewModels.News;
using RestSharp;

namespace ProjectFuse.Services;

public interface INewsParametres
{
    const int MaxSourses = 20;
    const int MaxPageSize = 100;
    const int MaxSearchTermLenght = 500;
}

public class NewsManagerService : INewsParametres
{
    private const string NewsApiKey = "44ac292571054b6787f228908e3cc15b";
    private readonly AppDbContext _db;

    public NewsManagerService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Article?>?> GetNewsAsync()
    {
        RestClient client = new("https://newsapi.org/v2/everything");
        NewsViewModel newsViewModel = new();

        List<Article?> articles = new();
        RestRequest request = new($"https://newsapi.org/v2/everything");
        request.AddHeader("X-Api-Key", NewsApiKey);

        if (!_db.NewsApiSettings.Any()) return newsViewModel.Articles;

        var newsSettings = await _db.NewsApiSettings.Select(x => x).FirstOrDefaultAsync();
        if (newsSettings is null)
            return new List<Article?>();

        if (newsSettings?.Sourses is not null)
        {
            request.AddParameter("sourses", newsSettings.Sourses);
        }

        if (newsSettings?.Domains is not null)
        {
            request.AddParameter("domains", newsSettings.Domains);
        }

        if (newsSettings?.DateFrom is not null)
        {
            request.AddParameter("from", newsSettings.DateFrom.Value.ToString("o"));
        }

        if (newsSettings?.DateTo is not null)
        {
            request.AddParameter("to", newsSettings.DateTo.Value.ToString("o"));
        }

        if (newsSettings?.Language is not null)
        {
            request.AddParameter("language", newsSettings.Language);
        }

        if (newsSettings?.PageSize != null || newsSettings?.PageSize >= INewsParametres.MaxPageSize ||
            newsSettings?.PageSize == 0)
        {
            request.AddParameter("pageSize", $"{INewsParametres.MaxPageSize}");
        }
        else
        {
            request.AddParameter("pageSize", $"{newsSettings?.PageSize}");
        }

        if (newsSettings?.SearchTerm is not null)
        {
            request.AddParameter("q", $"{newsSettings.SearchTerm}");
        }
        else request.AddParameter("pageSize", $"{INewsParametres.MaxPageSize}");

        RestResponse? response = await client.ExecuteAsync(request);

        if (response.ResponseStatus.ToString() == "Error")
        {
            return new List<Article?>();
        }

        if (response.Content != null)
        {
            NewsViewModel? news = JsonConvert.DeserializeObject<NewsViewModel?>(response.Content);
            if (news == null)
            {
                return new List<Article?>();
            }

            newsViewModel.Articles = articles;

            if (news.Articles != null)
                foreach (var item in news.Articles)
                {
                    newsViewModel.Articles.Add(item);
                }
        }

        return newsViewModel.Articles;
    }
}