using Dragi.Domain.MarketData.Models;

namespace Dragi.Domain.PortfolioManagement.DataObject;

public record CreateTransactionData
{
    public required string AssetTicker { get; init; }

    public required Price Price { get; init; }

    public required decimal NumberOfUnits { get; init; }
}
