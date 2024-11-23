using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using BookingManagementSystem.ViewModels.Account;

namespace BookingManagementSystem.ViewModels.Client;

public partial class WishlistViewModel : ObservableRecipient 
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Property> _propertyRepository;

    // List of content items
    public ObservableCollection<Property> Properties { get; set; } = [];

    public int CurrentUserID = LoginViewModel.CurrentUser.Id;

    [ObservableProperty]
    private bool isPropertyListEmpty;

    public int PropertyCountTotal;

    // List of Property's Name & Location
    public List<string> PropertyNameAndLocationList
    {
        get;
        set;
    } = [];

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

        // Load Property Name and Location string data list
        PropertyNameAndLocationList = Properties.Select(p => p.Name)
                                                .Concat(Properties.Select(p => p.Location))
                                                .ToList();
        PropertyCountTotal = Properties.Count;
    }

    private void Property_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

    public void RemoveWishAsync(Property property)
    {
        _propertyRepository.DeleteAsync(property.Id);
        Properties.Remove(property);
    }

    public void RemoveAllWishAsync()
    {
        foreach (var property in Properties)
        {
            _propertyRepository.DeleteAsync(property.Id);
        }
        Properties.Clear();
    }

}
