using Dragi.Common.Results;
using Dragi.Domain.MarketData.Models;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.ProfitLossSimulation.Models.ChangeRequests;

public abstract class AssetChangeRequest : ChangeRequest
{
    protected AssetChangeRequest(AssetWithPrices assetWithPrices)
    {
        AssetWithPrices = assetWithPrices;
    }

    protected AssetWithPrices AssetWithPrices { get; }

    protected abstract Result<Transaction> GetTransaction(Portfolio portfolio, DateTimeOffset date);

    public override Result ApplyChange(Portfolio portfolio, DateTimeOffset date)
    {
        var price = AssetWithPrices.Prices.GetValue(date);

        if (price == null)
        {
            return $"No price found in data for date {date}";
        }

        var getTransactionResult = GetTransaction(portfolio, date);

        if (getTransactionResult.IsFail)
        {
            return getTransactionResult;
        }

        var transaction = getTransactionResult.Value;

        var asset = AssetWithPrices.Asset;

        if (portfolio.Cash < transaction.GetValue())
        {
            return $"Not enough cash in portfolio ({portfolio.Cash}) to buy {asset.Ticker} ({transaction.GetValue()})";
        }

        portfolio.AddTransaction(transaction);

        return Result.Success;
    }
}
