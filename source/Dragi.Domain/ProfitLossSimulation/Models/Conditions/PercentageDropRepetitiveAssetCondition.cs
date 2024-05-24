
using Dragi.Domain.MarketData.Models;

namespace Dragi.Domain.ProfitLossSimulation.Models.Conditions;

public class PercentageDropRepetitiveAssetCondition : PercentageRepetitiveAssetCondition
{
    public PercentageDropRepetitiveAssetCondition(AssetWithPrices assetWithPrices, Price initialPrice, decimal percentageDrop)
        : base(assetWithPrices, initialPrice, percentageDrop)
    {
    }

    public override bool IsConditionSatisfied(DateTimeOffset dateTime)
    {
        var price = AssetWithPrices.Prices.GetValue(dateTime);
        if (price == null)
        {
            return false;
        }

        var priceDifference = LastConditionSatisfiedPrice.Value - price.Value;
        var priceDifferencePercentage = priceDifference / LastConditionSatisfiedPrice.Value;

        if (priceDifferencePercentage >= PercentageDifference)
        {
            LastConditionSatisfiedPrice = price;
            return true;
        }

        return false;
    }
}
