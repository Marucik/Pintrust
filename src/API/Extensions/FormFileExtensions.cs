using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class FormFileExtensions
    {

        public static async Task<string> SaveFileAndGetUrl(this IFormFile image, IWebHostEnvironment webHostEnvironment)
        {
            if (image.Length > 0)
            {
                var fileName = Guid.NewGuid();
                var filePath = Path.Combine($"{webHostEnvironment.ContentRootPath}/static/",
                                            $"{fileName.ToString()}.png");

                using (var stream = System.IO.File.Create(filePath))
                {
                    await image.CopyToAsync(stream);
                }

                return $"/static/{fileName}.png";
            }
            throw new Exception("File is corupted.");
        }
    }
}