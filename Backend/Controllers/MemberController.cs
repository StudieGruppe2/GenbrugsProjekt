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

    
    [HttpPost("login")]
    public IActionResult LoginToProfile(
        [FromForm] string Email,
        [FromForm] string Password)
    {
        Member? member = repository.LoginToProfile(Email, Password);

        if (member == null)
        {
            return Unauthorized();
        }

        // Sender det rigtige Member_Id tilbage
        return Ok(member.Member_id);
    }

    

}