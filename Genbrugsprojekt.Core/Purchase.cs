namespace Genbrugsprojekt.Core;

public class Purchase
{
    public string Purchase_Id { get; set; } = "";

    public string Annonce_Id { get; set; } = "";

    // Id'et på det medlem, der vil købe
    public string Buyer_Id { get; set; } = "";

    // Id'et på det medlem, der sælger (ejer annoncen)
    public string Seller_Id { get; set; } = "";

    // Købets tilstand: "Afventer", "Accepteret" eller "Afvist"
    public string Status { get; set; } = "Afventer";
}
