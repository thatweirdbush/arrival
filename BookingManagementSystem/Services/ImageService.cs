using BookingManagementSystem.Contracts.Services;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookingManagementSystem.Services;
public class ImageService : IImageService
{
    private readonly Cloudinary _cloudinary;
    private readonly ILogger<ImageService> _logger;

    private const string ParentFolder = "Arrival";

    public ImageService(IConfiguration configuration, ILogger<ImageService> logger)
    {
        _logger = logger;

        var cloudName = configuration["Cloudinary:CloudName"];
        var apiKey = configuration["Cloudinary:ApiKey"];
        var apiSecret = configuration["Cloudinary:ApiSecret"];

        if (string.IsNullOrWhiteSpace(cloudName) || string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiSecret))
            throw new InvalidOperationException("Cloudinary configuration is missing.");

        var account = new Account(cloudName, apiKey, apiSecret);
        _cloudinary = new Cloudinary(account) { Api = { Secure = true } };
    }

    public async Task<string> UploadImageAsync(Stream imageStream, string fileName, string folderName = ParentFolder)
    {
        if (imageStream == null || imageStream.Length == 0)
            throw new ArgumentException("Image stream cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be null or empty.");

        try
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, imageStream),
                Folder = $"{ParentFolder}/{folderName}", // Folder on Cloudinary, eg: Arrival/17
                UseFilename = true,
                UniqueFilename = false, // Use the original file name, no random prefix
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Image upload failed with status: {uploadResult.StatusCode}. Error: {uploadResult.Error?.Message}");

            return uploadResult.SecureUrl.AbsoluteUri; // Image URL
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload image.");
            throw;
        }
    }

    public async Task<bool> DeleteImageAsync(string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            throw new ArgumentException("Public ID cannot be null or empty.");

        try
        {
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            return deletionResult.Result == "ok";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to delete image with Public ID: {publicId}", publicId);
            throw;
        }
    }

    public async Task<bool> DeleteImageRangeAsync(IEnumerable<string> publicIds)
    {
        if (publicIds == null || !publicIds.Any())
            throw new ArgumentException("Public IDs cannot be null or empty.");

        try
        {
            // Create a list of Public IDs
            var deletionResult = await _cloudinary.DeleteResourcesAsync(publicIds.ToArray());

            // Check if all resources were deleted successfully
            return deletionResult.Deleted.Count == publicIds.Count();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to delete images with Public IDs: {string.Join(", ", publicIds)}", publicIds);
            throw;
        }
    }

}
