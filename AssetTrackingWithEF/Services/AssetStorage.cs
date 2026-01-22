using AssetTrackingWithEF.Models;

namespace AssetTrackingWithEF.Services
{
    public class AssetStorage
    {       
        public static List<Asset> LoadAssets()
        {
            using var context = new MyDbContext();
            context.Assets.ToList();
            return context.Assets.ToList();
        }

        public static List<string> GetCategories()
        {
            using var context = new MyDbContext();
            return context.Categories.Select(x => x.CategoryName).ToList();
        }

        public static List<string> GetOfficeLocations()
        {
            using var context = new MyDbContext();
            return context.Offices.Select(o => o.OfficeLocation).ToList();
        }

        public static void SaveAsset(Asset asset)
        {
            using var context = new MyDbContext();
            context.Assets.Add(asset);
        }

        public static void DeleteAsset(Asset asset) 
        {
            using var context = new MyDbContext();
            context.Assets.Remove(asset);
        }
    }
}
