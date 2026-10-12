namespace LibrarySystem;

public class PremiumMember : Member
{
    public PremiumMember(string personId, string fullName, string phone, decimal discountPercentage)
        : base(personId, fullName, phone, maxLoans: 10, discountPercentage: discountPercentage)
    {
    }

    public int ReadingPoints
    {
        get
        {
            int returnedLoans = 0;
            foreach (Loan loan in Loans)
            {
                if (loan.Status == LoanStatus.Returned)
                    returnedLoans++;
            }
            return returnedLoans * 5;
        }
    }
}
