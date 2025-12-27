# Investment News Fetcher

A C# console application that fetches worldwide investment news using the News API.

## Features

- 📰 Fetches latest investment and financial news from multiple sources worldwide
- 💼 Supports business headlines by country
- 🔍 Searches for investment-related keywords (stocks, market, finance, economy, trading)
- 🌐 Real-time news data from newsapi.org
- 📊 Clean console output with formatted news articles
- 🎯 Sample data mode for demonstration without API key

## Prerequisites

- .NET 10.0 SDK or later
- (Optional) News API key from [newsapi.org](https://newsapi.org) for real data

## Installation

1. Clone the repository:
```bash
git clone https://github.com/foojunyu/CsharpProject.git
cd CsharpProject/InvestmentNewsApp
```

2. Restore dependencies:
```bash
dotnet restore
```

## Configuration

To use real news data, you'll need a free API key from [newsapi.org](https://newsapi.org):

1. Sign up at https://newsapi.org to get your free API key
2. Set the API key as an environment variable:

```bash
export NEWS_API_KEY=your_api_key_here
```

Alternatively, you can create a `.env` file (see `.env.example`).

**Note:** The application will work without an API key by showing sample investment news data.

## Usage

Run the application:

```bash
cd InvestmentNewsApp
dotnet run
```

The application will:
- Check for the NEWS_API_KEY environment variable
- If found, fetch real investment news from the API
- If not found, display sample investment news data
- Show article titles, sources, authors, publication dates, descriptions, and URLs

## Project Structure

```
InvestmentNewsApp/
├── Models/
│   ├── Article.cs          # News article model
│   ├── Source.cs           # News source model
│   └── NewsResponse.cs     # API response model
├── Services/
│   └── NewsService.cs      # Service for fetching news
├── Program.cs              # Main application entry point
├── InvestmentNewsApp.csproj # Project configuration
└── .env.example            # Example configuration file
```

## How It Works

1. **NewsService**: Handles HTTP requests to the News API
   - `GetInvestmentNewsAsync()`: Fetches news with investment-related keywords
   - `GetTopBusinessHeadlinesAsync()`: Gets top business headlines by country
   - `GetSampleInvestmentNews()`: Provides sample data for demonstration

2. **Models**: Define the structure for news data
   - `Article`: Represents a news article
   - `Source`: Represents a news source
   - `NewsResponse`: Represents the API response

3. **Program.cs**: Main application that orchestrates the news fetching and display

## Example Output

```
===========================================
   Worldwide Investment News Fetcher
===========================================

✓ API Key found. Fetching real investment news...

Fetching latest investment news...
Found 100 articles. Displaying 5:

📌 Article 1:
   Title: Global Stock Markets Rally on Economic Data
   Source: Financial Times
   Author: John Doe
   Published: 2025-12-27 14:30
   Description: Major indices worldwide posted gains...
   URL: https://example.com/article1

...
```

## License

MIT License

## Contributing

Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.
