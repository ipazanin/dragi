using Dragi.Application.MarketData.Configurations;
using Dragi.Application.MarketData.DataTransferObjects;
using Dragi.Application.MarketData.Mappers;
using Dragi.Common.Results;
using Dragi.Domain.MarketData.Models;
using Dragi.Domain.MarketData.Services;

namespace Dragi.Application.MarketData.Services;

public class PolygonMarketDataFetcherService : IMarketDataFetcherService
{
    private const string BaseApiUrl = "https://api.polygon.io";
    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = false,
    };

    private readonly PolygonConfiguration _polygonConfiguration;
    private readonly IHttpClientFactory _httpClientFactory;

    public PolygonMarketDataFetcherService(PolygonConfiguration polygonConfiguration, IHttpClientFactory httpClientFactory)
    {
        _polygonConfiguration = polygonConfiguration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Result> IsValidTicker(string assetTicker, CancellationToken cancellationToken)
    {
        using var httpClient = _httpClientFactory.CreateClient();

        var requestUrl = $"{BaseApiUrl}/v3/reference/tickers/{assetTicker}?apiKey={_polygonConfiguration.ApiKey}";

        try
        {
            var marketDataResponse = await httpClient.GetAsync(requestUrl, cancellationToken);

            if (marketDataResponse is null)
                return new Result($"Invalid ticker {assetTicker}");

            marketDataResponse.EnsureSuccessStatusCode();

            return Result.Success;
        }
        catch
        {
            return new Result($"Invalid ticker {assetTicker}");
        }
    }

    /// <summary>
    /// https://polygon.io/docs/stocks/get_v2_aggs_ticker__stocksticker__range__multiplier___timespan___from___to
    /// </summary>
    /// <param name="assetTicker"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<Prices>> GetDailyPrices(
        string assetTicker,
        DateTimeOffset fromDate,
        DateTimeOffset toDate,
        CancellationToken cancellationToken)
    {
        using var httpClient = _httpClientFactory.CreateClient();

        var requestUrl = $"{BaseApiUrl}/v2/aggs/ticker/{assetTicker}/range/1/day/{fromDate:yyyy-MM-dd}/{toDate:yyyy-MM-dd}?adjusted=true&sort=asc&limit=5000&apiKey={_polygonConfiguration.ApiKey}";

        try
        {
            var marketDataResponse = await httpClient.GetFromJsonAsync<MarketDataResponse>(
                requestUri: requestUrl,
                options: s_jsonSerializerOptions,
                cancellationToken: cancellationToken);

            if (marketDataResponse is null)
                return new Result<Prices>($"Invalid response fetching market data for {assetTicker}");

            var prices = MarketDataMapper.MapToPrices(marketDataResponse);

            return new Result<Prices>(prices);
        }
        catch (Exception exception)
        {
            return new Result<Prices>(exception);
        }
    }

    public async Task<Result<Price>> GetLatestPriceFor(string assetTicker, CancellationToken cancellationToken)
    {
        using var httpClient = _httpClientFactory.CreateClient();

        var requestUrl = $"{BaseApiUrl}/v2/aggs/ticker/{assetTicker}/prev?adjusted=true&apiKey={_polygonConfiguration.ApiKey}";

        try
        {
            var marketDataResponse = await httpClient.GetFromJsonAsync<MarketDataResponse>(
                requestUri: requestUrl,
                options: s_jsonSerializerOptions,
                cancellationToken: cancellationToken);

            if (marketDataResponse is null)
                return new Result<Price>($"Invalid response fetching market data for {assetTicker}");

            var prices = MarketDataMapper.MapToPrices(marketDataResponse);

            var latestValue = prices.GetLastValue();
            if (latestValue is null)
                return new Result<Price>($"Invalid response fetching market data for {assetTicker}");

            return new Result<Price>(latestValue);
        }
        catch (Exception exception)
        {
            return new Result<Price>(exception);
        }
    }
}
