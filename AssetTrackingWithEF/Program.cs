using AssetTrackingWithEF.Helpers;
using AssetTrackingWithEF.Models;
using AssetTrackingWithEF.Services;
using System.Globalization;

bool hasQuit = false;
var assetsList = new List<Asset>();

while (!hasQuit)
{
    Console.Clear();
    Console.WriteLine(); 
    Console.WriteLine("ASSET TRACKING - TRACK YOUR ASSETS HERE");
    Console.WriteLine();
  
    if (assetsList.Count == 0)  MenuActions.AddDemoData(assetsList);

    MenuManager.ShowMenu();

    string choice = Console.ReadLine();
    Console.Clear();
    hasQuit = MenuManager.HandleChoice(choice, assetsList);
}

if (assetsList.Count > 0)
{
    //sort the list first by office and then by purchase date
    var NewProductList = assetsList.OrderBy(Product => Product.PurchaseDate).OrderBy(Product => Product.Office).ToList();

    ConsoleHelpers.WriteColoredText(ConsoleColor.Magenta, "\nHere is a list of all your products sorted by Office location\n");

    //string AlignmentSpecifiers = "{0,-10} {1,-10} {2,-10} {3,-10} {4,-15} {5,-15} {6,-10} {7,-20}";

    //Console.WriteLine(AlignmentSpecifiers,
    //"Type", "Brand", "Model", "Office", "Purchase Date", "Price in USD", "Currency", "Local Price Today");

    //Console.WriteLine(AlignmentSpecifiers,
    //    "-----", "-----", "-----", "-------", "-------------", "-----------", "---------", "--------------");



    foreach (Asset Product in NewProductList)
    {
        DateTime Today = DateTime.Now;
        DateTime ThreeYears = Product.PurchaseDate.AddYears(3);

        //calculate less than 3 months
        if (ThreeYears <= DateTime.Today.AddMonths(3) && ThreeYears >= DateTime.Today)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        //calculate less than 6 months
        else if (ThreeYears <= DateTime.Today.AddMonths(6) && ThreeYears >= DateTime.Today)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        // using CultureInfo.InvariantCulture to bypass the culture’s date separator '-' and use a '/' instead
        //Console.WriteLine(AlignmentSpecifiers,
        //Product.Category.CategoryName, Product.Brand, Product.ModelName, Product.Office, Product.PurchaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), Product.Price);
        Console.WriteLine($"{Product.Category.CategoryName}, {Product.Brand}, {Product.ModelName}, {Product.Office.OfficeLocation}, {Product.PurchaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}, {Product.Price}");
        Console.ResetColor();
    }
}
