
namespace SrpLab;

public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly GradeLetterPolicy _letterPolicy = new();
    private readonly TranscriptFormatter _transcriptFormatter = new();

    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(score));

        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }

        list.Add(score);
    }

    public decimal Average(string studentId)
    {
        if (!_scores.TryGetValue(studentId, out var list) || list.Count == 0)
            return 0m;

        return Math.Round(list.Average(), 2);
    }

    public string Letter(string studentId)
    {
        return _letterPolicy.GetLetter(Average(studentId));
    }

    public bool MeetsHonorRoll(string studentId)
    {
        var average = Average(studentId);
        var letter = Letter(studentId);

        return average >= 85 && (letter is "A" or "B");
    }

    public string TranscriptPlain(string studentId, string fullName)
    {
        return _transcriptFormatter.FormatPlain(
            studentId,
            fullName,
            Average(studentId),
            Letter(studentId),
            MeetsHonorRoll(studentId));
    }

    public string ExportCsv()
    {
        var records = _scores.Keys
            .OrderBy(id => id)
            .Select(id => new GradeRecord(
                id,
                Average(id),
                Letter(id),
                MeetsHonorRoll(id)));

        return _transcriptFormatter.ExportCsv(records);
    }
}
