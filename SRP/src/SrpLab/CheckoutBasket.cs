
namespace SrpLab;

public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private readonly CouponDiscountCalculator _discountCalculator = new();
    private readonly GiftWrapPricingPolicy _giftWrapPolicy = new();
    private readonly GiftMessageFormatter _giftMessageFormatter = new();
    private readonly PaymentAuthorizationStub _paymentAuthorization = new();

    private string? _couponRaw;
    private bool _giftWrap;

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0)
            throw new ArgumentOutOfRangeException(nameof(qty));

        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText) => _couponRaw = couponText;

    public void EnableGiftWrap() => _giftWrap = true;

    public decimal SubTotal() =>
        _lines.Sum(l => l.Price * l.Qty);

    public decimal DiscountAmount() =>
        _discountCalculator.CalculateDiscount(_couponRaw, SubTotal());

    public decimal GrandTotal()
    {
        var total = SubTotal() - DiscountAmount();
        total += _giftWrapPolicy.GetFee(_giftWrap);

        return Math.Max(0m, total);
    }

    public string GiftMessageCard(string fromName)
    {
        return _giftMessageFormatter.Format(
            fromName,
            _lines.Select(l => l.Sku),
            GrandTotal());
    }

    public string AuthorizePaymentStub(string cardLast4)
    {
        return _paymentAuthorization.Authorize(
            GrandTotal(),
            cardLast4,
            _lines.Count);
    }
}
