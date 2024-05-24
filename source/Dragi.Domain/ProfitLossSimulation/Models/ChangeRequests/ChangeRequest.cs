using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.ProfitLossSimulation.Models.ChangeRequests;

public abstract class ChangeRequest
{
    public abstract Result ApplyChange(
        Portfolio portfolio,
        DateTimeOffset date);
}
