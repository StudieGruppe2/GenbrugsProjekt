
using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]

public class AnnonceController : ControllerBase
{
    // Controlleren bruger interfacet i stedet for et bestemt repository
    //Det kaldes Dependency Inversion og gør det muligt at skifte mellem Memory og MongoDB
    private readonly IAnnonceRepository repository;

    // Dependency Injection: Repository modtages gennem constructoren - vi "giver det" i program.cs
    public AnnonceController(IAnnonceRepository repository)
    {
        // Gemmer repository, så det kan bruges i controllerens metoder
            this.repository = repository;
    }


    // ---------- HENT ANNONCER ----------
    // GET /Annonce - Henter alle annoncer
    [HttpGet]
    public List<Annonce> GetAll()
    {
        return repository.GetAll();
    }

    // GET /Annonce/{id} - Henter en annonce ud fra dens ID
    [HttpGet("{annonce_Id}")]
    public ActionResult<Annonce> GetById(int annonce_Id)
    {
        // Leder efter annoncen i repository
        Annonce? annonce = repository.GetById(annonce_Id);

        // Hvis annoncen ikke findes, returneres 404 Not Found
        if (annonce == null)
        {
            return NotFound("Annoncen findes ikke.");
        }

        // Returnerer annoncen til frontend
        return annonce;
    }

    // GET /Annonce/owner/{id} - Henter annoncer fra en bestemt bruger
    [HttpGet("owner/{Owner_Id}")]
    public List<Annonce> GetByOwnerId(int Owner_Id)
    {
        return repository.GetByOwnerId(Owner_Id);
    }


    // ---------- OPRET ANNONCE ----------
    // POST /Annonce - Opretter en ny annonce
    [HttpPost]
    public ActionResult<Annonce> CreateNewAd(Annonce annonce)
    {
        // Kontrollerer at annoncen har en titel og en ejer
        if (annonce.Title == "" || annonce.Owner_Id == 0)
        {
            return BadRequest("Titel og ejer skal udfyldes.");
        }

        // Sender annoncen videre til repository, som gemmer den
        return repository.CreateNewAd(annonce);
    }


    // ---------- SLET ANNONCE ----------
    // DELETE /Annonce/{id} - Sletter en annonce
    [HttpDelete("{Annonce_Id}")]
    public ActionResult DeleteAd(int Annonce_Id)
    {
        // Repository forsøger at slette annoncen
        bool result = repository.DeleteAd(Annonce_Id);

        // Hvis annoncen ikke findes, returneres en fejl
        if (!result)
        {
            return NotFound("Annoncen findes ikke.");
        }

        return Ok("Annoncen er slettet.");
    }


    // ---------- REDIGER ANNONCE ----------
    // PUT - Opdaterer en eksisterende annonce
    [HttpPut("{Annonce}")]
    public ActionResult UpdateAnnonce(Annonce Annonce)
    {
        // Repository forsøger at opdatere annoncen
        bool result = repository.UpdateAnnonce(Annonce);

        if (!result)
        {
            return NotFound("Annoncen findes ikke.");
        }

        return Ok("Annoncen er opdateret.");
    }


    // ---------- OPDATER STATUS ----------
    // De fire statusser en annonce må have
    private List<string> allowedStatus = new()
    {
        "Active", "Reserved", "Sold", "Inactive"
    };

    // PUT /Annonce/{id}/status?Status=Sold
    // Opdaterer status på en eksisterende annonce
    [HttpPut("{Annonce_Id}/status")]
    public IActionResult UpdateStatus(int Annonce_Id, string Status)
    {
        // Kontrollerer at den nye status er tilladt
        if (!allowedStatus.Contains(Status))
        {
            return BadRequest("Ugyldig status.");
        }

        // Repository forsøger at ændre annoncens status
        bool success = repository.UpdateAnnonceStatus(Annonce_Id, Status);

        // Hvis annoncen ikke findes, returneres 404 Not Found
        if (!success)
        {
            return NotFound("Annoncen findes ikke.");
        }

        // Returnerer 200 OK, hvis status blev opdateret
        return Ok();
    }
}
