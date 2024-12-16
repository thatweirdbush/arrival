using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Contracts.Services;
public interface IImageService
{
    /// <summary>
    /// Uploads an image to the cloud and returns its URL.
    /// </summary>
    /// <param name="imageStream">The stream of the image to upload.</param>
    /// <param name="fileName">The name of the file.</param>
    /// <returns>The URL of the uploaded image.</returns>
    Task<string> UploadImageAsync(Stream imageStream, string fileName);

    /// <summary>
    /// Deletes an image from the cloud by its public ID.
    /// </summary>
    /// <param name="publicId">The public ID of the image to delete.</param>
    /// <returns>True if deletion was successful, otherwise false.</returns>
    Task<bool> DeleteImageAsync(string publicId);
}
