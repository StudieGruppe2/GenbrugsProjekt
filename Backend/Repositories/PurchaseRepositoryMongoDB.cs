using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

// Repository der gemmer køb i en MongoDB-database.
public class PurchaseRepositoryMongoDB : IPurchaseRepository
{
    //Selve dataforbindelsen
    private readonly IMongoCollection<Purchase> purchaseCollection;
    public PurchaseRepositoryMongoDB()
    {
        // Fortæller MongoDB, at Purchase_Id er nøglen (_id)
        if (!BsonClassMap.IsClassMapRegistered(typeof(Purchase)))
        {
            BsonClassMap<Purchase> map = new BsonClassMap<Purchase>();
            map.AutoMap();
            map.MapIdMember(typeof(Purchase).GetProperty("Purchase_Id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        string connectionString = ""; // <-- Her skrives jeres egen connection string
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("GenbrugsplatformDB");

        purchaseCollection = database.GetCollection<Purchase>("purchase");
    }

    public List<Purchase> GetByBuyerId(int Buyer_Id)
    {
        FilterDefinition<Purchase> filter = Builders<Purchase>.Filter.Eq("_id", Buyer_Id);
        return purchaseCollection.Find(filter).ToList();
    }
    
    public List<Purchase> GetBySellerId(int Seller_Id)
    {
        FilterDefinition<Purchase> filter = Builders<Purchase>.Filter.Eq("_id", Seller_Id);
        return purchaseCollection.Find(filter).ToList();
    }

    public Purchase Add(Purchase purchase)
    {
        //Opretter et tomt filter, så alle købsanmodninger medtages
        FilterDefinition<Purchase> filter = Builders<Purchase>.Filter.Empty;

        // Sorterer købsanmodninger efter ID, fra højeste til laveste
        SortDefinition<Purchase> sort = Builders<Purchase>.Sort.Descending(p => p.Purchase_Id);

        // Finder købsanmodningen med det højeste ID
        Purchase? last = purchaseCollection.Find(filter).Sort(sort).FirstOrDefault();

        //hvis databasen er tom, starter ID på 1
        if (last == null)
        {
            purchase.Purchase_Id = 1;
        }
        else
        {
            //Giver købsanmodningen et ID, der er 1 højere end det højeste
            purchase.Purchase_Id = last.Purchase_Id + 1;
        }

        //Gemmer købsanmodningen i MongoDB
        purchaseCollection.InsertOne(purchase);

        return purchase;
    }

   

    // Opdaterer kun feltet Status. Resten af købet røres ikke.
    public bool UpdatePurchaseStatus(int Purchase_Id, string status)
    {
        var update = Builders<Purchase>.Update.Set("Purchase_Status", status);

        return update.MatchedCount>0;
    }
    
    
}
