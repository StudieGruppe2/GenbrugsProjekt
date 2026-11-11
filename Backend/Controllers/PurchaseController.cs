using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")] // adressen bliver /Purchase

public class PurchaseController : ControllerBase
{
    // Dependency Inversion: Controlleren afhænger af interfacet
    // i stedet for Memory eller MongoDB, så vi kan skifte repository
    // uden at ændre controllerens kode.

    private readonly IPurchaseRepository repository;
    
    public PurchaseController(IPurchaseRepository repository)
    {
        // Dependency Injection: Det valgte repository modtages udefra - gennem constructoren i stedet for at blive oprettet her.
        this.repository = repository;
    }

    //Henter alle køb, som et medlem har lavet som køber.
    [HttpGet("buyer/{buyerId}")]
    public ActionResult<List<Purchase>> GetByBuyerId(string buyerId)
    {
        return
    }
    
    //Henter alle køb, som er rettet mod et medlem som sælger,
    //altså de købsforespørgsler sælgeren har modtaget.
    [HttpGet("seller/{sellerId}")]
    public ActionResult<List<Purchase>> GetBySellerId(string sellerId)
    {
        return
    }
    
    //Opretter et nyt køb (en købsforespørgsel på en annonce).
    [HttpPost]
    public ActionResult<Purchase> Add(Purchase purchase)
    {
        return 
    }
    
    
    //Ændrer status på et køb. Sælgeren bruger det til at acceptere
    //eller afvise en købsforespørgsel.
    [HttpPut("{purchaseId}/status")]
    public ActionResult<bool> UpdatePurchaseStatus(string purchaseId,string status)
    {
        return 
    }
}