using Core;

namespace Backend.Repositories;

public class MemberRepositoryMemory : IMemberRepository
{
    private List<Member> members = new List<Member>();

    private int nextMemberId = 1;

    public MemberRepositoryMemory()
    {
        members.Add(new Member
        {
            Member_id = "m1",
            Name = "Anna Jensen",
            Mail = "anna@eaaa.dk",
            Phonenumber = "11223344",
            Password = "1234"
        });

        members.Add(new Member
        {
            Member_id = "m2",
            Name = "Bo Hansen",
            Mail = "bo@eaaa.dk",
            Phonenumber = "55667788",
            Password = "1234"
        });
    }

    public Member? GetById(string Member_id)
    {
        
    }

    public Member CreateProfile(Member member)
    {
        
    }
    
    public bool LoginToProfile(string Email, string Password)

}