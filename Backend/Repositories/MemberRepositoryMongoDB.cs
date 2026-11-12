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
            map.MapIdMember(typeof(Member).GetProperty("Member_id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        string connectionString = "";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("GenbrugsplatformDB"); // databasens navn
        
        memberCollection = database.GetCollection<Member>("Members");
        // Databasen og collectionen oprettes automatisk ved første gemning.
    }

    public Member? GetById(int Member_id)
    {
        FilterDefinition<Member> filter = Builders<Member>.Filter.Eq("_id", Member_id);
        return memberCollection.Find(filter).FirstOrDefault();
    }

    public Member CreateProfile(Member member)
    {
        // Opretter et tomt filter, så alle medlemmer medtages
        FilterDefinition<Member> filter = Builders<Member>.Filter.Empty;

        // Sorterer medlemmerne efter ID, fra højeste til laveste
        SortDefinition<Member> sort = Builders<Member>.Sort.Descending("_id");

        // Finder medlemmet med det højeste ID. Hvis databasen er tom, bliver last = null.
        Member? last = memberCollection.Find(filter).Sort(sort).FirstOrDefault();

        // Hvis der ikke findes medlemmer, starter ID på 1
        if (last == null)
        {
            member.Member_id = 1;
        }
        else
        {
            // Ellers får det nye medlem et ID, der er 1 højere end det højeste
            member.Member_id = last.Member_id + 1;
        }
        
        memberCollection.InsertOne(member);
        
        return member;
    }

    public Member? LoginToProfile(string Email, string Password)
    {
        var filter = Builders<Member>.Filter.Eq(m => m.Email, Email);

        Member? member = memberCollection.Find(filter).FirstOrDefault();

        if (member == null || member.Password != Password)
        {
            return null;
        }

        return member;
    }
}