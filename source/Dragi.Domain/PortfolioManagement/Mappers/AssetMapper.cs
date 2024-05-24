using Dragi.Domain.MarketData.Models;
using Dragi.Domain.PortfolioManagement.DataObject;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.PortfolioManagement.Mappers;

public static class AssetMapper
{
    public static Asset GetAsset(CreateAssetData createAssetData, Price latestPrice)
    {
        var asset = new Asset(
            ticker: createAssetData.Ticker,
            transactions: [],
            latestPrice: latestPrice);

        return asset;
    }
}
