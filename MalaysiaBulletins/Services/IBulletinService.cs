using MalaysiaBulletins.Models;

namespace MalaysiaBulletins.Services;

public interface IBulletinService
{
    Task<ApiResponse<List<BulletinMetadata>>> GetAllBulletinsAsync();
    Task<ApiResponse<List<Dictionary<string, object>>>> GetBulletinDataAsync(string bulletinId, int limit = 100);
}
