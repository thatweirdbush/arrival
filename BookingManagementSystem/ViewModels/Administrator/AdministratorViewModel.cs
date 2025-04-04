﻿using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Administrator;

public partial class AdministratorViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    public AdministratorViewModel(INavigationService navigationService)
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
