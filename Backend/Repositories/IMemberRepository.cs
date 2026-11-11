using Core;

namespace Backend.Repositories;

public interface IMemberRepository
{
    List<Member> GetById(string Member_id);
    
    Member CreateProfile(Member member);
    
    bool LoginToProfile(string Email, string Password); // I tvivl om det skal være bool???
}