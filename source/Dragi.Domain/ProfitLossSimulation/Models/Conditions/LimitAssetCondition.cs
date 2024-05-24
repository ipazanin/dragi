using Dragi.Domain.MarketData.Models;

namespace Dragi.Domain.ProfitLossSimulation.Models.Conditions;

public abstract class LimitAssetCondition : Condition
{
    protected LimitAssetCondition(
        AssetWithPrices assetWithPrices,
        decimal limitPrice)
    {
        AssetWithPrices = assetWithPrices;
        LimitPrice = limitPrice;
    }

    protected AssetWithPrices AssetWithPrices { get; }

    protected decimal LimitPrice { get; }

    /// <summary>
    /// Used to execute limit buy only once
    /// </summary>
    protected bool IsExecuted { get; set; }
}
