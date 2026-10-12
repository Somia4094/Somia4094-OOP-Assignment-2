namespace LibrarySystem;

public class Book : LibraryItem
{
    public Book(string catalogNumber, string title, decimal baseLateFee)
        : base(catalogNumber, title, loanPeriodDays: 21, baseLateFee: baseLateFee, lateFeeMultiplier: 1m)
    {
    }
}
