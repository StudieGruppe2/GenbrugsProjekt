using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class AnnonceRepositoryMongoDB : IAnnonceRepository
{
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
        IMongoDatabase database = client.GetDatabase("GenbrugsMarkedDB");

        annonceCollection = database.GetCollection<Annonce>("Annoncer");
    }

    public List<Annonce> GetAll()
    {
        
    }

    public Annonce? GetById(string annonce_id)
    {
        
    }

    public List<Annonce> GetByOwnerId(string owner_id)
    {
        
    }

    public Annonce CreateNewId(Annonce annonce)
    {
        
    }
        
    public bool DeleteAd(string annonce_id)
    {
        
    }

    public bool UpdateAnnonce (Annonce annonce)
    {
        
    }
    
}