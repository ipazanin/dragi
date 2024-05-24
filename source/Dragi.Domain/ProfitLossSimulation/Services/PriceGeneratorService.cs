using Dragi.Common.Results;
using Dragi.Common.Utilities;
using Dragi.Domain.MarketData.Models;

namespace Dragi.Domain.ProfitLossSimulation.Services;

public class PriceGeneratorService
{
    private readonly GaussianRandomGenerator _gaussianRandomGenerator;

    public PriceGeneratorService(GaussianRandomGenerator gaussianRandomGenerator)
    {
        _gaussianRandomGenerator = gaussianRandomGenerator;
    }

    public Result<Prices> GetPrices(
        Price initialPrice,
        DateTimeOffset endDate,
        decimal yearlyYieldPercentage,
        decimal dailyMovementStandardDeviationPercentage)
    {
        if (initialPrice.Timestamp >= endDate)
        {
            return "Start date must be before end date!";
        }

        var currentDate = initialPrice.Timestamp;
        var currentPrice = initialPrice.Value;
        var expectedPrice = currentPrice;

        var prices = new List<Price>();

        // Calculate the daily yield factor
        var dailyYieldFactor = yearlyYieldPercentage / 365.25M;

        while (currentDate < endDate)
        {
            currentDate = currentDate.AddDays(1);

            // Apply the daily yield factor to get the expected price
            expectedPrice *= 1 + dailyYieldFactor;

            // Apply random fluctuation around the expected price
            var standardDeviation = currentPrice * dailyMovementStandardDeviationPercentage;

            var randomNormal = _gaussianRandomGenerator.GetRandomNormalDistribution(
                mean: (double)expectedPrice,
                standardDeviation: (double)standardDeviation);

            var newPrice = decimal.Round((decimal)randomNormal, 2);

            var price = new Price
            {
                Timestamp = currentDate,
                Value = newPrice,
            };

            // Update the current price with the new simulated price
            currentPrice = newPrice;

            prices.Add(price);
        }

        return new Prices(prices);
    }
}
