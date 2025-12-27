using InvestmentNewsApp.Services;

Console.WriteLine("===========================================");
Console.WriteLine("   Worldwide Investment News Fetcher");
Console.WriteLine("===========================================");
Console.WriteLine();

// Check for API key in environment variable
var apiKey = Environment.GetEnvironmentVariable("NEWS_API_KEY");

if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("⚠️  No NEWS_API_KEY environment variable found.");
    Console.WriteLine("   To use real news data, get a free API key from https://newsapi.org");
    Console.WriteLine("   Then set it: export NEWS_API_KEY=your_api_key");
    Console.WriteLine();
    Console.WriteLine("📰 Showing sample investment news data for demonstration...");
    Console.WriteLine();
    
    // Show sample data when no API key is available
    using var newsService = new NewsService();
    var sampleNews = newsService.GetSampleInvestmentNews();
    DisplayNews(sampleNews);
}
else
{
    Console.WriteLine("✓ API Key found. Fetching real investment news...");
    Console.WriteLine();
    
    using var newsService = new NewsService(apiKey);
    
    Console.WriteLine("Fetching latest investment news...");
    var investmentNews = await newsService.GetInvestmentNewsAsync(pageSize: 5);
    
    if (investmentNews != null && investmentNews.Articles != null)
    {
        DisplayNews(investmentNews);
    }
    else
    {
        Console.WriteLine("⚠️  Could not fetch news. Please check your API key and internet connection.");
        Console.WriteLine("   Showing sample data instead...");
        Console.WriteLine();
        var sampleNews = newsService.GetSampleInvestmentNews();
        DisplayNews(sampleNews);
    }
}

Console.WriteLine();
Console.WriteLine("===========================================");
Console.WriteLine("   Thank you for using Investment News!");
Console.WriteLine("===========================================");

static void DisplayNews(InvestmentNewsApp.Models.NewsResponse? newsResponse)
{
    if (newsResponse == null || newsResponse.Articles == null || !newsResponse.Articles.Any())
    {
        Console.WriteLine("No news articles found.");
        return;
    }
    
    Console.WriteLine($"Found {newsResponse.TotalResults} articles. Displaying {newsResponse.Articles.Count}:");
    Console.WriteLine();
    
    for (int i = 0; i < newsResponse.Articles.Count; i++)
    {
        var article = newsResponse.Articles[i];
        Console.WriteLine($"📌 Article {i + 1}:");
        Console.WriteLine($"   Title: {article.Title}");
        Console.WriteLine($"   Source: {article.Source?.Name ?? "Unknown"}");
        Console.WriteLine($"   Author: {article.Author ?? "N/A"}");
        Console.WriteLine($"   Published: {article.PublishedAt:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"   Description: {article.Description ?? "No description available"}");
        Console.WriteLine($"   URL: {article.Url}");
        Console.WriteLine();
    }
}
