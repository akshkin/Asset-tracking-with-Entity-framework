using AssetTrackingWithEF.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetTrackingWithEF.Services
{
    public class AssetStorage
    {       
        public List<Asset> LoadAssets()
        {
            using var context = new MyDbContext();
            var assets = context.Assets.Include(a => a.Category).Include(a => a.Office).ToList();
            return assets;
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

        public void UpdateAsset(Asset updatedAsset)
        {
            using var context = new MyDbContext();

            var existing = context.Assets
                .FirstOrDefault(a => a.AssetId == updatedAsset.AssetId);

            if (existing == null)
                return;

            existing.Brand = updatedAsset.Brand;
            existing.ModelName = updatedAsset.ModelName;
            existing.Price = updatedAsset.Price;
            existing.PurchaseDate = updatedAsset.PurchaseDate;
            existing.CategoryId = updatedAsset.CategoryId;
            existing.OfficeId = updatedAsset.OfficeId;

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
