using Dragi.Common.Results;
using Dragi.Domain.MarketData.Models;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.ProfitLossSimulation.Models.ChangeRequests;

public class ConstantBuySellChangeRequest : AssetChangeRequest
{
    private readonly decimal _numberOfAssetsToBuy;

    public ConstantBuySellChangeRequest(
        AssetWithPrices assetWithPrices,
        decimal numberOfAssetsToBuy) : base(assetWithPrices)
    {
        _numberOfAssetsToBuy = numberOfAssetsToBuy;
    }

    protected override Result<Transaction> GetTransaction(Portfolio portfolio, DateTimeOffset date)
    {
        var price = AssetWithPrices.Prices.GetValue(date);
        if (price == null)
        {
            return $"No price found in data for date {date}";
        }

        var asset = AssetWithPrices.Asset;

        var transaction = new Transaction
        {
            Id = default,
            AssetTicker = asset.Ticker,
            Price = price,
            NumberOfUnits = _numberOfAssetsToBuy,
            Asset = asset,
        };

        return transaction;
    }
}
