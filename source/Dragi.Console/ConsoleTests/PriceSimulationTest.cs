using Dragi.Common.Utilities;
using Dragi.Domain.MarketData.Models;
using Dragi.Domain.ProfitLossSimulation.Services;

namespace Dragi.Console.ConsoleTests;

public static class PriceSimulationTest
{
    public static void TestSimulationAccuracy()
    {
        var priceGeneratorService = new PriceGeneratorService(new GaussianRandomGenerator());

        var initialPrice = new Price { Timestamp = new DateTime(2024, 1, 1), Value = 100 };

        var percentages = new List<decimal>();

        foreach (var _ in Enumerable.Range(0, 100))
        {
            var prices = priceGeneratorService.GetPrices(
                initialPrice: initialPrice,
                endDate: new DateTime(2025, 1, 1),
                yearlyYieldPercentage: 0.08M,
                dailyMovementStandardDeviationPercentage: 0.1M);

            var lastPrice = prices.Value.GetLastValue()!;

            var percentage = (lastPrice.Value - initialPrice.Value) % initialPrice.Value;
            percentages.Add(percentage);
            System.Console.WriteLine($"{percentage}%");
        }

        System.Console.WriteLine($"Expected yield: 8%");
        System.Console.WriteLine($"Standard deviation: 2%");
        System.Console.WriteLine($"Actual average yield fro 100 simulations: {decimal.Round(percentages.Average(), 2)}%");
    }

    public static void TestPriceMovement()
    {
        var priceGeneratorService = new PriceGeneratorService(new GaussianRandomGenerator());

        var initialPrice = new Price { Timestamp = new DateTime(2024, 1, 1), Value = 100 };

        var prices = priceGeneratorService.GetPrices(
            initialPrice: initialPrice,
            endDate: new DateTime(2028, 1, 1),
            yearlyYieldPercentage: 0.08M,
            dailyMovementStandardDeviationPercentage: 0.1M);

        foreach (var price in prices.Value.GetPrices())
        {
            System.Console.WriteLine(price);
        }
    }
}
