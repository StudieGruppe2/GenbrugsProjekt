using Core;

namespace Backend.Repositories;

public interface IMemberRepository
{
    Member? GetById(string memberId);

    Member CreateProfile(Member member);

    LoginResult LoginToProfile(string email, string password);
}
