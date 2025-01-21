global using CloudinaryDotNet;
global using CloudinaryDotNet.Actions;
using System.IO;

namespace Wajba.Services.ImageService;

public class ImageService : IImageService
{
    private readonly Cloudinary _cloudinary;

    public ImageService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "categories"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new UserFriendlyException("Failed to upload the image.");
        }

        return uploadResult.SecureUrl.ToString();
    }

    public Task<string> UploadAsync(Stream stream, string fileName)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, stream),
            Folder = "categories"
        };
        var uploadResult = _cloudinary.Upload(uploadParams);
        if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new UserFriendlyException("Failed to upload the image.");
        }
        return Task.FromResult(uploadResult.SecureUrl.ToString());
    }
}