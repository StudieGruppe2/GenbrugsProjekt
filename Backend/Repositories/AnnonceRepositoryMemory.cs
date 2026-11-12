using Core;

namespace Backend.Repositories;

public class AnnonceRepositoryMemory : IAnnonceRepository
{
    private List<Annonce> annoncer = new List<Annonce>();

    private int nextAnnonceId = 1;

      // Constructor - opretter testdata
    public AnnonceRepositoryMemory()
    {
        annoncer.Add(new Annonce
        {
            Annonce_Id = 1,
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
            Annonce_Id = 2,
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
            Annonce_Id = 3,
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

    public Annonce? GetById(int Annonce_Id)
    {
        foreach (Annonce annonce in annoncer)
        {
            if (annonce.Annonce_Id == Annonce_Id)
            {
                return annonce;
            }
        }
        return null;
    } 

    public List<Annonce> GetByOwnerId(string Owner_Id)
    {
        foreach (Annonce annonce in annoncer)
        {
            if (annonce.Owner_Id == Owner_Id)
            {
                return annoncer;
            }
        }

        return null;
    }

    public Annonce CreateNewAd(Annonce annonce)
    {
        annonce.Annonce_Id = nextAnnonceId;
        nextAnnonceId++;

        annoncer.Add(annonce);
        return annonce;
    }

    public bool DeleteAd(int Annonce_Id)
    {
        Annonce?  annonce = GetById(Annonce_Id);
        if (annonce == null)
        {
            return false;
        }
        else
        {
            annoncer.Remove(annonce);
        }
        return true;
    }

    public bool UpdateAnnonce(Annonce annonce)
    {
        // Finder den eksisterende annonce ud fra ID
        Annonce? OldAnnonce = GetById(annonce.Annonce_Id);

        // Hvis annoncen ikke findes, returneres false
        if (OldAnnonce == null)
        {
            return false;
        }

        // Finder placeringen af den gamle annonce i listen
        int index = annoncer.IndexOf(OldAnnonce);

        // Erstatter den gamle annonce med den opdaterede
        annoncer[index] = annonce;

        return true;
    }

    // Ændrer status på en annonce, fx fra "Active" til "Reserved"
    public bool UpdateAnnonceStatus(int Annonce_Id, string Status)
    {
        Annonce? annonce = annoncer.FirstOrDefault(a => a.Annonce_Id == Annonce_Id);
        if (annonce == null)
        {
            return false;
        }

        annonce.Status = Status;
        return true;
    }

}