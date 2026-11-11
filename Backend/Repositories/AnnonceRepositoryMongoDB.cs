using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class AnnonceRepositoryMongoDB : IAnnonceRepository
{
    public AnnonceRepositoryMongoDB()
    {
        
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