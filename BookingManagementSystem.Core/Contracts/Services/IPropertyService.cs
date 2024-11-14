using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Core.Contracts.Services;
public interface IPropertyService
{
    Task SavePropertyAsync(Property property);
    Task<Property> GetPropertyInProgressAsync(int propertyId);
}
