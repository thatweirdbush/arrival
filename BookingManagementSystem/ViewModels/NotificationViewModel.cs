using BookingManagementSystem.Core.Contracts.Repositories;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.ViewModels;

namespace BookingManagementSystem.ViewModels;

public partial class NotificationViewModel : ObservableRecipient, INavigationAware
{
    private readonly IRepository<Notification> _notificationRepository;
    public ObservableCollection<Notification> Notifications { get; } = [];

    [ObservableProperty]
    private bool isListEmpty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isSelectedUnreadFilter;

    private int _currentPage = 1;
    private const int PageSize = 5;

    public NotificationViewModel(IRepository<Notification> notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Initialize filter
        IsSelectedUnreadFilter = false;

        // Initialize data list with pagination
        await LoadNextPageAsync();

        // Initial check
        CheckListCount();
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
        if (IsLoading) return;

        try
        {
            // Begin loading
            IsLoading = true;

            // Load next page
            IEnumerable<Notification> pagedItems;

            if (IsSelectedUnreadFilter)
            {
                pagedItems = await _notificationRepository.GetPagedFilteredAndSortedAsync(
                     n => !n.IsRead,
                     p => p.DateSent,
                     sortDescending: true,
                     _currentPage,
                     PageSize);
            }
            else
            {
                pagedItems = await _notificationRepository.GetPagedSortedAsync(
                     p => p.DateSent,
                     sortDescending: true,
                     _currentPage,
                     PageSize);
            }

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

    public async Task RemoveAsync(Notification notification)
    {
        await _notificationRepository.DeleteAsync(notification.Id);

        Notifications.Remove(notification);
    }

    public async Task RemoveAllAsync()
    {
        foreach (var notification in Notifications)
        {
            await _notificationRepository.DeleteAsync(notification.Id);
        }        
        await _notificationRepository.SaveChangesAsync();

        Notifications.Clear();
        CheckListCount();
    }

    public async Task MarkAsReadAsync(Notification notification)
    {
        notification.IsRead = true;
        await _notificationRepository.UpdateAsync(notification);
    }

    public async Task MarkAsUnreadAsync(Notification notification)
    {
        notification.IsRead = false;
        await _notificationRepository.UpdateAsync(notification);
    }

    public async Task MarkAllAsReadAsync()
    {
        foreach (var notification in Notifications)
        {
            notification.IsRead = true;
            await _notificationRepository.UpdateAsync(notification);
        }
        await _notificationRepository.SaveChangesAsync();
    }

    public Task UpdateAsync(Notification notification)
    {
        return _notificationRepository.UpdateAsync(notification);
    }

    public Task SaveChangesAsync()
    {
        return _notificationRepository.SaveChangesAsync();
    }

    public async Task RefreshAsync()
    {
        _currentPage = 1;

        Notifications.Clear();
        await LoadNextPageAsync();
        CheckListCount();
    }
}
