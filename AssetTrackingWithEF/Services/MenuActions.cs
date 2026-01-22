using AssetTrackingWithEF.Helpers;
using AssetTrackingWithEF.Models;

namespace AssetTrackingWithEF.Services;

public static class MenuActions
{
    public static void ShowAssetsTable(List<Asset> assetsList, int index = -1)
    {
        if (assetsList.Count == 0)
        {
            Console.WriteLine("No assets yet");
        }

        Console.WriteLine();
        Console.WriteLine($"{"No.",-4}{"Category",-25}{"Brand",-15}{"Model",-15}{"Price",-10}{"Due Date",-10}");

        for (int i = 0; i < assetsList.Count; i++)
        {
            var asset = assetsList[i];
            if (i == index)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine($"{asset.AssetId,-4}{asset.Category.CategoryName,-25}{asset.Brand,-15}{asset.ModelName,-15}{asset.Price,-10}{asset.PurchaseDate,-10}");
            Console.ResetColor();
        }
        Console.WriteLine();
    }

    public static void AddAsset(List<Asset> assetsList)
    {
        Console.WriteLine(@"Enter product type - 'Computer' or 'Phone' :  ", "Product Type");
        var categories = new List<string> { "Computer", "Mobile" };

        string selectedCategory = ConsoleHelpers.RenderAndSelectFromList(categories, ConsoleHelpers.RenderList);
        Category category = new Category();

        category.CategoryName = selectedCategory;

        string Brand = Validators.ValidateInput("Enter brand of the product : ", "Brand");
        string Model = Validators.ValidateInput("Enter product model : ", "Model");


        var offices = new List<string> { "New York", "London", "Tokyo" };
        string officeLocation = ConsoleHelpers.RenderAndSelectFromList(offices, ConsoleHelpers.RenderList);

        Office office = new Office();

        office.OfficeLocation = officeLocation;

        DateTime PurchaseDate = Validators.ValidateDate("Enter purchase date in format YYYY-MM-DD : ");

        double PricePaid = Validators.ValidateDouble("Enter price in USD for the product : ");

        Asset newProduct = new Asset();
        newProduct.Brand = Brand;
        newProduct.ModelName = Model;
        newProduct.PurchaseDate = PurchaseDate;
        newProduct.Price = PricePaid;
        newProduct.Category = category;
        newProduct.Office = office;

        assetsList.Add(newProduct);

        ConsoleHelpers.WriteColoredText(ConsoleColor.Green, "Product added succesfully!\n");
        Console.WriteLine("Press any key to go back to main menu");
        Console.ReadKey();
    }

    public static void EditAsset(List<Asset> assetsList)
    {
        if (assetsList.Count == 0)
        {
            Console.WriteLine("No assets added yet");
        }
        else
        {
            Console.WriteLine("Which asset do you want to edit? Use arrow keys to navigate up and dowm");

            Asset selectedAsset = ConsoleHelpers.RenderAndSelectFromList(assetsList, MenuActions.ShowAssetsTable);
                
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

                ConsoleHelpers.WriteColoredText(ConsoleColor.Green, "Successfully saved changes");
                Console.WriteLine("Press any key to go back to main menu");
                Console.ReadKey();
            }
        }
    }

    //public static int GetAssetId(List<Asset> assetsList, Asset selectedAsset)
    //{
    //    Asset asset = assetsList.FirstOrDefault(a => a.AssetId == selectedAsset.AssetId);

    //    return asset.AssetId;
    //}

    public static void DeleteAsset(List<Asset> assetsList)
    {
        Console.WriteLine("Which asset do you want to edit? Use arrow keys to navigate up and dowm");

        Asset selectedAsset = ConsoleHelpers.RenderAndSelectFromList(assetsList, MenuActions.ShowAssetsTable);

        if (selectedAsset != null) 
        {
            Console.WriteLine("Are you sure you want to delete this asset? Type 'y' for yes and 'n' for no");
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Y)
            {
                assetsList.Remove(selectedAsset);
                ConsoleHelpers.WriteColoredText(ConsoleColor.Green, "Successfully deleted asset");
            }
            else if (key == ConsoleKey.N) 
            {
                return;
            }
        };
    }

    public static void AddDemoData(List<Asset> assetsList)
{
        Asset newProduct1 = new Asset();
        Category category = new Category();
        category.CategoryName = "Laptop";
        Office office = new Office();
        office.OfficeLocation = "New York";
        newProduct1.Brand = "Apple";
        newProduct1.ModelName = "Macbook pro";
        newProduct1.PurchaseDate = new DateTime(2025,12,10);
        newProduct1.Price = 3000.00;
        newProduct1.Category = category;
        newProduct1.Office = office;

        assetsList.Add(newProduct1);

        Asset newProduct2 = new Asset();
        Category category2 = new Category();
        category2.CategoryName = "Phone";
        Office office2 = new Office();
        office.OfficeLocation = "London";
        newProduct2.Brand = "Apple";
        newProduct2.ModelName = "iPhone 13";
        newProduct2.PurchaseDate = new DateTime(2025,12,10);
        newProduct2.Price = 2000.00;
        newProduct2.Category = category2;
        newProduct2.Office = office;

        assetsList.Add(newProduct2);
    }
}
