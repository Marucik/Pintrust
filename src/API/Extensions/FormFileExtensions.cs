using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
  public static class FormFileExtensions
  {
    public static async Task<string> SaveFileAndGetUrl(this IFormFile image)
    {
      if (image.Length > 0)
      {
        var fileName = Guid.NewGuid();
        var filePath = Path.Combine(@"C:\Users\rakoc\pintrust\backend\src\API\static",
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