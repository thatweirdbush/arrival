using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class ReviewRepository : Repository<Review>
{
    public ReviewRepository()
    {
        _entities.AddRange(
        [
            new() { Id = 1,
                    PropertyId = 1,
                    UserId = 1,
                    Rating = 5,
                    Comment = "Excellent property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 2,
                    PropertyId = 2,
                    UserId = 2,
                    Rating = 4,
                    Comment = "Great property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 3,
                    PropertyId = 3,
                    UserId = 3,
                    Rating = 3,
                    Comment = "Good property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 4,
                    PropertyId = 4,
                    UserId = 4,
                    Rating = 2,
                    Comment = "Average property",
                    CreatedAt = DateTime.Now
            },
            new() { Id = 5,
                    PropertyId = 5,
                    UserId = 5,
                    Rating = 1,
                    Comment = "Poor property",
                    CreatedAt = DateTime.Now
            }
        ]);
    }
}
