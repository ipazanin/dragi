using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.MarketData.Models;

public class AssetWithPrices
{
    public required Asset Asset { get; init; }

    public required Prices Prices { get; init; }
}
