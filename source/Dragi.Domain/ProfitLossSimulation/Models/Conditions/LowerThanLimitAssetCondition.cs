
using Dragi.Domain.MarketData.Models;

namespace Dragi.Domain.ProfitLossSimulation.Models.Conditions;

public class LowerThanLimitAssetCondition : LimitAssetCondition
{
    public LowerThanLimitAssetCondition(
        AssetWithPrices assetWithPrices,
        decimal limitPrice) : base(assetWithPrices, limitPrice)
    {
    }

    public override bool IsConditionSatisfied(DateTimeOffset dateTime)
    {
        if (IsExecuted)
        {
            return false;
        }

        var price = AssetWithPrices.Prices.GetValue(dateTime);

        if (price == null)
        {
            return false;
        }

        if (price.Value <= LimitPrice)
        {
            IsExecuted = true;
            return true;
        }

        return false;
    }
}
