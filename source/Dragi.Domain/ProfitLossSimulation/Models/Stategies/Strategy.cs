using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.ProfitLossSimulation.Models.ChangeRequests;
using Dragi.Domain.ProfitLossSimulation.Models.Conditions;

namespace Dragi.Domain.ProfitLossSimulation.Models.Stategies;

public class Strategy
{
    private readonly ChangeRequest _changeRequest;
    private readonly Condition _condition;

    public Strategy(
        string name,
        ChangeRequest changeRequest,
        Condition condition)
    {
        Name = name;
        _changeRequest = changeRequest;
        _condition = condition;
    }

    public string Name { get; }

    public Result ExecuteStrategy(
        Portfolio portfolio,
        DateTimeOffset date)
    {
        var isConditionSatisfied = _condition.IsConditionSatisfied(date);

        if (!isConditionSatisfied)
        {
            return Result.Success;
        }

        var changeResult = _changeRequest.ApplyChange(portfolio, date);

        return changeResult;
    }
}
