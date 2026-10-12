namespace LibrarySystem;

public class Member : Person
{
    private readonly List<Loan> _loans = new();

    public int MaxLoans { get; }
    public decimal DiscountPercentage { get; }
    public IReadOnlyList<Loan> Loans => _loans.AsReadOnly();

    protected Member(string personId, string fullName, string phone, int maxLoans, decimal discountPercentage)
        : base(personId, fullName, phone)
    {
        if (maxLoans <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxLoans), "Maximum loans must be positive.");
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount must be between 0 and 100 percent.");

        MaxLoans = maxLoans;
        DiscountPercentage = discountPercentage;
    }

    public Loan BorrowItem(string loanId, LibraryItem item, DateTime borrowDate)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item), "Item cannot be null.");

        int activeLoans = 0;
        foreach (Loan loan in _loans)
        {
            if (loan.Status == LoanStatus.Borrowed)
                activeLoans++;
        }

        if (activeLoans >= MaxLoans)
            throw new InvalidOperationException($"This member cannot borrow more than {MaxLoans} active items.");

        Loan newLoan = new(loanId, borrowDate, this, item);
        _loans.Add(newLoan);
        return newLoan;
    }
}
