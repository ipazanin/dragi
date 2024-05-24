using Dragi.Common.Results;
using Dragi.Domain.MarketData.Models;
using Dragi.Domain.MarketData.Services;

namespace Dragi.Application.MarketData.Services;

public class AlphaVantageMarketDataFetcherService : IMarketDataFetcherService
{
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
