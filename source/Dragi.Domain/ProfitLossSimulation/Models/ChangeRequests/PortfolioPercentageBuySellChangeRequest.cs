using Dragi.Common.Results;
using Dragi.Domain.MarketData.Models;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.ProfitLossSimulation.Models.ChangeRequests;

public class PortfolioPercentageBuySellChangeRequest : AssetChangeRequest
{
    private readonly decimal _percentageOfPortfolio;

    public PortfolioPercentageBuySellChangeRequest(
        decimal percentageOfPortfolio,
        AssetWithPrices assetWithPrices) : base(assetWithPrices)
    {
        _percentageOfPortfolio = percentageOfPortfolio;
    }

    protected override Result<Transaction> GetTransaction(Portfolio portfolio, DateTimeOffset date)
    {
        var price = AssetWithPrices.Prices.GetValue(date);
        if (price == null)
        {
            return $"No price found in data for date {date}";
        }

        var asset = AssetWithPrices.Asset;

        var cashAmount = portfolio.Cash * _percentageOfPortfolio;
        var numberOfAssetsToBuy = decimal.Round(cashAmount / price.Value, 2);

        var transaction = new Transaction
        {
            Id = default,
            AssetTicker = asset.Ticker,
            Price = price,
            NumberOfUnits = numberOfAssetsToBuy,
            Asset = asset,
        };

        return transaction;
    }
}
