
using Core;

namespace Backend.Repositories;

public class MemberRepositoryMemory : IMemberRepository
{
    // ---------- DATALAGER ----------
    // Gemmer medlemmerne i en liste i hukommelsen i stedet for i en database
    private List<Member> members = new List<Member>();


    // ---------- TESTDATA ----------
    // Constructoren køres, når repositoryet bliver oprettet
    // Her oprettes to testbrugere, så vi kan teste login og andre funktioner
    public MemberRepositoryMemory()
    {
        members.Add(new Member
        {
            Member_id = 1,
            Name = "Anna Jensen",
            Email = "anna@eaaa.dk",
            Phonenumber = "11223344",
            Password = "1234"
        });

        members.Add(new Member
        {
            Member_id = 2,
            Name = "Bo Hansen",
            Email = "bo@eaaa.dk",
            Phonenumber = "55667788",
            Password = "1234"
        });
    }


    // ---------- HENT MEDLEM ----------
    // Finder et bestemt medlem ud fra dets ID
    public Member? GetById(int Member_id)
    {
        // Gennemgår medlemmerne én ad gangen
        foreach (Member member in members)
        {
            // Hvis medlemmets ID matcher det ønskede ID, returneres medlemmet
            if (member.Member_id == Member_id)
            {
                return member;
            }
        }

        // Hvis medlemmet ikke findes, returneres null
        return null;
    }


    // ---------- OPRET PROFIL ----------
    // Opretter en ny brugerprofil og gemmer den i listen
    public Member CreateProfile(Member member)
    {
        // Hvis listen er tom, får medlemmet ID 1
        // Ellers findes det højeste eksisterende ID, og der lægges 1 til
        member.Member_id = members.Count == 0 ? 1 : members.Max(m => m.Member_id) + 1;

        // Tilføjer det nye medlem til listen
        members.Add(member);

        // Returnerer det oprettede medlem
        return member;
    }


    // ---------- LOGIN ----------
    // Undersøger om der findes et medlem med den indtastede e-mail og adgangskode
    public Member? LoginToProfile(string Email, string Password)
    {
        // FirstOrDefault gennemgår listen og finder det første medlem,
        // hvor både e-mail og adgangskode passer
        // Hvis ingen passer, returneres null
        Member? member = members.FirstOrDefault(
            m => m.Email == Email && m.Password == Password);

        // Returnerer medlemmet, hvis det findes, ellers null
        return member;
    }
}
