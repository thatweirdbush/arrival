using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Windows.Storage.Pickers;
using Microsoft.UI.Xaml.Controls;
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
            await ViewModel!.AddPhotoRangeAsync(files);
        }
    }

    private async Task RemovePhotos_Click(object sender, RoutedEventArgs e)
    {
        // Get selected items and remove them from the list
        var selectedItems = PhotosListView.SelectedItems.Cast<string>().ToList();

        if (selectedItems.Count == 0) return;

        // Show confirmation dialog
        var result = await new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "Remove selected photo(s)?",
            Content = "Once you remove, you can't get them back.",
            PrimaryButtonText = "Remove",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        }.ShowAsync();

        // Check if the user clicked the remove button
        if (result == ContentDialogResult.Primary)
        {
            await ViewModel!.RemovePhotoRangeAsync(selectedItems);
        }
    }

    private async Task RemoveAllPhotos_Click(object sender, RoutedEventArgs e)
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
            await ViewModel!.RemoveAllPhotosAsync();
        }
    }

    private async void OnCommandBarElementClicked(object sender, RoutedEventArgs e)
    {
        var element = (sender as AppBarButton)!.Tag;
        switch (element)
        {
            case "edit":
                EditPhotos_Click(sender, e);
                break;
            case "cancel":
                CancelEditing_Click(sender, e);
                break;
            case "add":
                AddPhotosButton_Click(sender, e);
                break;
            case "remove":
                await RemovePhotos_Click(sender, e);
                break;
            case "remove-all":
                await RemoveAllPhotos_Click(sender, e);
                break;
        }
    }

    private void PhotosListView_DragItemsCompleted(ListViewBase sender, DragItemsCompletedEventArgs args)
    {
        // Get the reordered photos
        var reorderedPhotos = PhotosListView.Items.Cast<string>().ToList();

        // Update the ViewModel's Photos list
        ViewModel!.PhotoUrls.Clear();

        foreach (var photo in reorderedPhotos)
        {
            ViewModel.PhotoUrls.Add(photo);
        }
    }
}
