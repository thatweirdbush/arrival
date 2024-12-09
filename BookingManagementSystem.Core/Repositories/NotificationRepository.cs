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
                    Title = "Booking Confirmed",
                    Message = "Your booking to \"Cozy room in Montmartre\" has been confirmed, we are looking forward to welcoming you",
                    DateSent = DateTime.Now.ToUniversalTime(),
                    IsRead = true
            },
            new() { Id = 2,
                    UserId = 2,
                    Title = "Booking Confirmed",
                    Message = "Your booking to \"Hidden Bamboo Bali\" has been confirmed, we are looking forward to welcoming you",
                    DateSent = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromDays(1)),
                    IsRead = true
            },
            new() { Id = 3,
                    UserId = 3,
                    Title = "Booking Canceled",
                    Message = "Your booking \"Nature stone house for rent in Siquijor\" has been canceled, we are sorry for the inconvenience",
                    DateSent = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromDays(2)),
                    IsRead = false
            },
            new() { Id = 4,
                    UserId = 4,
                    Title = "Booking Confirmed",
                    Message = "Your booking \"Abong 2 A-Frame House Great View\" has been confirmed, we are looking forward to welcoming you",
                    DateSent = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromDays(3)),
                    IsRead = false
            },
            new() { Id = 5,
                    UserId = 5,
                    Title = "Booking Canceled",
                    Message = "Your booking to \"Hidden Bamboo Bali\" has been canceled, we are sorry for the inconvenience",
                    DateSent = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromDays(4)),
                    IsRead = true
            }
        ]);
    }
}
