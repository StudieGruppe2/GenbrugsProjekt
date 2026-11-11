using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class AnnonceRepositoryMongoDB : IAnnonceRepository
{
    //Selve dataforbindelsen
    private readonly IMongoCollection<Annonce> annonceCollection;
    public AnnonceRepositoryMongoDB()
    {
          // Fortæller MongoDB, at Annonce_Id er nøglen (_id)
        if (!BsonClassMap.IsClassMapRegistered(typeof(Annonce)))
        {
            BsonClassMap<Annonce> map = new BsonClassMap<Annonce>();
            map.AutoMap();
            map.MapIdMember(typeof(Annonce).GetProperty("Annonce_Id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        string connectionString = ""; // <-- Her skrives jeres egen connection string
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("GenbrugsplatformDB");

        annonceCollection = database.GetCollection<Annonce>("Annoncer");
    }

    
    
    //Henter alle annoncer
    public List<Annonce> GetAll()
    {
        FilterDefinition<Annonce> filter = Builders<Annonce>.Filter.Empty;
        return annonceCollection.Find(filter).ToList();
    }

   
    
    // Henter 1 annonce udfra id
    public Annonce? GetById(int Annonce_Id)
    {
        FilterDefinition<Annonce> filter = Builders<Annonce>.Filter.Eq("_id", Annonce_Id);
        return annonceCollection.Find(filter).FirstOrDefault();
    }

   
    
    // henter alle annoncer tilhørende ejeren af annoncerne
    public List<Annonce> GetByOwnerId(string Owner_Id)
    {
        FilterDefinition<Annonce> filter = Builders<Annonce>.Filter.Eq("_id", Owner_Id);
        return annonceCollection.Find(filter).ToList();
    }

    
    
    
    //Laver ny annonce
    public Annonce CreateNewAd(Annonce annonce)
    {
        // Opretter et tomt filter, så alle annoncer medtages
        FilterDefinition<Annonce> filter = Builders<Annonce>.Filter.Empty;

        // Sorterer annoncerne efter ID, fra højeste til laveste
        SortDefinition<Annonce> sort = Builders<Annonce>.Sort.Descending("_id");

        // Finder annoncen med det højeste ID. Hvis databasen er tom, bliver last = null.
        Annonce? last = annonceCollection.Find(filter).Sort(sort).FirstOrDefault();

        // Hvis der ikke findes annoncer, starter ID på 1
        if (last == null)
        {
            annonce.Annonce_Id = 1;
        }
        else
        {
            // Ellers får den nye annonce et ID, der er 1 højere end det højeste
            annonce.Annonce_Id = last.Annonce_Id + 1;
        }
        
        //Sender og gemmer i db
        annonceCollection.InsertOne(annonce);
        return annonce;
    }
      
    
    
    
    // sletter en annonce
    public bool DeleteAd(int Annonce_Id)
    {
        FilterDefinition<Annonce> filter = Builders<Annonce>.Filter.Eq("_id", Annonce_Id);
        annonceCollection.DeleteOne(filter);

        if (Annonce_Id != null)
        {
            return true; // sletningen lykkedes
        }
        else
        {
            return false; // Sletningen lykkedes ikke
        }
        
    }




    //opdatere en annonce
    public bool UpdateAnnonce(Annonce annonce)
    {
        // Finder annoncen ud fra dens ID
        var filter = Builders<Annonce>.Filter
            .Eq(a => a.Annonce_Id, annonce.Annonce_Id);

        // Angiver hvilke felter der må opdateres
        var update = Builders<Annonce>.Update
            .Set(a => a.Title, annonce.Title)
            .Set(a => a.Description, annonce.Description)
            .Set(a => a.Price, annonce.Price);

        // Opdaterer annoncen i MongoDB
        var result = annonceCollection.UpdateOne(filter, update);

        // Returnerer true, hvis annoncen blev fundet
        return result.MatchedCount > 0;
    }
}