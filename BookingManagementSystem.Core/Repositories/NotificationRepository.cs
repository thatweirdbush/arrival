using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class NotificationRepository : Repository<Notification>
{
    public NotificationRepository()
    {
        _entities.AddRange(
        [
            new() { Id = 1,
                    UserId = 1,
                    Message = "Your booking has been confirmed",
                    DateSent = DateTime.Now
            },
            new() { Id = 2,
                    UserId = 2,
                    Message = "Your booking has been canceled",
                    DateSent = DateTime.Now
            },
            new() { Id = 3,
                    UserId = 3,
                    Message = "Your booking is pending", DateSent
                    = DateTime.Now
            },
            new() { Id = 4,
                    UserId = 4,
                    Message = "Your booking has been confirmed",
                    DateSent = DateTime.Now
            },
            new() { Id = 5,
                    UserId = 5,
                    Message = "Your booking has been canceled",
                    DateSent = DateTime.Now
            }
        ]);
    }
}
