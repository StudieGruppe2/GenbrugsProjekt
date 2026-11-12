
using Core;

namespace Backend.Repositories;

// Repository der gemmer købsanmodninger i en liste i hukommelsen
public class PurchaseRepositoryMemory : IPurchaseRepository
{
    // ---------- DATALAGER ----------
    // Listen gemmer alle købsanmodninger, mens backend kører
    private readonly List<Purchase> purchases = new();


    // ---------- HENT KØBSANMODNINGER ----------
    // Henter alle købsanmodninger, som en bestemt bruger har sendt
    public List<Purchase> GetByBuyerId(int Buyer_Id)
    {
        // Where finder alle købsanmodninger, hvor Buyer_Id matcher brugerens ID
        // ToList omdanner resultatet til en liste
        return purchases.Where(p => p.Buyer_Id == Buyer_Id).ToList();
    }

    // Henter alle købsanmodninger, som en bestemt sælger har modtaget
    public List<Purchase> GetBySellerId(int Seller_Id)
    {
        // Where finder alle købsanmodninger, hvor Seller_Id matcher sælgerens ID
        return purchases.Where(p => p.Seller_Id == Seller_Id).ToList();
    }


    // ---------- OPRET KØBSANMODNING ----------
    // Opretter en ny købsanmodning og gemmer den i listen
    public Purchase Add(Purchase purchase)
    {
        // Hvis listen er tom, får købsanmodningen ID 1
        // Ellers finder vi det højeste ID og lægger 1 til
        purchase.Purchase_Id = purchases.Count == 0 ? 1 : purchases.Max(p => p.Purchase_Id) + 1;

        // En ny købsanmodning starter altid med status Pending (afventer svar)
        purchase.Status = "Pending";

        // Tilføjer købsanmodningen til listen
        purchases.Add(purchase);

        // Returnerer den oprettede købsanmodning
        return purchase;
    }


    // ---------- OPDATER STATUS ----------
    // Ændrer status på en købsanmodning, når sælgeren accepterer eller afslår
    public bool UpdatePurchaseStatus(int purchaseId, string status)
    {
        // Finder købsanmodningen ud fra dens ID
        // FirstOrDefault returnerer null, hvis købsanmodningen ikke findes
        Purchase? purchase = purchases.FirstOrDefault(p => p.Purchase_Id == purchaseId);

        // Hvis købsanmodningen ikke findes, returneres false
        if (purchase == null)
        {
            return false;
        }

        // Ændrer købsanmodningens status til den nye status
        purchase.Status = status;

        // Returnerer true, fordi status blev opdateret
        return true;
    }
}
