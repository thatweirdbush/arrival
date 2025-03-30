using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class NotificationRepository : Repository<Notification>
{
    public NotificationRepository(DbContext context) : base(context)
    {
    }
}
