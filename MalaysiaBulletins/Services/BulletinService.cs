using System.Text.Json;
using MalaysiaBulletins.Models;

namespace MalaysiaBulletins.Services;

public class BulletinService : IBulletinService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://api.data.gov.my";
    private readonly JsonSerializerOptions _jsonOptions;

    // Known bulletin-related dataset IDs from data.gov.my
    private readonly List<BulletinMetadata> _knownBulletins = new()
    {
        new BulletinMetadata
        {
            Id = "earthquake",
            Title = "Earthquake Data",
            Description = "Earthquake occurrences in Malaysia region",
            Agency = "Malaysian Meteorological Department",
            Category = "Weather & Environment"
        },
        new BulletinMetadata
        {
            Id = "weather_forecast",
            Title = "Weather Forecast",
            Description = "Weather forecasts and bulletins for Malaysia",
            Agency = "Malaysian Meteorological Department",
            Category = "Weather & Environment"
        },
        new BulletinMetadata
        {
            Id = "kawalan_serangga",
            Title = "Insect Control Bulletin",
            Description = "Pest control and insect management information",
            Agency = "Ministry of Health",
            Category = "Health & Safety"
        },
        new BulletinMetadata
        {
            Id = "public_services_bulletin",
            Title = "Public Services Bulletin",
            Description = "Government public service announcements and bulletins",
            Agency = "Various Government Agencies",
            Category = "Public Services"
        },
        new BulletinMetadata
        {
            Id = "fuelprice",
            Title = "Fuel Price Bulletin",
            Description = "Weekly fuel price updates for RON95, RON97, and Diesel",
            Agency = "Ministry of Finance",
            Category = "Economy & Finance"
        },
        new BulletinMetadata
        {
            Id = "air_quality",
            Title = "Air Quality Index Bulletin",
            Description = "Air quality measurements and pollution index across Malaysia",
            Agency = "Department of Environment",
            Category = "Weather & Environment"
        }
    };

    public BulletinService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public Task<ApiResponse<List<BulletinMetadata>>> GetAllBulletinsAsync()
    {
        var response = new ApiResponse<List<BulletinMetadata>>
        {
            Success = true,
            Data = _knownBulletins
        };

        return Task.FromResult(response);
    }

    public async Task<ApiResponse<List<Dictionary<string, object>>>> GetBulletinDataAsync(string bulletinId, int limit = 100)
    {
        try
        {
            var url = $"{_baseUrl}/data-catalogue?id={bulletinId}&limit={limit}";
            Console.WriteLine($"Fetching data from: {url}");

            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<List<Dictionary<string, object>>>
                {
                    Success = false,
                    ErrorMessage = $"API returned status code: {response.StatusCode}"
                };
            }

            var content = await response.Content.ReadAsStringAsync();
            
            if (string.IsNullOrWhiteSpace(content))
            {
                return new ApiResponse<List<Dictionary<string, object>>>
                {
                    Success = false,
                    ErrorMessage = "Empty response from API"
                };
            }

            var dataResponse = JsonSerializer.Deserialize<DataGovMyResponse>(content, _jsonOptions);

            if (dataResponse?.Data == null || dataResponse.Data.Count == 0)
            {
                return new ApiResponse<List<Dictionary<string, object>>>
                {
                    Success = true,
                    Data = new List<Dictionary<string, object>>(),
                    ErrorMessage = "No data available for this bulletin"
                };
            }

            return new ApiResponse<List<Dictionary<string, object>>>
            {
                Success = true,
                Data = dataResponse.Data
            };
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<List<Dictionary<string, object>>>
            {
                Success = false,
                ErrorMessage = $"Network error: {ex.Message}"
            };
        }
        catch (JsonException ex)
        {
            return new ApiResponse<List<Dictionary<string, object>>>
            {
                Success = false,
                ErrorMessage = $"JSON parsing error: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<Dictionary<string, object>>>
            {
                Success = false,
                ErrorMessage = $"Unexpected error: {ex.Message}"
            };
        }
    }
}
