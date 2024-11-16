using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class PaymentRepository : Repository<Payment>
{
    public PaymentRepository()
    {
        _entities.AddRange(
        [
            new() { Id = 1,
                    UserId = 1,
                    BookingId = 1,
                    Amount = 299.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 2,
                    UserId = 2,
                    BookingId = 2,
                    Amount = 199.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 3,
                    UserId = 3,
                    BookingId = 3,
                    Amount = 49.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 4,
                    UserId = 4,
                    BookingId = 4,
                    Amount = 99.9M,
                    PaymentDate = DateTime.Now
            },
            new() { Id = 5,
                    UserId = 5,
                    BookingId = 5,
                    Amount = 69.9M,
                    PaymentDate = DateTime.Now
            }
        ]);
    }
}
