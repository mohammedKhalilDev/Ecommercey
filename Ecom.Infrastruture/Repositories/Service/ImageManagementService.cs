using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastruture.Repositories.Service
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider fileProvider;

        public ImageManagementService(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var savedImageSrc = new List<string>();

            var imgDir = Path.Combine("wwwroot", "Images", src);

            if (Directory.Exists(imgDir) is not true)
            {
                Directory.CreateDirectory(imgDir);
            }

            foreach (var file in files)
            {
                var imgName = file.FileName;
                var imgSrc = $"/Images/{src}/{file.Name}";

                var root = Path.Combine(imgDir, imgName);

                using (FileStream stream = new FileStream(root, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                savedImageSrc.Add(imgSrc);
            }

            return savedImageSrc;
        }

        public void DeleteImageAsync(string src)
        {
            var info = fileProvider.GetFileInfo(src);

            var root = info.PhysicalPath;

            File.Delete(root);
        }
    }
}
