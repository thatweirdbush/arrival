using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class BookingRepository : Repository<Booking>
{
    public BookingRepository()
    {
        _entities.AddRange(
        [
            new()
            {
                Id = 1,
                PropertyId = 1,
                UserId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(3),
                TotalPrice = 299.9M
            },
            new()
            {
                Id = 2,
                PropertyId = 2,
                UserId = 2,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                TotalPrice = 199.9M
            },
            new()
            {
                Id = 3,
                PropertyId = 3,
                UserId = 3,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1),
                TotalPrice = 49.9M
            },
            new()
            {
                Id = 4,
                PropertyId = 4,
                UserId = 4,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(4),
                TotalPrice = 99.9M
            },
            new()
            {
                Id = 5,
                PropertyId = 5,
                UserId = 5,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(5),
                TotalPrice = 69.9M
            }
        ]);
    }
}
