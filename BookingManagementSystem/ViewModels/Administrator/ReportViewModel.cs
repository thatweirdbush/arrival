using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Administrator;

public partial class ReportViewModel : ObservableRecipient, INavigationAware
{
    private readonly IRepository<BadReport> _badReportRepository;
    public ObservableCollection<BadReport> BadReports { get; set; } = [];

    [ObservableProperty]
    private bool isListEmpty;

    [ObservableProperty]
    private bool isLoading;

    private int _currentPage = 1;
    private const int PageSize = 5;

    public ReportViewModel(IRepository<BadReport> badReportRepository)
    {
        _badReportRepository = badReportRepository;
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Initialize data list with pagination
        await LoadNextPageAsync();

        // Initial check
        CheckListCount();
    }

    public void OnNavigatedFrom()
    {
    }

    public async Task LoadNextPageAsync()
    {
        if (IsLoading) return;

        try
        {
            // Begin loading
            IsLoading = true;

            // Load next page
            var pagedItems = await _badReportRepository.GetPagedSortedAsync(
                p => p.ReportDate,
                sortDescending: true,
                _currentPage,
                PageSize);

            foreach (var item in pagedItems)
            {
                BadReports.Add(item);
            }

            _currentPage++;
        }
        finally
        {
            // End loading
            IsLoading = false;
        }
    }

    private void CheckListCount()
    {
        IsListEmpty = BadReports.Count == 0;
    }

    public async Task RemoveReportAsync(BadReport report)
    {
        await _badReportRepository.DeleteAsync(report.Id);

        BadReports.Remove(report);
    }

    public async Task RemoveAllReportsAsync()
    {
        foreach (var report in BadReports)
        {
            await _badReportRepository.DeleteAsync(report.Id);
        }
        await _badReportRepository.SaveChangesAsync();

        BadReports.Clear();
    }

    public Task SaveChangesAsync()
    {
        return _badReportRepository.SaveChangesAsync();
    }

    public async Task RefreshAsync()
    {
        _currentPage = 1;

        BadReports.Clear();
        await LoadNextPageAsync();
    }
}
