namespace Core;

public class Annonce
{
    public string Annonce_Id { get; set; } = "";
    
    public string Title { get; set; } = "";

    public string Category { get; set; } = "";

    // Et link til et billede af varen
    public string Picture { get; set; } = "";

    public string Description { get; set; } = "";

    public double Price { get; set; }

    // Id'et på det medlem, der ejer annoncen
    public string Owner_Id { get; set; } = "";

    public DateTime Published_Date { get; set; }

    // Lokale på Eaaa, hvor varen ligger - eller "Ikke på Eaaa"
    public string Location { get; set; } = "";

    // Annoncens tilstand: "Aktiv", "Reserveret", "Solgt" eller "Inaktiv"
    public string Status { get; set; } = "Aktiv";
}
