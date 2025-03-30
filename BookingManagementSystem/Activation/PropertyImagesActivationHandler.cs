using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using Windows.Storage;

namespace BookingManagementSystem.Activation;

public class PropertyImagesActivationHandler
{
    public static async Task CopyPropertyImagesToLocalFolderAsync()
    {
        // Check if images have been copied before
        var localSettings = ApplicationData.Current.LocalSettings;
        if (localSettings.Values.TryGetValue("PropertyImages", out var value) && (bool)value)
        {
            return;
        }

        // Access the Assets/property-images folder
        var installationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        var assetsFolder = await installationFolder.GetFolderAsync("Assets");
        var imagesFolder = await assetsFolder.GetFolderAsync("property-images");

        // Create a folder in LocalFolder to store the images
        var localFolder = ApplicationData.Current.LocalFolder;
        var destinationFolder = await localFolder.CreateFolderAsync("property-images", CreationCollisionOption.OpenIfExists);

        // Copy all files and subfolders recursively
        await CopyFolderContentsAsync(imagesFolder, destinationFolder);

        // Mark this operation as completed in local settings
        localSettings.Values["PropertyImages"] = true;
    }

    private static async Task CopyFolderContentsAsync(StorageFolder sourceFolder, StorageFolder destinationFolder)
    {
        // Copy all files in the current folder
        var files = await sourceFolder.GetFilesAsync();
        foreach (var file in files)
        {
            await file.CopyAsync(destinationFolder, file.Name, NameCollisionOption.ReplaceExisting);
        }

        // Recursively copy all subfolders
        var subfolders = await sourceFolder.GetFoldersAsync();
        foreach (var subfolder in subfolders)
        {
            var newDestinationSubfolder = await destinationFolder.CreateFolderAsync(subfolder.Name, CreationCollisionOption.OpenIfExists);
            await CopyFolderContentsAsync(subfolder, newDestinationSubfolder);
        }
    }


    public static string GetLocalImageFolderPath() => ApplicationData.Current.LocalFolder.Path + @"\property-images\";

    public static async Task BindingPropertyImagesWithLocalFolderAsync()
    {
        // Access the LocalFolder/property-images folder
        var _propertyRepository = App.GetService<IRepository<Property>>();

        // Loop through all properties and bind the images
        var properties = await _propertyRepository.GetAllAsync();
        var tasks = properties.Select(async property =>
        {
            var imagePaths = property.ImagePaths.ToList(); // Convert ICollection to List
            for (var i = 0; i < imagePaths.Count; i++)
            {
                imagePaths[i] = GetLocalImageFolderPath() + imagePaths[i];
            }
            property.ImagePaths = imagePaths; // Update the property with the modified list
            await Task.CompletedTask;
        });

        await Task.WhenAll(tasks);
    }
}
