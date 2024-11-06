using System.Collections.ObjectModel;
using System.Windows.Input;

using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
