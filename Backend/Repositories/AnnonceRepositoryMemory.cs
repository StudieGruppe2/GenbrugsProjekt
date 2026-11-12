
using Core;

namespace Backend.Repositories;

public class AnnonceRepositoryMemory : IAnnonceRepository
{
    // Listen gemmer annoncerne i hukommelsen i stedet for en database
    private List<Annonce> annoncer = new List<Annonce>();


    // ---------- TESTDATA ----------

    // Constructoren køres, når repositoryet bliver oprettet
    // Her oprettes tre testannoncer, så vi har data at arbejde med
    public AnnonceRepositoryMemory()
    {
        annoncer.Add(new Annonce
        {
            Annonce_Id = 1,
            Title = "Vinterjakke str. M",
            Category = "Jakker",
            Description = "Varm dunjakke, brugt én vinter.",
            Price = 250,
            Owner_Id = 1,
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
            Owner_Id = 1,
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
            Owner_Id = 2,
            Published_Date = DateTime.Now,
            Location = "Genbrugsrum B2.12"
        });
    }


    // ---------- HENT ANNONCER ----------
    // Henter alle annoncer fra listen
    public List<Annonce> GetAll()
    {
        return annoncer;
    }

    // Finder en bestemt annonce ud fra dens ID
    public Annonce? GetById(int Annonce_Id)
    {
        // Gennemgår annoncerne én ad gangen
        foreach (Annonce annonce in annoncer)
        {
            // Hvis annoncens ID matcher det ønskede ID, returneres annoncen
            if (annonce.Annonce_Id == Annonce_Id)
            {
                return annonce;
            }
        }

        // Hvis ingen annonce har det ønskede ID, returneres null
        return null;
    }

    // Henter alle annoncer, som en bestemt bruger ejer
    public List<Annonce> GetByOwnerId(int Owner_Id)
    {
        // Where filtrerer listen og finder annoncer med det valgte Owner_Id
        // ToList omdanner resultatet til en liste
        return annoncer.Where(a => a.Owner_Id == Owner_Id).ToList();
    }


    // ---------- OPRET ANNONCE ----------
    // Opretter en ny annonce og gemmer den i listen
    public Annonce CreateNewAd(Annonce annonce)
    {
        // Hvis listen er tom, får annoncen ID 1
        // Ellers findes det højeste ID, og der lægges 1 til
        annonce.Annonce_Id = annoncer.Count == 0 ? 1 : annoncer.Max(a => a.Annonce_Id) + 1;

        // Tilføjer den nye annonce til listen
        annoncer.Add(annonce);

        // Returnerer den oprettede annonce
        return annonce;
    }


    // ---------- SLET ANNONCE ----------
    // Sletter en annonce ud fra dens ID
    public bool DeleteAd(int Annonce_Id)
    {
        // Finder annoncen ved hjælp af GetById()
        Annonce? annonce = GetById(Annonce_Id);
        
        if (annonce == null)// Hvis annoncen ikke findes, returneres false
        {
            return false;
        }
        
        annoncer.Remove(annonce);// Fjerner annoncen fra listen

        // Returnerer true, fordi annoncen blev slettet
        return true;
    }


    // ---------- OPDATER ANNONCE ----------
    // Opdaterer oplysningerne på en eksisterende annonce
    public bool UpdateAnnonce(Annonce annonce)
    {
        Annonce? OldAnnonce = GetById(annonce.Annonce_Id); // Finder den eksisterende annonce ud fra dens ID

        // Hvis annoncen ikke findes, returneres false
        if (OldAnnonce == null)
        {
            return false;
        }
        
        int index = annoncer.IndexOf(OldAnnonce); // Finder placeringen af den gamle annonce i listen
        
        annoncer[index] = annonce;// Erstatter den gamle annonce med den opdaterede annonce
        
        return true; // Returnerer true, fordi annoncen blev opdateret
    }


    // ---------- OPDATER STATUS ----------
    // Ændrer status på en annonce, fx fra Active til Reserved
    public bool UpdateAnnonceStatus(int Annonce_Id, string Status)
    {
        Annonce? annonce = GetById(Annonce_Id); // Finder annoncen ud fra dens ID
        
        if (annonce == null) // Hvis annoncen ikke findes, returneres false
        {
            return false;
        }
        
        annonce.Status = Status;// Ændrer annoncens status til den nye status
        
        return true; // Returnerer true, fordi status blev opdateret
    }
}
