﻿using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Administrator;

public partial class ReportViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<BadReport> _badReportRepository;

    // List of content items
    public IEnumerable<BadReport> BadReports { get; set; } = Enumerable.Empty<BadReport>();

    public ReportViewModel(INavigationService navigationService, IRepository<BadReport> badReportRepository)
    {
        _navigationService = navigationService;
        _badReportRepository = badReportRepository;
        OnNavigatedTo(0);
    }
    public async void OnNavigatedTo(object parameter)
    {
        // Load BadReport data list
        var reports = await _badReportRepository.GetAllAsync();
        BadReports = reports;
    }

    public void OnNavigatedFrom()
    {
    }
}
