using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using BookingManagementSystem.Core.Commons.Enums;
using Microsoft.EntityFrameworkCore;
using BookingManagementSystem.ViewModels.Account;
using System.Windows.Input;
using BookingManagementSystem.Contracts.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace BookingManagementSystem.ViewModels.Host;

public partial class ListingViewModel : ObservableRecipient, INavigationAware
{
    private readonly IRepository<Property> _propertyRepository;
    private readonly INavigationService _navigationService;
    public ObservableCollection<Property> Properties { get; set; } = new();
    public List<Property> CachedProperties { get; set; } = new();

    // List of Property's Name & Location for searching
    public List<string> PropertyNameAndLocationList { get; set; } = new();

    [ObservableProperty]
    private bool isPropertyListEmpty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private LoadingState currentLoadingState;

    [ObservableProperty]
    private bool isUserLoggedIn;

    private int CurrentUserId;

    private int _currentPage = 1;
    private const int PageSize = 5;

    public ICommand GetStartedCommand { get; }

    public ListingViewModel(IRepository<Property> propertyRepository, INavigationService navigationService)
    {
        _propertyRepository = propertyRepository;
        _navigationService = navigationService;
        GetStartedCommand = new RelayCommand(GetStarted);
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (LoginViewModel.CurrentUser == null)
        {
            CheckListCount();
            CheckUserLoggedIn();
            return;
        }

        // Get the current user id
        CurrentUserId = LoginViewModel.CurrentUser.Id;

        // Set the default loading state
        CurrentLoadingState = LoadingState.Default;

        // Initialize data list with pagination & search data
        await LoadNextPageAsync();
        await InitializeSearchDataAsync();

        // Initial check
        CheckListCount();
        CheckUserLoggedIn();
    }

    public void OnNavigatedFrom()
    {
    }

    public async Task InitializeSearchDataAsync()
    {
        // Load Property Name and Location string data list
        var data = await _propertyRepository.GetAllAsync(p => p.HostId == CurrentUserId);
        CachedProperties = data.ToList();
        PropertyNameAndLocationList = CachedProperties.Select(p => p.Name)
                                                        .Concat(CachedProperties.Select(p => p.Location))
                                                        .ToList();
    }

    public async Task LoadNextPageAsync()
    {
        if (LoginViewModel.CurrentUser == null) return;

        if (IsLoading) return;

        try
        {
            // Begin loading
            IsLoading = true;

            // Load next page, including Listed, Unlisted, and InProgress properties
            var result = await _propertyRepository.GetPagedFilteredAndSortedAsync(
                queryBuilder: q => q.Include(p => p.Country) // Also include with one that has no country yet
                                    .Where(p => p.CountryId == null || p.Country != null)
                                    .Where(p => p.HostId == CurrentUserId),
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
        if (LoginViewModel.CurrentUser == null) return;
        if (Properties.Count == 0) return;

        // Show confirmation dialog
        var result = await new ContentDialog
        {
            XamlRoot = App.MainWindow.Content.XamlRoot,
            Title = "Remove all items?",
            Content = "Once you remove all, you can't get them back.",
            PrimaryButtonText = "Remove all",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        }.ShowAsync();

        // If clicked the Remove all button
        if (result == ContentDialogResult.Primary)
        {
            // No need to call SaveChangesAsync() here because it's a raw SQL query execution
            await _propertyRepository.DeleteAllAsync(LoginViewModel.CurrentUser!.Id);

            Properties.Clear();
            CheckListCount();
        }
    }

    private void CheckListCount()
    {
        IsPropertyListEmpty = Properties.Count == 0;
    }

    private void CheckUserLoggedIn()
    {
        IsUserLoggedIn = LoginViewModel.CurrentUser != null;
    }

    public void Search(string query)
    {
        if (LoginViewModel.CurrentUser == null) return;

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

    public async Task<int> GetSingleSearchedItemId(string query)
    {
        var result = await _propertyRepository.GetPagedFilteredAndSortedAsync(
            queryBuilder: q => q.Where(p => p.Name.Equals(query) || p.Location.Equals(query)),
            keySelector: p => p.CreatedAt,
            sortDescending: true,
            pageNumber: 1,
            pageSize: 1);
        return result.Items.FirstOrDefault()!.Id;
    }

    public void ResetPaginationIndex()
    {
        _currentPage = 1;
    }

    public async Task RefreshAsync()
    {
        if (LoginViewModel.CurrentUser == null) return;

        CurrentLoadingState = LoadingState.Default;

        ResetPaginationIndex();
        Properties.Clear();

        await LoadNextPageAsync();
        await InitializeSearchDataAsync();
        CheckListCount();
    }

    public void GetStarted()
    {
        if (LoginViewModel.CurrentUser == null)
        {
            // Navigate to Login Page
            _navigationService.NavigateTo(typeof(LoginViewModel).FullName!);
        }
        else
        {
            // Navigate to Create Listing Page
            _navigationService.NavigateTo(typeof(CreateListingViewModel).FullName!);
        }
    }
}
