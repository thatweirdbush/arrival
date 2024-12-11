using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Administrator;

public partial class ReportViewModel : ObservableRecipient, INavigationAware
{
    private readonly IRepository<BadReport> _badReportRepository;

    // List of content items
    public ObservableCollection<BadReport> BadReports { get; set; } = [];

    public ReportViewModel(IRepository<BadReport> badReportRepository)
    {
        _badReportRepository = badReportRepository;
    }
    public async void OnNavigatedTo(object parameter)
    {
        // Load BadReport data list
        var reports = await _badReportRepository.GetAllAsync();
        foreach (var report in reports)
        {
            BadReports.Add(report);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
