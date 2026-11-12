using Core;

namespace Backend.Repositories;

// Interfacet bestemmer, hvilke metoder alle MemberRepositories skal have - det er "kontrakten"
// Både MemberRepositoryMemory og MemberRepositoryMongoDB bruger dette interface
public interface IMemberRepository
{
    // Finder et medlem ud fra dets ID
    // Returnerer null, hvis medlemmet ikke findes
    Member? GetById(int Member_id);

    // Opretter en ny brugerprofil og returnerer det oprettede medlem
    Member CreateProfile(Member member);

    // Finder et medlem ud fra e-mail og adgangskode
    // Returnerer medlemmet ved korrekt login og null ved forkert login
    Member? LoginToProfile(string Email, string Password);
}