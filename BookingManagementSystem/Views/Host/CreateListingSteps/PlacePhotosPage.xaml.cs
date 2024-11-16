using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PlacePhotosPage : Page
{
    public PlacePhotosViewModel? ViewModel
    {
        get; set;
    }

    public PlacePhotosPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is PlacePhotosViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        base.OnNavigatedTo(e);
    }

    private async void AddPhotosButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Create a new instance of the FileOpenPicker
        var openPicker = new FileOpenPicker
        {
            SuggestedStartLocation = PickerLocationId.Downloads,
            ViewMode = PickerViewMode.Thumbnail
        };
        // Filter to include a sample subset of file types
        openPicker.FileTypeFilter.Add(".png");
        openPicker.FileTypeFilter.Add(".jpg");
        openPicker.FileTypeFilter.Add(".jpeg");

        // Get the current window
        var window = App.MainWindow;

        // Retrieve the window handle (HWND) of the current WinUI 3 window.
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

        // Initialize the file picker with the window handle (HWND).
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

        // Open the picker for the user to pick a file
        var files = await openPicker.PickMultipleFilesAsync();
        if (files.Count > 0)
        {
            // Get the app's LocalFolder
            var localFolder = ApplicationData.Current.LocalFolder;
            foreach (var file in files)
            {
                // Copy file to LocalFolder folder
                var copiedFile = await file.CopyAsync(localFolder, file.Name, NameCollisionOption.ReplaceExisting);
                ViewModel?.Photos.Add(copiedFile.Path);
                PhotosListView.Items.Add(new BitmapImage(new Uri(copiedFile.Path)));
            }
        }
    }
}
