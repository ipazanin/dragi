using Dragi.Common.Results;
using Dragi.Domain.MarketData.Services;

namespace Dragi.Domain.PortfolioManagement.Validators;

public class AssetValidator
{
    private readonly IMarketDataFetcherService _marketDataFetcherService;

    public AssetValidator(IMarketDataFetcherService marketDataFetcherService)
    {
        _marketDataFetcherService = marketDataFetcherService;
    }

    public async Task<Result> ValidateAssetTicker(string assetTicker, CancellationToken cancellationToken)
    {
        var isValidTickerResult = await _marketDataFetcherService.IsValidTicker(assetTicker, cancellationToken);

        if (isValidTickerResult.IsFail)
        {
            return isValidTickerResult.FailReason;
        }

        return Result.Success;
    }
}
