
namespace SrpLab;

/// <summary>
/// Manages warehouse pick-list data and coordinates specialized services.
/// </summary>
public sealed class WarehousePickList
{
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();

    private readonly StockAllocationCalculator _allocationCalculator = new();
    private readonly WarehouseWalkingOrder _walkingOrderCalculator = new();
    private readonly PickerScriptFormatter _scriptFormatter = new();
    private readonly WmsXmlFormatter _xmlFormatter = new();

    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        _lines.Add((sku, aisle, bin, qtyNeeded, qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
    {
        return _allocationCalculator.Allocate(_lines);
    }

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder()
    {
        return _walkingOrderCalculator.Calculate(_lines);
    }

    public string PickerScript()
    {
        return _scriptFormatter.Format(
            WalkingOrder(),
            Allocate(),
            _lines);
    }

    public string WmsXmlBatch(string batchId)
    {
        return _xmlFormatter.Format(batchId, Allocate());
    }
}
