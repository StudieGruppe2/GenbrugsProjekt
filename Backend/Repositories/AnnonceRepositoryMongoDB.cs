
using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class AnnonceRepositoryMongoDB : IAnnonceRepository
{
    // Forbindelsen til samlingen "Annoncer" i MongoDB
    private readonly IMongoCollection<Annonce> annonceCollection;


    // ---------- FORBINDELSE TIL MONGODB ----------
    // Constructoren køres, når repositoryet bliver oprettet
    public AnnonceRepositoryMongoDB()
    {
        // Fortæller MongoDB, at Annonce_Id skal bruges som dokumentets _id
        if (!BsonClassMap.IsClassMapRegistered(typeof(Annonce)))
        {
            BsonClassMap<Annonce> map = new BsonClassMap<Annonce>();
            map.AutoMap();
            map.MapIdMember(typeof(Annonce).GetProperty("Annonce_Id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        // Opretter forbindelse til MongoDB
        string connectionString = ""; // Indsæt jeres connection string her
        MongoClient client = new MongoClient(connectionString);

        // Vælger databasen
        IMongoDatabase database = client.GetDatabase("GenbrugsplatformDB");

        // Vælger collectionen, hvor annoncerne bliver gemt
        annonceCollection = database.GetCollection<Annonce>("Annoncer");
    }


    // ---------- HENT ANNONCER ----------
    // Henter alle annoncer fra MongoDB
    public List<Annonce> GetAll()
    {
        // Et tomt filter betyder, at alle annoncer skal hentes
        FilterDefinition<Annonce> filter = Builders<Annonce>.Filter.Empty;

        // Finder annoncerne og returnerer dem som en liste
        return annonceCollection.Find(filter).ToList();
    }

    // Finder en bestemt annonce ud fra dens ID
    public Annonce? GetById(int Annonce_Id)
    {
        // Finder annoncen, hvor _id svarer til Annonce_Id
        FilterDefinition<Annonce> filter =
            Builders<Annonce>.Filter.Eq("_id", Annonce_Id);

        // Returnerer annoncen eller null, hvis den ikke findes
        return annonceCollection.Find(filter).FirstOrDefault();
    }

    // Henter alle annoncer, som en bestemt bruger ejer
    public List<Annonce> GetByOwnerId(int Owner_Id)
    {
        // Finder annoncer, hvor Owner_Id matcher brugerens ID
        FilterDefinition<Annonce> filter =
            Builders<Annonce>.Filter.Eq(a => a.Owner_Id, Owner_Id);

        // Returnerer alle brugerens annoncer som en liste
        return annonceCollection.Find(filter).ToList();
    }


    // ---------- OPRET ANNONCE ----------
    // Opretter en ny annonce og gemmer den i MongoDB
    public Annonce CreateNewAd(Annonce annonce)
    {
        // Finder annoncen med det højeste ID
        FilterDefinition<Annonce> filter = Builders<Annonce>.Filter.Empty;
        SortDefinition<Annonce> sort = Builders<Annonce>.Sort.Descending(a => a.Annonce_Id);

        Annonce? last = annonceCollection.Find(filter).Sort(sort).FirstOrDefault();

        // Hvis databasen er tom, starter ID på 1
        if (last == null)
        {
            annonce.Annonce_Id = 1;
        }
        else
        {
            // Ellers får annoncen et ID, der er 1 højere end det højeste
            annonce.Annonce_Id = last.Annonce_Id + 1;
        }

        // Gemmer den nye annonce i MongoDB
        annonceCollection.InsertOne(annonce);

        // Returnerer den oprettede annonce
        return annonce;
    }


    // ---------- SLET ANNONCE ----------
    // Sletter en annonce ud fra dens ID
    public bool DeleteAd(int Annonce_Id)
    {
        // Finder annoncen med det valgte ID
        FilterDefinition<Annonce> filter =
            Builders<Annonce>.Filter.Eq(a => a.Annonce_Id, Annonce_Id);

        // Forsøger at slette annoncen fra databasen
        DeleteResult result = annonceCollection.DeleteOne(filter);

        // Returnerer true, hvis en annonce blev slettet
        // Ellers returneres false
        return result.DeletedCount > 0;
    }


    // ---------- OPDATER ANNONCE ----------
    // Opdaterer en eksisterende annonce
    public bool UpdateAnnonce(Annonce annonce)
    {
        // Finder den eksisterende annonce ud fra dens ID
        FilterDefinition<Annonce> filter =
            Builders<Annonce>.Filter.Eq(a => a.Annonce_Id, annonce.Annonce_Id);

        // Erstatter den gamle annonce med den opdaterede annonce
        // Det svarer til, hvordan UpdateAnnonce virker i Memory-versionen
        ReplaceOneResult result = annonceCollection.ReplaceOne(filter, annonce);

        // Returnerer true, hvis annoncen blev fundet
        // Ellers returneres false
        return result.MatchedCount > 0;
    }


    // ---------- OPDATER STATUS ----------
    // Ændrer status på en annonce, fx fra Active til Reserved
    public bool UpdateAnnonceStatus(int Annonce_Id, string Status)
    {
        // Finder annoncen ud fra dens ID
        FilterDefinition<Annonce> filter =
            Builders<Annonce>.Filter.Eq(a => a.Annonce_Id, Annonce_Id);

        // Angiver, at det kun er annoncens Status, der skal ændres
        UpdateDefinition<Annonce> update =
            Builders<Annonce>.Update.Set(a => a.Status, Status);

        // Opdaterer annoncen i MongoDB
        UpdateResult result = annonceCollection.UpdateOne(filter, update);

        // Returnerer true, hvis annoncen blev fundet og opdateringen kunne udføres
        return result.MatchedCount > 0;
    }
}
