using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem.Core.Repositories;
public class AmenityRepository : Repository<Amenity>
{
    public AmenityRepository(DbContext context) : base(context)
    {
    }
}
