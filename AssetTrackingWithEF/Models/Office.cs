namespace AssetTrackingWithEF.Models;

public class Office
{
    public int OfficeId { get; set; }
    public string OfficeLocation { get; set; }
    public string CurrencyCode { get; set; }
    public decimal ConversionRateFromUSD { get; set; }

}
