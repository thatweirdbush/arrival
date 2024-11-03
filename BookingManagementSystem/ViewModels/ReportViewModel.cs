using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels;

public partial class ReportViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IDao _dao;

    // List of content items
    public IEnumerable<BadReport> BadReports { get; set; } = Enumerable.Empty<BadReport>();

    public ReportViewModel(INavigationService navigationService, IDao dao)
    {
        _navigationService = navigationService;
        _dao = dao;
        OnNavigatedTo(_dao);
    }
    public async void OnNavigatedTo(object parameter)
    {
        // Load BadReport data list
        var reports = await _dao.GetBadReportListDataAsync();
        BadReports = reports;
    }

    public void OnNavigatedFrom()
    {
    }
}
