namespace Dragi.Application.MarketData.Configurations;

public record AlphaVantageConfiguration
{
    public required string ApiKey { get; init; }

    public required int DailyRequestLimit { get; init; }
}
