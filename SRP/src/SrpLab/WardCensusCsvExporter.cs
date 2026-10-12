
namespace SrpLab;

public sealed class WardCensusCsvExporter
{
    public string Export(
        IReadOnlyDictionary<int, string> bedPatients,
        IReadOnlyDictionary<int, int> acuityScores)
    {
        var lines = new List<string> { "bed,patient,acuity" };

        foreach (var bed in bedPatients.Keys.OrderBy(x => x))
        {
            lines.Add($"{bed},{bedPatients[bed]},{acuityScores[bed]}");
        }

        return string.Join('\n', lines);
    }
}
