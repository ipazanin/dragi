using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Services;

namespace Dragi.Domain.MarketData.Models;

public class AssetWithPrices
{
    public required Asset Asset { get; init; }

    public required Prices Prices { get; init; }
}
