using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using Microsoft.UI.Xaml.Navigation;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.ViewModels;
using Microsoft.UI.Xaml;

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

    private async void AddPhotosButton_Click(object sender, RoutedEventArgs e)
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
                ViewModel?.Photos.Insert(0, copiedFile);
            }
        }
    }

    private void btnSelect_Click(object sender, RoutedEventArgs e)
    {
        PhotosListView.SelectionMode = ListViewSelectionMode.Multiple;
        btnSelect.Visibility = Visibility.Collapsed;
        btnCancel.Visibility = Visibility.Visible;
        btnDelete.Visibility = Visibility.Visible;
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        PhotosListView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Visibility.Collapsed;
        btnDelete.Visibility = Visibility.Collapsed;
        btnSelect.Visibility = Visibility.Visible;
    }

    private async void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items and remove them from the list
        var selectedItems = PhotosListView.SelectedItems.ToList();

        // Check if there are selected items
        if (selectedItems.Count == 0)
        {
            return;
        }

        // Show confirmation dialog
        var confirm = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Delete this photo?",
            Content = "Once you delete it, you can't get it back.",
            PrimaryButtonText = "Delete it",
            CloseButtonText = "Cancel"
        };

        var result = await confirm.ShowAsync();

        // Check if the user clicked the delete button
        if (result != ContentDialogResult.Primary)
        {
            return;
        }

        // Remove the selected items from the list
        foreach (var item in selectedItems)
        {
            ViewModel?.Photos.Remove((StorageFile)item);
        }
    }

    private void btnAddNewListing_Click(object sender, RoutedEventArgs e)
    {
        AddPhotosButton_Click(sender, e);
    }
}
