using System.Text.Json.Serialization;

namespace MalaysiaBulletins.Models;

public class DataGovMyResponse
{
    [JsonPropertyName("data")]
    public List<Dictionary<string, object>>? Data { get; set; }
}
