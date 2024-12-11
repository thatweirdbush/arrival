using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
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
    public ObservableCollection<Property> PriorityProperties { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;

    public ListingRequestViewModel(INavigationService navigationService, IRepository<Property> propertyRepository)
    {
        _navigationService = navigationService;
        _propertyRepository = propertyRepository;
    }

    public void OnNavigatedTo(object parameter)
    {
        /// No need to load Priority data list here because
        /// the ComboBox's default selection will trigger the SelectionChanged event
        /// which will load the data based on the selected value

        // Initial check
        CheckPropertyListCount();

        // Subscribe to CollectionChanged event
        PriorityProperties.CollectionChanged += (s, e) => CheckPropertyListCount();
    }

    public void OnNavigatedFrom()
    {
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

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = PriorityProperties.Count == 0;
    }

    public async void LoadPriorityListDataAsync()
    {
        var data = await _propertyRepository.GetAllAsync();
        data = data.Where(p => p.IsPriority || p.IsFavourite);

        PriorityProperties.Clear();
        foreach (var item in data)
        {
            PriorityProperties.Add(item);
        }
    }

    public async void GetRequestedPropertyListDataAsync()
    {
        var data = await _propertyRepository.GetAllAsync();
        data = data.Where(p => p.IsRequested);

        PriorityProperties.Clear();
        foreach (var item in data)
        {
            PriorityProperties.Add(item);
        }
    }

    public async void GetAllPropertyListDataAsync()
    {
        var data = await _propertyRepository.GetAllAsync();

        PriorityProperties.Clear();
        foreach (var item in data)
        {
            PriorityProperties.Add(item);
        }
    }

    public async void GetElitePropertyListDataAsync()
    {
        var data = await _propertyRepository.GetAllAsync();
        data = data.Where(p => p.IsPriority);

        PriorityProperties.Clear();
        foreach (var item in data)
        {
            PriorityProperties.Add(item);
        }
    }

    public async void GetTrendingPropertyListDataAsync()
    {
        var data = await _propertyRepository.GetAllAsync();
        data = data.Where(p => p.IsFavourite);

        PriorityProperties.Clear();
        foreach (var item in data)
        {
            PriorityProperties.Add(item);
        }
    }
}
