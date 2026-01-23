
using AssetTrackingWithEF.Models;
using AssetTrackingWithEF.Services;
using static AssetTrackingWithEF.Helpers.Validators;

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
        Console.WriteLine($"{"No.",-4}{"Category",-15}{"Brand",-15}{"Model",-15}{"Price",-10}{"Due Date",-15}{"Office",-12}{"Expiry Status",-15}");
        Console.WriteLine(new String('-', 100));

        for (int i = 0; i < assetsList.Count; i++)
        {
            var asset = assetsList[i];
            var status = Validators.GetExpiryStatus(asset);

            if (status == ExpiryStatus.ThreeMonths)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (status == ExpiryStatus.SixMonths)
                Console.ForegroundColor = ConsoleColor.Yellow;

            string statusText = status == ExpiryStatus.ThreeMonths ? "<3 months" : status == ExpiryStatus.SixMonths ? "<6 months" : "---";

            if (i == index)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine($"{asset.AssetId,-4}{asset.Category.CategoryName,-15}{asset.Brand,-15}{asset.ModelName,-15}{asset.Price,-10}{asset.PurchaseDate.ToShortDateString(),-15}{asset.Office.OfficeLocation, -12}{statusText, -15}");
            Console.ResetColor();
        }
    }

    public static void RenderSortOptionsAndShowTable()
    {
        Console.WriteLine("How do want to sort your assets?");

        var options = new List<string> { "Id", "Brand", "Category", "Date", "Office" };
        string selectedOption = ConsoleHelpers.RenderAndSelectFromList(options, ConsoleHelpers.RenderList);

        Console.WriteLine();
        Console.WriteLine($"Here are your sorted assets by {selectedOption}");
        Console.WriteLine();

        MenuActions.ShowAssetsTable(selectedOption.ToLower());
    }

}
