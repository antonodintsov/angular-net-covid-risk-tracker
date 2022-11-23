using WebAPI.Model;

namespace WebAPI.Services
{
    public interface IRiskLevelsService
    {
        Task<Dictionary<string, RiskLevelInfo>> GetRiskLevelsAsync(RiskLevelType type);
    }
}
