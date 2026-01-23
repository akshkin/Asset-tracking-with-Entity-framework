using AssetTrackingWithEF.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetTrackingWithEF.Services
{
    public class AssetStorage
    {       
        public List<Asset> LoadAssets(string sortBy= null)
        {
            using var context = new MyDbContext();

            var query = context.Assets.Include(a => a.Category).Include(a => a.Office).AsQueryable();
            return sortBy switch
            {
                "id" => query.OrderBy(a => a.AssetId).ToList(),
                "brand" => query.OrderBy(a => a.Brand).ToList(),
                "category" => query.OrderBy(a => a.Category.CategoryName).ToList(),
                "date" => query.OrderBy(a => a.PurchaseDate).ToList(),
                "office" => query.OrderBy(a => a.Office.OfficeLocation).ToList(),
                _ => query.ToList(),
            };
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
