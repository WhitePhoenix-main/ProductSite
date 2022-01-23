using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ProductsSite
{
    public interface IProductsRepository
    {
        Task<(bool success, string? errorMessage)> SaveFileAsync(ProductRecord product, IFormFile formFile);
        public string GetPic(ProductRecord product);

        public string GetFolder(ProductRecord product);

        public void DelFolderWithFiles(string path);
    }

    public class ProductsRepository : IProductsRepository
    {
        public ProductsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; init; }

        private string GetDir(ProductRecord product)
        {
            return Path.Combine(_configuration.GetSection("Storage").Value, product.Id.ToString());
        }

        public string GetFolder(ProductRecord product)
        {
            return GetDir(product);
        }

        public string GetPic(ProductRecord product)
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

        public async Task<(bool success, string? errorMessage)> SaveFileAsync(ProductRecord product, IFormFile formFile)
        {
            string prev = product.PreviewName;
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
            if (product.OnPreview)
            {
                product.PreviewName = fileName;
            }
            else
            {
                product.PreviewName = prev;
            }

            return (true, null);
        }

        public void DelFolderWithFiles(string path)//DelDirectoryWithFiles
        {
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    File.Delete(Path.Combine(path, file));
                }
                var directories = Directory.GetDirectories(path);
                foreach (var dirPath in directories)
                {
                    DelFolderWithFiles(dirPath);
                }
                Directory.Delete(path);
            }
        }
    }
}