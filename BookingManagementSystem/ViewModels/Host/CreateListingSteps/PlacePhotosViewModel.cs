using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlacePhotosViewModel : BaseStepViewModel
{
    public ObservableCollection<string> Photos
    {
        get; set;
    } = [];

    [ObservableProperty]
    private bool isPhotoListEmpty;
    private readonly IPropertyService _propertyService;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;

    public PlacePhotosViewModel(IPropertyService propertyService)
    {
        _propertyService = propertyService;

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

    public override void ValidateStep()
    {
        if (Photos.Count > 0)
        {
            // Add photos path to the Property instance
            foreach (var photo in Photos)
            {
                PropertyOnCreating.ImagePaths.Add(photo);
            }
            IsStepCompleted = true;
        }
    }
}
