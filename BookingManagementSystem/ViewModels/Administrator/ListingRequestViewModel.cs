using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Commons.Enums;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Client;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels.Administrator;

public partial class ListingRequestViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Property> _propertyRepository;

    // List of content items
    public IEnumerable<FilterType> FilterTypes
    {
        get; set;
    }
    public ObservableCollection<Property> PriorityProperties { get; set; } = [];
    public List<Property> CachedProperties { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private FilterType selectedFilter;
    public int CurrentPage => _currentPage;
    private int _currentPage = 1;
    private const int PageSize = 5; // Default page size

    public enum FilterType
    {
        Current,
        Elites,
        Trendings,
        Requests
    }

    public ListingRequestViewModel(INavigationService navigationService, IRepository<Property> propertyRepository)
    {
        _navigationService = navigationService;
        _propertyRepository = propertyRepository;
        FilterTypes = Enum.GetValues(typeof(FilterType)).Cast<FilterType>();
        SelectedFilter = FilterType.Current;
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

    public async void OnNavigatedTo(object parameter)
    {
        await InitializeCacheAsync();
        await LoadNextPageAsync();

        // Initial check
        CheckPropertyListCount();
    }

    public void OnNavigatedFrom()
    {
    }

    partial void OnSelectedFilterChanged(FilterType value)
    {
        _ = ResetPagination();
    }

    public async Task InitializeCacheAsync()
    {
        // Begin loading
        IsLoading = true;

        // Load all properties from the database
        var data = await _propertyRepository.GetAllAsync();
        CachedProperties = data.Where(p => p.Status.Equals(PropertyStatus.Listed)).ToList();

        // End loading
        IsLoading = false;
    }

    // Implement Incremental Loading
    public async Task LoadNextPageAsync()
    {
        if (IsLoading) return;

        try
        {
            // Begin loading
            IsLoading = true;

            // Load next page based on current filter
            var filteredProperties = ApplyCurrentFilter();
            var pagedProperties = GetPagedProperties(filteredProperties, _currentPage, PageSize);

            foreach (var property in pagedProperties)
            {
                PriorityProperties.Add(property);
            }

            _currentPage++;
        }
        finally
        {
            // End loading
            IsLoading = false;
            await Task.CompletedTask;
        }
    }

    private IEnumerable<Property> ApplyCurrentFilter()
    {
        return SelectedFilter switch
        {
            FilterType.Current => CachedProperties,
            FilterType.Elites => CachedProperties.Where(p => p.IsPriority),
            FilterType.Trendings => CachedProperties.Where(p => p.IsFavourite),
            FilterType.Requests => CachedProperties.Where(p => p.IsRequested),
            _ => CachedProperties
        };
    }

    // General method for paginating data
    private IEnumerable<Property> GetPagedProperties(IEnumerable<Property> source, int page, int pageSize)
    {
        return source.Skip((page - 1) * pageSize).Take(pageSize);
    }

    // Reload the data list from cache
    public async Task ResetPagination()
    {
        _currentPage = 1;
        PriorityProperties.Clear();
        await LoadNextPageAsync();
        CheckPropertyListCount();
    }

    // Reload the cache & data list from the database
    public async Task RefreshAsync()
    {
        PriorityProperties.Clear();
        await InitializeCacheAsync();
        await ResetPagination();
    }

    public Task UpdateAsync(Property property)
    {
        return _propertyRepository.UpdateAsync(property);
    }

    public Task SaveChangesAsync()
    {
        return _propertyRepository.SaveChangesAsync();
    }

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = PriorityProperties.Count == 0;
    }
}
