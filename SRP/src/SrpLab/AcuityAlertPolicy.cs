
namespace SrpLab;

public sealed class AcuityAlertPolicy
{
    public bool ShouldFirePager(int acuity)
    {
        return acuity >= 8;
    }

    public string GetTone(int acuity)
    {
        return acuity >= 8
            ? "ESCALATE"
            : acuity >= 4
                ? "WATCH"
                : "STABLE";
    }
}
