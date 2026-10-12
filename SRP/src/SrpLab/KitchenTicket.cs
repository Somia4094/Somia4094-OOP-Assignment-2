
namespace SrpLab;

/// <summary>
/// Manages kitchen ticket items and coordinates kitchen services.
/// </summary>
public sealed class KitchenTicket
{
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();

    private readonly AllergenDetector _allergenDetector = new();
    private readonly KitchenEtaCalculator _etaCalculator = new();
    private readonly ThermalTicketFormatter _ticketFormatter = new();
    private readonly ExpoLaneHintPolicy _laneHintPolicy = new();

    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        _items.Add((
            item,
            ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(),
            prepMinutes));
    }

    public IReadOnlyList<string> DetectAllergens()
    {
        return _allergenDetector.Detect(
            _items.Select(i => (IEnumerable<string>)i.Ingredients));
    }

    public int EstimatedReadyMinutes(int openStations)
    {
        return _etaCalculator.Calculate(
            _items.Select(i => i.PrepMinutes),
            openStations,
            DetectAllergens().Count > 0);
    }

    public string RenderThermalTicket(int orderNumber)
    {
        var items = _items.Select(i => (i.Item, i.PrepMinutes));

        return _ticketFormatter.Format(
            orderNumber,
            EstimatedReadyMinutes(2),
            items,
            DetectAllergens());
    }

    public string ExpoLaneHint()
    {
        return _laneHintPolicy.GetHint(
            DetectAllergens().Count > 0,
            EstimatedReadyMinutes(2));
    }
}
