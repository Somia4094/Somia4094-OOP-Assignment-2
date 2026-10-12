namespace LibrarySystem;

public class Loan
{
    public string LoanId { get; }
    public DateTime BorrowDate { get; }
    public Member Member { get; }
    public LibraryItem Item { get; }
    public DateTime DueDate => BorrowDate.AddDays(Item.LoanPeriodDays);
    public LoanStatus Status { get; private set; }
    public DateTime? ReturnDate { get; private set; }

    public decimal LateFee
    {
        get
        {
            if (Status != LoanStatus.Returned || ReturnDate is null || ReturnDate.Value <= DueDate)
                return 0m;

            int lateDays = (ReturnDate.Value.Date - DueDate.Date).Days;
            decimal grossFee = lateDays * Item.DailyLateFee;
            decimal discount = grossFee * Member.DiscountPercentage / 100m;
            return grossFee - discount;
        }
    }

    internal Loan(string loanId, DateTime borrowDate, Member member, LibraryItem item)
    {
        if (string.IsNullOrWhiteSpace(loanId))
            throw new ArgumentException("Loan ID cannot be empty.", nameof(loanId));
        if (member is null)
            throw new ArgumentNullException(nameof(member));
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        LoanId = loanId;
        BorrowDate = borrowDate;
        Member = member;
        Item = item;
        Status = LoanStatus.Borrowed;
        item.MarkBorrowed();
    }

    public void Return(DateTime returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only a borrowed loan can be returned.");
        if (returnDate.Date < BorrowDate.Date)
            throw new ArgumentException("Return date cannot be earlier than the borrow date.", nameof(returnDate));

        ReturnDate = returnDate;
        Status = LoanStatus.Returned;
        Item.MarkReturned();
    }

    public void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only a borrowed loan can be marked as lost.");

        Status = LoanStatus.Lost;
        Item.MarkReturned();
    }
}
