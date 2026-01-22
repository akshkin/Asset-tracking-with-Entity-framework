using AssetTrackingWithEF.Models;

namespace AssetTrackingWithEF.Services
{
    public class AssetStorage
    {
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Assets;Trusted_Connection=True;";

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
