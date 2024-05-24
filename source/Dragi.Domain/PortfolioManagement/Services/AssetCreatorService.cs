using Dragi.Common.Results;
using Dragi.Domain.MarketData.Services;
using Dragi.Domain.PortfolioManagement.DataObject;
using Dragi.Domain.PortfolioManagement.Mappers;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Repositories;
using Dragi.Domain.PortfolioManagement.Validators;

namespace Dragi.Domain.PortfolioManagement.Services;

public class AssetCreatorService
{
    private readonly IAssetCreatorRepository _assetCreatorRepository;
    private readonly IMarketDataFetcherService _marketDataFetcherService;
    private readonly AssetValidator _assetValidator;

    public AssetCreatorService(
        IAssetCreatorRepository assetCreatorRepository,
        IMarketDataFetcherService marketDataFetcherService,
        AssetValidator assetValidator)
    {
        _assetCreatorRepository = assetCreatorRepository;
        _marketDataFetcherService = marketDataFetcherService;
        _assetValidator = assetValidator;
    }

    public async Task<Result<Asset>> CreateAsset(CreateAssetData createAssetData, CancellationToken cancellationToken)
    {
        var isTickerValidResult = await _assetValidator.ValidateAssetTicker(createAssetData.Ticker, cancellationToken);

        if (isTickerValidResult.IsFail)
        {
            return isTickerValidResult.FailReason;
        }

        var getLatestPriceResult = await _marketDataFetcherService.GetLatestPriceFor(createAssetData.Ticker, cancellationToken);

        if (getLatestPriceResult.IsFail)
        {
            return getLatestPriceResult.FailReason;
        }

        var asset = AssetMapper.GetAsset(createAssetData, getLatestPriceResult.Value);

        var createAssetResult = await _assetCreatorRepository.CreateAsset(asset, cancellationToken);

        return createAssetResult;
    }
}
