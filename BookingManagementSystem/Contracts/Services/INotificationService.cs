using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Contracts.Services;
public interface INotificationService
{
    int UnreadNotificationCount { get; }
    event Action<int> NotificationListChanged;

    Task InitializeCacheAsync();
    Task<IEnumerable<Notification>> GetPagedNotificationsAsync(int page, int pageSize, bool unreadOnly = false);
    Task AddNotificationAsync(Notification notification);
    Task RemoveNotificationAsync(int notificationId);
    Task RemoveNotificationRangeAsync(IEnumerable<int> notificationIds);
    Task RemoveAllNotificationsAsync();
    Task SaveChangesAsync();
    Task MarkAsReadAsync(Notification notification);
    Task MarkAsReadRangeAsync(IEnumerable<Notification> notifications);
    Task MarkAsUnreadAsync(Notification notification);
    Task MarkAsUnreadRangeAsync(IEnumerable<Notification> notifications);
    Task MarkAllAsReadAsync();
    Task<IEnumerable<Notification>> GetAllNotificationsAsync();
    Task ResetUserNotificationsAsync();
}
