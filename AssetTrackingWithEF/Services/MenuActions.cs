using AssetTrackingWithEF.Helpers;
using AssetTrackingWithEF.Models;

namespace AssetTrackingWithEF.Services;

public static class MenuActions
{
    private static readonly AssetStorage _storage = new AssetStorage();
    public static void ShowAssetsTable(string sortBy = null, int index = -1)
    {
        var assetsList = _storage.LoadAssets(sortBy);
        var categories = _storage.GetCategories();

        if (assetsList.Count == 0)
        {
            Console.WriteLine("No assets yet");
        }

        Console.WriteLine();
       
        ConsoleHelpers.RenderAssets(assetsList, -1);
        Console.WriteLine();
    }

    public static void AddAsset()
    {
        Console.WriteLine(@"Enter product type - 'Computer' or 'Phone' :  ", "Product Type");

        var categories = _storage.GetCategories();
        var categoryNames = categories.Select(c => c.CategoryName).ToList();

        var offices = _storage.GetOfficeLocations();
        var officeLocations = offices.Select(o => o.OfficeLocation).ToList();

        string selectedCategory = ConsoleHelpers.RenderAndSelectFromList(categoryNames, ConsoleHelpers.RenderList);

        string Brand = Validators.ValidateInput("Enter brand of the product : ", "Brand");
        string Model = Validators.ValidateInput("Enter product model : ", "Model");

        string selectedOffice = ConsoleHelpers.RenderAndSelectFromList(officeLocations, ConsoleHelpers.RenderList);

        DateTime PurchaseDate = Validators.ValidateDate("Enter purchase date in format YYYY-MM-DD : ");

        double PricePaid = Validators.ValidateDouble("Enter price in USD for the product : ");

        var assetCategory = categories.First(c => c.CategoryName == selectedCategory);
        var assetOffice = offices.First(o => o.OfficeLocation == selectedOffice);

        Asset newProduct = new Asset();
        newProduct.Brand = Brand;
        newProduct.ModelName = Model;
        newProduct.PurchaseDate = PurchaseDate;
        newProduct.Price = PricePaid;
        newProduct.CategoryId = assetCategory.CategoryId;
        newProduct.OfficeId = assetOffice.OfficeId;

        _storage.SaveAsset(newProduct);

        ConsoleHelpers.WriteColoredText(ConsoleColor.Green, "Product added succesfully!\n");
        Console.WriteLine("Press any key to go back to main menu");
        Console.ReadKey();
    }

    public static void EditAsset()
    {
        var assetsList = _storage.LoadAssets();

        if (assetsList.Count == 0)
        {
            Console.WriteLine("No assets added yet");
        }
        else
        {
            Console.WriteLine("Which asset do you want to edit? Use arrow keys to navigate up and dowm");

            Asset selectedAsset = ConsoleHelpers.RenderAndSelectFromList(assetsList, ConsoleHelpers.RenderAssets);


            if (selectedAsset != null)
            {
                Console.WriteLine("What would you like to edit? Press 'Enter' to keep the previous value");
                Console.WriteLine($"Current Brand is {selectedAsset.Brand}.");
                Console.Write("New brand (press 'Enter' to keep the same value) : ");
                string brand = Console.ReadLine();

                if(!string.IsNullOrEmpty(brand)) selectedAsset.Brand = brand;

                Console.WriteLine($"Current Model name is {selectedAsset.ModelName}.");
                Console.Write("New Model (press 'Enter' to keep the same value) : ");
                string model = Console.ReadLine();
                if (!string.IsNullOrEmpty(model)) selectedAsset.ModelName = model;

                Console.WriteLine($"Current Purchase Date is {selectedAsset.PurchaseDate}.");
                Console.Write("New date: ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    selectedAsset.PurchaseDate = selectedAsset.PurchaseDate;
                }
                else
                {
                    DateTime PurchaseDate = Validators.ValidateDate("Enter purchase date in format YYYY-MM-DD : ");
                    selectedAsset.PurchaseDate = PurchaseDate;             
                }
        
                Console.WriteLine($"Current Price is {selectedAsset.Price}");
                Console.Write("New price: ");
                string newPrice = Console.ReadLine();
                if (string.IsNullOrEmpty(newPrice))
                {
                    selectedAsset.Price = selectedAsset.Price;
                } 
                else
                {
                    double price = Validators.ValidateDouble("Enter new price in USD: ");
                    selectedAsset.Price = price;            
                }

                _storage.UpdateAsset(selectedAsset);

                ConsoleHelpers.WriteColoredText(ConsoleColor.Green, "Successfully saved changes");
                Console.WriteLine("Press any key to go back to main menu");
                Console.ReadKey();
            }
        }
    }

 
    public static void DeleteAsset()
    {
        var assetsList = _storage.LoadAssets();
        Console.WriteLine("Which asset do you want to edit? Use arrow keys to navigate up and dowm");

        Asset selectedAsset = ConsoleHelpers.RenderAndSelectFromList(assetsList, ConsoleHelpers.RenderAssets);

        if (selectedAsset != null) 
        {
            Console.WriteLine();
            ConsoleHelpers.WriteColoredText(ConsoleColor.Red, "Are you sure you want to delete this asset? Type 'y' for yes and 'n' for no");
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Y)
            {
                _storage.DeleteAsset(selectedAsset);
                Console.WriteLine();
                ConsoleHelpers.WriteColoredText(ConsoleColor.Green, "Successfully deleted asset");
                Console.WriteLine("Press any key to go back to main menu");
                Console.ReadKey();
            }
            else if (key == ConsoleKey.N) 
            {
                return;
            }
        };
    }

    public static void AddDemoData()
    {
        var categories = _storage.GetCategories();
        var offices = _storage.GetOfficeLocations();
        var assets = _storage.LoadAssets(null); //sortBy is null

        Asset newProduct1 = new Asset();
        newProduct1.Brand = "Apple";
        newProduct1.ModelName = "Macbook pro";
        newProduct1.PurchaseDate = new DateTime(2025,12,10);
        newProduct1.Price = 3000.00;
        newProduct1.CategoryId = categories.FirstOrDefault(c => c.CategoryName == "Laptop").CategoryId;
        newProduct1.OfficeId = offices.FirstOrDefault(o => o.OfficeLocation == "New York").OfficeId;

        Asset newProduct2 = new Asset();
        newProduct2.Brand = "Apple";
        newProduct2.ModelName = "iPhone 13";
        newProduct2.PurchaseDate = new DateTime(2025,12,10);
        newProduct2.Price = 2000.00;
        newProduct2.CategoryId = categories.FirstOrDefault(c => c.CategoryName == "Phone").CategoryId;
        newProduct2.OfficeId = offices.FirstOrDefault(o => o.OfficeLocation == "London").OfficeId;


        if (assets.Count == 0)
        {
            _storage.SaveAsset(newProduct1);
            _storage.SaveAsset(newProduct2);
        }
    }

    public static void GenerateAssetsReport()
    {
        var assetsList = _storage.LoadAssets();

        ConsoleHelpers.GenerateReport(assetsList);
    }
}
