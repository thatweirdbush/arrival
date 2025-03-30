using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Windows.Storage;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlacePhotosViewModel : BaseStepViewModel
{
    private readonly IPropertyService _propertyService;
    private readonly IImageService _imageService;

    [ObservableProperty]
    private bool isPhotoListEmpty;

    [ObservableProperty]
    private bool isLoading;

    public ObservableCollection<string> PhotoUrls { get; set; } = [];

    public Property PropertyOnCreating => _propertyService.PropertyOnCreating!;

    public PlacePhotosViewModel(IPropertyService propertyService, IImageService imageService)
    {
        _propertyService = propertyService;
        _imageService = imageService;

        // Attach CollectionChanged event to track changes in PhotoUrls list
        PhotoUrls.CollectionChanged += PhotoUrls_CollectionChanged;

        // Initialize core properties
        TryInitializePhotos();

        // Initialize the IsPhotoListEmpty property
        IsPhotoListEmpty = !PhotoUrls.Any();
    }

    private void PhotoUrls_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        IsPhotoListEmpty = !PhotoUrls.Any();
        ValidateProcess();
    }

    public void TryInitializePhotos()
    {
        // Load existing photo URLs from the Property's ImagePaths
        foreach (var url in PropertyOnCreating.ImagePaths)
        {
            PhotoUrls.Add(url);
        }
    }

    public override void ValidateProcess()
    {
        IsStepCompleted = PhotoUrls.Any();
    }

    public override void SaveProcess()
    {
        // Clear() is called because user can go back to this page and reorder the photos
        PropertyOnCreating.ImagePaths.Clear();

        // Add photos path to the Property's ImagePaths
        foreach (var url in PhotoUrls)
        {
            PropertyOnCreating.ImagePaths.Add(url);
        }
    }

    public async Task AddPhotoRangeAsync(IEnumerable<StorageFile> files)
    {
        IsLoading = true;
        var folderName = PropertyOnCreating.Id.ToString(); // Folder name is the Property's Id

        foreach (var file in files)
        {
            // Upload each file to Cloudinary
            using var stream = await file.OpenStreamForReadAsync();
            var uploadedUrl = await _imageService.UploadImageAsync(stream, file.Name, folderName);

            // Add the uploaded URL to the PhotoUrls collection
            if (!PhotoUrls.Contains(uploadedUrl))
            {
                PhotoUrls.Add(uploadedUrl);
            }
        }

        IsLoading = false;
    }

    public async Task RemovePhotoAsync(string photoUrl)
    {
        IsLoading = true;

        // Extract the public ID from the URL
        var publicId = ExtractPublicIdFromUrl(photoUrl);

        // Delete the photo from Cloudinary
        var success = await _imageService.DeleteImageAsync(publicId);
        if (success)
        {
            // Remove the URL from the PhotoUrls collection
            PhotoUrls.Remove(photoUrl);
        }

        IsLoading = false;
    }

    public async Task RemovePhotoRangeAsync(IEnumerable<string> photoUrls)
    {
        IsLoading = true;

        // Extract the public IDs from the URLs
        var publicIds = photoUrls.Select(ExtractPublicIdFromUrl);

        // Delete the photos from Cloudinary
        var success = await _imageService.DeleteImageRangeAsync(publicIds);
        if (success)
        {
            // Create a copy of the PhotoUrls collection to avoid modifying it while iterating
            var urlsToRemove = photoUrls.ToList();

            // Remove the URLs from the PhotoUrls collection
            foreach (var photoUrl in urlsToRemove)
            {
                PhotoUrls.Remove(photoUrl);
            }
        }

        IsLoading = false;
    }

    public Task RemoveAllPhotosAsync()
    {
        return RemovePhotoRangeAsync(PhotoUrls);
    }

    private static string ExtractPublicIdFromUrl(string url)
    {
        var uri = new Uri(url);
        var segments = uri.AbsolutePath.Split('/');

        if (segments.Length < 3)
        {
            throw new InvalidOperationException("Invalid URL structure.");
        }

        // Extract folder and file name from the URL
        var parentFolder = segments[^3]; // "Arrival"
        var folder = segments[^2];      // Property ID, e.g., "17"
        var fileName = Path.GetFileNameWithoutExtension(segments[^1]); // File name without extension

        return $"{parentFolder}/{folder}/{fileName}"; // Example: "Arrival/17/photo"
    }
}
