using BookingManagementSystem.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class CountryInfoRepository : Repository<CountryInfo>
{
    public CountryInfoRepository(DbContext context) : base(context)
    {
    }
}
