using System.Text.Json.Serialization;

namespace WebAPI.Model
{
    public record StateSummary(
        [property: JsonPropertyName("fips")] string Fips,
        [property: JsonPropertyName("country")] string Country,
        [property: JsonPropertyName("state")] string State,
        [property: JsonPropertyName("riskLevels")] RiskLevels RiskLevels
    );
}
