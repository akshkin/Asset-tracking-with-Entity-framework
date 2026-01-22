using AssetTrackingWithEF.Models;

namespace AssetTrackingWithEF.Services
{
    public class AssetStorage
    {
        private MyDbContext _context = new MyDbContext();



       
        public static List<Asset> LoadAssets()
        {
            return  new List<Asset>();
        }

        public List<Asset> GetAssets()
        {
            var assets = new List<Asset>();
            return assets;
        }

        public static void SaveAssets(List<Asset> assets)
        {
        }
    }
}
