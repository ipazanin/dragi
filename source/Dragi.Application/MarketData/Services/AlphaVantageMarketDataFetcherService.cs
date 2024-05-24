using Dragi.Application.MarketData.Configurations;
using Dragi.Common.Results;
using Dragi.Domain.MarketData.Models;
using Dragi.Domain.MarketData.Services;

namespace Dragi.Application.MarketData.Services;

public class AlphaVantageMarketDataFetcherService : IMarketDataFetcherService
{
    private readonly AlphaVantageConfiguration _alphaVantageConfiguration;

    public AlphaVantageMarketDataFetcherService(AlphaVantageConfiguration alphaVantageConfiguration)
    {
        _alphaVantageConfiguration = alphaVantageConfiguration;
    }

    public Task<Result<Prices>> GetDailyPrices(
        string assetTicker,
        DateTimeOffset fromDate,
        DateTimeOffset toDate,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Price>> GetLatestPriceFor(
        string assetTicker,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> IsValidTicker(
        string assetTicker,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
