using WebAPI.Model;

namespace WebAPI.Builders
{
    public interface IRiskLevelDataBuilder<in T>
    {
        KeyValuePair<string, RiskLevelInfo> Build(T source, RiskLevelType riskLevelType);
    }
}
