namespace Dragi.Domain.ProfitLossSimulation.Models.Conditions;

public class MonthlyCondition : Condition
{
    private readonly int _dayInMonth;

    public MonthlyCondition(int dayInMonth)
    {
        _dayInMonth = dayInMonth switch
        {
            > 28 or < 1 => 1,
            _ => dayInMonth,
        };
    }

    public override bool IsConditionSatisfied(DateTimeOffset dateTime)
    {
        if (dateTime.Day == _dayInMonth)
        {
            return true;
        }

        return false;
    }
}
