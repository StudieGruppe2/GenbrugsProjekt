using Core;

namespace Backend.Repositories;

// Repository der gemmer køb i en liste i hukommelsen.

public class PurchaseRepositoryMemory : IPurchaseRepository
{
    // Her ligger alle køb, mens programmet kører.
    private readonly List<Purchase> purchases = new();
 
    // Bruges til at låse listen, så flere forespørgsler på samme tid

    public List<Purchase> GetBySellerId(string sellerId)
    {
        return 
    }

    public Purchase Add(Purchase purchase)
    {
        return 
    }

    public bool UpdatePurchaseStatus(string purchaseId, string status)
    {
        return 
    } 
    
    

}