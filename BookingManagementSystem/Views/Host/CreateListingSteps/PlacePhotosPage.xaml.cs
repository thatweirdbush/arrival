using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Windows.Storage.Pickers;
using Windows.Storage;
using Microsoft.UI.Xaml.Navigation;
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
                ViewModel?.Photos.Add(copiedFile);
            }
        }
    }

    private void EditPhotos_Click(object sender, RoutedEventArgs e)
    {
        PhotosListView.SelectionMode = ListViewSelectionMode.Multiple;
        btnEdit.Visibility = Visibility.Collapsed;
        btnCancel.Visibility = Visibility.Visible;
        btnRemove.Visibility = Visibility.Visible;
    }

    private void CancelEditing_Click(object sender, RoutedEventArgs e)
    {
        PhotosListView.SelectionMode = ListViewSelectionMode.Single;
        btnCancel.Visibility = Visibility.Collapsed;
        btnRemove.Visibility = Visibility.Collapsed;
        btnEdit.Visibility = Visibility.Visible;
    }

    private async void RemovePhotos_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items and remove them from the list
        var selectedItems = PhotosListView.SelectedItems.ToList();
        if (selectedItems.Count == 0)
        {
            return;
        }
        // Show confirmation dialog
        var confirm = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove this photo?",
            Content = "Once you remove it, you can't get it back.",
            PrimaryButtonText = "Remove it",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        };
        var result = await confirm.ShowAsync();

        // Check if the user clicked the remove button
        if (result == ContentDialogResult.Primary)
        {
            // Remove the selected items from the list
            foreach (var item in selectedItems)
            {
                ViewModel?.Photos.Remove((StorageFile)item);
            }
        }
    }

    private async void RemoveAllPhotos_Click(object sender, RoutedEventArgs e)
    {
        // Show confirmation dialog
        var confirm = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove all photos?",
            Content = "Once you remove all, you can't get them back.",
            PrimaryButtonText = "Remove all",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        };

        var result = await confirm.ShowAsync();

        // Check if the user clicked the remove button
        if (result == ContentDialogResult.Primary)
        {
            ViewModel?.Photos.Clear();
        }
    }

    private void OnCommandBarElementClicked(object sender, RoutedEventArgs e)
    {
        var element = (sender as AppBarButton)!.Label;
        switch (element)
        {
            case "Add":
                AddPhotosButton_Click(sender, e);
                break;
            case "Edit":
                EditPhotos_Click(sender, e);
                break;
            case "Cancel":
                CancelEditing_Click(sender, e);
                break;
            case "Remove":
                RemovePhotos_Click(sender, e);
                break;
            case "Remove all":
                RemoveAllPhotos_Click(sender, e);
                break;
        }
    }

    private void PhotosListView_DragItemsCompleted(ListViewBase sender, DragItemsCompletedEventArgs args)
    {
        // Get the reordered photos
        var reorderedPhotos = PhotosListView.Items.Cast<StorageFile>().ToList();

        // Update the ViewModel's Photos list
        ViewModel!.Photos.Clear();
        foreach (var photo in reorderedPhotos)
        {
            ViewModel.Photos.Add(photo);
        }
    }
}
