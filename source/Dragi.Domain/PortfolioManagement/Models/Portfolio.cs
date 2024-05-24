namespace Dragi.Domain.PortfolioManagement.Models;

public class Portfolio
{
    public Portfolio(string name, decimal initialCash, IReadOnlyList<Asset> assets)
    {
        Name = name;
        Cash = initialCash;
        Assets = [.. assets];
    }

    public string Name { get; }

    public decimal Cash { get; private set; }

    private HashSet<Asset> Assets { get; }

    public IReadOnlyList<Asset> GetAssets()
    {
        return [.. Assets];
    }

    public void AddTransaction(Transaction transaction)
    {
        transaction.Asset.AddTransaction(transaction);

        Cash -= transaction.GetValue();

        Assets.Add(transaction.Asset);
    }

    public virtual void DepositCash(decimal cash)
    {
        Cash += cash;
    }

    public virtual void WithdrawCash(decimal cash)
    {
        Cash -= cash;
    }

    public decimal GetValue()
    {
        return Cash + Assets.Sum(asset => asset.GetValue());
    }
}
