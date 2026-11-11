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

    // Måske at ActionResult skal være der
    [HttpGet("buyer/{buyerId}")]
    public ActionResult<List<Purchase>> GetByBuyerId(string buyerId)
    {
        return
    }
    
    // Samme spørgsmål her 
    [HttpGet("seller/{sellerId}")]
    public ActionResult<List<Purchase>> GetBySellerId(string sellerId)
    {
        return
    }
    
    // POST api/purchase
    [HttpPost]
    public ActionResult<Purchase> Add(Purchase purchase)
    {
        return 
    }
    
    
    // PUT api/purchase/{purchaseId}/status
    [HttpPut("{purchaseId}/status")]
    public ActionResult<bool> UpdatePurchaseStatus(string purchaseId, string status)
    {
        return 
    }
}