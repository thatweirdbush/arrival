using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using BookingManagementSystem.ViewModels.Account;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels.Client;

public partial class WishlistViewModel : ObservableRecipient 
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Property> _propertyRepository;

    // List of content items
    public ObservableCollection<Property> Properties { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;
    public int CurrentUserID = LoginViewModel.CurrentUser?.Id ?? 0;

    public WishlistViewModel(INavigationService navigationService, IRepository<Property> propertyRepository)
    {
        _navigationService = navigationService;
        _propertyRepository = propertyRepository;
        CheckPropertyListCount();

        // Subscribe to CollectionChanged event
        Properties.CollectionChanged += (s, e) => {
            if (e.NewItems != null)
            {
                foreach (Property property in e.NewItems)
                {
                    property.PropertyChanged += Property_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (Property property in e.OldItems)
                {
                    property.PropertyChanged -= Property_PropertyChanged;
                }
            }
            CheckPropertyListCount();
        };

        // Initial check
        LoadPropertyList();
    }

    [RelayCommand]
    public void OnItemClick(Property? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(RentalDetailViewModel).FullName!, clickedItem.Id);
        }
    }

    private void Property_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Property.IsFavourite) && sender is Property property)
        {
            if (!property.IsFavourite)
            {
                Properties.Remove(property);
            }
        }
    }

    public async void LoadPropertyList()
    {
        // Load Property data list filtered by User/Host Id
        var properties = await _propertyRepository.GetAllAsync();
        foreach (var item in properties.Where(p => p.IsFavourite))
        {
            Properties.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = Properties.Count == 0;
    }

    public void RemoveWishlistAsync(Property property)
    {
        _propertyRepository.DeleteAsync(property.Id);
        Properties.Remove(property);
    }

    public void RemoveAllWishlistAsync()
    {
        foreach (var property in Properties)
        {
            _propertyRepository.DeleteAsync(property.Id);
        }
        Properties.Clear();
    }
}
