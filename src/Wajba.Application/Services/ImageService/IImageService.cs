global using Microsoft.AspNetCore.Http;
using System.IO;

namespace Wajba.Services.ImageService;

public interface IImageService
{
   
    Task<string> UploadAsync(IFormFile file);
    Task<string> UploadAsync(Stream stream, string fileName);
}
