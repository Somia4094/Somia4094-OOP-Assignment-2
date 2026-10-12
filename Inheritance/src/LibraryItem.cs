namespace LibrarySystem;

public class LibraryItem
{
    public string CatalogNumber { get; }
    public string Title { get; }
    public int LoanPeriodDays { get; }
    public decimal BaseLateFee { get; private set; }
    private decimal LateFeeMultiplier { get; }
    public decimal DailyLateFee => BaseLateFee * LateFeeMultiplier;
    public bool IsWithdrawn { get; private set; }
    public bool IsOnLoan { get; private set; }

    protected LibraryItem(string catalogNumber, string title, int loanPeriodDays,
        decimal baseLateFee, decimal lateFeeMultiplier)
    {
        if (string.IsNullOrWhiteSpace(catalogNumber))
            throw new ArgumentException("Catalog number cannot be empty.", nameof(catalogNumber));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (loanPeriodDays <= 0)
            throw new ArgumentOutOfRangeException(nameof(loanPeriodDays), "Loan period must be positive.");
        if (baseLateFee <= 0)
            throw new ArgumentOutOfRangeException(nameof(baseLateFee), "Base late fee must be greater than zero.");
        if (lateFeeMultiplier <= 0)
            throw new ArgumentOutOfRangeException(nameof(lateFeeMultiplier), "Late-fee multiplier must be positive.");

        CatalogNumber = catalogNumber;
        Title = title;
        LoanPeriodDays = loanPeriodDays;
        BaseLateFee = baseLateFee;
        LateFeeMultiplier = lateFeeMultiplier;
    }

    public void SetBaseLateFee(decimal newBaseLateFee)
    {
        if (newBaseLateFee <= 0)
            throw new ArgumentOutOfRangeException(nameof(newBaseLateFee), "New late fee must be greater than zero.");
        BaseLateFee = newBaseLateFee;
    }

    public void Withdraw() => IsWithdrawn = true;
    public void Restore() => IsWithdrawn = false;

    internal void MarkBorrowed()
    {
        if (IsWithdrawn)
            throw new InvalidOperationException("A withdrawn item cannot be borrowed.");
        if (IsOnLoan)
            throw new InvalidOperationException("This item is already on loan.");
        IsOnLoan = true;
    }

    internal void MarkReturned() => IsOnLoan = false;
}
