namespace Dragi.Domain.MarketData.Models;

public record Price
{
    public required DateTimeOffset Timestamp { get; init; }

    public required decimal Value { get; init; }
}
