namespace Dragi.Domain.ProfitLossSimulation.Models.Conditions;

public abstract class Condition
{
    public abstract bool IsConditionSatisfied(DateTimeOffset dateTime);
}
