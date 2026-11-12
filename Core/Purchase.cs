namespace Core;

public class Purchase
{
    public int Purchase_Id { get; set; } = Random.Shared.Next();

    public int Annonce_Id { get; set; } = Random.Shared.Next();

    // Id'et på det medlem, der vil købe
     public int Buyer_Id { get; set;} = Random.Shared.Next();

    // Id'et på det medlem, der sælger (ejer annoncen)
    public int Seller_Id { get; set; } = Random.Shared.Next();

    // Købets tilstand: "Afventer", "Accepteret" eller "Afvist"
    public string Status { get; set; } = "Afventer";
}
