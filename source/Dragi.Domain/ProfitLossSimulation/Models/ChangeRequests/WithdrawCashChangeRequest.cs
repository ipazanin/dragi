using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.ProfitLossSimulation.Models.ChangeRequests;

public class WithdrawCashChangeRequest : ChangeRequest
{
    private readonly decimal _withdrawAmount;

    public WithdrawCashChangeRequest(decimal withdrawAmount)
    {
        _withdrawAmount = withdrawAmount;
    }

    public override Result ApplyChange(Portfolio portfolio, DateTimeOffset date)
    {
        portfolio.WithdrawCash(_withdrawAmount);

        return Result.Success;
    }
}
