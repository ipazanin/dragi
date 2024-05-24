namespace Dragi.Application.MarketData.DataTransferObjects;

/// <summary>
/// https://polygon.io/docs/stocks/get_v2_aggs_ticker__stocksticker__range__multiplier___timespan___from___to
/// </summary>
public record MarketDataResponse
{
    [JsonPropertyName("adjusted")]
    public required bool Adjusted { get; init; }

    [JsonPropertyName("next_url")]
    public string? NextUrl { get; init; }

    [JsonPropertyName("queryCount")]
    public required int QueryCount { get; init; }

    [JsonPropertyName("request_id")]
    public required string RequestId { get; init; }

    [JsonPropertyName("resultsCount")]
    public required int ResultsCount { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("ticker")]
    public required string Ticker { get; init; }

    [JsonPropertyName("results")]
    public required IReadOnlyList<MarketDataResult> Results { get; set; }
}
