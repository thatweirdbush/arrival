﻿using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlacePhotosViewModel : BaseStepViewModel
{
    public ObservableCollection<StorageFile> Photos
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

        // Initialize core properties
        TryInitializePhotos();

        // Initialize the IsPhotoListEmpty property
        IsPhotoListEmpty = Photos.Count == 0;
    }

    private void Photos_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        // Update IsPhotoListEmpty every time the list changes
        IsPhotoListEmpty = Photos.Count == 0;
        ValidateProcess();
    }

    public async void TryInitializePhotos()
    {
        // Load photos from Property's ImagePaths
        foreach (var path in PropertyOnCreating.ImagePaths)
        {
            var photo = await StorageFile.GetFileFromPathAsync(path);
            Photos.Add(photo);
        }
    }

    public override void ValidateProcess()
    {
        IsStepCompleted = Photos.Count > 0;
    }

    public override void SaveProcess()
    {
        // Clear() is called because user can go back to this page and reorder the photos
        PropertyOnCreating.ImagePaths.Clear();

        // Add photos path to the Property's ImagePaths
        foreach (var photo in Photos)
        {
            PropertyOnCreating.ImagePaths.Add(photo.Path);
        }
    }
}
