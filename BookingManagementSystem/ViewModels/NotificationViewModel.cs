using BookingManagementSystem.Core.Contracts.Repositories;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Contracts.Services;

namespace BookingManagementSystem.ViewModels;

public partial class NotificationViewModel : ObservableRecipient, INavigationAware
{
    [ObservableProperty]
    private bool isNotificationListEmpty;
    public INavigationService NavigationService
    {
        get;
    }
    public IRepository<Notification> NotificationRepository
    {
        get;
    }
    public ObservableCollection<Notification> Notifications { get; } = [];

    public NotificationViewModel(INavigationService navigationService, IRepository<Notification> notificationRepository)
    {
        NavigationService = navigationService;
        NotificationRepository = notificationRepository;

        // Initial check
        CheckNotificationListCount();

        // Subscribe to CollectionChanged event
        Notifications.CollectionChanged += (s, e) => CheckNotificationListCount();
    }

    public async Task LoadNotificationData(bool isUnreadFilter = false)
    {
        // Clear notification data list first
        Notifications.Clear();

        // Get notification data list
        var notifications = await NotificationRepository.GetAllAsync();

        if (isUnreadFilter)
        {
            notifications = notifications.Where(n => !n.IsRead).ToList();
        }
        foreach (var notification in notifications)
        {
            Notifications.Add(notification);
        }
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Get notification data list
        await LoadNotificationData();
    }
    public void OnNavigatedFrom()
    {
    }

    private void CheckNotificationListCount()
    {
        IsNotificationListEmpty = Notifications.Count == 0;
    }

    public async Task RemoveNotificationAsync(Notification notification)
    {
        await NotificationRepository.DeleteAsync(notification.Id);
        Notifications.Remove(notification);
    }

    public async Task RemoveAllNotificationAsync()
    {
        foreach (var notification in Notifications)
        {
            await NotificationRepository.DeleteAsync(notification.Id);
        }
        Notifications.Clear();
    }

    public void MarkAllNotificationsAsRead()
    {
        foreach (var notification in Notifications)
        {
            notification.IsRead = true;
        }
    }
}
