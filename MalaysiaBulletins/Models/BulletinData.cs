using System.Text.Json.Serialization;

namespace MalaysiaBulletins.Models;

public class BulletinData
{
    [JsonPropertyName("date")]
    public string? Date { get; set; }
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("content")]
    public string? Content { get; set; }
    
    [JsonPropertyName("category")]
    public string? Category { get; set; }
    
    [JsonPropertyName("source")]
    public string? Source { get; set; }
}
