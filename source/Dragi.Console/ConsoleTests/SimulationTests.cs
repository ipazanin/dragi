using Dragi.Common.Utilities;
using Dragi.Domain.MarketData.Models;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.ProfitLossSimulation.Models;
using Dragi.Domain.ProfitLossSimulation.Models.ChangeRequests;
using Dragi.Domain.ProfitLossSimulation.Models.Conditions;
using Dragi.Domain.ProfitLossSimulation.Models.Stategies;
using Dragi.Domain.ProfitLossSimulation.Services;

namespace Dragi.Console.ConsoleTests;

public static class SimulationTests
{
    public static void Test()
    {
        var pricesGenerator = new PriceGeneratorService(new GaussianRandomGenerator());

        var portfolio = new Portfolio(
            name: "Dragi portfolio",
            initialCash: 60_000M,
            assets: []);

        var startDate = DateTime.UtcNow;
        var endDate = new DateTime(2040, 1, 1);
        var initialPrice = new Price
        {
            Timestamp = startDate,
            Value = 500,
        };
        var prices = pricesGenerator.GetPrices(initialPrice, endDate, 0.1M, 0.05M);
        var latestPrice = prices.Value.GetLastValue();
        var asset = new Asset("SPY", [], latestPrice!);
        var assetWithPrices = new AssetWithPrices
        {
            Prices = prices.Value,
            Asset = asset
        };

        var depositCashChangeRequest = new DepositCashChangeRequest(depositAmount: 1000M);
        var monthlyCondition = new MonthlyCondition(dayInMonth: 20);
        var cashDepositStrategy = new Strategy(
            name: "Monthly cash deposit strategy",
            changeRequest: depositCashChangeRequest,
            condition: monthlyCondition);

        var constantBuySellChangeRequest = new ConstantBuySellChangeRequest(
            assetWithPrices: assetWithPrices,
            numberOfAssetsToBuy: 2);
        var constantBuyStrategy = new Strategy(
            name: "Buy SPY monthly strategy",
            changeRequest: constantBuySellChangeRequest,
            condition: monthlyCondition);

        // var portfolioPercentageBuySellChangeRequest = new PortfolioPercentageBuySellChangeRequest(
        //     percentageOfPortfolio: 0.2M,
        //     assetWithPrices: assetWithPrices);
        // var percentageDropRepetitiveCondition = new PercentageDropRepetitiveAssetCondition(
        //     assetWithPrices: assetWithPrices,
        //     initialPrice: initialPrice,
        //     percentageDrop: 0.03M);
        // var percentageDropBuyStrategy = new Strategy(
        //     name: "Buy SPY when drops strategy",
        //     changeRequest: portfolioPercentageBuySellChangeRequest,
        //     condition: percentageDropRepetitiveCondition);

        var simulation = new Simulation(
            name: "Dragi portfolio simulation",
            strategies: [cashDepositStrategy, constantBuyStrategy]);

        var simulationResult = simulation.StartSimulation(portfolio, startDate, endDate);

        foreach (var log in simulationResult.Logs)
        {
            System.Console.WriteLine(log);
        }

        System.Console.WriteLine($"Start value: {simulationResult.StartValue}");
        System.Console.WriteLine($"End value: {simulationResult.EndValue}");
        System.Console.WriteLine($"Invested value: {simulationResult.AdditionalCashInvested}");
        System.Console.WriteLine($"End value: {simulationResult.EndValue}");
        System.Console.WriteLine($"Profit Loss: {simulationResult.GetProfitLoss()}");
        System.Console.WriteLine($"Profit Loss %: {simulationResult.GetProfitLossPercentage()}%");
    }
}
