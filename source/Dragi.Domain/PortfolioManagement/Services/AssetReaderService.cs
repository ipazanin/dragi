using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Repositories;

namespace Dragi.Domain.PortfolioManagement.Services;

public class AssetReaderService
{
    private readonly IAssetReaderRepository _assetReaderRepository;

    public AssetReaderService(IAssetReaderRepository assetReaderRepository)
    {
        _assetReaderRepository = assetReaderRepository;
    }

    public async Task<Result<Asset>> GetAsset(string ticker, CancellationToken cancellationToken)
    {
        var getAssetResult = await _assetReaderRepository.GetAsset(ticker, cancellationToken);

        return getAssetResult;
    }
}
