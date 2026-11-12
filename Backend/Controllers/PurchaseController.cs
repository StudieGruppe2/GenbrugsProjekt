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

    // Bruges til at tjekke og reservere annoncen, når nogen anmoder om køb
    private readonly IAnnonceRepository annonceRepository;
    
    public PurchaseController(IPurchaseRepository repository, IAnnonceRepository annonceRepository)
    {
        // Dependency Injection: Det valgte repository modtages udefra - gennem constructoren i stedet for at blive oprettet her.
        this.repository = repository;
        this.annonceRepository = annonceRepository;
    }

    //Henter alle køb, som et medlem har lavet som køber.
    [HttpGet("buyer/{Buyer_Id}")]
    public ActionResult<List<Purchase>> GetByBuyerId(int Buyer_Id)
    {
        return repository.GetByBuyerId(Buyer_Id);
    }
    
    //Henter alle køb, som er rettet mod et medlem som sælger,
    //altså de købsforespørgsler sælgeren har modtaget.
    [HttpGet("seller/{Seller_Id}")]
    public ActionResult<List<Purchase>> GetBySellerId(int Seller_Id)
    {
        return  repository.GetBySellerId(Seller_Id);
    }
    
    //Opretter et nyt køb (en købsforespørgsel på en annonce).
    //Frontend sender kun Annonce_Id og Buyer_Id - resten finder backend selv.
    [HttpPost]
    public ActionResult<Purchase> Add(Purchase purchase)
    {
        // Findes annoncen?
        Annonce? annonce = annonceRepository.GetById(purchase.Annonce_Id);
        if (annonce == null)
        {
            return NotFound("Annoncen findes ikke.");
        }

        // Kan den stadig købes?
        if (annonce.Status != "Active")
        {
            return BadRequest("Annoncen er ikke længere til salg.");
        }

        // Sælgeren er altid ejeren af annoncen
        purchase.Seller_Id = annonce.Owner_Id;

        if (purchase.Buyer_Id == purchase.Seller_Id)
        {
           return BadRequest("you cannot buy your own item!"); 
        }

        // Annoncen reserveres, mens sælgeren tager stilling
        annonceRepository.UpdateAnnonceStatus(annonce.Annonce_Id, "Reserved");

        return  repository.Add(purchase);
    } 
    
    
    
    // De eneste tilladte statusser for et køb
    private List<string> allowedStatus = new List<string> { "Pending", "Accepted", "Rejected" };

    //Ændrer status på et køb. Sælgeren bruger det til at acceptere
    //eller afvise en købsforespørgsel.
    //Rute: /purchase/{purchaseId}/status?status=Accepted
    [HttpPut("{purchaseId}/status")]
    public ActionResult<bool> UpdatePurchaseStatus(int purchaseId,string status)
    {
        if (!allowedStatus.Contains(status))
        {
            return BadRequest("Status skal være Pending, Accepted eller Rejected.");
        }

        bool succesfuldt = repository.UpdatePurchaseStatus(purchaseId, status);
        if (!succesfuldt)
        {
           return NotFound("Købet er ikke fundet"); 
        }

        return Ok();
        
    }
}
