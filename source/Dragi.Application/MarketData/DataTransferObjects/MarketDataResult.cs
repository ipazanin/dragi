namespace Dragi.Application.MarketData.DataTransferObjects;

public record MarketDataResult
{
    [JsonPropertyName("T")]
    public string? Ticker { get; init; }

    [JsonPropertyName("c")]
    public required decimal ClosePrice { get; init; }

    [JsonPropertyName("h")]
    public required decimal HighestPrice { get; init; }

    [JsonPropertyName("l")]
    public required decimal LowestPrice { get; init; }

    [JsonPropertyName("n")]
    public required int NumberOfTransactions { get; init; }

    [JsonPropertyName("o")]
    public required decimal OpenPrice { get; init; }

    /// <summary>
    /// The Unix Msec timestamp for the start of the aggregate window.
    /// </summary>
    [JsonPropertyName("t")]
    public required long Timestamp { get; init; }

    [JsonPropertyName("v")]
    public required decimal TradingVolume { get; init; }

    [JsonPropertyName("vw")]
    public required decimal VolumeWeightedAveragePrice { get; init; }
}
