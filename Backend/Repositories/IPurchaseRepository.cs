using Core;

namespace Backend.Repositories;

public interface IPurchaseRepository
{
    List<Purchase> GetByBuyerId(string buyerId);

    List<Purchase> GetBySellerId(string sellerId);

    Purchase Add(Purchase purchase);

    bool UpdatePurchaseStatus(string purchaseId, string status);
}
