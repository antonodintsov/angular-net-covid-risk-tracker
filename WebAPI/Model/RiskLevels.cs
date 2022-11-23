using System.Text.Json.Serialization;

namespace WebAPI.Model
{
    public record RiskLevels(
        [property: JsonPropertyName("overall")] int Overall,
        [property: JsonPropertyName("testPositivityRatio")] int TestPositivityRatio,
        [property: JsonPropertyName("caseDensity")] int CaseDensity,
        [property: JsonPropertyName("infectionRate")] int InfectionRate
    );
}
