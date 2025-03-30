using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Client;

public partial class MapViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    public MapViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void OnNavigatedTo(object parameter)
    {
    }

    public void OnNavigatedFrom()
    {
    }
}
