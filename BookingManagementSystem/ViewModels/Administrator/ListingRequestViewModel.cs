using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Commons.Enums;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Client;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.ViewModels.Administrator;

public partial class ListingRequestViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Property> _propertyRepository;

    // List of content items
    public IEnumerable<FilterType> FilterTypes { get; set; }
    public ObservableCollection<Property> PriorityProperties { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private FilterType selectedFilter;
    public int CurrentPage => _currentPage;
    private int _currentPage = 1;
    private const int PageSize = 5; // Default page size

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
        // Initialize data list with pagination
        await LoadNextPageAsync();

        // Initial check
        CheckPropertyListCount();
    }

    public void OnNavigatedFrom()
    {
    }

    partial void OnSelectedFilterChanged(FilterType oldValue, FilterType newValue)
    {
        if (oldValue == newValue) return;
        _ = RefreshAsync();
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
            var result = await _propertyRepository.GetPagedFilteredAndSortedAsync(
                queryBuilder: GetCurrentFilterExpression(),
                keySelector: p => p.CreatedAt,
                sortDescending: true,
                pageNumber: _currentPage,
                pageSize: PageSize);

            foreach (var property in result.Items)
            {
                PriorityProperties.Add(property);
            }

            _currentPage++;
        }
        finally
        {
            // End loading
            IsLoading = false;
        }
    }

    // Get the current filter expression
    private Func<IQueryable<Property>, IQueryable<Property>> GetCurrentFilterExpression()
    {
        return query => query
            .Include(p => p.Country) // Include Country Info
            .Where(SelectedFilter switch
            {
                FilterType.Current => p => p.Status == PropertyStatus.Listed,
                FilterType.Elites => p => p.Status == PropertyStatus.Listed && p.IsPriority,
                FilterType.Trendings => p => p.Status == PropertyStatus.Listed && p.IsFavourite,
                FilterType.Requests => p => p.Status == PropertyStatus.Listed && p.IsRequested,
                _ => p => p.Status == PropertyStatus.Listed
            });
    }

    // Refresh the data list from database
    public async Task RefreshAsync()
    {
        _currentPage = 1;
        PriorityProperties.Clear();
        await LoadNextPageAsync();
        CheckPropertyListCount();
    }

    public async Task UpdateRangeAsync(IEnumerable<Property> properties)
    {
        await _propertyRepository.UpdateRangeAsync(properties);
        await _propertyRepository.SaveChangesAsync();
    }

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = !PriorityProperties.Any();
    }
}
