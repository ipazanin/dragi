using Dragi.Common.Results;
using Dragi.Domain.MarketData.Models;

namespace Dragi.Domain.MarketData.Services;

public interface IMarketDataFetcherService
{
    public Task<Result> IsValidTicker(string assetTicker, CancellationToken cancellationToken);

    public Task<Result<Prices>> GetDailyPrices(
        string assetTicker,
        DateTimeOffset fromDate,
        DateTimeOffset toDate,
        CancellationToken cancellationToken);

    public Task<Result<Price>> GetLatestPriceFor(string assetTicker, CancellationToken cancellationToken);
}
