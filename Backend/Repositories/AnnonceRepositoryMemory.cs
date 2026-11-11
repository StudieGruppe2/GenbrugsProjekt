using Core;

namespace Backend.Repositories;

public class AnnonceRepositoryMemory : IAnnonceRepository
{
    private List<Annonce> annoncer = new List<Annonce>();


      // Constructor - opretter testdata
    public AnnonceRepositoryMemory()
    {
        annoncer.Add(new Annonce
        {
            Annonce_Id = "a1",
            Title = "Vinterjakke str. M",
            Category = "Jakker",
            Description = "Varm dunjakke, brugt én vinter.",
            Price = 250,
            Owner_Id = "m1",
            Published_Date = DateTime.Now.AddDays(-3),
            Location = "Genbrugsrum A1.04"
        });

        annoncer.Add(new Annonce
        {
            Annonce_Id = "a2",
            Title = "Sorte jeans str. 32/32",
            Category = "Bukser",
            Description = "Næsten som nye.",
            Price = 150,
            Owner_Id = "m1",
            Published_Date = DateTime.Now.AddDays(-1),
            Location = "Ikke på Eaaa"
        });

        annoncer.Add(new Annonce
        {
            Annonce_Id = "a3",
            Title = "Strikket sweater",
            Category = "Trøjer",
            Description = "Hjemmestrikket uld, str. L.",
            Price = 100,
            Owner_Id = "m2",
            Published_Date = DateTime.Now,
            Location = "Genbrugsrum B2.12"
        });
        
    }

    public List<Annonce> GetAll()
    {
        return annoncer;
    }

    public Annonce? GetById(string annonce_id)
    {
        
    } 

    public List<Annonce> GetByOwnerId(string owner_id)
    {
        
    }

    public Annonce CreateNedAd(Annonce annonce)
    {
        
    }

    public bool DeleteAd(string annonce_id)
    {
        
    }

    public bool UpdateAnnonce( Annonce annonce)
    {
        
    }



}