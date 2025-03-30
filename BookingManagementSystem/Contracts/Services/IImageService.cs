namespace BookingManagementSystem.Contracts.Services;

public interface IImageService
{
    /// <summary>
    /// Uploads an image to the cloud and returns its URL.
    /// </summary>
    /// <param name="imageStream">The stream of the image to upload.</param>
    /// <param name="fileName">The name of the file.</param>
    /// <returns>The URL of the uploaded image.</returns>
    Task<string> UploadImageAsync(Stream imageStream, string fileName, string folderName);

    /// <summary>
    /// Deletes an image from the cloud by its public ID.
    /// </summary>
    /// <param name="publicId">The public ID of the image to delete.</param>
    /// <returns>True if deletion was successful, otherwise false.</returns>
    Task<bool> DeleteImageAsync(string publicId);

    /// <summary>
    /// Deletes a range of images from the cloud by their public IDs.
    /// </summary>
    /// <param name="publicIds">The public IDs of the images to delete.</param>
    /// <returns>True if deletion was successful, otherwise false.</returns></returns>
    Task<bool> DeleteImageRangeAsync(IEnumerable<string> publicIds);
}
