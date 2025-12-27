using MalaysiaBulletins.Services;
using Microsoft.Extensions.DependencyInjection;

// Setup dependency injection
var serviceProvider = new ServiceCollection()
    .AddHttpClient()
    .AddScoped<IBulletinService, BulletinService>()
    .BuildServiceProvider();

var bulletinService = serviceProvider.GetRequiredService<IBulletinService>();

Console.WriteLine("===========================================");
Console.WriteLine("Malaysia Bulletins Fetcher");
Console.WriteLine("===========================================");
Console.WriteLine();

// Get all available bulletins
Console.WriteLine("Fetching all available Malaysia bulletins...");
var bulletinsResponse = await bulletinService.GetAllBulletinsAsync();

if (!bulletinsResponse.Success || bulletinsResponse.Data == null)
{
    Console.WriteLine($"Error: {bulletinsResponse.ErrorMessage}");
    return;
}

Console.WriteLine($"\nFound {bulletinsResponse.Data.Count} bulletin types:");
Console.WriteLine();

// Display all bulletins
for (int i = 0; i < bulletinsResponse.Data.Count; i++)
{
    var bulletin = bulletinsResponse.Data[i];
    Console.WriteLine($"{i + 1}. {bulletin.Title}");
    Console.WriteLine($"   ID: {bulletin.Id}");
    Console.WriteLine($"   Description: {bulletin.Description}");
    Console.WriteLine($"   Agency: {bulletin.Agency}");
    Console.WriteLine($"   Category: {bulletin.Category}");
    Console.WriteLine();
}

// Fetch data for each bulletin
Console.WriteLine("===========================================");
Console.WriteLine("Fetching bulletin data...");
Console.WriteLine("===========================================");
Console.WriteLine();

foreach (var bulletin in bulletinsResponse.Data)
{
    Console.WriteLine($"\nFetching: {bulletin.Title} ({bulletin.Id})");
    Console.WriteLine(new string('-', 60));
    
    var dataResponse = await bulletinService.GetBulletinDataAsync(bulletin.Id, 5);
    
    if (!dataResponse.Success)
    {
        Console.WriteLine($"   ❌ Error: {dataResponse.ErrorMessage}");
        continue;
    }
    
    if (dataResponse.Data == null || dataResponse.Data.Count == 0)
    {
        Console.WriteLine($"   ℹ️ No data available");
        continue;
    }
    
    Console.WriteLine($"   ✅ Retrieved {dataResponse.Data.Count} records");
    
    // Display first 3 records
    int displayCount = Math.Min(3, dataResponse.Data.Count);
    for (int i = 0; i < displayCount; i++)
    {
        var record = dataResponse.Data[i];
        Console.WriteLine($"\n   Record {i + 1}:");
        
        // Display all fields in the record
        foreach (var field in record)
        {
            var value = field.Value?.ToString() ?? "N/A";
            // Truncate long values
            if (value.Length > 100)
            {
                value = value.Substring(0, 97) + "...";
            }
            Console.WriteLine($"      {field.Key}: {value}");
        }
    }
    
    if (dataResponse.Data.Count > displayCount)
    {
        Console.WriteLine($"\n   ... and {dataResponse.Data.Count - displayCount} more records");
    }
}

Console.WriteLine();
Console.WriteLine("===========================================");
Console.WriteLine("Bulletin fetch complete!");
Console.WriteLine("===========================================");
