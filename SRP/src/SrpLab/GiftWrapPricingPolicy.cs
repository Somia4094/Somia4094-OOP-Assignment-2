
namespace SrpLab;

public sealed class GiftWrapPricingPolicy
{
    public decimal GetFee(bool giftWrapEnabled)
    {
        return giftWrapEnabled ? 4.99m : 0m;
    }
}
