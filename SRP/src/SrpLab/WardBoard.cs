
namespace SrpLab;

/// <summary>
/// Tracks ward beds and coordinates specialized ward services.
/// </summary>
public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();

    private readonly AcuityScorer _acuityScorer = new();
    private readonly AcuityAlertPolicy _alertPolicy = new();
    private readonly PagerAlertLog _pagerLog = new();
    private readonly HandoffNoteFormatter _handoffFormatter = new();
    private readonly WardCensusCsvExporter _censusExporter = new();

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0)
            throw new ArgumentOutOfRangeException(nameof(bed));

        if (string.IsNullOrWhiteSpace(patientId))
            throw new ArgumentException("patient required");

        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        _vitalsScore[bed] = ScoreAcuity(heartRate, spo2);

        if (_alertPolicy.ShouldFirePager(_vitalsScore[bed]))
            _pagerLog.AddCodeYellow(bed);
    }

    public int ScoreAcuity(int heartRate, int spo2)
    {
        return _acuityScorer.Score(heartRate, spo2);
    }

    public string BuildHandoffNote(int bed)
    {
        _bedPatient.TryGetValue(bed, out var patient);

        var acuity = patient is null ? 0 : _vitalsScore[bed];

        return _handoffFormatter.Format(bed, patient, acuity);
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        return _pagerLog.Drain();
    }

    public string ExportCensusCsv()
    {
        return _censusExporter.Export(_bedPatient, _vitalsScore);
    }
}
