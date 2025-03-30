using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host;

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
