
using AssetTrackingWithEF.Models;
using AssetTrackingWithEF.Services;

namespace AssetTrackingWithEF.Helpers;

public static class ConsoleHelpers
{
    public static void WriteColoredText(ConsoleColor Color, string Message)
    {
        Console.ForegroundColor = Color;
        Console.WriteLine(Message);
        Console.ResetColor();
    }


    public static T RenderAndSelectFromList<T>(List<T> list, Action<List<T>, int> renderMethod)
    {
        Console.WriteLine("Use arrow keys to select");
        Console.WriteLine();
        int index = 0;
        int startTop = Console.CursorTop;

        while (true)
        {
            Console.SetCursorPosition(0, startTop); //clear just the repeated rendered categories

            renderMethod(list, index); //render the list

            var key = Console.ReadKey(true).Key;

            switch (key) 
            {
                case ConsoleKey.UpArrow:
                    if (index > 0) index--; 
                    break;
                case ConsoleKey.DownArrow:
                    if (index < list.Count - 1) index++;
                    break;
                case ConsoleKey.Enter:
                    return list[index];
                case ConsoleKey.Escape:
                    return default;
            }
        }
    }

    public static void RenderList(List<string> list, int index)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i == index)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine(list[i]);
            Console.ResetColor();
        }
    }

    public static void RenderAssets(List<Asset> assetsList, int index)
    {
        Console.WriteLine();
        Console.WriteLine($"{"No.",-4}{"Category",-15}{"Brand",-15}{"Model",-15}{"Price",-18}{"Due Date",-15}{"Office",-12}{"Expiry Status",-15}");
        Console.WriteLine(new String('-', 110));

        for (int i = 0; i < assetsList.Count; i++)
        {
            var asset = assetsList[i];

            HighlightExpiry(asset);
            var status = Validators.GetExpiryStatus(asset);

            string statusText = status == ExpiryStatus.ThreeMonths ? "<3 months" : status == ExpiryStatus.SixMonths ? "<6 months" : "---";

            if (i == index)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            string priceString = Validators.GetConvertedPriceToLocalCurrency(asset);

            Console.WriteLine($"{asset.AssetId,-4}{asset.Category.CategoryName,-15}{asset.Brand,-15}{asset.ModelName,-15}{priceString,-18}{asset.PurchaseDate.ToShortDateString(),-15}{asset.Office.OfficeLocation, -12}{statusText, -15}");
            Console.ResetColor();
        }
    }

    public static void RenderSortOptionsAndShowTable()
    {
        Console.WriteLine("How do want to sort your assets?");

        var options = new List<string> { "Id", "Brand", "Category", "Date", "Office" };
        string selectedOption = ConsoleHelpers.RenderAndSelectFromList(options, ConsoleHelpers.RenderList);

        if (selectedOption != null)
        {
            Console.WriteLine();
            Console.WriteLine($"Here are your sorted assets by {selectedOption}");

            MenuActions.ShowAssetsTable(selectedOption.ToLower());

            ConsoleHelpers.GoBackToMainMenu();
        }
    }

    public static void HighlightExpiry(Asset asset)
    {
        var status = Validators.GetExpiryStatus(asset);

        if (status == ExpiryStatus.ThreeMonths)
            Console.ForegroundColor = ConsoleColor.Red;
        else if (status == ExpiryStatus.SixMonths)
            Console.ForegroundColor = ConsoleColor.Yellow;

    }

    public static void GenerateReport(List<Asset> assetsList)
    {
        var assetsByGroup = assetsList.GroupBy(a => a.Office);
            
        foreach(var office in assetsByGroup)
        {
            string assetStringPluralOrSingular = office.Count() == 1 ? "asset" : "assets";
            Console.WriteLine();
            WriteColoredText(ConsoleColor.Magenta ,$"{office.Key.OfficeLocation} ({office.Count()} {assetStringPluralOrSingular})");

            foreach(var asset in office)
            {
                HighlightExpiry(asset);
                string priceString = Validators.GetConvertedPriceToLocalCurrency(asset);
                Console.WriteLine($"   {asset.AssetId,-4}{asset.Category.CategoryName,-12}{asset.Brand,-15}{asset.ModelName,-15}{priceString,-18}{asset.PurchaseDate.ToShortDateString(),-15}");
                Console.ResetColor();
            }
        }

        var officesWithTotalValue = Validators.GetTotalAssetValue(assetsList);

        Console.WriteLine();
        Console.WriteLine("===============================");
        Console.WriteLine("TOTAL ASSETS VALUE PER OFFICE:");
        Console.WriteLine("===============================");

        Console.WriteLine();

        foreach( var office in officesWithTotalValue)
        {
            Console.WriteLine($"{office.office.OfficeLocation}: {office.totalValue} {office.office.CurrencyCode}");
        }
        Console.WriteLine();
    }

    public static void GoBackToMainMenu()
    {
        Console.WriteLine("Press any key to go back to main menu");
        Console.ReadKey();
    }
}
