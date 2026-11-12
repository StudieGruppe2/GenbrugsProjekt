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
    public Member GetById(int Member_id)
    {
        Member? member = repository.GetById(Member_id);
        if (member == null)
        {
            return null;
        }

        return member;
    }

    //opret profil
    [HttpPost]
    public Member CreateProfile(Member member)
    {
        repository.CreateProfile(member);
        return member;
    }

    //Login til profil
    [HttpPost("login")]
    //IActionResult bruges, fordi login kan returner forskellige HTTP-svar:
    //Fx 200 OK ved succesfuldt login
    //401 Unauthorized ved forkert login
    public IActionResult LoginToProfile(
        [FromForm] string Email, // FromForm betyder, at Email og Password modtages fra de formulardata, som frontend sender.
        [FromForm] string Password)
    {
        // Kalder repository, som kontrollerer om loginoplysningerne er korrekte, og returnerer true eller false.
        bool loginSuccess = repository.LoginToProfile(Email, Password);

        if (loginSuccess)
        {
            return Ok();
        }

        return Unauthorized();
    }
    

}