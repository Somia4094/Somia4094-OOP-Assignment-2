namespace LibrarySystem;

public class DVD : LibraryItem
{
    public DVD(string catalogNumber, string title, decimal baseLateFee)
        : base(catalogNumber, title, loanPeriodDays: 7, baseLateFee: baseLateFee, lateFeeMultiplier: 2m)
    {
    }
}
