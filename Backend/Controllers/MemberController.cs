using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")] // url adressen bliver xxx/Member

public class MemberController : ControllerBase
{
    // Controlleren bruger interfacet i stedet for et bestemt repository
    //Det kaldes Dependency Inversion og gør det muligt at skifte mellem Memory og MongoDB
    private readonly IMemberRepository repository;

    // Dependency Injection: Repository modtages gennem constructoren - vi "giver det" i program.cs
    public MemberController(IMemberRepository repository)
    {       
        // Gemmer repository, så det kan bruges i controllerens metoder
        this.repository = repository;
    }

    // ---------- HENT MEDLEM ----------
    // GET /Member/{Member_id} - Henter et medlem ud fra dets ID
    [HttpGet("{Member_id}")]
    public Member GetById(int Member_id)
    {
        Member? member = repository.GetById(Member_id); // Leder efter medlemmet i repository ved hjælp af dets ID

        // Hvis medlemmet ikke findes, returneres null
        if (member == null)
        {
            return null;
        }
        
        return member; // Returnerer medlemmet, hvis det findes
    }


    // ---------- OPRET PROFIL ----------
    // POST /Member - Opretter en ny brugerprofil
    [HttpPost]
    public Member CreateProfile(Member member)
    {
        repository.CreateProfile(member);// Sender Member-objektet til repository, som gemmer profilen
        
        return member;// Returnerer det oprettede Member-objekt til frontend
    }


    // ---------- LOGIN ----------
    // POST /Member/login - Kontrollerer brugerens loginoplysninger
    [HttpPost("login")]
    public IActionResult LoginToProfile(
        [FromForm] string Email,
        [FromForm] string Password)
    {
        // FromForm betyder, at Email og Password modtages som formulardata fra frontend
        // Sender e-mail og adgangskode til repository
        // Repository undersøger, om der findes et medlem med de oplysninger
        Member? member = repository.LoginToProfile(Email, Password);
        
        if (member == null)  // Hvis der ikke findes et medlem med oplysningerne, afvises login
        {
            return Unauthorized();// Returnerer HTTP 401 Unauthorized til frontend
        }

        // Hvis login lykkes, returneres HTTP 200 OK med medlemmets ID
        // Frontend bruger ID'et til at gemme loginstatus i LoginState
        return Ok(member.Member_id);
    }
}