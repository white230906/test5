using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TetPee.Service.MediaService;

namespace TetPee.Service.CloudinaryService;

public class Service: IService
{
    private readonly Cloudinary _cloudinary;
    private readonly CloudinaryOptions _cloudinaryOptions = new CloudinaryOptions();

    public Service(IConfiguration configuration)
    {
        configuration.GetSection(nameof(CloudinaryOptions)).Bind(_cloudinaryOptions);
        _cloudinary = new Cloudinary(new Account(
            _cloudinaryOptions.CloudName,
            _cloudinaryOptions.ApiKey,
            _cloudinaryOptions.ApiSecret
            )
        );
    }
    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file.Length == 0 || file == null)
        {
            throw new ArgumentException("file is empty or null",nameof(file));
        }

        if (!IsImageFile(file))
        {
            throw new ArgumentException("file is not an image", nameof(file));
        }
        await using var stream = file.OpenReadStream();
        var uploadParam = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParam); 
        return uploadResult.SecureUri.ToString();
    }

    private bool IsImageFile(IFormFile file)
    {
        var allowExtension = new [] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(file.FileName);
        return allowExtension.Contains(extension);
    }
}