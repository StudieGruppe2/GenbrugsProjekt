using Core; // Giver adgang til Annonce-klassen fra Core

namespace Backend.Repositories;

// Interfacet fungerer som en kontrakt, der bestemmer hvilke metoder
// både AnnonceRepositoryMemory og AnnonceRepositoryMongoDB skal have
public interface IAnnonceRepository
{
    // Henter alle annoncer
    // List<Annonce> betyder, at metoden returnerer en liste med annoncer
    List<Annonce> GetAll();

    // Finder én bestemt annonce ud fra dens ID
    // Annonce? betyder, at metoden kan returnere en annonce eller null
    Annonce? GetById(int Annonce_Id);

    // Henter alle annoncer, som tilhører en bestemt bruger
    // Owner_Id er ID'et på den bruger, der ejer annoncerne
    // Returnerer en liste, som også kan være tom
    List<Annonce> GetByOwnerId(int Owner_Id);

    // Opretter en ny annonce
    // Returnerer den oprettede annonce
    Annonce CreateNewAd(Annonce annonce);

    // Sletter en annonce ud fra dens ID
    // true = annoncen blev slettet, false = annoncen blev ikke fundet
    bool DeleteAd(int Annonce_Id);

    // Opdaterer oplysningerne på en eksisterende annonce
    // Returnerer true, hvis annoncen blev fundet, ellers false
    bool UpdateAnnonce(Annonce annonce);

    // Ændrer status på en bestemt annonce
    // Annonce_Id fortæller hvilken annonce, der skal opdateres
    // Status er den nye status, fx Active, Reserved, Sold eller Inactive
    bool UpdateAnnonceStatus(int Annonce_Id, string Status);
}