using Core;

namespace Backend.Repositories;

public interface IMemberRepository
{
    Member? GetById(int Member_id);
    
    void CreateProfile(Member member);
    
    bool LoginToProfile(string Email, string Password);
}