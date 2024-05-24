
using Dragi.Domain.MarketData.Models;

namespace Dragi.Domain.ProfitLossSimulation.Models.Conditions;

public class PercentageRiseRepetitiveAssetCondition : PercentageRepetitiveAssetCondition
{
    public PercentageRiseRepetitiveAssetCondition(AssetWithPrices assetWithPrices, Price initialPrice, decimal percentageDifference)
        : base(assetWithPrices, initialPrice, percentageDifference)
    {
    }

    public override bool IsConditionSatisfied(DateTimeOffset dateTime)
    {
        var price = AssetWithPrices.Prices.GetValue(dateTime);
        if (price == null)
        {
            return false;
        }

        var priceDifference = price.Value - LastConditionSatisfiedPrice.Value;
        var priceDifferencePercentage = priceDifference / price.Value;

        if (priceDifferencePercentage >= PercentageDifference)
        {
            LastConditionSatisfiedPrice = price;
            return true;
        }

        return false;
    }
}
