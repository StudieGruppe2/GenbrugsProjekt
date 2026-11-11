using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")] // url adressen bliver xxx/Member

public class MemberController : ControllerBase
{
    // Dependency Inversion: Controlleren afhænger af interfacet
    // i stedet for Memory eller MongoDB, så vi kan skifte repository
    // uden at ændre controllerens kode.
    private readonly IMemberRepository repository;

    public MemberController(IMemberRepository repository)
    {
        // Dependency Injection: Det valgte repository modtages udefra - gennem constructoren i stedet for at blive oprettet her.
        this.repository = repository;
    }

    //find medlem med id
    [HttpGet("{Member_id}")]
    public Member GetById(string Member_id)
    {
        return 
    }

    //opret profil
    [HttpPost("{Member}")]
    public Member CreateProfile(Member member)
    {
        return 
    }

    //Login til profil
    [HttpPost("{Email},{Password} ")]
    //IActionResult bruges, fordi login kan returner forskellige HTTP-svar:
    //Fx 200 OK ved succesfuldt login
    //401 Unauthorized ved forkert login
    public IActionResult LoginToProfile(string Email, string Password)
    {
        return
    }
    

}