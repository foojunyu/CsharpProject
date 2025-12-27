namespace MalaysiaBulletins.Models;

public class BulletinMetadata
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Agency { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime? LastUpdated { get; set; }
}
