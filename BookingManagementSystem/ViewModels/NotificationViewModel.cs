using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.ViewModels.Account;
using BookingManagementSystem.Contracts.Services;

namespace BookingManagementSystem.ViewModels;

public partial class NotificationViewModel : ObservableRecipient, INavigationAware
{
    private readonly INotificationService _notificationService;
    public ObservableCollection<Notification> Notifications { get; } = [];

    [ObservableProperty]
    private bool isListEmpty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isSelectedUnreadFilter;

    [ObservableProperty]
    private bool isUserLoggedIn;

    private int _currentPage = 1;
    private const int PageSize = 5;

    public NotificationViewModel(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (LoginViewModel.CurrentUser == null)
        {
            CheckListCount();
            CheckUserLoggedIn();
            return;
        }

        // Initialize filter
        IsSelectedUnreadFilter = false;

        // Initialize data list with pagination
        await LoadNextPageAsync();

        // Initial check
        CheckListCount();
        CheckUserLoggedIn();
    }
    public void OnNavigatedFrom()
    {
    }

    // Do nothing if the filter is not changed
    partial void OnIsSelectedUnreadFilterChanged(bool oldValue, bool newValue)
    {
        if (oldValue == newValue) return;
        _ = RefreshAsync();
    }

    public async Task LoadNextPageAsync()
    {
        if (LoginViewModel.CurrentUser == null) return;

        if (IsLoading) return;

        try
        {
            // Begin loading
            IsLoading = true;

            // Load next page
            var pagedItems = await _notificationService.GetPagedNotificationsAsync(_currentPage, PageSize, unreadOnly: IsSelectedUnreadFilter);

            foreach (var notification in pagedItems)
            {
                Notifications.Add(notification);
            }

            _currentPage++;
        }
        finally
        {
            // End loading
            IsLoading = false;
        }
    }

    public void CheckListCount()
    {
        IsListEmpty = Notifications.Count == 0;
    }

    private void CheckUserLoggedIn()
    {
        IsUserLoggedIn = LoginViewModel.CurrentUser != null;
    }

    public async Task RemoveRangeAsync(IEnumerable<Notification> notifications)
    {
        var notificationIds = notifications.Select(n => n.Id);
        await _notificationService.RemoveNotificationRangeAsync(notificationIds);

        foreach (var notification in notifications)
        {
            Notifications.Remove(notification);
        }
    }

    public async Task RemoveAllAsync()
    {
        await _notificationService.RemoveAllNotificationsAsync();

        Notifications.Clear();
        CheckListCount();
    }

    public Task MarkAsReadAsync(Notification notification)
    {
        return _notificationService.MarkAsReadAsync(notification);
    }

    public async Task MarkAllAsReadAsync()
    {
        await _notificationService.MarkAllAsReadAsync();
        await RefreshAsync();
    }

    public Task MarkAsUnreadRangeAsync(IEnumerable<Notification> notifications)
    {
        return _notificationService.MarkAsUnreadRangeAsync(notifications);
    }

    public Task MarkAsReadRangeAsync(IEnumerable<Notification> notifications)
    {
        return _notificationService.MarkAsReadRangeAsync(notifications);
    }

    public async Task RefreshAsync()
    {
        _currentPage = 1;

        Notifications.Clear();
        await LoadNextPageAsync();
        CheckListCount();
    }
}
