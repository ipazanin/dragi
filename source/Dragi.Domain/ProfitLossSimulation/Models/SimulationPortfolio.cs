using Dragi.Domain.PortfolioManagement.Models;

public class SimulationPortfolio : Portfolio
{
    public SimulationPortfolio(
        string name,
        decimal initialCash,
        IReadOnlyList<Asset> assets,
        Action<decimal> cashUpdated) : base(name, initialCash, assets)
    {
        CashUpdated = cashUpdated;
    }

    public Action<decimal> CashUpdated { get; }

    public override void DepositCash(decimal cash)
    {
        base.DepositCash(cash);
        CashUpdated.Invoke(cash);
    }

    public override void WithdrawCash(decimal cash)
    {
        base.WithdrawCash(cash);
        CashUpdated.Invoke(-cash);
    }
}
