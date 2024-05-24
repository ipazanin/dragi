using Dragi.Domain.MarketData.Models;

namespace Dragi.Domain.PortfolioManagement.Models;

public class Asset : IEquatable<Asset>
{
    public Asset(string ticker, IReadOnlyList<Transaction> transactions, Price latestPrice)
    {
        Ticker = ticker;
        Transactions = [.. transactions];
        LatestPrice = latestPrice;
    }

    public string Ticker { get; }

    private List<Transaction> Transactions { get; }

    public Price LatestPrice { get; private set; }

    public bool IsSoldOut => GetNumberOfUnits() == 0M;

    public void SetLatestPrice(Price latestPrice)
    {
        LatestPrice = latestPrice;
    }

    public decimal GetNumberOfUnits()
    {
        return Transactions.Sum(t => t.NumberOfUnits);
    }

    public decimal GetValue()
    {
        return LatestPrice.Value * GetNumberOfUnits();
    }

    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
    }

    public IReadOnlyList<Transaction> GetTransactions()
    {
        return Transactions;
    }

    public decimal GetProfitLossChange()
    {
        var buyValue = Transactions.Sum(transaction => transaction.GetValue());

        var currentValue = GetValue();

        return currentValue - buyValue;
    }

    public decimal GetProfitLossChangePercentage()
    {
        var profitLossChange = GetProfitLossChange();
        var currentValue = GetValue();

        var profitLossChangePercentage = 100 * profitLossChange / currentValue;

        return profitLossChangePercentage;
    }

    public bool Equals(Asset? other)
    {
        if (other is null)
        {
            return false;
        }

        return Ticker.Equals(other.Ticker, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Asset);
    }

    public override int GetHashCode()
    {

        return Ticker.GetHashCode();
    }

    public static bool operator ==(Asset? lhs, Asset? rhs)
    {
        if (lhs == null && rhs == null)
        {
            return true;
        }

        if (lhs is null)
        {
            return false;
        }

        return lhs.Equals(rhs);
    }

    public static bool operator !=(Asset? lhs, Asset? rhs)
    {
        return !(lhs == rhs);
    }
}
