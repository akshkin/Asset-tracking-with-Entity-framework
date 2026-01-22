using AssetTrackingWithEF.Models;

namespace AssetTrackingWithEF.Services
{
    public class AssetStorage
    {       
        public List<Asset> LoadAssets()
        {
            using var context = new MyDbContext();
            context.Assets.ToList();
            return context.Assets.ToList();
        }

        public List<string> GetCategories()
        {
            using var context = new MyDbContext();
            return context.Categories.Select(x => x.CategoryName).ToList();
        }

        public List<string> GetOfficeLocations()
        {
            using var context = new MyDbContext();
            return context.Offices.Select(o => o.OfficeLocation).ToList();
        }

        public void SaveAsset(Asset asset)
        {
            using var context = new MyDbContext();
            context.Assets.Add(asset);
        }

        public void DeleteAsset(Asset asset) 
        {
            using var context = new MyDbContext();
            context.Assets.Remove(asset);
        }
    }
}
