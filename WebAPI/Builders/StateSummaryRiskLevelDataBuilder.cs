using WebAPI.Model;

namespace WebAPI.Builders
{
    public class StateSummaryRiskLevelDataBuilder : IRiskLevelDataBuilder<StateSummary>
    {
        private static readonly Dictionary<RiskLevelType, Func<StateSummary, int>> RiskLevelInfoValueProviders =
            new()
            {
                { RiskLevelType.Overall, summary => summary.RiskLevels.Overall },
                { RiskLevelType.CaseDensity, summary => summary.RiskLevels.CaseDensity },
                { RiskLevelType.TestPositivityRatio, summary => summary.RiskLevels.TestPositivityRatio },
                { RiskLevelType.InfectionRate, summary => summary.RiskLevels.InfectionRate }
            };

        public KeyValuePair<string, RiskLevelInfo> Build(StateSummary stateSummary, RiskLevelType riskLevelType)
        {
            return new KeyValuePair<string, RiskLevelInfo>(GetKey(stateSummary), GetValue(stateSummary, riskLevelType));
        }

        private string GetKey(StateSummary stateSummary)
        {
            return stateSummary.Fips;
        }

        private RiskLevelInfo GetValue(StateSummary stateSummary, RiskLevelType riskLevelType)
        {
            var value = RiskLevelInfoValueProviders.ContainsKey(riskLevelType) ? RiskLevelInfoValueProviders[riskLevelType].Invoke(stateSummary) : default;
            return new RiskLevelInfo(value);
        }
    }
}
