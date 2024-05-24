using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Repositories;

namespace Dragi.Persistence.PortfolioManagement;

public class InMemoryAssetRepository : IAssetCreatorRepository, IAssetReaderRepository
{
    private readonly Dictionary<string, Asset> _assetDictionary = new();

    public Task<Result<Asset>> CreateAsset(Asset asset, CancellationToken cancellationToken)
    {
        if (_assetDictionary.ContainsKey(asset.Ticker))
        {
            return Task.FromResult(new Result<Asset>($"Asset with ticker {asset.Ticker} already exists!"));
        }

        _assetDictionary[asset.Ticker] = asset;

        return Task.FromResult(new Result<Asset>(asset));
    }

    public Task<Result<Asset>> GetAsset(string ticker, CancellationToken cancellationToken)
    {
        var getAssetResult = _assetDictionary.TryGetValue(ticker, out var asset) switch
        {
            true => new Result<Asset>(asset),
            false => new Result<Asset>($"Asset with ticker {ticker} was not found"),
        };

        return Task.FromResult(getAssetResult);
    }
}
