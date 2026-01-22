namespace AssetTrackingWithEF.Models;

public class Asset
{
    public int AssetId { get; set; }
    public string Brand { get; set; }
    public string ModelName { get; set; }
    public double Price {  get; set; }
    public DateTime PurchaseDate { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }

    public Office Office { get; set; }
    public int OfficeId { get; set; }
}
