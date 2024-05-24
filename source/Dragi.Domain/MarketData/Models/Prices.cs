namespace Dragi.Domain.MarketData.Models;

public record Prices
{
    public Prices(IReadOnlyList<Price> prices)
    {
        SortedPrices = new SortedList<DateTimeOffset, Price>(prices.Count);

        foreach (var price in prices)
        {
            SortedPrices.Add(price.Timestamp, price);
        }
    }

    private SortedList<DateTimeOffset, Price> SortedPrices { get; }

    public Price? GetLastValue()
    {
        if (SortedPrices.Count == 0)
        {
            return null;
        }

        var latestPrice = SortedPrices.LastOrDefault();

        return latestPrice.Value;
    }

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// TODO: Use binary search
    /// </remarks>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public Price? GetValue(DateTimeOffset timestamp)
    {
        if (SortedPrices.Count == 0)
        {
            return null;
        }

        foreach (var (priceTimestamp, price) in SortedPrices)
        {
            if (priceTimestamp.Date >= timestamp.Date)
            {
                return price;
            }
        }

        return GetLastValue();
    }

    public IReadOnlyList<Price> GetPrices()
    {
        return [.. SortedPrices.Values];
    }
}
