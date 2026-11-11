using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class MemberRepositoryMongoDB : IMemberRepository
{
    // Database forbindelse til collectionen med medlemmer
    private readonly IMongoCollection<Member> memberCollection;

    public MemberRepositoryMongoDB()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(Member)))
        {
            BsonClassMap<Member> map = new BsonClassMap<Member>();
            map.AutoMap();
            map.MapIdMember(typeof(Member).GetProperty("MemberId")!);
            BsonClassMap.RegisterClassMap(map);
        }

        string connectionString = "";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("GenbrugsplatformDB"); // databasens navn
        
        memberCollection = database.GetCollection<Member>("Members");
        // Databasen og collectionen oprettes automatisk ved første gemning.
    }

    public Member? GetById(string Member_id)
    {
        
    }

    public Member CreateProfile(Member member)
    {
        
    }

    public bool LoginToProfile(string Email, string Password)
    {
        
    }
}