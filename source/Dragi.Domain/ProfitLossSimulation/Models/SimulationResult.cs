namespace Dragi.Domain.ProfitLossSimulation.Models;

public record SimulationResult
{
    public required decimal StartValue { get; init; }

    public required decimal EndValue { get; init; }

    public required decimal AdditionalCashInvested { get; init; }

    public required IReadOnlyList<string> Logs { get; init; }

    public decimal GetProfitLoss()
    {
        return EndValue - (StartValue + AdditionalCashInvested);
    }

    public decimal GetProfitLossPercentage()
    {
        var profitLoss = GetProfitLoss();

        var profitLossFactor = profitLoss / EndValue;
        var profitLossPercentage = decimal.Round(profitLossFactor * 100, 2);

        return profitLossPercentage;
    }
}
