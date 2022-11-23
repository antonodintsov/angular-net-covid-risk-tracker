using Microsoft.Extensions.Options;
using WebAPI.Builders;
using WebAPI.Converters;
using WebAPI.Model;

namespace WebAPI.Services
{
    public class CovidActNowMetricsService : IMetricsService<StateSummary>, IRiskLevelsService
    {
        private readonly IDataConverter _dataConverter;
        private readonly IRiskLevelDataBuilder<StateSummary> _riskLevelDataBuilder;

        internal readonly CovidActNowSettings Settings;

        public CovidActNowMetricsService(IDataConverter dataConverter, IRiskLevelDataBuilder<StateSummary> riskLevelDataBuilder, IOptions<CovidActNowSettings> options)
        {
            _dataConverter = dataConverter;
            _riskLevelDataBuilder = riskLevelDataBuilder;
            Settings = options.Value;
        }

        public virtual async Task<List<StateSummary>> ReadStatesDataAsync()
        {
            using HttpClient client = new()
            {
                BaseAddress = new Uri(Settings.BaseUrl)
            };

            var stream = await client.GetStreamAsync($"{Settings.StatesDataRequest}?apiKey={Settings.ApiKey}");
            return await _dataConverter.Convert<List<StateSummary>>(stream);
        }

        public virtual async Task<Dictionary<string, RiskLevelInfo>> GetRiskLevelsAsync(RiskLevelType riskLevelType)
        {
            var statesData = await ReadStatesDataAsync();
            return statesData.Select(x => _riskLevelDataBuilder.Build(x, riskLevelType)).ToDictionary(x => x.Key, y => y.Value);
        }
    }
}
