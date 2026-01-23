using AssetTrackingWithEF.Models;

namespace AssetTrackingWithEF.Helpers;

public static class Validators
{
    public static string ValidateInput(string question, string Field)
    {
        while (true)
        {
            Console.Write(question);
            string input = Console.ReadLine();

            if (!string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                ConsoleHelpers.WriteColoredText(ConsoleColor.Red, $"{Field} cannot be empty. Please try again");
            }
        }
    }

    public static DateTime ValidateDate(string question)
    {
        while (true)
        {
            Console.Write(question);
            string response = Console.ReadLine();
            DateTime PurchaseDate = new DateTime();

            if (DateTime.TryParse(response, out PurchaseDate))
            {
                return PurchaseDate;
            }
            else
            {
                ConsoleHelpers.WriteColoredText(ConsoleColor.Red, "Invalid date format. Please enter Date of purchase in the format YYYY-MM-DD");
            }
        }
    }

    public static double ValidateDouble(string question)
    {
        while (true)
        {
            Console.Write(question);
            string response = Console.ReadLine();
            double PricePaid = 0;

            if (double.TryParse(response, out PricePaid))
            {
                return PricePaid = double.Parse(response);
            }
            else
            {
                ConsoleHelpers.WriteColoredText(ConsoleColor.Red, "Invalid number. Please try again");
            }
        }
    }

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
