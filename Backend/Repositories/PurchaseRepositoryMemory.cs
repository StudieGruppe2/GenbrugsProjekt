using Core;

namespace Backend.Repositories;

// Repository der gemmer køb i en liste i hukommelsen.

public class PurchaseRepositoryMemory : IPurchaseRepository
{
    // Her ligger alle køb, mens programmet kører.
    private readonly List<Purchase> purchases = new();

    private int nextPurchasesId = 1;
 
    // Bruges til at låse listen, så flere forespørgsler på samme tid
    public List<Purchase> GetByBuyerId(int buyerId)
    {
        return purchases.Where(p => p.Buyer_Id == buyerId).ToList();
    }

    // Henter alle køb, hvor medlemmet er sælger
    public List<Purchase> GetBySellerId(int sellerId)
    {
        return purchases.Where(p => p.Seller_Id == sellerId).ToList();
    }


    public Purchase Add(Purchase purchase)
    {
        purchase.Buyer_Id = nextPurchasesId;
        nextPurchasesId++;

        purchases.Add(purchase);
        return purchase;
    }

    public bool UpdatePurchaseStatus(string purchaseId, string status)
    {
          Purchase? purchase = purchases.FirstOrDefault(p => p.Purchase_Id == purchaseId);
        if (purchase == null)
        {
            return false;
        }

        purchase.Status = status;
        return true;
    } 
    
    

}