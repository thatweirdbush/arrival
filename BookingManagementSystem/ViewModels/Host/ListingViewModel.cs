using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Commons.Enums;

namespace BookingManagementSystem.ViewModels.Host;

public partial class ListingViewModel : ObservableRecipient, INavigationAware
{
    private readonly IRepository<Property> _propertyRepository;
    public ObservableCollection<Property> Properties { get; set; } = [];
    public List<Property> CachedProperties { get; set; } = [];

    // List of Property's Name & Location for searching
    public List<string> PropertyNameAndLocationList { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private LoadingState currentLoadingState;

    private int _currentPage = 1;
    private const int PageSize = 5;

    public ListingViewModel(IRepository<Property> propertyRepository)
    {
        _propertyRepository = propertyRepository;

        // Set the default loading state
        CurrentLoadingState = LoadingState.Default;
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Initialize data list with pagination & search data
        await LoadNextPageAsync();
        InitializeSearchDataAsync();

        // Initial check
        CheckListCount();
    }

    public void OnNavigatedFrom()
    {
    }

    public async void InitializeSearchDataAsync()
    {
        // Load Property Name and Location string data list
        var data = await _propertyRepository.GetAllAsync();
        CachedProperties = data.ToList();
        PropertyNameAndLocationList = CachedProperties.Select(p => p.Name)
                                                        .Concat(CachedProperties.Select(p => p.Location))
                                                        .ToList();
    }

    public async Task LoadNextPageAsync()
    {
        if (IsLoading) return;

        try
        {
            // Begin loading
            IsLoading = true;

            // Load next page, including Listed, Unlisted, and InProgress properties
            var pagedItems = await _propertyRepository.GetPagedSortedAsync(
                p => p.UpdatedAt,
                sortDescending: true,
                _currentPage,
                PageSize);

            foreach (var property in pagedItems)
            {
                Properties.Add(property);
            }

            _currentPage++;
        }
        finally
        {
            // End loading
            IsLoading = false;
        }
    }

    public async Task RemoveAsync(Property property)
    {
        await _propertyRepository.DeleteAsync(property.Id);
        await _propertyRepository.SaveChangesAsync();

        Properties.Remove(property);
        CheckListCount();
    }

    public async Task RemoveRangeAsync(IEnumerable<Property> properties)
    {
        await _propertyRepository.DeleteRangeAsync(properties.Select(p => p.Id));
        await _propertyRepository.SaveChangesAsync();

        foreach (var property in properties)
        {
            Properties.Remove(property);
        }
        CheckListCount();
    }

    public async Task RemoveAllAsync()
    {
        // No need to call SaveChangesAsync() here because it's a raw SQL query execution
        await _propertyRepository.DeleteAllAsync();

        Properties.Clear();
        CheckListCount();
    }

    private void CheckListCount()
    {
        IsPropertyListEmpty = Properties.Count == 0;
    }

    public void Search(string query)
    {
        // Set the current loading state
        CurrentLoadingState = LoadingState.Search;

        // Start loading the search data
        LoadSearchedData(query);
        CheckListCount();
    }

    public void LoadSearchedData(string query)
    {
        try
        {
            // Begin loading
            IsLoading = true;

            // Load next page, including Listed, Unlisted, and InProgress properties
            var pagedItems = CachedProperties.Where(p => p.Name.Equals(query) || p.Location.Equals(query));

            foreach (var property in pagedItems)
            {
                Properties.Add(property);
            }
        }
        finally
        {
            // End loading
            IsLoading = false;
        }
    }

    public async Task<int> GetSingleSearchedItem(string query)
    {
        var data = await _propertyRepository.GetPagedFilteredAndSortedAsync(
            p => p.Name.Equals(query) || p.Location.Equals(query),
            p => p.UpdatedAt,
            sortDescending: true,
            1,
            1);
        return data.FirstOrDefault()!.Id;
    }

    public void ResetPaginationIndex()
    {
        _currentPage = 1;
    }

    public async Task RefreshAsync()
    {
        CurrentLoadingState = LoadingState.Default;

        ResetPaginationIndex();
        Properties.Clear();

        await LoadNextPageAsync();
        CheckListCount();
        InitializeSearchDataAsync();
    }
}
