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
            return null;
        }

        return annonce;
    }

    [HttpGet("owner/{Owner_Id}")]
    public List<Annonce> GetByOwnerId(string Owner_Id)
    {
        return repository.GetByOwnerId(Owner_Id);
    }

    // Opretter en ny annonce
    [HttpPost]
    public ActionResult<Annonce> CreateNewAd (Annonce annonce)
    {
        if (annonce.Title == "" || annonce.Owner_Id == "")
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

    [HttpPut("{Annonce_Id}")]
    public ActionResult UpdateAnnonceStatus(Annonce Annonce)
    {
        
    }


// status på annonce: Active, Reserved, Sold, Inactive





   
}