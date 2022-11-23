using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using WebAPI.Builders;
using WebAPI.Converters;
using WebAPI.Model;

namespace WebAPI.Services
{
    public class CachedCovidActNowMetricsService : CovidActNowMetricsService
    {
        private readonly IMemoryCache _memoryCache;

        public CachedCovidActNowMetricsService(
            IDataConverter dataConverter,
            IRiskLevelDataBuilder<StateSummary> riskLevelDataBuilder, 
            IMemoryCache memoryCache, 
            IOptions<CovidActNowSettings> options) : base(dataConverter, riskLevelDataBuilder, options)
        {
            _memoryCache = memoryCache;
        }

        public override Task<List<StateSummary>> ReadStatesDataAsync()
        {
            return _memoryCache.GetOrCreateAsync($"{GetType()}.{nameof(ReadStatesDataAsync)}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = Settings.CacheLifetime;
                return base.ReadStatesDataAsync();
            });
        }
    }
}
