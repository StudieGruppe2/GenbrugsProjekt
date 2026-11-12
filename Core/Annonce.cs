namespace Core;

public class Annonce
{
    public int Annonce_Id { get; set; } = Random.Shared.Next();
    
    public string Title { get; set; } = "";

    public string Category { get; set; } = "";

    // Et link til et billede af varen
    public string Picture { get; set; } = "";

    public string Description { get; set; } = "";

    public double Price { get; set; }

    // Id'et på det medlem, der ejer annoncen
    public int Owner_Id { get; set; }

    public DateTime Published_Date { get; set; }

    // Lokale på Eaaa, hvor varen ligger - eller "Ikke på Eaaa"
    public string Location { get; set; } = "";

    // Annoncens tilstand: "Active", "Reserved", "Sold" eller "Inactive"
    public string Status { get; set; } = "Active";
}
