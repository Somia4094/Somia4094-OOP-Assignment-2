
namespace SrpLab;

public sealed class ExpoLaneHintPolicy
{
    public string GetHint(bool hasAllergens, int estimatedMinutes)
    {
        if (hasAllergens)
            return "LANE-ALLERGY";

        return estimatedMinutes > 20
            ? "LANE-SLOW"
            : "LANE-FAST";
    }
}
