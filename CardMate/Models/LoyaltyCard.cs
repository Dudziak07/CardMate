using SQLite;

namespace CardMate.Models;
public class LoyaltyCard
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string StoreName { get; set; }
    public string OwnerName { get; set; }
    public string CardNumber { get; set; }
    public string Description { get; set; }

    public string LogoResourceName { get; set; }

    public string BarcodeFormat { get; set; }

    public DateTime DateAdded { get; set; }
    public DateTime LastEdited { get; set; }
}
