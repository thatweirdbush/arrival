using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class AmenityRepository : Repository<Amenity>
{
    public AmenityRepository()
    {
        _entities.AddRange(
        [
            new() { Id = 1,
                    Name = "Free Wi-Fi",
                    Description = "High-speed internet"
            },
            new() { Id = 2,
                    Name = "Free Parking",
                    Description = "On-site parking"
            },
            new() { Id = 3,
                    Name = "Swimming Pool",
                    Description = "Outdoor pool"
            },
            new() { Id = 4,
                    Name = "Gym",
                    Description = "Fitness center"
            },
            new() { Id = 5,
                    Name = "Restaurant",
                    Description = "On-site restaurant"
            }
        ]);
    }
}
