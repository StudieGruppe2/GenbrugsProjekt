using Core;

namespace Backend.Repositories;

public interface IMemberRepository
{
    Member? GetById(int Member_id);
    
    Member CreateProfile(Member member);
    
    Member?  LoginToProfile(string Email, string Password);
}