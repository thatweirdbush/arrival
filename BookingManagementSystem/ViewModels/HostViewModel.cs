using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels;

public partial class HostViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    public HostViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void OnNavigatedFrom()
    {
    
    }

    public void OnNavigatedTo(object parameter)
    {
    
    }
}
