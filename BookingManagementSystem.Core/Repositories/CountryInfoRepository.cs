using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class CountryInfoRepository : Repository<CountryInfo>
{
    public CountryInfoRepository(DbContext context) : base(context)
    {
    }
}
