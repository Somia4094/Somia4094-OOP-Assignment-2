
namespace SrpLab;

public sealed class GiftMessageFormatter
{
    public string Format(
        string fromName,
        IEnumerable<string> itemSkus,
        decimal grandTotal)
    {
        var items = string.Join(", ", itemSkus);

        return $"Dear friend,\nA gift from {fromName} awaits ({items}).\nTotal surprise value: {grandTotal:C}\n";
    }
}
