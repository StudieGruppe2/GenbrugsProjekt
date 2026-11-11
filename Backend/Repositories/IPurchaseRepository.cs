using Core;

namespace Backend.Repositories;

//Kontrakten for Controller og Repo (alt datalagring af køb)
public interface IPurchaseRepository 
{ 
    // Henter alle køb, hvor det givne medlem er køber.
    // Giver en tom liste, hvis der ikke er nogen.
    List<Purchase> GetByBuyerId(int buyer_Id);
    
    // Henter alle køb, hvor det givne medlem er sælger
    // (bruges til at vise indkomne købsforespørgsler).
    List<Purchase> GetBySellerId(int seller_Id);
    
    // Gemmer et nyt køb og returnerer det, nu med et udfyldt Purchase_Id.
    Purchase Add(Purchase purchase);
    
    // Ændrer status på et eksisterende køb.
    // Returnerer true, hvis købet fandt og blev opdateret, ellers false.
    bool UpdatePurchaseStatus(string purchase_Id, string status);
}