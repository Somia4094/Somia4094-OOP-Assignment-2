# Task 3 — Library System: Design With Inheritance

## Contents
- `ClassDiagram.png` — UML class diagram for Task 3.1.
- `src/` — .NET 8 console project for Tasks 3.2 and 3.3.

## Run
From the `Inheritance` directory:

```bash
dotnet run --project src/LibrarySystem.csproj
```

## Design notes
- `Person`, `Member`, `Staff`, and `LibraryItem` use protected constructors, so only concrete child types can be instantiated.
- `Member` keeps a private `List<Loan>` and exposes an `IReadOnlyList<Loan>` view.
- Each child passes its fixed values to its parent constructor.
- Staff pay uses one `GetMonthlyPay()` method; the head librarian's 400 allowance is passed to `Staff`.
- Each item's `DailyLateFee` uses its configured multiplier, with no type switch.
- The commented lines in `Program.cs` marked `MUST NOT COMPILE` demonstrate compile-time restrictions.
