
using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

// Repository der gemmer købsanmodninger i MongoDB i stedet for i hukommelsen
public class PurchaseRepositoryMongoDB : IPurchaseRepository
{
    // ---------- FORBINDELSE TIL MONGODB ----------
    // Collectionen i MongoDB, hvor købsanmodningerne gemmes
    private readonly IMongoCollection<Purchase> purchaseCollection;

    // Constructoren køres, når repositoryet bliver oprettet
    public PurchaseRepositoryMongoDB()
    {
        // Fortæller MongoDB, at Purchase_Id skal bruges som dokumentets _id
        if (!BsonClassMap.IsClassMapRegistered(typeof(Purchase)))
        {
            BsonClassMap<Purchase> map = new BsonClassMap<Purchase>();
            map.AutoMap();
            map.MapIdMember(typeof(Purchase).GetProperty("Purchase_Id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        // Opretter forbindelse til MongoDB
        string connectionString = ""; // Indsæt jeres connection string her
        MongoClient client = new MongoClient(connectionString);

        // Vælger databasen
        IMongoDatabase database = client.GetDatabase("GenbrugsplatformDB");

        // Vælger collectionen, hvor købsanmodningerne gemmes
        purchaseCollection = database.GetCollection<Purchase>("purchase");
    }


    // ---------- HENT KØBSANMODNINGER ----------
    // Henter alle købsanmodninger, som en bestemt bruger har sendt
    public List<Purchase> GetByBuyerId(int Buyer_Id)
    {
        // Finder alle købsanmodninger, hvor Buyer_Id matcher brugerens ID
        FilterDefinition<Purchase> filter = Builders<Purchase>.Filter.Eq(p => p.Buyer_Id, Buyer_Id);

        // Returnerer købsanmodningerne som en liste
        return purchaseCollection.Find(filter).ToList();
    }

    // Henter alle købsanmodninger, som en bestemt sælger har modtaget
    public List<Purchase> GetBySellerId(int Seller_Id)
    {
        // Finder alle købsanmodninger, hvor Seller_Id matcher sælgerens ID
        FilterDefinition<Purchase> filter = Builders<Purchase>.Filter.Eq(p => p.Seller_Id, Seller_Id);

        // Returnerer købsanmodningerne som en liste
        return purchaseCollection.Find(filter).ToList();
    }


    // ---------- OPRET KØBSANMODNING ----------
    // Opretter en ny købsanmodning og gemmer den i MongoDB
    public Purchase Add(Purchase purchase)
    {
        // Et tomt filter betyder, at alle købsanmodninger medtages
        FilterDefinition<Purchase> filter = Builders<Purchase>.Filter.Empty;

        // Sorterer købsanmodningerne efter ID, så det højeste kommer først
        SortDefinition<Purchase> sort = Builders<Purchase>.Sort.Descending(p => p.Purchase_Id);

        // Finder købsanmodningen med det højeste ID
        // Hvis databasen er tom, returneres null
        Purchase? last = purchaseCollection.Find(filter).Sort(sort).FirstOrDefault();

        // Hvis databasen er tom, starter ID på 1
        if (last == null)
        {
            purchase.Purchase_Id = 1;
        }
        else
        {
            // Ellers får købsanmodningen et ID, der er 1 højere end det højeste
            purchase.Purchase_Id = last.Purchase_Id + 1;
        }

        // En ny købsanmodning starter altid med status Pending (afventer svar)
        purchase.Status = "Pending";

        // Gemmer købsanmodningen i MongoDB
        purchaseCollection.InsertOne(purchase);

        // Returnerer den oprettede købsanmodning
        return purchase;
    }


    // ---------- OPDATER STATUS ----------
    // Ændrer status på en købsanmodning, når sælgeren accepterer eller afslår
    public bool UpdatePurchaseStatus(int purchaseId, string status)
    {
        // Finder købsanmodningen ud fra dens ID
        FilterDefinition<Purchase> filter = Builders<Purchase>.Filter.Eq(p => p.Purchase_Id, purchaseId);

        // Angiver, at det kun er købsanmodningens Status, der skal ændres
        UpdateDefinition<Purchase> update = Builders<Purchase>.Update.Set(p => p.Status, status);

        // Opdaterer købsanmodningen i MongoDB
        UpdateResult result = purchaseCollection.UpdateOne(filter, update);

        // Returnerer true, hvis købsanmodningen blev fundet
        // Ellers returneres false
        return result.MatchedCount > 0;
    }
}
