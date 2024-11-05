using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels;

public partial class ListingRequestViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Property> _propertyRepository;

    // List of content items
    public ObservableCollection<Property> PriorityProperties { get; set; } = [];

    public ListingRequestViewModel(INavigationService navigationService, IRepository<Property> propertyRepository)
    {
        _navigationService = navigationService;
        _propertyRepository = propertyRepository;
        GetPriorityPropertyListDataAsync();
    }

    public void OnNavigatedTo(object parameter)
    {
        // Load Priority Property data list
        GetPriorityPropertyListDataAsync();
    }

    public void OnNavigatedFrom()
    {
    }

    public async void GetPriorityPropertyListDataAsync()
    {
        PriorityProperties.Clear();

        var data = await _propertyRepository.GetAllAsync();
        var priorityProperties = data.Where(p => p.IsPriority || p.IsFavourite);

        foreach (var item in priorityProperties)
        {
            PriorityProperties.Add(item);
        }
    }

    public async void GetRequestedPropertyListDataAsync()
    {
        PriorityProperties.Clear();

        var data = await _propertyRepository.GetAllAsync();
        var requestProperties = data.Where(p => !p.IsPriority && !p.IsFavourite);

        foreach (var item in requestProperties)
        {
            PriorityProperties.Add(item);
        }
    }

    public async void GetAllPropertyListDataAsync()
    {
        PriorityProperties.Clear();

        var data = await _propertyRepository.GetAllAsync();

        foreach (var item in data)
        {
            PriorityProperties.Add(item);
        }
    }

    public async void GetElitePropertyListDataAsync()
    {
        PriorityProperties.Clear();

        var data = await _propertyRepository.GetAllAsync();
        var eliteProperties = data.Where(p => p.IsPriority);

        foreach (var item in eliteProperties)
        {
            PriorityProperties.Add(item);
        }
    }

    public async void GetTrendingPropertyListDataAsync()
    {
        PriorityProperties.Clear();

        var data = await _propertyRepository.GetAllAsync();
        var trendingProperties = data.Where(p => p.IsFavourite);

        foreach (var item in trendingProperties)
        {
            PriorityProperties.Add(item);
        }
    }
}
