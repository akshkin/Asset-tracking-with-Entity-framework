using AssetTrackingWithEF.Models;

namespace AssetTrackingWithEF.Helpers;

public static class Calculations
{
    public static ExpiryStatus GetExpiryStatus(Asset asset)
    {
        var expiryDate = asset.PurchaseDate.AddYears(3);
        var today = DateTime.Today;

        if (expiryDate <= today.AddMonths(3) && expiryDate >= today)
            return ExpiryStatus.ThreeMonths;

        if (expiryDate <= today.AddMonths(6) && expiryDate >= today)
            return ExpiryStatus.SixMonths;

        return ExpiryStatus.None;
    }

    public static string GetConvertedPriceToLocalCurrency(Asset asset)
    {
        decimal convertedPriceToLocalCurrency = (decimal)asset.Price * asset.Office.ConversionRateFromUSD;
        string priceString = convertedPriceToLocalCurrency + " " + asset.Office.CurrencyCode;
        return priceString;
    }

    public static IEnumerable<(Office office, decimal totalValue)> GetTotalAssetValue(List<Asset> assetsList)
    {
        return assetsList.GroupBy(a => a.Office).Select(officeGroup => (
            office: officeGroup.Key,
            totalValue: officeGroup.Sum(a => (decimal)a.Price * a.Office.ConversionRateFromUSD)
        ));
    }
}
