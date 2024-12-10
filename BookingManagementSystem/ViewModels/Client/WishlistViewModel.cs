using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using BookingManagementSystem.ViewModels.Account;
using CommunityToolkit.Mvvm.Input;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Commons.Enums;
using Microsoft.UI.Dispatching;

namespace BookingManagementSystem.ViewModels.Client;

public partial class WishlistViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Property> _propertyRepository;

    // List of content items
    public ObservableCollection<Property> Properties { get; set; } = [];
    public List<Property> CachedProperties { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private LoadingState currentLoadingState;

    public int CurrentPage => _currentPage;
    public int CurrentUserID = LoginViewModel.CurrentUser?.Id ?? 0;
    private int _currentPage = 1;
    private const int PageSize = 5; // Default page size

    public WishlistViewModel(INavigationService navigationService, IRepository<Property> propertyRepository)
    {
        _navigationService = navigationService;
        _propertyRepository = propertyRepository;
    }

    public async void OnNavigatedTo(object parameter)
    {
        await InitializeCacheAsync();
        await LoadPropertyListAsync();

        // Initial check
        CheckPropertyListCount();

        // Subscribe to CollectionChanged event
        Properties.CollectionChanged += (s, e) =>
        {
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
    }

    public void OnNavigatedFrom()
    {
        // Unsubscribe from CollectionChanged event
        Properties.CollectionChanged -= (s, e) =>
        {
            if (e.NewItems != null)
            {
                foreach (Property property in e.NewItems)
                {
                    property.PropertyChanged -= Property_PropertyChanged;
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
    }
    public async Task InitializeCacheAsync()
    {
        // Load all properties from the database
        var data = await _propertyRepository.GetAllAsync();
        CachedProperties = data.Where(p => p.Status.Equals(PropertyStatus.Listed) && p.IsFavourite).ToList();
    }

    public async Task LoadPropertyListAsync()
    {
        // Avoid calling multiple times at the same time
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            var paginatedProperties = CachedProperties
                .Skip((_currentPage - 1) * PageSize) // Skip those already loaded
                .Take(PageSize); // Get next items

            // Add the new items to the list
            DispatcherQueue.GetForCurrentThread().TryEnqueue(() =>
            {
                foreach (var property in paginatedProperties)
                {
                    Properties.Add(property);
                }
            });

            _currentPage++; // Increment the current page
        }
        finally
        {
            IsLoading = false;
            await Task.CompletedTask;
        }
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

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = Properties.Count == 0;
    }

    public void RemoveWishlistAsync(Property property)
    {
        property.IsFavourite = false;
        property.UpdatedAt = DateTime.Now.ToUniversalTime();
        _propertyRepository.UpdateAsync(property);

        // Update cache and UI
        CachedProperties.Remove(property);
        Properties.Remove(property);
    }

    public void RemoveAllWishlistAsync()
    {
        foreach (var property in Properties)
        {
            property.IsFavourite = false;
            property.UpdatedAt = DateTime.Now.ToUniversalTime();
            _propertyRepository.UpdateAsync(property);
        }
        _propertyRepository.SaveChangesAsync();

        // Update cache and UI
        CachedProperties.Clear();
        Properties.Clear();
    }

    public Task SaveChangesAsync()
    {
        return _propertyRepository.SaveChangesAsync();
    }

    public async Task RefreshPropertiesAsync()
    {
        CurrentLoadingState = LoadingState.Default;
        _currentPage = 1;
        await InitializeCacheAsync();
        Properties.Clear();
        await LoadPropertyListAsync();
    }
}
