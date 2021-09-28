using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ProductsSite
{
    public interface IProductsRepository
    {
        Task<(bool success, string? errorMessage)> SaveFileAsync(Product product, IFormFile formFile);
        public string GetPic(Product product);

        public string GetFolder(Product product);

        public void DelFolder(string path);
    }

    public class ProductsRepository : IProductsRepository
    {
        public ProductsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; init; }

        private string GetDir(Product product)
        {
            return Path.Combine(_configuration.GetSection("Storage").Value, product.Id.ToString());
        }

        public string GetFolder(Product product)
        {
            return GetDir(product);
        }

        public string GetPic(Product product)
        {
            if (product.PreviewName is null)
            {
                return _configuration.GetSection("DefaultPic").Value;
            }
            else
            {
                var path = Path.Combine(GetDir(product), product.PreviewName);
                return path.Substring(path.IndexOf("\\pics"));
            }
        }

        public async Task<(bool success, string? errorMessage)> SaveFileAsync(Product product, IFormFile formFile)
        {
            string dir = GetDir(product);
            if (!System.IO.Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string fileName = Path.GetFileName(formFile.FileName);
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = "picture.png";
            string path = Path.Combine(dir, fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            await using var stream = System.IO.File.Open(path, FileMode.CreateNew);
            await formFile.CopyToAsync(stream);
            stream.Close();
            product.PreviewName = fileName;
            return (true, null);
        }

        public void DelFolder(string path)
        {
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    File.Delete(Path.Combine(path, file));
                }
                Directory.Delete(path);
            }
        }
    }
}