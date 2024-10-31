using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels;

public partial class AdministratorViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IDao _dao;
    public AdministratorViewModel(INavigationService navigationService, IDao dao)
    {
        _navigationService = navigationService;
        _dao = dao;
    }

    public void OnNavigatedFrom()
    {

    }

    public void OnNavigatedTo(object parameter)
    {

    }
}
