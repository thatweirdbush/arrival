using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Contracts.Services;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;

namespace BookingManagementSystem.Services;
public class ImageService : IImageService
{
    private readonly Cloudinary _cloudinary;

    public ImageService(IConfiguration configuration)
    {
        var cloudName = configuration["Cloudinary:CloudName"];
        var apiKey = configuration["Cloudinary:ApiKey"];
        var apiSecret = configuration["Cloudinary:ApiSecret"];

        var account = new Account(cloudName, apiKey, apiSecret);
        _cloudinary = new Cloudinary(account);
        _cloudinary.Api.Secure = true;
    }

    public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
    {
        if (imageStream == null || imageStream.Length == 0)
            throw new ArgumentException("Image stream cannot be null or empty.");

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, imageStream),
            Folder = "properties", // Folder on Cloudinary
            UseFilename = true,
            UniqueFilename = false, // Use the original file name, no random prefix
            Overwrite = true
        };

        var uploadResult = await Task.Run(() => _cloudinary.Upload(uploadParams));

        if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception($"Image upload failed: {uploadResult.Error?.Message}");

        return uploadResult.SecureUrl.AbsoluteUri; // Image URL
    }

    public async Task<bool> DeleteImageAsync(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);
        var deletionResult = await Task.Run(() => _cloudinary.Destroy(deletionParams));

        return deletionResult.Result == "ok";
    }
}
