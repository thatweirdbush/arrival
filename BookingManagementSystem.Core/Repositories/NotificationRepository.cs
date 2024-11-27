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
                    Title = "Your booking to \"Cozy room in Montmartre\" has been confirmed",
                    Message = "Your booking has been confirmed, we are looking forward to welcoming you",
                    DateSent = DateTime.Now
            },
            new() { Id = 2,
                    UserId = 2,
                    Title = "Your booking to \"Hidden Bamboo Bali\" has been confirmed",
                    Message = "Your booking has been confirmed, we are looking forward to welcoming you",
                    DateSent = DateTime.Now.Subtract(TimeSpan.FromDays(1))
            },
            new() { Id = 3,
                    UserId = 3,
                    Title = "Your booking to \"Nature stone house for rent in Siquijor\" has been canceled",
                    Message = "Your booking has been canceled, we are sorry for the inconvenience",
                    DateSent = DateTime.Now.Subtract(TimeSpan.FromDays(2))
            },
            new() { Id = 4,
                    UserId = 4,
                    Title = "Your booking to \"Abong 2 A-Frame House Great View\" has been confirmed",
                    Message = "Your booking has been confirmed, we are looking forward to welcoming you",
                    DateSent = DateTime.Now.Subtract(TimeSpan.FromDays(3))
            },
            new() { Id = 5,
                    UserId = 5,
                    Title = "Your booking to \"Hidden Bamboo Bali\" has been canceled",
                    Message = "Your booking has been canceled, we are sorry for the inconvenience",
                    DateSent = DateTime.Now.Subtract(TimeSpan.FromDays(4))
            }
        ]);
    }
}
