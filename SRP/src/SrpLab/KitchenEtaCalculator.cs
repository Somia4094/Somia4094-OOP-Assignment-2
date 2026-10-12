
namespace SrpLab;

public sealed class KitchenEtaCalculator
{
    public int Calculate(
        IEnumerable<int> prepMinutes,
        int openStations,
        bool hasAllergens)
    {
        if (openStations <= 0)
            openStations = 1;

        var items = prepMinutes.ToList();

        var sequential = items.Sum();
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);

        if (hasAllergens)
            parallel += 3;

        var longest = items.Count == 0 ? 0 : items.Max();

        return Math.Max(parallel, longest);
    }
}
