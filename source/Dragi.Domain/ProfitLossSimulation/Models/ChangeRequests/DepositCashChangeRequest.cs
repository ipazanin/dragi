using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.ProfitLossSimulation.Models.ChangeRequests;

public class DepositCashChangeRequest : ChangeRequest
{
    private readonly decimal _depositAmount;

    public DepositCashChangeRequest(decimal depositAmount)
    {
        _depositAmount = depositAmount;
    }

    public override Result ApplyChange(Portfolio portfolio, DateTimeOffset date)
    {
        portfolio.DepositCash(_depositAmount);

        return Result.Success;
    }
}
