using SaleWebsite.Models;

namespace SaleWebsite.Helper_Functions
{
    public class FileSystemForImages
    {
        public static bool RemoveImageFromLocal(string path, Image image)
        {
            var filePath = Path.Combine(path, image.Url.TrimStart('/'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
