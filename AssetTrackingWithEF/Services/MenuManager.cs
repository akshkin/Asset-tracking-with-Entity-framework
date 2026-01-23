using AssetTrackingWithEF.Helpers;
using AssetTrackingWithEF.Models;

namespace AssetTrackingWithEF.Services;

internal class MenuManager
{
    public static void ShowHeader(string headerString)
    {
        ConsoleHelpers.WriteColoredText(ConsoleColor.Blue, headerString.ToUpper());
        Console.WriteLine();
    }
    public static void ShowMenu()
    {
        Console.WriteLine("Pick an option:");
        Console.WriteLine("(1) Show assets list (by category or office)");
        Console.WriteLine("(2) Add a new Asset");
        Console.WriteLine("(3) Edit an Asset");
        Console.WriteLine("(4) Delete an Asset");
        Console.WriteLine("(5) Save and Quit");
    }

    public static bool HandleChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                ShowHeader("All your assets");
                ConsoleHelpers.RenderSortOptionsAndShowTable();
                Console.WriteLine("Press any key to go back to main menu");
                Console.ReadKey();
                return false;

            case "2":
                ShowHeader("Add a new asset");
                MenuActions.AddAsset();
                return false;

            case "3":
                ShowHeader("Edit an asset");
                MenuActions.EditAsset();
                return false;

            case "4":
                ShowHeader("Delete an asset");
                MenuActions.DeleteAsset();
                return false;

            case "5":
                Console.WriteLine("All assets saved");
                return true;

            default:
                ConsoleHelpers.WriteColoredText(ConsoleColor.Red, "Invalid option");
                Console.ReadKey();
                return false;
        }
    }

}
