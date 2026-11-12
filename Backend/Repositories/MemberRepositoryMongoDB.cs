
using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class MemberRepositoryMongoDB : IMemberRepository
{
    // ---------- FORBINDELSE TIL MONGODB ----------
    // Collectionen i MongoDB, hvor medlemmerne bliver gemt
    private readonly IMongoCollection<Member> memberCollection;

    // Constructoren køres, når repositoryet bliver oprettet
    public MemberRepositoryMongoDB()
    {
        // Fortæller MongoDB, at Member_id skal bruges som dokumentets _id
        if (!BsonClassMap.IsClassMapRegistered(typeof(Member)))
        {
            BsonClassMap<Member> map = new BsonClassMap<Member>();
            map.AutoMap();
            map.MapIdMember(typeof(Member).GetProperty("Member_id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        // Opretter forbindelse til MongoDB
        string connectionString = ""; // Indsæt jeres connection string her
        var client = new MongoClient(connectionString);

        // Vælger databasen
        var database = client.GetDatabase("GenbrugsplatformDB");

        // Vælger collectionen, hvor medlemmerne gemmes
        memberCollection = database.GetCollection<Member>("Members");
    }


    // ---------- HENT MEDLEM ----------
    // Finder et bestemt medlem ud fra dets ID
    public Member? GetById(int Member_id)
    {
        // Finder medlemmet, hvor _id svarer til Member_id
        FilterDefinition<Member> filter =
            Builders<Member>.Filter.Eq(m => m.Member_id, Member_id);

        // Returnerer medlemmet eller null, hvis det ikke findes
        return memberCollection.Find(filter).FirstOrDefault();
    }


    // ---------- OPRET PROFIL ----------
    // Opretter en ny brugerprofil og gemmer den i MongoDB
    public Member CreateProfile(Member member)
    {
        // Finder medlemmet med det højeste ID
        FilterDefinition<Member> filter = Builders<Member>.Filter.Empty;
        SortDefinition<Member> sort = Builders<Member>.Sort.Descending(m => m.Member_id);

        Member? last = memberCollection.Find(filter).Sort(sort).FirstOrDefault();

        // Hvis databasen er tom, starter ID på 1
        if (last == null)
        {
            member.Member_id = 1;
        }
        else
        {
            // Ellers får medlemmet et ID, der er 1 højere end det højeste
            member.Member_id = last.Member_id + 1;
        }

        // Gemmer det nye medlem i MongoDB
        memberCollection.InsertOne(member);

        // Returnerer det oprettede medlem
        return member;
    }


    // ---------- LOGIN ----------
    // Undersøger om der findes et medlem med den indtastede e-mail og adgangskode
    public Member? LoginToProfile(string Email, string Password)
    {
        // Finder et medlem, hvor både e-mail og adgangskode passer
        FilterDefinition<Member> filter = Builders<Member>.Filter.Where(
            m => m.Email == Email && m.Password == Password);

        // Returnerer medlemmet, hvis det findes, ellers null
        return memberCollection.Find(filter).FirstOrDefault();
    }
}
