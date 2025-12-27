using System.Text.Json;
using InvestmentNewsApp.Models;

namespace InvestmentNewsApp.Services;

public class NewsService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://newsapi.org/v2";
    
    public NewsService(string? apiKey = null)
    {
        _httpClient = new HttpClient();
        
        // Use provided API key or demo key
        // Note: Users should get their own API key from https://newsapi.org
        var key = apiKey ?? "demo";
        _httpClient.DefaultRequestHeaders.Add("X-Api-Key", key);
    }
    
    /// <summary>
    /// Fetches investment and business news from multiple sources
    /// </summary>
    public async Task<NewsResponse?> GetInvestmentNewsAsync(int pageSize = 10)
    {
        try
        {
            // Query for investment-related keywords
            var query = "investment OR stocks OR market OR finance OR economy OR trading";
            var url = $"{BaseUrl}/everything?q={Uri.EscapeDataString(query)}&language=en&sortBy=publishedAt&pageSize={pageSize}";
            
            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                return null;
            }
            
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            return JsonSerializer.Deserialize<NewsResponse>(content, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching news: {ex.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// Fetches top business headlines
    /// </summary>
    public async Task<NewsResponse?> GetTopBusinessHeadlinesAsync(string country = "us", int pageSize = 10)
    {
        try
        {
            var url = $"{BaseUrl}/top-headlines?category=business&country={country}&pageSize={pageSize}";
            
            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                return null;
            }
            
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            return JsonSerializer.Deserialize<NewsResponse>(content, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching headlines: {ex.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// Gets mock/sample investment news for demonstration when API key is not available
    /// </summary>
    public NewsResponse GetSampleInvestmentNews()
    {
        return new NewsResponse
        {
            Status = "ok",
            TotalResults = 3,
            Articles = new List<Article>
            {
                new Article
                {
                    Source = new Source { Name = "Financial Times" },
                    Author = "Sample Author",
                    Title = "Global Stock Markets Rally on Economic Data",
                    Description = "Major indices worldwide posted gains as investors digest positive economic indicators and corporate earnings.",
                    Url = "https://example.com/article1",
                    PublishedAt = DateTime.Now.AddHours(-2),
                    Content = "Stock markets around the world rallied today..."
                },
                new Article
                {
                    Source = new Source { Name = "Bloomberg" },
                    Author = "Investment Analyst",
                    Title = "Tech Sector Leads Market Growth in Q4",
                    Description = "Technology companies continue to outperform other sectors with strong revenue growth and innovation.",
                    Url = "https://example.com/article2",
                    PublishedAt = DateTime.Now.AddHours(-5),
                    Content = "The technology sector has been a major driver..."
                },
                new Article
                {
                    Source = new Source { Name = "Reuters" },
                    Author = "Market Reporter",
                    Title = "Central Banks Signal Continued Economic Support",
                    Description = "Major central banks around the world indicate plans to maintain supportive monetary policies.",
                    Url = "https://example.com/article3",
                    PublishedAt = DateTime.Now.AddHours(-8),
                    Content = "Central banks across major economies have signaled..."
                }
            }
        };
    }
}
