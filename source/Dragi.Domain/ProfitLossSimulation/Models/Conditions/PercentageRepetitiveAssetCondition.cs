
using Dragi.Domain.MarketData.Models;

namespace Dragi.Domain.ProfitLossSimulation.Models.Conditions;

public abstract class PercentageRepetitiveAssetCondition : Condition
{
    protected PercentageRepetitiveAssetCondition(
        AssetWithPrices assetWithPrices,
        Price initialPrice,
        decimal percentageDifference)
    {
        AssetWithPrices = assetWithPrices;
        LastConditionSatisfiedPrice = initialPrice;
        PercentageDifference = percentageDifference;
    }

    protected AssetWithPrices AssetWithPrices { get; }

    protected decimal PercentageDifference { get; }

    protected Price LastConditionSatisfiedPrice { get; set; }
}
