using Dragi.Application.MarketData.DataTransferObjects;
using Dragi.Domain.MarketData.Models;

namespace Dragi.Application.MarketData.Mappers;

public static class MarketDataMapper
{
    public static Prices MapToPrices(MarketDataResponse marketData)
    {
        var prices = new List<Price>(marketData.Results.Count);

        foreach (var item in marketData.Results)
        {
            var price = new Price
            {
                Value = item.ClosePrice,
                Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(item.Timestamp),
            };

            prices.Add(price);
        }

        return new Prices(prices);
    }
}
