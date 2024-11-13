using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlacePhotosViewModel : ObservableRecipient
{
    public ObservableCollection<StorageFile> Photos
    {
        get; set;
    } = [];

    [ObservableProperty]
    private bool isPhotoListEmpty;

    public PlacePhotosViewModel()
    {
        // Attach CollectionChanged event to track changes in Photos list
        Photos.CollectionChanged += Photos_CollectionChanged;

        // Initialize the IsPhotoListEmpty property
        IsPhotoListEmpty = Photos.Count == 0;
    }

    private void Photos_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        // Update IsPhotoListEmpty every time the list changes
        IsPhotoListEmpty = Photos.Count == 0;
    }
}
