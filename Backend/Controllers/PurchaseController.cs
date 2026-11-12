
using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

// Gør klassen til en API-controller
[ApiController]

// Bestemmer URL-adressen til /Purchase
[Route("[controller]")]

public class PurchaseController : ControllerBase
{
    // Controlleren bruger interfacet i stedet for et bestemt repository
    //Det kaldes Dependency Inversion og gør det muligt at skifte mellem Memory og MongoDB
    private readonly IPurchaseRepository repository;

    // Bruges til at hente og opdatere annoncer i forbindelse med køb
    private readonly IAnnonceRepository annonceRepository;

    // Dependency Injection: Repository modtages gennem constructoren - vi "giver det" i program.cs
    public PurchaseController(IPurchaseRepository repository, IAnnonceRepository annonceRepository)
    {
        // Gemmer de to repositories, så controlleren kan bruge deres metoder
        this.repository = repository;
        this.annonceRepository = annonceRepository;
    }


    // ---------- HENT KØBSANMODNINGER ----------
    // GET /Purchase/buyer/{Buyer_Id} - Henter alle købsanmodninger, som en bestemt bruger selv har sendt
    [HttpGet("buyer/{Buyer_Id}")]
    public ActionResult<List<Purchase>> GetByBuyerId(int Buyer_Id)
    {
        return repository.GetByBuyerId(Buyer_Id);// Repository finder købsanmodningerne ud fra køberens ID
    }

    // GET /Purchase/seller/{Seller_Id} - Henter alle købsanmodninger, som en bestemt sælger har modtaget
    [HttpGet("seller/{Seller_Id}")]
    public ActionResult<List<Purchase>> GetBySellerId(int Seller_Id)
    {
        return  repository.GetBySellerId(Seller_Id); // Repository finder købsanmodningerne ud fra sælgerens ID
    }


    // ---------- OPRET KØBSANMODNING ----------
    // POST /Purchase - Opretter en ny købsanmodning på en annonce
    // Frontend sender Annonce_Id og Buyer_Id, mens backend finder Seller_Id
    [HttpPost]
    public ActionResult<Purchase> Add(Purchase purchase)
    {
        Annonce? annonce = annonceRepository.GetById(purchase.Annonce_Id);  // Finder annoncen ud fra det ID, der er sendt fra frontend

        // Hvis annoncen ikke findes, returneres HTTP 404 Not Found
        if (annonce == null)
        {
            return NotFound("Annoncen findes ikke.");
        }

        // Kontrollerer om annoncen stadig er aktiv og kan købes
            // En annonce med status Reserved, Sold eller Inactive kan ikke købes
        if (annonce.Status != "Active")
        {
            return BadRequest("Annoncen er ikke længere til salg.");
        }

        // Finder sælgerens ID gennem annoncen - Sælgeren er altid den bruger, der ejer annoncen
        purchase.Seller_Id = annonce.Owner_Id;

        // Kontrollerer om køberen og sælgeren er den samme bruger
        if (purchase.Buyer_Id == purchase.Seller_Id)
        {
            return BadRequest("you cannot buy your own item!");
        }

        // Ændrer annoncens status til Reserved, så andre brugere ikke kan sende en købsanmodning på samme annonce
        annonceRepository.UpdateAnnonceStatus(annonce.Annonce_Id, "Reserved");
        
        return  repository.Add(purchase);  // Sender købsanmodningen videre til repository, som gemmer den
    }


    // ---------- OPDATER STATUS PÅ KØBSANMODNING ----------
    // De tre statusser, som en købsanmodning må have: Pending = afventer svar, Accepted = accepteret, Rejected = afvist
    private List<string> allowedStatus = new List<string> { "Pending", "Accepted", "Rejected" };

    // PUT /Purchase/{purchaseId}/status?status=Accepted - Bruges når sælgeren accepterer eller afslår en købsanmodning
    [HttpPut("{purchaseId}/status")]
    public ActionResult<bool> UpdatePurchaseStatus(int purchaseId,string status)
    {
        if (!allowedStatus.Contains(status))// Kontrollerer om status er en af de tre tilladte værdier
        {
            // Returnerer HTTP 400 Bad Request, hvis status ikke er tilladt
            return BadRequest("Status skal være Pending, Accepted eller Rejected.");
        }

        // Beder repository om at opdatere købsanmodningens status
        // Repository returnerer true, hvis opdateringen lykkes
        bool succesfuldt = repository.UpdatePurchaseStatus(purchaseId, status);
        
        if (!succesfuldt)// Hvis købsanmodningen ikke blev fundet eller opdateret
        {
            return NotFound("Købet er ikke fundet"); // Returnerer HTTP 404 Not Found
        }
        
        return Ok(); // Returnerer HTTP 200 OK, hvis status blev opdateret
    }
}
