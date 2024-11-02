using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels;

public partial class ListingRequestViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IDao _dao;

    // List of content items
    public ObservableCollection<Property> PriorityProperties { get; set; } = [];

    public ListingRequestViewModel(INavigationService navigationService, IDao dao)
    {
        _navigationService = navigationService;
        _dao = dao;
        OnNavigatedTo(_dao);
    }

    public async void OnNavigatedTo(object parameter)
    {
        PriorityProperties.Clear();

        // Load Priority Property data list
        var data = await _dao.GetPropertyListDataAsync();
        var priorityProperties = data.Where(p => p.DestinationTypes.Equals(DestinationType.Trending));

        foreach (var item in data)
        {
            PriorityProperties.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
