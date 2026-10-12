using LibrarySystem;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Library System: inheritance demo ===");

        // These lines MUST NOT COMPILE: the parent classes have protected constructors.
        // Person person = new("P1", "Person", "01000000000");
        // Member member = new("M1", "Member", "01000000000", 3, 0m);
        // Staff staff = new("S1", "Staff", "01000000000", DateTime.Today, 5000m, 0m);
        // LibraryItem item = new("C1", "Item", 1, 2m, 1m);

        StudentMember student = new("M-ST-01", "Mona Student", "01011111111");
        // These lines MUST NOT COMPILE: identity is get-only, Loans is IReadOnlyList, IsOnLoan has no public setter.
        // student.FullName = "Changed Name";
        // student.Loans.Add(new Loan("L-OUTSIDE", DateTime.Today, student, new Book("B-OUT", "Outside", 2m)));
        // new Book("B-STATE", "State", 2m).IsOnLoan = true;

        Console.WriteLine("\n-- Validation: withdrawn item --");
        Book withdrawnBook = new("B-WITHDRAWN", "Withdrawn book", 2m);
        withdrawnBook.Withdraw();
        TryBorrow(student, "L-W1", withdrawnBook, new DateTime(2026, 10, 1));

        Console.WriteLine("\n-- Validation: item already on loan --");
        Book sharedBook = new("B-SHARED", "Shared book", 2m);
        student.BorrowItem("L-SHARED-1", sharedBook, new DateTime(2026, 10, 1));
        StudentMember anotherStudent = new("M-ST-02", "Omar Student", "01022222222");
        TryBorrow(anotherStudent, "L-SHARED-2", sharedBook, new DateTime(2026, 10, 2));

        Console.WriteLine("\n-- Validation: student tries to borrow a fourth active item --");
        StudentMember studentForLimit = new("M-ST-03", "Salma Student", "01033333333");
        studentForLimit.BorrowItem("L-LIMIT-1", new Book("B-L1", "Book 1", 1m), new DateTime(2026, 10, 1));
        studentForLimit.BorrowItem("L-LIMIT-2", new Book("B-L2", "Book 2", 1m), new DateTime(2026, 10, 1));
        studentForLimit.BorrowItem("L-LIMIT-3", new Book("B-L3", "Book 3", 1m), new DateTime(2026, 10, 1));
        TryBorrow(studentForLimit, "L-LIMIT-4", new Book("B-L4", "Book 4", 1m), new DateTime(2026, 10, 1));

        Console.WriteLine("\n-- Polymorphic staff monthly pay --");
        HeadLibrarian head = new("S-HEAD", "Hala Head", "01055555555", new DateTime(2020, 3, 1), 10000m);
        List<Staff> staffMembers = new()
        {
            new Librarian("S-LIB", "Ali Librarian", "01044444444", new DateTime(2022, 5, 1), 7000m),
            head,
            new Shelver("S-SHELF", "Nour Shelver", "01066666666", new DateTime(2024, 1, 1), 5000m, "Fiction")
        };
        foreach (Staff staffMember in staffMembers)
            Console.WriteLine($"{staffMember.FullName}: {staffMember.GetMonthlyPay():0.00}");

        Console.WriteLine("\n-- Polymorphic item loan periods and daily late fees --");
        List<LibraryItem> items = new()
        {
            new Book("B-DEMO", "C# Book", 10m),
            new DVD("D-DEMO", "Learning DVD", 10m),
            new Magazine("M-DEMO", "Science Magazine", 10m)
        };
        foreach (LibraryItem item in items)
            Console.WriteLine($"{item.Title}: {item.LoanPeriodDays} days, daily fee = {item.DailyLateFee:0.00}");

        Console.WriteLine("\n-- Premium member returns a DVD five days late --");
        PremiumMember premium = new("M-PR-01", "Youssef Premium", "01077777777", discountPercentage: 20m);
        DVD premiumDvd = new("D-LATE", "Late DVD", 10m);
        Loan lateLoan = premium.BorrowItem("L-LATE", premiumDvd, new DateTime(2026, 10, 1));
        DateTime lateReturnDate = lateLoan.DueDate.AddDays(5);
        lateLoan.Return(lateReturnDate);
        Console.WriteLine($"Due date: {lateLoan.DueDate:yyyy-MM-dd}");
        Console.WriteLine($"Return date: {lateLoan.ReturnDate:yyyy-MM-dd}");
        Console.WriteLine($"Late fee: {lateLoan.LateFee:0.00}");
        Console.WriteLine($"Reading points: {premium.ReadingPoints}");

        Console.WriteLine("\n-- Validation: return the same loan twice --");
        TryReturn(lateLoan, lateReturnDate.AddDays(1));

        Console.WriteLine("\n-- Validation: mark a returned loan as lost --");
        TryMarkLost(lateLoan);

        Console.WriteLine("\n-- Validation: non-positive raise and late fee --");
        TryRaise(staffMembers[0], 0m);
        TryChangeLateFee(head, items[0], -1m);

        Console.WriteLine("\nDemo finished.");
    }

    private static void TryBorrow(Member member, string loanId, LibraryItem item, DateTime borrowDate)
    {
        try
        {
            member.BorrowItem(loanId, item, borrowDate);
            Console.WriteLine("Unexpected: the borrow was accepted.");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Rejected: {exception.Message}");
        }
    }

    private static void TryReturn(Loan loan, DateTime returnDate)
    {
        try
        {
            loan.Return(returnDate);
            Console.WriteLine("Unexpected: the return was accepted.");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Rejected: {exception.Message}");
        }
    }

    private static void TryMarkLost(Loan loan)
    {
        try
        {
            loan.MarkAsLost();
            Console.WriteLine("Unexpected: the lost status was accepted.");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Rejected: {exception.Message}");
        }
    }

    private static void TryRaise(Staff staff, decimal percentage)
    {
        try
        {
            staff.GiveRaise(percentage);
            Console.WriteLine("Unexpected: the raise was accepted.");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Rejected: {exception.Message}");
        }
    }

    private static void TryChangeLateFee(HeadLibrarian head, LibraryItem item, decimal amount)
    {
        try
        {
            head.ChangeLateFee(item, amount);
            Console.WriteLine("Unexpected: the late fee was accepted.");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Rejected: {exception.Message}");
        }
    }
}
