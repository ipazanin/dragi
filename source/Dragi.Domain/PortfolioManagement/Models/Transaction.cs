using Dragi.Domain.MarketData.Models;

namespace Dragi.Domain.PortfolioManagement.Models;

public record Transaction
{
    public required int Id { get; init; }

    public required Price Price { get; init; }

    public required decimal NumberOfUnits { get; init; }

    public required string AssetTicker { get; init; }

    public required Asset Asset { get; init; }

    public decimal GetValue()
    {
        return Price.Value * NumberOfUnits;
    }
}
