
namespace SrpLab;

/// <summary>
/// Coordinates loan application data and delegates specialized responsibilities.
/// </summary>
public sealed class LoanDesk
{
    public decimal RequestedAmount { get; }
    public int CreditScore { get; }
    public int EmploymentMonths { get; }
    public bool HasCollateral { get; }

    private readonly LoanRiskCalculator _riskCalculator = new();
    private readonly LoanDocumentPolicy _documentPolicy = new();
    private readonly LoanDecisionLetterFormatter _letterFormatter = new();
    private readonly LoanCsvFormatter _csvFormatter = new();

    public LoanDesk(
        decimal requestedAmount,
        int creditScore,
        int employmentMonths,
        bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;
    }

    public decimal RiskScore()
    {
        return _riskCalculator.Calculate(
            RequestedAmount,
            CreditScore,
            EmploymentMonths,
            HasCollateral);
    }

    public bool IsEligible()
    {
        return _riskCalculator.IsEligible(RiskScore(), CreditScore);
    }

    public IReadOnlyList<string> RequiredDocuments()
    {
        return _documentPolicy.GetRequiredDocuments(
            RequestedAmount,
            EmploymentMonths,
            HasCollateral,
            IsEligible());
    }

    public string DecisionLetter(string applicantName)
    {
        return _letterFormatter.Format(
            applicantName,
            RequestedAmount,
            RiskScore(),
            IsEligible(),
            RequiredDocuments());
    }

    public string UnderwriterCsvRow(string applicationId)
    {
        return _csvFormatter.Format(
            applicationId,
            CreditScore,
            EmploymentMonths,
            HasCollateral,
            RiskScore(),
            IsEligible());
    }
}
