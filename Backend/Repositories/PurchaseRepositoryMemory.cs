using Core;

namespace Backend.Repositories;

// Repository der gemmer køb i en liste i hukommelsen.

public class PurchaseRepositoryMemory : IPurchaseRepository
{
    // Her ligger alle køb, mens programmet kører.
    private readonly List<Purchase> purchases = new();

    private int nextPurchasesId = 1;
 
    // Bruges til at låse listen, så flere forespørgsler på samme tid
    public List<Purchase> GetByBuyerId(int Buyer_Id)
    {
        return purchases.Where(p => p.Buyer_Id == Buyer_Id).ToList();
    }

    // Henter alle køb, hvor medlemmet er sælger
    public List<Purchase> GetBySellerId(int Seller_Id)
    {
        return purchases.Where(p => p.Seller_Id == Seller_Id).ToList();
    }


    public Purchase Add(Purchase purchase)
    {
        purchase.Purchase_Id = purchases.Count == 0 ? 1 : purchases.Max(m => m.Purchase_Id) + 1; 


        // Et nyt køb starter altid som "Pending"
        purchase.Status = "Pending";

        purchases.Add(purchase);
        return purchase;
    }
    

    public bool UpdatePurchaseStatus(int purchaseId, string status)
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
