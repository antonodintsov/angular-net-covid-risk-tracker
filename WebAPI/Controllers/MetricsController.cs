using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsService<StateSummary> _metricsService;
        private readonly IRiskLevelsService _riskLevelsService;

        public MetricsController(IMetricsService<StateSummary> metricsService, IRiskLevelsService riskLevelsService)
        {
            _metricsService = metricsService;
            _riskLevelsService = riskLevelsService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<StateSummary>> StatesSummary()
        {
            return await _metricsService.ReadStatesDataAsync();
        }

        [HttpGet]
        [Route("[action]/{type}")]
        public async Task<Dictionary<string, RiskLevelInfo>> StatesRiskLevels(RiskLevelType type)
        {
            return await _riskLevelsService.GetRiskLevelsAsync(type);
        }
    }
}
