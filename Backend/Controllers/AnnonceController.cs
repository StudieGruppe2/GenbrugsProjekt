using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]

public class AnnonceController : ControllerBase
{
    // Dependency Inversion: Controlleren afhænger af interfacet
    // i stedet for Memory eller MongoDB, så vi kan skifte repository
    // uden at ændre controllerens kode.

    private readonly IAnnonceRepository repository;

    public AnnonceController(IAnnonceRepository repository)
    {
         // Dependency Injection: Det valgte repository modtages udefra - gennem constructoren i stedet for at blive oprettet her.
         this.repository = repository;
    }

    [HttpGet]
    public List<Annonce> GetAll()
    {
        return repository.GetAll();
    }

    // Henter en annonce ud fra id
    [HttpGet("{annonce_Id}")]
    public ActionResult<Annonce> GetById(int annonce_Id)
    {
        Annonce? annonce = repository.GetById(annonce_Id);
        if (annonce == null)
        {
            return NotFound("Annoncen findes ikke.");
        }

        return annonce;
    }

    [HttpGet("owner/{Owner_Id}")]
    public List<Annonce> GetByOwnerId(int Owner_Id)
    {
        return repository.GetByOwnerId(Owner_Id);
    }

    // Opretter en ny annonce
    [HttpPost]
    public ActionResult<Annonce> CreateNewAd (Annonce annonce)
    {
        if (annonce.Title == "" || annonce.Owner_Id == 0)
        {
            return BadRequest("Title and Owner_Id are required");
        }

        return repository.CreateNewAd(annonce);
    }
    
    

    [HttpDelete("{Annonce_Id}")]
    public ActionResult DeleteAd(int Annonce_Id)
    {
        bool result = repository.DeleteAd(Annonce_Id);
        if (!result)
        {
           return BadRequest("Ad Not Found"); 
        }
        return Ok("Ad Deleted");
    }

    
    
    [HttpPut("{Annonce}")]
    public ActionResult UpdateAnnonce(Annonce Annonce)
    {
        bool result = repository.UpdateAnnonce(Annonce);
        if (!result)
        {
            return BadRequest("Ad not found");
        }
        return Ok("Ad Updated");
    }

    
    
    // De eneste tilladte statusser
    private List<string> allowedStatus = new List<string> { "Active", "Reserved", "Sold", "Inactive" };

    // Rute: /annonce/{Annonce_Id}/status?Status=Sold
    // Put fordi vi opdaterer en annonce, der allerede findes
    [HttpPut("{Annonce_Id}/status")]
    public IActionResult UpdateStatus(int Annonce_Id, string Status)
    {
        // Stopper, hvis status ikke er en af de fire tilladte
        if (!allowedStatus.Contains(Status))
        {
            return BadRequest("Status skal være Active, Reserved, Sold eller Inactive.");
        }

        bool success = repository.UpdateAnnonceStatus(Annonce_Id, Status);
        if (!success)
        {
            return NotFound("Annoncen findes ikke.");
        }

        return Ok();
    }
}
