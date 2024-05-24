namespace Dragi.Domain.ProfitLossSimulation.Models.Conditions;

public class TimeCondition : Condition
{
    private readonly TimeSpan _cycleTime;

    public TimeCondition(DateTimeOffset initialDateTime, TimeSpan cycleTime)
    {
        _cycleTime = cycleTime;
        LastDateTime = initialDateTime;
    }

    private DateTimeOffset LastDateTime { get; set; }

    public override bool IsConditionSatisfied(DateTimeOffset dateTime)
    {
        var dateTimeDiff = dateTime - LastDateTime;

        if (dateTimeDiff > _cycleTime)
        {
            LastDateTime = dateTime;

            return true;
        }

        return false;
    }
}
