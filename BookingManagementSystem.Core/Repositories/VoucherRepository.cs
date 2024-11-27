using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Repositories;
public class VoucherRepository : Repository<Voucher>
{
    public VoucherRepository()
    {
        _entities.AddRange(
        [
            new() { Id = 1,
                    Code = "Voucher 1",
                    Quantity =  10,
                    DiscountPercentage = 10,

                    ValidUntil = DateTime.Now.AddDays(30),
                    IsUsed = true
            },
            new() { Id = 2,
                    Code = "Voucher 2",
                    Quantity =  10,
                    DiscountPercentage = 20,
                    ValidUntil = DateTime.Now.AddDays(60),
                    IsUsed = false
            },
            new() { Id = 3,
                    Code = "Voucher 3",
                    Quantity =  10,
                    DiscountPercentage = 30,
                    ValidUntil = DateTime.Now.AddDays(90),
                    IsUsed = false
            },
            new() { Id = 4,
                    Code = "Voucher 4",
                    Quantity =  5,
                    DiscountPercentage = 40,
                    ValidUntil = DateTime.Now.AddDays(120),
                    IsUsed = false
            },
            new() { Id = 5,
                    Code = "Voucher 5",
                    Quantity =  1,
                    DiscountPercentage = 50,
                    ValidUntil = DateTime.Now.AddDays(150),
                    IsUsed = true
            }
        ]);
    }
}
