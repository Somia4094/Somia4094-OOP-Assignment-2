
namespace SrpLab;

public sealed class HandoffNoteFormatter
{
    public string Format(int bed, string? patient, int acuity)
    {
        if (patient is null)
            return $"Bed {bed}: empty";

        var tone = acuity >= 8
            ? "ESCALATE"
            : acuity >= 4
                ? "WATCH"
                : "STABLE";

        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
    }
}
