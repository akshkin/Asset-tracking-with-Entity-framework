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

        public List<Category> GetCategories()
        {
            using var context = new MyDbContext();
            return context.Categories.ToList();
        }

        public List<Office> GetOfficeLocations()
        {
            using var context = new MyDbContext();
            return context.Offices.ToList();
        }

        public void SaveAsset(Asset asset)
        {
            using var context = new MyDbContext();
            context.Assets.Add(asset);
            context.SaveChanges();
        }

        public void DeleteAsset(Asset asset) 
        {
            using var context = new MyDbContext();
            context.Assets.Remove(asset);
            context.SaveChanges();
        }
    }
}
