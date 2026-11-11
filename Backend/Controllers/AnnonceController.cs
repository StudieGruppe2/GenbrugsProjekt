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
    [HttpGet("{annonce_id}")]
    public ActionResult<Annonce> GetById(string annonce_id)
    {
        repository.GetById(annonce_id);
        return
    }

    [HttpGet("owner/{ownerId}")]
    public List<Annonce> GetByOwnerId(string owner_id)
    {
        return
    }

    // Opretter en ny annonce
    [HttpPost]
    public ActionResult<Annonce> CreateNewAd (Annonce annonce)
    {
        return
    }

    [HttpDelete("{Annonce_Id}")]
    public bool DeleteAd(string Annonce_Id)
    {
        return 
    }

    [HttpPut("{Annonce}")]
    public bool UpdateAnnonce(Annonce Annonce)
    {
        return 
    }








   
}