using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

// Repository der gemmer køb i en MongoDB-database.
public class PurchaseRepositoryMongoDB : IPurchaseRepository
{
    // Samlingen ("tabellen") i MongoDB, som alle køb ligger i.
    private readonly IMongoCollection<Purchase> purchaseCollection;
    
        // Databasen sendes ind udefra (dependency injection), så klassen
        // ikke selv skal kende forbindelsesstrengen.
    
        if (!BsonClassMap.IsClassMapRegistered(typeof(Purchase)))
    {
        BsonClassMap<Member> map = new BsonClassMap<Purchase>();
        map.AutoMap();
        map.MapIdMember(typeof(Purchase).GetProperty("PurchaseID")!);
        BsonClassMap.RegisterClassMap(map);
    }

    string connectionString = "";
    var client = new MongoClient(connectionString);
    var database = client.GetDatabase("GenbrugsplatformDB"); // databasens navn
        
    PurchaseCollection = database.GetCollection<Purchase>("Purchases");
    // Databasen og collectionen oprettes automatisk ved første gemning.

    public List<Purchase> GetBySellerId(string sellerId)
    {
        return 
    }

    // Vi nulstiller id'et, så MongoDB selv laver et nyt ObjectId.
    // Det forhindrer også, at en bruger kan sende sit eget id 
    public Purchase Add(Purchase purchase)
    {
        return 
    }

    // Opdaterer kun feltet Status. Resten af købet røres ikke.
    public bool UpdatePurchaseStatus(string purchaseId, string status)
    {
        return 
    }
    
    
}