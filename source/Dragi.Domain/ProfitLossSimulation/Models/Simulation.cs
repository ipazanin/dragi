using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.ProfitLossSimulation.Models.Stategies;

namespace Dragi.Domain.ProfitLossSimulation.Models;

public class Simulation
{
    private readonly IReadOnlyList<Strategy> _strategies;

    public Simulation(string name, IReadOnlyList<Strategy> strategies)
    {
        Name = name;
        _strategies = strategies;
    }

    public string Name { get; }

    public SimulationResult StartSimulation(
        Portfolio initialPortfolio,
        DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        var simulationAssets = initialPortfolio
            .GetAssets()
            .Select(asset => new Asset(asset.Ticker, asset.GetTransactions(), asset.LatestPrice))
            .ToArray();

        var currentDate = startDate;
        var additionalCashInvested = 0m;

        var cashUpdated = (decimal cash) =>
        {
            additionalCashInvested += cash;
        };

        var simulationPortfolio = new SimulationPortfolio(
            name: Name,
            initialCash: initialPortfolio.Cash,
            assets: simulationAssets,
            cashUpdated: cashUpdated);

        var logs = new List<string>();

        while (currentDate.Date <= endDate.Date)
        {
            foreach (var strategy in _strategies)
            {
                var executeStrategyResult = strategy.ExecuteStrategy(simulationPortfolio, currentDate);
                if (executeStrategyResult.IsFail)
                {
                    logs.Add(executeStrategyResult.FailReason);
                }
            }

            currentDate += TimeSpan.FromDays(1);
        }

        var simulationResult = new SimulationResult
        {
            StartValue = initialPortfolio.GetValue(),
            EndValue = simulationPortfolio.GetValue(),
            AdditionalCashInvested = additionalCashInvested,
            Logs = logs,
        };

        return simulationResult;
    }
}
