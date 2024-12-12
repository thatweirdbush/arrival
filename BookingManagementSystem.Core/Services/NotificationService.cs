using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Services;
public class NotificationService : INotificationService
{
    private readonly IRepository<Notification> _notificationRepository;
    private HashSet<Notification> _cachedUnreadNotifications = [];
    public int UnreadNotificationCount => _cachedUnreadNotifications.Count;
    public event Action<int> NotificationListChanged;

    public NotificationService(IRepository<Notification> notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    /// <summary>
    /// This will be called outside of the constructor
    /// From ShellViewModel, after the ShellPage is loaded
    /// </summary>
    /// <returns></returns>
    public async Task InitializeCacheAsync()
    {
        // Load initial notifications, unread only
        _cachedUnreadNotifications = new HashSet<Notification>(
            await _notificationRepository.GetAllAsync(n => !n.IsRead));
    }

    /// <summary>
    /// Get all notifications from the database
    /// Rarely used, only for admin purposes
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Notification>> GetAllNotificationsAsync()
    {
        return _notificationRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Notification>> GetPagedNotificationsAsync(int page, int pageSize, bool unreadOnly = false)
    {
        IEnumerable<Notification> data;

        if (unreadOnly)
        {
            data = await _notificationRepository.GetPagedFilteredAndSortedAsync(
                n => !n.IsRead,
                n => n.DateSent,
                sortDescending: true,
                page,
                pageSize);
        }
        else
        {
            data = await _notificationRepository.GetPagedSortedAsync(
                n => n.DateSent,
                sortDescending: true,
                page,
                pageSize);
        }

        UpdateCachedUnreadNotifications(data, page);
        return data;
    }

    private void UpdateCachedUnreadNotifications(IEnumerable<Notification> notifications, int page)
    {
        if (page == 1)
        {
            // Replace old cache with the first page of unread notifications
            _cachedUnreadNotifications = new HashSet<Notification>(notifications.Where(n => !n.IsRead));
        }
        else
        {
            // Add newly loaded unread notifications to cache
            _cachedUnreadNotifications.UnionWith(notifications.Where(n => !n.IsRead));
        }
    }

    public Task SaveChangesAsync()
    {
        return _notificationRepository.SaveChangesAsync();
    }

    public async Task AddNotificationAsync(Notification notification)
    {
        // Add the notification to the cached unread list
        if (!notification.IsRead)
        {
            _cachedUnreadNotifications.Add(notification);
        }

        // Notify subscribers that the notification list has changed
        NotificationListChanged.Invoke(UnreadNotificationCount);

        // Add the notification to the database
        await _notificationRepository.AddAsync(notification);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task RemoveNotificationAsync(int notificationId)
    {
        // Creating a dummy object instead of FirstOrDefault() method
        // => Reduces the complexity from O(n) to O(1).
        var tempNotification = new Notification { Id = notificationId };

        // Remove the notification from the cached unread list
        _cachedUnreadNotifications.Remove(tempNotification);

        // Notify subscribers that the notification list has changed
        NotificationListChanged.Invoke(UnreadNotificationCount);

        // Remove the notification from the database
        await _notificationRepository.DeleteAsync(notificationId);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task RemoveNotificationRangeAsync(IEnumerable<int> notificationIds)
    {
        // Remove notifications from the cached unread list
        _cachedUnreadNotifications.RemoveWhere(n => notificationIds.Contains(n.Id));

        // Notify subscribers that the notification list has changed
        NotificationListChanged.Invoke(UnreadNotificationCount);

        // Batch delete from the database
        await _notificationRepository.DeleteRangeAsync(notificationIds);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task RemoveAllNotificationsAsync()
    {
        // Clear the cached unread list
        _cachedUnreadNotifications.Clear();

        // Notify subscribers that the notification list has changed
        NotificationListChanged.Invoke(UnreadNotificationCount);

        // Delete all notifications from the database
        // No need to call SaveChangesAsync() since it's a raw SQL query execution
        await _notificationRepository.DeleteAllAsync();
    }

    public async Task MarkAsReadAsync(Notification notification)
    {
        if (notification.IsRead) return;

        notification.IsRead = true;

        // If it's not in the list, the Remove() method will do nothing
        _cachedUnreadNotifications.Remove(notification);

        // Notify subscribers that the notification list has changed
        NotificationListChanged.Invoke(UnreadNotificationCount);

        // Update the database
        await _notificationRepository.UpdateAsync(notification);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task MarkAsUnreadAsync(Notification notification)
    {
        if (!notification.IsRead) return;

        notification.IsRead = false;

        // HashSet automatically handles duplicates
        _cachedUnreadNotifications.Add(notification);

        // Notify subscribers that the notification list has changed
        NotificationListChanged.Invoke(UnreadNotificationCount);

        await _notificationRepository.UpdateAsync(notification);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task MarkAsReadRangeAsync(IEnumerable<Notification> notifications)
    {
        // Get the unread notifications
        notifications = notifications.Where(n => !n.IsRead);

        // Update the on query list
        foreach (var notification in notifications)
        {
            notification.IsRead = true;

            // If it's not in the list, the Remove() method will do nothing
            _cachedUnreadNotifications.Remove(notification);
        }

        // Notify subscribers that the notification list has changed
        NotificationListChanged.Invoke(UnreadNotificationCount);

        // Batch update to the database
        await _notificationRepository.UpdateRangeAsync(notifications);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task MarkAsUnreadRangeAsync(IEnumerable<Notification> notifications)
    {
        // Get the read notifications
        notifications = notifications.Where(n => n.IsRead);

        // Update the on query list
        foreach (var notification in notifications)
        {
            notification.IsRead = false;
            _cachedUnreadNotifications.Add(notification);
        }

        // Notify subscribers that the notification list has changed
        NotificationListChanged.Invoke(UnreadNotificationCount);

        // Batch update to the database
        await _notificationRepository.UpdateRangeAsync(notifications);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task MarkAllAsReadAsync()
    {
        var unreadNotifications = await _notificationRepository.GetAllAsync(n => !n.IsRead);

        foreach (var notification in unreadNotifications)
        {
            notification.IsRead = true;
        }

        // Clear cached unread notifications
        _cachedUnreadNotifications.Clear();

        // Notify subscribers that the notification list has changed
        NotificationListChanged.Invoke(UnreadNotificationCount);

        // Batch update to the database
        await _notificationRepository.UpdateRangeAsync(unreadNotifications);
        await _notificationRepository.SaveChangesAsync();
    }
}
