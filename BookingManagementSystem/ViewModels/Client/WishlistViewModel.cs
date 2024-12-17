using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.ViewModels.Account;
using CommunityToolkit.Mvvm.Input;
using BookingManagementSystem.Contracts.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.ViewModels.Client;

public partial class WishlistViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Property> _propertyRepository;

    // List of content items
    public ObservableCollection<Property> Properties { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;

    [ObservableProperty]
    private bool isLoading;

    public int CurrentUserID = LoginViewModel.CurrentUser?.Id ?? 0;
    private int _currentPage = 1;
    private const int PageSize = 5;

    public WishlistViewModel(INavigationService navigationService, IRepository<Property> propertyRepository)
    {
        _navigationService = navigationService;
        _propertyRepository = propertyRepository;
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

    public async Task LoadNextPageAsync()
    {
        if (IsLoading) return;

        try
        {
            // Begin loading
            IsLoading = true;

            // Load next page
            var result = await _propertyRepository.GetPagedFilteredAndSortedAsync(
                queryBuilder: q => q.Include(p => p.Country)
                                    .Where(p => p.Status.Equals(PropertyStatus.Listed) && p.IsFavourite),
                keySelector: p => p.CreatedAt,
                sortDescending: true,
                pageNumber: _currentPage,
                pageSize: PageSize);

            foreach (var property in result.Items)
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

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = Properties.Count == 0;
    }

    public async Task RemoveRangeAsync(IEnumerable<Property> properties)
    {
        foreach (var property in properties)
        {
            property.IsFavourite = false;
            property.UpdatedAt = DateTime.Now.ToUniversalTime();
        }

        await _propertyRepository.UpdateRangeAsync(properties);
        await _propertyRepository.SaveChangesAsync();

        foreach (var property in properties)
        {
            Properties.Remove(property);
        }
    }

    public void RemoveAllWishlistAsync()
    {
        foreach (var property in Properties)
        {
            property.IsFavourite = false;
            property.UpdatedAt = DateTime.Now.ToUniversalTime();
        }

        _propertyRepository.UpdateRangeAsync(Properties);
        _propertyRepository.SaveChangesAsync();

        Properties.Clear();
    }

    public async Task UpdateAsync(Property property)
    {
        await _propertyRepository.UpdateAsync(property);
        await _propertyRepository.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<Property> properties)
    {
        await _propertyRepository.UpdateRangeAsync(properties);
        await _propertyRepository.SaveChangesAsync();
    }

    public async Task RefreshAsync()
    {
        _currentPage = 1;

        Properties.Clear();
        await LoadNextPageAsync();
        CheckPropertyListCount();
    }
}
