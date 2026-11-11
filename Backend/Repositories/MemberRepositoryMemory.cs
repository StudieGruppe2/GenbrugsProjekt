using Core;

namespace Backend.Repositories;

public class MemberRepositoryMemory : IMemberRepository
{
    //Datalager
    private List<Member> members = new List<Member>();

    private int nextMemberId = 1;

    // Constructor - opretter testdata, når Memory-repositoryet oprettes
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

    public Member? GetById(int Member_id)
    {
        foreach (Member member in members)
        {
            if (member.Member_id == Member_id)
            {
                return member;
            }
        }
        return null;
    }

    public void CreateProfile(Member member)
    {
        member.Member_id = nextMemberId;
        nextMemberId++;

        members.Add(member);
    }

    public bool LoginToProfile(string Email, string Password)
    {
        foreach (Member member in members)
        {
            if (Email == member.Email && Password == member.Password)
            {
                return true;
            }
        }
        return false;
    }

}